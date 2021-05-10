/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using XFS4IoT;

namespace XFS4IoTServer
{
    public class CommandDispatcher : ICommandDispatcher
    {
        public CommandDispatcher(IEnumerable<XFSConstants.ServiceClass> Services, ILogger Logger, AssemblyName AssemblyName = null)
        {
            this.Logger = Logger.IsNotNull();
            this.ServiceClasses = Services.IsNotNull();

            // Find all the classes (in the named assembly if a name is give,) which 
            // have the CommandHandlerAttribute, and match them with the 'Type' value on 
            // that attribute. 
            // that is, for [CommandHandler(typeof(MessageA))] class HandlerA : ICommandHandler
            // record that MessageA is handled by HandlerA

            Add(from assem in AppDomain.CurrentDomain.GetAssemblies()
                where assem.IsDynamic == false && (AssemblyName == null || assem.GetName().FullName == AssemblyName.FullName)
                from type in assem.ExportedTypes
                from CustomAttributeData attrib in type.CustomAttributes
                where attrib.AttributeType == typeof(CommandHandlerAttribute)
                where attrib.ConstructorArguments.Count == 2
                where attrib.ConstructorArguments[0].ArgumentType == typeof(XFSConstants.ServiceClass) && ServiceClasses.Contains((XFSConstants.ServiceClass)attrib.ConstructorArguments[0].Value)
                where attrib.ConstructorArguments[1].ArgumentType == typeof(Type)
                let namedArg = attrib.ConstructorArguments[1].Value
                select (message: namedArg as Type, handler: type)
                );

            var handlers = string.Join("\n", from next in MessageHandlers select $"{next.Key} => {next.Value}");
            Logger.Log(Constants.Component, $"Dispatch, found Command Handler classes:\n{handlers}");

            // Double check that command handler classes were declared with the right constructor so that we'll be able to use them. 
            // Would be nice to be able to test this at compile time. 
            foreach (Type handlerClass in MessageHandlers.Values)
            {
                try
                {
                    ConstructorInfo constructorInfo = handlerClass.GetConstructor(new Type[] { typeof(ICommandDispatcher), typeof(ILogger) });
                    Contracts.IsNotNull(constructorInfo, $"Failed to find constructor for {handlerClass}");
                }
                catch (Exception e) when (e.Message.Contains("Failed to find constructor"))
                {
                    Contracts.Fail($"Handler class {handlerClass.FullName} doesn't have a constructor that takes ({nameof(ICommandDispatcher)},{nameof(ILogger)}). Exception: {e.Message}");
                }
            }
        }

        public Task Dispatch(IConnection Connection, MessageBase Command)
        {
            Connection.IsNotNull($"Invalid parameter in the {nameof(Dispatch)} method. {nameof(Connection)}");
            Command.IsNotNull($"Invalid parameter in the {nameof(Dispatch)} method. {nameof(Command)}");

            using var cts = new CancellationTokenSource();
            return CreateHandler(Command.GetType()).Handle(Connection, Command, cts.Token) ?? Task.CompletedTask;
        }

        public Task DispatchError(IConnection Connection, MessageBase Command, Exception CommandErrorexception)
        {
            Connection.IsNotNull($"Invalid parameter in the {nameof(Dispatch)} method. {nameof(Connection)}");
            Command.IsNotNull($"Invalid parameter in the {nameof(Dispatch)} method. {nameof(Command)}");
            CommandErrorexception.IsNotNull($"Invalid parameter in the {nameof(Dispatch)} method. {nameof(CommandErrorexception)}");

            return CreateHandler(Command.GetType()).HandleError( Connection, Command, CommandErrorexception ) ?? Task.CompletedTask;
        }

        private ICommandHandler CreateHandler(Type type)
        {
            Type handlerClass = MessageHandlers[type];
            Contracts.IsTrue(
                typeof(ICommandHandler).IsAssignableFrom(handlerClass),
                $"Class {handlerClass.Name} is registered to handle {type.Name} but isn't a {nameof(ICommandHandler)}");

            Logger.Log(Constants.Component, $"Dispatch: Handling a {type} message with {handlerClass.Name}");

            // Create a new handler object. Effectively the same as: 
            // ICommandHandler handler = new handlerClass( this, Logger );
            return handlerClass.GetConstructor(new Type[] { typeof(ICommandDispatcher), typeof(ILogger) })
                               .IsNotNull($"Failed to find constructor for {handlerClass}")
                               .Invoke(parameters: new object[] { this, Logger })
                               .IsA<ICommandHandler>();
        }

        private void Add(IEnumerable<(Type, Type)> types)
        {
            foreach (var (Message, Handler) in types)
                MessageHandlers.Add(Message, Handler);
        }

        private readonly Dictionary<Type, Type> MessageHandlers = new();

        private readonly ILogger Logger;

        public IEnumerable<Type> Commands { get => MessageHandlers.Keys; }
        public IEnumerable<XFSConstants.ServiceClass> ServiceClasses { get; }
    }
}

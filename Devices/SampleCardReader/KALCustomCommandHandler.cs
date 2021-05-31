using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using XFS4IoT;

using XFS4IoTFramework;

using XFS4IoTServer;

namespace KAL.XFS4IoTSP.CardReader.Sample
{
    [CommandHandler(XFS4IoT.XFSConstants.ServiceClass.CardReader, typeof(KALCustomCommand))]
    public class KALCustomCommandHandler : ICommandHandler
    {
        private readonly ICommandDispatcher SP;
        private readonly ILogger Logger;

        public KALCustomCommandHandler(ICommandDispatcher SP, ILogger Logger )
        {
            this.SP = SP ?? throw new ArgumentNullException(nameof(SP));
            this.Logger = Logger ?? throw new ArgumentNullException(nameof(Logger));
        }
        public async Task Handle(IConnection Connection, object Command, CancellationToken Cancel)
        {
            var customerCommand = Command as KALCustomCommand;

            await Connection.SendMessageAsync( new KALCustomCompletion( customerCommand.Headers.RequestId,
                new KALCustomCompletion.PayloadData { ResultInfo="This is the result"}
                )
                );
        }

        public Task HandleError(IConnection Connection, object Command, Exception CommandException)
        {
            throw new NotImplementedException();
        }
    }
}

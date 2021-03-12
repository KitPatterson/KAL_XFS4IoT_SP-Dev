// (C) KAL ATM Software GmbH, 2021

using System;

namespace XFS4IoT
{
    /// <summary>
    /// Indicate that a class can be used as a command in a message dispatcher
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public sealed class CommandAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
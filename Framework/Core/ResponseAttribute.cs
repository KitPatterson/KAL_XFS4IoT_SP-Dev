// (C) KAL ATM Software GmbH, 2021

using System;

namespace XFS4IoT
{
    /// <summary>
    /// Indicate that a class can be used as a response in a message dispatcher. 
    /// </summary>
    public sealed class ResponseAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
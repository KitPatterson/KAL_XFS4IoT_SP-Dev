// (C) KAL ATM Software GmbH, 2021
using System;

namespace XFS4IoT.UnitTestCore
{
    public class TestFatalErrorException : Exception
    {
        public TestFatalErrorException(string message) : base(message) { }
    };
}

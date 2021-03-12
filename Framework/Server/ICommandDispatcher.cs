// (C) KAL ATM Software GmbH, 2021

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XFS4IoT;

namespace XFS4IoTServer
{
    public interface ICommandDispatcher
    {
        Task Dispatch(IConnection Connection, object Command, CancellationToken Cancel);

        Task DispatchError(IConnection Connection, object Command, Exception CommandException);
    }
}
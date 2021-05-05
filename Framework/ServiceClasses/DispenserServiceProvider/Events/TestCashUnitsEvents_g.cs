/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Dispenser interface.
 * TestCashUnitsEvents_g.cs uses automatically generated parts.
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;
using System.Threading.Tasks;

namespace XFS4IoTFramework.Dispenser
{
    internal class TestCashUnitsEvents : DispenserEvents, ITestCashUnitsEvents
    {

        public TestCashUnitsEvents(IConnection connection, string requestId)
            : base(connection, requestId)
        { }

        public async Task CashUnitErrorEvent(XFS4IoT.Dispenser.Events.CashUnitErrorEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.Dispenser.Events.CashUnitErrorEvent(requestId, Payload));

        public async Task CashUnitThresholdEvent(XFS4IoT.Dispenser.Events.CashUnitThresholdEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.Dispenser.Events.CashUnitThresholdEvent(requestId, Payload));

        public async Task NoteErrorEvent(XFS4IoT.Dispenser.Events.NoteErrorEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.Dispenser.Events.NoteErrorEvent(requestId, Payload));

        public async Task InfoAvailableEvent(XFS4IoT.Dispenser.Events.InfoAvailableEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.Dispenser.Events.InfoAvailableEvent(requestId, Payload));

        public async Task CashUnitInfoChangedEvent(XFS4IoT.Dispenser.Events.CashUnitInfoChangedEvent.PayloadData Payload) => await connection.SendMessageAsync(new XFS4IoT.Dispenser.Events.CashUnitInfoChangedEvent(requestId, Payload));

    }
}

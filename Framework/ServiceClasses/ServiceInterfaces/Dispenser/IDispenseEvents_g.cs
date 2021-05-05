/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Dispenser interface.
 * IDispenseEvents_g.cs uses automatically generated parts.
\***********************************************************************************************/


using XFS4IoTServer;
using System.Threading.Tasks;

namespace XFS4IoTFramework.Dispenser
{
    public interface IDispenseEvents
    {

        Task CashUnitErrorEvent(XFS4IoT.Dispenser.Events.CashUnitErrorEvent.PayloadData Payload);

        Task CashUnitThresholdEvent(XFS4IoT.Dispenser.Events.CashUnitThresholdEvent.PayloadData Payload);

        Task DelayedDispenseEvent(XFS4IoT.Dispenser.Events.DelayedDispenseEvent.PayloadData Payload);

        Task StartDispenseEvent(XFS4IoT.Dispenser.Events.StartDispenseEvent.PayloadData Payload);

        Task PartialDispenseEvent(XFS4IoT.Dispenser.Events.PartialDispenseEvent.PayloadData Payload);

        Task SubDispenseOkEvent(XFS4IoT.Dispenser.Events.SubDispenseOkEvent.PayloadData Payload);

        Task IncompleteDispenseEvent(XFS4IoT.Dispenser.Events.IncompleteDispenseEvent.PayloadData Payload);

        Task NoteErrorEvent(XFS4IoT.Dispenser.Events.NoteErrorEvent.PayloadData Payload);

        Task InfoAvailableEvent(XFS4IoT.Dispenser.Events.InfoAvailableEvent.PayloadData Payload);

    }
}

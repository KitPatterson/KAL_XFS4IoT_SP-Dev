/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Dispenser interface.
 * IDispenserDevice.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XFS4IoTServer;
using XFS4IoT.Dispenser.Completions;
using XFS4IoT.Dispenser.Commands;

// KAL specific implementation of dispenser. 
namespace XFS4IoTFramework.Dispenser
{
    public interface IDispenserDevice : IDevice
    {
        /// <summary>
        /// {}
        /// </summary>
        Task<DispenseCompletion.PayloadData> Dispense(IDispenseEvents events, 
                                                      DispenseCommand.PayloadData payload, 
                                                      CancellationToken cancellation);

        /// <summary>
        /// PresentCashAsync
        /// Present cash to the specified output position
        /// </summary>
        Task<PresentCashResult> PresentCashAsync(IPresentEvents events, 
                                                 PresentCashRequest presentInfo, 
                                                 CancellationToken cancellation);

        /// <summary>
        /// This command will move items from the intermediatestacker and transport them to a reject cash unit (i.e. a cash unit with*type* \"rejectCassette\"). The *count*field of the reject cash unit is incremented by the number of items thatwere thought to be present at the time of the reject or the numbercounted by the device during the reject. Note that the reject bin countis unreliable.
        /// </summary>
        Task<RejectResult> RejectAsync(IRejectEvents events, 
                                       CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<RetractCompletion.PayloadData> Retract(IRetractEvents events, 
                                                    RetractCommand.PayloadData payload, 
                                                    CancellationToken cancellation);

        /// <summary>
        /// OpenShutterAsync
        /// Perform shutter operation to open.
        /// </summary>
        Task<OpenCloseShutterResult> OpenShutterAsync(OpenCloseShutterRequest shutterInfo,
                                                      CancellationToken cancellation);

        /// <summary>
        /// CloseShutterAsync
        /// Perform shutter operation to close.
        /// </summary>
        Task<OpenCloseShutterResult> CloseShutterAsync(OpenCloseShutterRequest shutterInfo, 
                                                       CancellationToken cancellation);

        /// <summary>
        /// ResetDeviceAsync
        /// Perform a hardware reset which will attempt to return the CashDispenser device to a known good state.
        /// </summary>
        Task<ResetDeviceResult> ResetDeviceAsync(IResetEvents events, 
                                                 ResetDeviceRequest resetDeviceInfo, 
                                                 CancellationToken cancellation);

        /// <summary>
        /// Perform a hardware reset which will attempt to return the CashDispenser device to a known good state.
        /// </summary>
        Task<TestCashUnitsCompletion.PayloadData> TestCashUnits(ITestCashUnitsEvents events, 
                                                                TestCashUnitsCommand.PayloadData payload, 
                                                                CancellationToken cancellation);

        /// <summary>
        /// CountAsync
        /// Perform count operation to empty the specified physical cash unit(s). 
        /// All items dispensed from the cash unit are counted and moved to the specified output location.
        /// </summary>
        Task<CountResult> CountAsync(ICountEvents events, 
                                     CountRequest countInfo,
                                     CancellationToken cancellation);

        /// <summary>
        /// PrepareDispenseAsync
        /// On some hardware it can take a significant amount of time for the dispenser to get ready to dispense media. 
        /// On this type of hardware the this method can be used to improve transaction performance.
        /// </summary>
        Task<PrepareDispenseResult> PrepareDispenseAsync(PrepareDispenseRequest prepareDispenseInfo,
                                                         CancellationToken cancellation);

    }
}

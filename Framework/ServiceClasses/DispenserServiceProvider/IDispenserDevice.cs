/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Dispenser interface.
 * IDispenserDevice.cs uses automatically generated parts.
\***********************************************************************************************/


using System.Threading;
using System.Threading.Tasks;
using XFS4IoTServer;

// KAL specific implementation of dispenser. 
namespace XFS4IoTFramework.Dispenser
{
    public interface IDispenserDevice : IDevice
    {

        /// <summary>
        /// This command is used to obtain a list of supported mixalgorithms and available house mix tables.
        /// </summary>
        Task<XFS4IoT.Dispenser.Completions.GetMixTypesCompletion.PayloadData> GetMixTypes(IGetMixTypesEvents events, 
                                                                                          XFS4IoT.Dispenser.Commands.GetMixTypesCommand.PayloadData payload, 
                                                                                          CancellationToken cancellation);

        /// <summary>
        /// This command is used to obtain the house mix tablespecified by the supplied mix number.
        /// </summary>
        Task<XFS4IoT.Dispenser.Completions.GetMixTableCompletion.PayloadData> GetMixTable(IGetMixTableEvents events, 
                                                                                          XFS4IoT.Dispenser.Commands.GetMixTableCommand.PayloadData payload, 
                                                                                          CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.Dispenser.Completions.GetPresentStatusCompletion.PayloadData> GetPresentStatus(IGetPresentStatusEvents events, 
                                                                                                    XFS4IoT.Dispenser.Commands.GetPresentStatusCommand.PayloadData payload, 
                                                                                                    CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.Dispenser.Completions.DenominateCompletion.PayloadData> Denominate(IDenominateEvents events, 
                                                                                        XFS4IoT.Dispenser.Commands.DenominateCommand.PayloadData payload, 
                                                                                        CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.Dispenser.Completions.DispenseCompletion.PayloadData> Dispense(IDispenseEvents events, 
                                                                                    XFS4IoT.Dispenser.Commands.DispenseCommand.PayloadData payload, 
                                                                                    CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.Dispenser.Completions.PresentCompletion.PayloadData> Present(IPresentEvents events, 
                                                                                  XFS4IoT.Dispenser.Commands.PresentCommand.PayloadData payload, 
                                                                                  CancellationToken cancellation);

        /// <summary>
        /// This command will move items from the intermediatestacker and transport them to a reject cash unit (i.e. a cash unit with*type* \"rejectCassette\"). The *count*field of the reject cash unit is incremented by the number of items thatwere thought to be present at the time of the reject or the numbercounted by the device during the reject. Note that the reject bin countis unreliable.
        /// </summary>
        Task<XFS4IoT.Dispenser.Completions.RejectCompletion.PayloadData> Reject(IRejectEvents events, 
                                                                                XFS4IoT.Dispenser.Commands.RejectCommand.PayloadData payload, 
                                                                                CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.Dispenser.Completions.RetractCompletion.PayloadData> Retract(IRetractEvents events, 
                                                                                  XFS4IoT.Dispenser.Commands.RetractCommand.PayloadData payload, 
                                                                                  CancellationToken cancellation);

        /// <summary>
        /// This command opens the shutter.
        /// </summary>
        Task<XFS4IoT.Dispenser.Completions.OpenShutterCompletion.PayloadData> OpenShutter(IOpenShutterEvents events, 
                                                                                          XFS4IoT.Dispenser.Commands.OpenShutterCommand.PayloadData payload, 
                                                                                          CancellationToken cancellation);

        /// <summary>
        /// |-  This command closes the shutter.
        /// </summary>
        Task<XFS4IoT.Dispenser.Completions.CloseShutterCompletion.PayloadData> CloseShutter(ICloseShutterEvents events, 
                                                                                            XFS4IoT.Dispenser.Commands.CloseShutterCommand.PayloadData payload, 
                                                                                            CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.Dispenser.Completions.SetMixTableCompletion.PayloadData> SetMixTable(ISetMixTableEvents events, 
                                                                                          XFS4IoT.Dispenser.Commands.SetMixTableCommand.PayloadData payload, 
                                                                                          CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.Dispenser.Completions.ResetCompletion.PayloadData> Reset(IResetEvents events, 
                                                                              XFS4IoT.Dispenser.Commands.ResetCommand.PayloadData payload, 
                                                                              CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.Dispenser.Completions.TestCashUnitsCompletion.PayloadData> TestCashUnits(ITestCashUnitsEvents events, 
                                                                                              XFS4IoT.Dispenser.Commands.TestCashUnitsCommand.PayloadData payload, 
                                                                                              CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.Dispenser.Completions.CountCompletion.PayloadData> Count(ICountEvents events, 
                                                                              XFS4IoT.Dispenser.Commands.CountCommand.PayloadData payload, 
                                                                              CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.Dispenser.Completions.PrepareDispenseCompletion.PayloadData> PrepareDispense(IPrepareDispenseEvents events, 
                                                                                                  XFS4IoT.Dispenser.Commands.PrepareDispenseCommand.PayloadData payload, 
                                                                                                  CancellationToken cancellation);

    }
}

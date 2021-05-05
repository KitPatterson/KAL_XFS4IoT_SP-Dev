/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashManagement interface.
 * ICashManagementDevice.cs uses automatically generated parts.
\***********************************************************************************************/


using System.Threading;
using System.Threading.Tasks;
using XFS4IoTServer;

// KAL specific implementation of cashmanagement. 
namespace XFS4IoTFramework.CashManagement
{
    public interface ICashManagementDevice : IDevice
    {

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.CashManagement.Completions.GetCashUnitInfoCompletion.PayloadData> GetCashUnitInfo(IGetCashUnitInfoEvents events, 
                                                                                                       XFS4IoT.CashManagement.Commands.GetCashUnitInfoCommand.PayloadData payload, 
                                                                                                       CancellationToken cancellation);

        /// <summary>
        /// This command only applies to Teller devices. It allows theapplication to obtain counts for each currency assigned to the teller.These counts represent the total amount of currency dispensed by theteller in all transactions.This command also enables the application to obtain the positionassigned to each teller. If the input parameter is NULL, this commandwill return information for all tellers and all currencies. The tellerinformation is persistent.
        /// </summary>
        Task<XFS4IoT.CashManagement.Completions.GetTellerInfoCompletion.PayloadData> GetTellerInfo(IGetTellerInfoEvents events, 
                                                                                                   XFS4IoT.CashManagement.Commands.GetTellerInfoCommand.PayloadData payload, 
                                                                                                   CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.CashManagement.Completions.GetItemInfoCompletion.PayloadData> GetItemInfo(IGetItemInfoEvents events, 
                                                                                               XFS4IoT.CashManagement.Commands.GetItemInfoCommand.PayloadData payload, 
                                                                                               CancellationToken cancellation);

        /// <summary>
        /// This command is used to retrieve the entire note classification information pre-set inside the device or set via the CashManagement.SetClassificationList command.This provides the functionality to blacklist notes and allows additional flexibility, for example to specify that notes can be taken out of circulation by specifying them as unfit. Any items not returned in this list will be handled according to normal classification rules.
        /// </summary>
        Task<XFS4IoT.CashManagement.Completions.GetClassificationListCompletion.PayloadData> GetClassificationList(IGetClassificationListEvents events, 
                                                                                                                   XFS4IoT.CashManagement.Commands.GetClassificationListCommand.PayloadData payload, 
                                                                                                                   CancellationToken cancellation);

        /// <summary>
        /// This command allows the application to initialize countsfor each currency assigned to the teller. The values set by this commandare persistent. This command only applies to Teller ATMs.
        /// </summary>
        Task<XFS4IoT.CashManagement.Completions.SetTellerInfoCompletion.PayloadData> SetTellerInfo(ISetTellerInfoEvents events, 
                                                                                                   XFS4IoT.CashManagement.Commands.SetTellerInfoCommand.PayloadData payload, 
                                                                                                   CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.CashManagement.Completions.SetCashUnitInfoCompletion.PayloadData> SetCashUnitInfo(ISetCashUnitInfoEvents events, 
                                                                                                       XFS4IoT.CashManagement.Commands.SetCashUnitInfoCommand.PayloadData payload, 
                                                                                                       CancellationToken cancellation);

        /// <summary>
        /// This command unlocks the safe door or starts the timedelay count down prior to unlocking the safe door, if the devicesupports it. The command completes when the door is unlocked or thetimer has started.
        /// </summary>
        Task<XFS4IoT.CashManagement.Completions.OpenSafeDoorCompletion.PayloadData> OpenSafeDoor(IOpenSafeDoorEvents events, 
                                                                                                 XFS4IoT.CashManagement.Commands.OpenSafeDoorCommand.PayloadData payload, 
                                                                                                 CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.CashManagement.Completions.StartExchangeCompletion.PayloadData> StartExchange(IStartExchangeEvents events, 
                                                                                                   XFS4IoT.CashManagement.Commands.StartExchangeCommand.PayloadData payload, 
                                                                                                   CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.CashManagement.Completions.EndExchangeCompletion.PayloadData> EndExchange(IEndExchangeEvents events, 
                                                                                               XFS4IoT.CashManagement.Commands.EndExchangeCommand.PayloadData payload, 
                                                                                               CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.CashManagement.Completions.CalibrateCashUnitCompletion.PayloadData> CalibrateCashUnit(ICalibrateCashUnitEvents events, 
                                                                                                           XFS4IoT.CashManagement.Commands.CalibrateCashUnitCommand.PayloadData payload, 
                                                                                                           CancellationToken cancellation);

        /// <summary>
        /// This command is used to specify the entire note classification list. Any items not specified in this list will be handled according to normal classification rules. This information is persistent. Information set by this command overrides any existingclassification list.If a note is reclassified, it is handled as though it was a note of the new classification. For example, a fit note reclassified as unfit would be treated as though it were unfit, which may mean that the note is not dispensed.Reclassification cannot be used to change a note’s classification to a higher level, for example, a note recognized as counterfeit by the device cannot be reclassified as genuine. In addition, it is not possible to re-classify a level 2 note as level 1.If two or more classification elements specify overlapping note definitions, but different *level* values then the first one takes priority.
        /// </summary>
        Task<XFS4IoT.CashManagement.Completions.SetClassificationListCompletion.PayloadData> SetClassificationList(ISetClassificationListEvents events, 
                                                                                                                   XFS4IoT.CashManagement.Commands.SetClassificationListCommand.PayloadData payload, 
                                                                                                                   CancellationToken cancellation);

    }
}

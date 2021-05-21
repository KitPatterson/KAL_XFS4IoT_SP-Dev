/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.

\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoTServer;
using XFS4IoT.Completions;
using XFS4IoT.Dispenser.Commands;
using XFS4IoT.Dispenser.Completions;
using XFS4IoTServer.Common;

namespace XFS4IoTFramework.Dispenser
{
    /// <summary>
    /// This field is used if items are to be moved to internal areas of the device, including cash units, 
    /// the intermediate stacker, or the transport.
    /// </summary>
    public class Retract
    {
        public CashDispenserCapabilitiesClass.RetractAreaEnum RetractArea { get; private set; }

        /// <summary>
        /// Index is valid if the RetractArea is set to Retract, otherwise this value can be omitted
        /// </summary>
        public int? Index { get; private set; }

        public Retract(CashDispenserCapabilitiesClass.RetractAreaEnum RetractArea, 
                       int? Index = null)
        {
            this.RetractArea = RetractArea;
            this.Index = Index;
        }
    }

    /// <summary>
    /// Denomination
    /// Representing output data of the Denominate and PresentStatus
    /// </summary>
    public sealed class Denomination
    {
        public Denomination(Dictionary<string, double> CurrencyAmounts, Dictionary<string, int> Values)
        {
            this.CurrencyAmounts = CurrencyAmounts;
            this.Values = Values;
        }

        /// <summary>
        /// Currencies to use for dispensing cash
        /// </summary>
        public Dictionary<string, double> CurrencyAmounts { get; private set; }

        /// <summary>
        /// Key is cash unit name and the value is the number of cash to be used
        /// </summary>
        public Dictionary<string, int> Values { get; private set; }
    }

    /// <summary>
    /// OpenCloseShutterRequest
    /// Open or Close shutter for the specified output position
    /// </summary>
    public sealed class OpenCloseShutterRequest
    {
        public enum ActionEnum
        {
            Open,
            Close
        }

        /// <summary>
        /// OpenCloseShutterRequest
        /// Open or Close shutter for the specified output position
        /// </summary>
        /// <param name="Action">Either Open or Close for the shutter operation</param>
        /// <param name="ShutterPosition">Postion of shutter to control.</param>
        public OpenCloseShutterRequest(ActionEnum Action, CashDispenserCapabilitiesClass.OutputPositionEnum ShutterPosition)
        {
            this.Action = Action;
            this.ShutterPosition = ShutterPosition;
        }

        public ActionEnum Action { get; private set; }

        public CashDispenserCapabilitiesClass.OutputPositionEnum ShutterPosition { get; private set; }
    }

    /// <summary>
    /// OpenCloseShutterResult
    /// Return result of shutter operation.
    /// </summary>
    public sealed class OpenCloseShutterResult : DeviceResult
    {
        public OpenCloseShutterResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                      string ErrorDescription = null,
                                      ErrorCodeEnum? ErrorCode = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
        }

        public OpenCloseShutterResult(MessagePayload.CompletionCodeEnum CompletionCode)
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
        }

        public enum ErrorCodeEnum
        {
            UnsupportedPosition,
            ShutterNotOpen,
            ShutterOpen,
            ShutterClosed,
            ShutterNotClosed,
            ExchangeActive,
        }

        /// <summary>
        /// Specifies the error code on closing or opening shutter
        /// </summary>
        public ErrorCodeEnum? ErrorCode { get; private set; }
    }

    /// <summary>
    /// CountRequest
    /// Count operation and move notes to the specified position
    /// </summary>
    public sealed class CountRequest
    {
        /// <summary>
        /// CountRequest
        /// Count operation to perform
        /// </summary>
        /// <param name="Position">Cash to move</param>
        /// <param name="PhysicalPositionName">Count from specified physical position name</param>
        public CountRequest(CashDispenserCapabilitiesClass.OutputPositionEnum Position, string PhysicalPositionName)
        {
            this.EmptyAll = false;
            this.Position = Position;
            this.PhysicalPositionName = PhysicalPositionName;
        }
        /// <summary>
        /// CountRequest
        /// Count cash from all physical units
        /// </summary>
        /// <param name="Position">Cash to move</param>
        public CountRequest(CashDispenserCapabilitiesClass.OutputPositionEnum Position)
        {
            this.EmptyAll = true;
            this.Position = Position;
            this.PhysicalPositionName = null;
        }

        /// <summary>
        /// Specifies whether all cash units are to be emptied. If this value is TRUE then physicalPositionName is ignored.
        /// </summary>
        public bool EmptyAll { get; private set; }

        /// <summary>
        /// Position of moving notes
        /// </summary>
        public CashDispenserCapabilitiesClass.OutputPositionEnum Position { get; private set; }
        /// <summary>
        /// Specifies which cash unit to empty and count. This name is the same as the 
        /// *physicalPositionName* in the [CashManagement.GetCashUnitInfo](#cashmanagement.getcashunitinfo) completion message.
        /// </summary>
        public string PhysicalPositionName { get; private set; }
    }

    /// <summary>
    /// CountResult
    /// Return result of counting notes operation.
    /// </summary>
    public sealed class CountResult : DeviceResult
    {
        /// <summary>
        /// CountResult
        /// </summary>
        /// <param name="CompletionCode"></param>
        /// <param name="ErrorDescription"></param>
        /// <param name="ErrorCode"></param>
        public CountResult(MessagePayload.CompletionCodeEnum CompletionCode,
                           string ErrorDescription = null,
                           CountCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null) 
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
        }

        /// <summary>
        /// Specifies the error code on closing or opening shutter
        /// </summary>
        public CountCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; private set; }
    }

    /// <summary>
    /// PresentCashRequest
    /// Present operation to perform
    /// </summary>
    public sealed class PresentCashRequest
    {
        /// <summary>
        /// PresentCashRequest
        /// Present operation to perform
        /// </summary>
        /// <param name="Position">Output position where cash are presented.</param>
        public PresentCashRequest(CashDispenserCapabilitiesClass.OutputPositionEnum Position)
        {
            this.Position = Position;
        }

        /// <summary>
        /// Position of moving cash
        /// </summary>
        public CashDispenserCapabilitiesClass.OutputPositionEnum Position { get; private set; }
    }

    /// <summary>
    /// PresentCashResult
    /// Return result of presenting cash operation
    /// </summary>
    public sealed class PresentCashResult : DeviceResult
    {
        /// <summary>
        /// PresentCashResult
        /// Return result of presenting cash operation
        /// </summary>
        /// <param name="CompletionCode"></param>
        /// <param name="ErrorDescription"></param>
        /// <param name="ErrorCode"></param>
        /// <param name="NumBunchesRemaining">
        /// If this value is specified the number of additional bunches of items remaining to be presented as a result of the current operation. 
        /// If the number of additional bunches is at least one, but the precise number is unknown, NumBunchesRemaining will be -1. 
        /// If there are no bunches remaining, set to zero
        /// </param>
        public PresentCashResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                 string ErrorDescription = null,
                                 PresentCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null,
                                 int NumBunchesRemaining = 0) :
            base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.NumBunchesRemaining = NumBunchesRemaining;
        }
        public PresentCashResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                 int NumBunchesRemaining = 0) 
            : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.NumBunchesRemaining = NumBunchesRemaining;
        }

        /// <summary>
        /// Specifies the error code on presenting cash
        /// </summary>
        public PresentCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; private set; }

        /// <summary>
        /// If this value is specified the number of additional bunches of items remaining to be presented as a result of the current operation. 
        /// If the number of additional bunches is at least one, but the precise number is unknown, NumBunchesRemaining will be -1. 
        /// If there are no bunches remaining, set to zero
        /// </summary>

        public int NumBunchesRemaining { get; private set; }
    }

    /// <summary>
    /// ResetDeviceRequest
    /// The parameter class for the reset device operation
    /// </summary>
    public sealed class ResetDeviceRequest
    {
        /// <summary>
        /// ResetRequest
        /// The parameter class for the reset device operation
        /// </summary>
        /// <param name="CashUnit">This value specifies the name of the single cash unit to be used for the storage of any items found.</param>
        /// <param name="RetractArea">This field is used if items are to be moved to internal areas of the device, including cash units, the intermediate stacker, or the transport.</param>
        /// <param name="OutputPosition">The output position to which items are to be moved if the RetractArea is specified to OutputPosition.</param>
        public ResetDeviceRequest(string CashUnit, Retract RetractArea, CashDispenserCapabilitiesClass.OutputPositionEnum OutputPosition)
        {
            this.CashUnit = CashUnit;
            this.RetractArea = RetractArea;
            this.OutputPosition = OutputPosition;
        }

        /// <summary>
        /// This value specifies the name of the single cash unit to be used for the storage of any items found.
        /// </summary>
        public string CashUnit { get; private set; }
        /// <summary>
        /// This field is used if items are to be moved to internal areas of the device, including cash units, 
        /// the intermediate stacker, or the transport.
        /// </summary>
        public Retract RetractArea { get; private set; }
        /// <summary>
        /// The output position to which items are to be moved if the RetractArea is specified to OutputPosition.
        /// Following values are possible:
        /// 
        /// * ```default``` - The default configuration.
        /// * ```left``` - The left output position.
        /// * ```right``` - The right output position.
        /// * ```center``` - The center output position.
        /// * ```top``` - The top output position.
        /// * ```bottom``` - The bottom output position.
        /// * ```front``` - The front output position.
        /// * ```rear``` - The rear output position.
        /// </summary>
        public CashDispenserCapabilitiesClass.OutputPositionEnum OutputPosition { get; private set; }
    }

    /// <summary>
    /// ResetDeviceResult
    /// Return result of reset device
    /// </summary>
    public sealed class ResetDeviceResult : DeviceResult
    {
        /// <summary>
        /// ResetDeviceResult
        /// Return result of reset device
        /// </summary>
        /// <param name="CompletionCode"></param>
        /// <param name="ErrorDescription"></param>
        /// <param name="ErrorCode"></param>
        public ResetDeviceResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                 string ErrorDescription = null,
                                 ResetCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
        }

        /// <summary>
        /// Specifies the error code on reset device
        /// </summary>
        public ResetCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; private set; }
    }

    /// <summary>
    /// RejectResult
    /// Return result of reject operation
    /// </summary>
    public sealed class RejectResult : DeviceResult
    {
        /// <summary>
        /// RejectResult
        /// Return result of reject operation
        /// </summary>
        /// <param name="CompletionCode"></param>
        /// <param name="ErrorDescription"></param>
        /// <param name="ErrorCode"></param>
        public RejectResult(MessagePayload.CompletionCodeEnum CompletionCode,
                            string ErrorDescription = null,
                            RejectCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
        }

        /// <summary>
        /// Specifies the error code on reset device
        /// </summary>
        public RejectCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; private set; }
    }

    /// <summary>
    /// PrepareDispenseRequest
    /// Prepare to get ready to dispense media before dispense operation
    /// </summary>
    public sealed class PrepareDispenseRequest
    {

        /// <summary>
        /// Start - Initiates the action to prepare for the next dispense operation. 
        /// Stop - Stops the previously activated dispense preparation.
        /// </summary>
        public enum ActionEnum
        {
            Start,
            Stop,
        }

        /// <summary>
        /// PrepareDispenseRequest
        /// Prepare to get ready to dispense media before dispense operation
        /// </summary>
        /// <param name="Action">Action to prepare dispense operation</param>
        public PrepareDispenseRequest(ActionEnum Action)
        {
            this.Action = Action;
        }

        public ActionEnum Action { get; private set; }
    }

    /// <summary>
    /// PrepareDispenseResult
    /// Return result of preparation of the dispense operation.
    /// </summary>
    public sealed class PrepareDispenseResult : DeviceResult
    {
        public enum ErrorCodeEnum
        {
            ExchangeActive,
        }

        public PrepareDispenseResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                     string ErrorDescription = null,
                                     ErrorCodeEnum? ErrorCode = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
        }

        public ErrorCodeEnum? ErrorCode { get; private set; } = null;
    }
}
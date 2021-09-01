/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 * 
\***********************************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoTServer;
using XFS4IoT.Completions;
using XFS4IoT.Keyboard.Commands;
using XFS4IoT.Keyboard.Completions;
using XFS4IoTFramework.Common;
using XFS4IoT;

namespace XFS4IoTFramework.Keyboard
{
    [Flags]
    public enum KeyboardBeepEnum
    {
        Off = 0,
        Active = 0x1,
        InActive = 0x2,
    }

    public sealed class DefineLayoutResult : DeviceResult
    {
        public enum ErrorCodeEnum
        {
            ModeNotSupported,
            FrameCoordinate,
            KeyCoordinate,
            FrameOverlap,
            KeyOverlap,
            TooManyFrames,
            TooManyKeys,
            KeyAlreadyDefined
        }

        public DefineLayoutResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                  string ErrorDescription = null,
                                  DefineLayoutCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null)
            : base(CompletionCode, ErrorDescription)
        {
            this.ErrorCode = ErrorCode;
            this.PINBlock = null;
        }

        public DefineLayoutResult(MessagePayload.CompletionCodeEnum CompletionCode)
                : base(CompletionCode, null)
        {
            this.ErrorCode = null;
            this.PINBlock = PINBlock;
        }

        public DefineLayoutCompletion.PayloadData.ErrorCodeEnum? ErrorCode { get; init; }

        /// <summary>
        /// Encrypted PIN block
        /// </summary>
        public List<byte> PINBlock { get; init; }
    }

    public sealed class DataEntryRequest
    {
        public DataEntryRequest()
        { }

        /// <summary>
        /// Specifies the maximum number of digits which can be returned to the application in the output parameter. 
        /// </summary>
        public int MaxLen { get; init; }

        /// <summary>
        /// If autoEnd is set to true, the Service Provider terminates the command when the maximum number of digits are entered.
        /// Otherwise, the input is terminated by the user using one of the termination keys. 
        /// autoEnd is ignored when maxLen is set to zero.
        /// </summary>
        public bool AutoEnd { get; init; }

        /// <summary>
        /// Specifies a mask of those FDKs which are active during the execution of the command.
        /// </summary>
        public string ActiveFDKs { get; init; }

        /// <summary>
        /// Specifies a mask of those (other) Function Keys which are active during the execution of the command.
        /// </summary>
        public string ActiveKeys { get; init; }

        /// <summary>
        /// Specifies a mask of those FDKs which must terminate the execution of the command.
        /// </summary>
        public string TerminateFDKs { get; init; }

        /// <summary>
        /// Specifies a mask of those (other) Function Keys which must terminate the execution of the command.
        /// </summary>
        public string TerminateKeys { get; init; }
    }
}
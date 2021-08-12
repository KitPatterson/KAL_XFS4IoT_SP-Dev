/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Keyboard interface.
 * GetLayout_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.Keyboard.Completions
{
    [DataContract]
    [Completion(Name = "Keyboard.GetLayout")]
    public sealed class GetLayoutCompletion : Completion<GetLayoutCompletion.PayloadData>
    {
        public GetLayoutCompletion(int RequestId, GetLayoutCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, EntryModeClass EntryMode = null, List<FramesClass> Frames = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.EntryMode = EntryMode;
                this.Frames = Frames;
            }

            public enum ErrorCodeEnum
            {
                ModeNotSupported
            }

            /// <summary>
            /// Specifies the error code if applicable. The following values are possible:
            /// * ```modeNotSupported``` - The specified entry mode is not supported.
            /// </summary>
            [DataMember(Name = "errorCode")]
            public ErrorCodeEnum? ErrorCode { get; init; }

            [DataContract]
            public sealed class EntryModeClass
            {
                public EntryModeClass(bool? Data = null, bool? Pin = null, bool? Secure = null)
                {
                    this.Data = Data;
                    this.Pin = Pin;
                    this.Secure = Secure;
                }

                /// <summary>
                /// Specifies that the layout be applied to the [Keyboard.DataEntry](#keyboard.dataentry) method.
                /// </summary>
                [DataMember(Name = "data")]
                public bool? Data { get; init; }

                /// <summary>
                /// Specifies that the layout be applied to the [Keyboard.PinEntry](#keyboard.pinentry) method.
                /// </summary>
                [DataMember(Name = "pin")]
                public bool? Pin { get; init; }

                /// <summary>
                /// Specifies that the layout be applied to the [Keyboard.SecurekeyEntry](#keyboard.securekeyentry) method.
                /// </summary>
                [DataMember(Name = "secure")]
                public bool? Secure { get; init; }

            }

            /// <summary>
            /// Specifies entry mode to be returned. It can be one of the following flags, or zero to return all supported entry modes.
            /// </summary>
            [DataMember(Name = "entryMode")]
            public EntryModeClass EntryMode { get; init; }

            [DataContract]
            public sealed class FramesClass
            {
                public FramesClass(int? XPos = null, int? YPos = null, int? XSize = null, int? YSize = null, FloatActionClass FloatAction = null, List<FksClass> Fks = null)
                {
                    this.XPos = XPos;
                    this.YPos = YPos;
                    this.XSize = XSize;
                    this.YSize = YSize;
                    this.FloatAction = FloatAction;
                    this.Fks = Fks;
                }

                /// <summary>
                /// For ETS, specifies the left coordinate of the frame as an offset from the left edge of the screen. 
                /// For all other device types, this value is ignored
                /// </summary>
                [DataMember(Name = "xPos")]
                public int? XPos { get; init; }

                /// <summary>
                /// For ETS, specifies the top coordinate of the frame as an offset from the top edge of the screen. 
                /// For all other device types, this value is ignored
                /// </summary>
                [DataMember(Name = "yPos")]
                public int? YPos { get; init; }

                /// <summary>
                /// For ETS, specifies the width of the frame. For all other device types, this value is ignored
                /// </summary>
                [DataMember(Name = "xSize")]
                public int? XSize { get; init; }

                /// <summary>
                /// For ETS, specifies the height of the frame. For all other device types, this value is ignored
                /// </summary>
                [DataMember(Name = "ySize")]
                public int? YSize { get; init; }

                [DataContract]
                public sealed class FloatActionClass
                {
                    public FloatActionClass(bool? FloatX = null, bool? FloatY = null)
                    {
                        this.FloatX = FloatX;
                        this.FloatY = FloatY;
                    }

                    /// <summary>
                    /// Specifies that the Keyboard device will randomly shift the layout in a horizontal direction
                    /// </summary>
                    [DataMember(Name = "floatX")]
                    public bool? FloatX { get; init; }

                    /// <summary>
                    /// Specifies that the Keyboard device will randomly shift the layout in a vertical direction
                    /// </summary>
                    [DataMember(Name = "floatY")]
                    public bool? FloatY { get; init; }

                }

                /// <summary>
                /// Specifies if the device can float the touch keyboards
                /// </summary>
                [DataMember(Name = "floatAction")]
                public FloatActionClass FloatAction { get; init; }

                [DataContract]
                public sealed class FksClass
                {
                    public FksClass(int? XPos = null, int? YPos = null, int? XSize = null, int? YSize = null, string Fk = null, string ShiftFK = null)
                    {
                        this.XPos = XPos;
                        this.YPos = YPos;
                        this.XSize = XSize;
                        this.YSize = YSize;
                        this.Fk = Fk;
                        this.ShiftFK = ShiftFK;
                    }

                    /// <summary>
                    /// Specifies the position of the top left corner of the FK relative to the left hand side of the layout.
                    /// For ETS devices, must be in the range defined in the frame. 
                    /// For non-ETS devices, must be a value between 0 and 999, where 0 is the left edge and 999 is the right edge.
                    /// </summary>
                    [DataMember(Name = "xPos")]
                    public int? XPos { get; init; }

                    /// <summary>
                    /// Specifies the position of the top left corner of the FK relative to the left hand side of the layout.
                    /// For ETS devices, must be in the range defined in the frame. 
                    /// For non-ETS devices, must be a value between 0 and 999, where 0 is the top edge and 999 is the bottom edge.
                    /// </summary>
                    [DataMember(Name = "yPos")]
                    public int? YPos { get; init; }

                    /// <summary>
                    /// Specifies the FK width. 
                    /// For ETS, width is measured in pixels. For non-ETS devices, width is expressed as a value between 
                    /// 1 and 1000, where 1 is the smallest possible size and 1000 is the full width of the layout.
                    /// </summary>
                    [DataMember(Name = "xSize")]
                    public int? XSize { get; init; }

                    /// <summary>
                    /// Specifies the FK height.
                    /// For ETS, height is measured in pixels. 
                    /// For non-ETS devices, height is expressed as a value between 1 and 1000, where 1 is the smallest 
                    /// possible size and 1000 is the full height of the layout.
                    /// </summary>
                    [DataMember(Name = "ySize")]
                    public int? YSize { get; init; }

                    /// <summary>
                    /// Specifies the FK code associated with the physical area in non-shifted mode.
                    /// </summary>
                    [DataMember(Name = "fk")]
                    [DataTypes(Pattern = "^fk([0-9]|[A-F]|Enter|Cancel|Clear|Backspace|Help|DecPoint|Shift|RES0[1-8]|OEM0[1-6]|0{2,3})$|^fdk(0[1-9]|[12][0-9]|3[0-2])$")]
                    public string Fk { get; init; }

                    /// <summary>
                    /// Specifies the FK code associated with the physical key in shifted mode.
                    /// </summary>
                    [DataMember(Name = "shiftFK")]
                    [DataTypes(Pattern = "^fk([0-9]|[A-F]|Enter|Cancel|Clear|Backspace|Help|DecPoint|Shift|RES0[1-8]|OEM0[1-6]|0{2,3})$|^fdk(0[1-9]|[12][0-9]|3[0-2])$")]
                    public string ShiftFK { get; init; }

                }

                /// <summary>
                /// Defining details of the keys in the keyboard.
                /// </summary>
                [DataMember(Name = "fks")]
                public List<FksClass> Fks { get; init; }

            }

            /// <summary>
            /// There can be one or more frame structures included
            /// </summary>
            [DataMember(Name = "frames")]
            public List<FramesClass> Frames { get; init; }

        }
    }
}

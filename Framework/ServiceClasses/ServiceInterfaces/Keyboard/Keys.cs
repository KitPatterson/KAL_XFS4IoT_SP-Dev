/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFS4IoTFramework.Keyboard
{
    /// <summary>
    /// Specifies entry mode to which the layout applies. The following values are possible:
    /// * ```Data``` - Specifies that the layout be applied to the DataEntry method.
    /// * ```Pin``` - Specifies that the layout be applied to the PinEntry method.
    /// * ```Secure``` - Specifies that the layout be applied to the SecureKeyEntry method.
    /// </summary>
    public enum EntryModeEnum
    {
        Data,
        Pin,
        Secure
    }

    public sealed class FrameClass
    {
        [Flags]
        public enum FloatActionEnum
        {
            NotSupported = 0,
            FloatX = 0x0001,
            FloatY = 0x0002,
        }

        public FrameClass(int XPos, 
                          int YPos, 
                          int XSize, 
                          int YSize, 
                          FloatActionEnum FloatAction, 
                          List<FunctionKeyClass> FunctionKeys)
        {
            this.XPos = XPos;
            this.YPos = YPos;
            this.XSize = XSize;
            this.YSize = YSize;
            this.FloatAction = FloatAction;
            this.FunctionKeys = FunctionKeys;
        }

        /// <summary>
        /// For [ETS](#keyboard.generalinformation.ets), specifies the left coordinate of the frame as an offset from the left edge of the screen. 
        /// For all other device types, this value is ignored
        /// </summary>
        public int XPos { get; init; }

        /// <summary>
        /// For [ETS](#keyboard.generalinformation.ets), specifies the top coordinate of the frame as an offset from the top edge of the screen. 
        /// For all other device types, this value is ignored
        /// </summary>
        public int YPos { get; init; }

        /// <summary>
        /// For [ETS](#keyboard.generalinformation.ets), specifies the width of the frame. For all other device types, this value is ignored
        /// </summary>
        public int XSize { get; init; }

        /// <summary>
        /// For [ETS](#keyboard.generalinformation.ets), specifies the height of the frame. For all other device types, this value is ignored
        /// </summary>
        public int YSize { get; init; }

        /// <summary>
        /// Specifies if the device can float the touch keyboards
        /// </summary>
        public FloatActionEnum FloatAction { get; init; }

        public sealed class FunctionKeyClass
        {
            public enum FunctionKeyTypeEnum
            {
                unused,
                zero,
                one,
                two,
                three,
                four,
                five,
                six,
                seven,
                eight,
                nine,
                enter,
                cancel,
                clear,
                backspace,
                help,
                decPoint,
                doubleZero,
                tripleZero,
                res1,
                res2,
                res3,
                res4,
                res5,
                res6,
                res7,
                res8,
                oem1,
                oem2,
                oem3,
                oem4,
                oem5,
                oem6,
                a,
                b,
                c,
                d,
                e,
                f,
                shift,
                fdk01,
                fdk02,
                fdk03,
                fdk04,
                fdk05,
                fdk06,
                fdk07,
                fdk08,
                fdk09,
                fdk10,
                fdk11,
                fdk12,
                fdk13,
                fdk14,
                fdk15,
                fdk16,
                fdk17,
                fdk18,
                fdk19,
                fdk20,
                fdk21,
                fdk22,
                fdk23,
                fdk24,
                fdk25,
                fdk26,
                fdk27,
                fdk28,
                fdk29,
                fdk30,
                fdk31,
                fdk32
            }

            public FunctionKeyClass(int XPos, 
                                    int YPos,
                                    int XSize, 
                                    int YSize, 
                                    KeyTypeEnum KeyType, 
                                    FunctionKeyTypeEnum FK, 
                                    FunctionKeyTypeEnum ShiftFK)
            {
                this.XPos = XPos;
                this.YPos = YPos;
                this.XSize = XSize;
                this.YSize = YSize;
                this.KeyType = KeyType;
                this.FK = FK;
                this.ShiftFK = ShiftFK;
            }

            /// <summary>
            /// Specifies the position of the top left corner of the FK relative to the left hand side of the layout.
            /// For [ETS](#keyboard.generalinformation.ets) devices, must be in the range defined in the frame. 
            /// For non-ETS devices, must be a value between 0 and 999, where 0 is the left edge and 999 is the right edge.
            /// </summary>
            public int XPos { get; init; }

            /// <summary>
            /// Specifies the position of the top left corner of the Function Key (FK) relative to the left hand side of the layout.
            /// For [ETS](#keyboard.generalinformation.ets) devices, must be in the range defined in the frame. 
            /// For non-ETS devices, must be a value between 0 and 999, where 0 is the top edge and 999 is the bottom edge.
            /// </summary>
            public int YPos { get; init; }

            /// <summary>
            /// Specifies the Function Key (FK) width. 
            /// For [ETS](#keyboard.generalinformation.ets), width is measured in pixels. For non-ETS devices, width is expressed as a value between 
            /// 1 and 1000, where 1 is the smallest possible size and 1000 is the full width of the layout.
            /// </summary>
            public int XSize { get; init; }

            /// <summary>
            /// Specifies the Function Key (FK) height.
            /// For [ETS](#keyboard.generalinformation.ets), height is measured in pixels. 
            /// For non-ETS devices, height is expressed as a value between 1 and 1000, where 1 is the smallest 
            /// possible size and 1000 is the full height of the layout.
            /// </summary>
            public int YSize { get; init; }

            public enum KeyTypeEnum
            {
                FK,
                FDK
            }

            /// <summary>
            /// Defines the type of XFS key definition value is represented by *fk* and *shiftFK*.
            /// If the key is physically present on the device but it is not used, this property can be omitted.
            /// The following values are possible:
            /// * ```FK``` - Function Keys are being used.
            /// * ```FDK``` - Function Descriptor Keys are being used.
            /// </summary>
            public KeyTypeEnum KeyType { get; init; }

            /// <summary>
            /// Specifies the Function Key associated with the physical area in non-shifted mode.
            /// This property is not required if the *keyType* is omitted.
            /// </summary>
            public FunctionKeyTypeEnum FK { get; init; }

            /// <summary>
            /// Specifies the Function Key associated with the physical key in shifted mode.
            /// This property is not required if the *keyType* is omitted.
            /// </summary>
            public FunctionKeyTypeEnum ShiftFK { get; init; }


            public static readonly Dictionary<string, FunctionKeyTypeEnum> StringFunctionKeyTypeMap = new()
            {
                { "a", FunctionKeyTypeEnum.a },
                { "b", FunctionKeyTypeEnum.b },
                { "c", FunctionKeyTypeEnum.c },
                { "d", FunctionKeyTypeEnum.d },
                { "e", FunctionKeyTypeEnum.e },
                { "f", FunctionKeyTypeEnum.f },
                { "one", FunctionKeyTypeEnum.one },
                { "two", FunctionKeyTypeEnum.two },
                { "three", FunctionKeyTypeEnum.three },
                { "four", FunctionKeyTypeEnum.four },
                { "five", FunctionKeyTypeEnum.five },
                { "six", FunctionKeyTypeEnum.six },
                { "seven", FunctionKeyTypeEnum.seven },
                { "eight", FunctionKeyTypeEnum.eight },
                { "nine", FunctionKeyTypeEnum.nine },
                { "zero", FunctionKeyTypeEnum.zero },
                { "enter", FunctionKeyTypeEnum.enter },
                { "cancel", FunctionKeyTypeEnum.cancel },
                { "clear", FunctionKeyTypeEnum.clear },
                { "backspace", FunctionKeyTypeEnum.backspace },
                { "help", FunctionKeyTypeEnum.help },
                { "decPoint", FunctionKeyTypeEnum.decPoint },
                { "doubleZero", FunctionKeyTypeEnum.doubleZero },
                { "tripleZero", FunctionKeyTypeEnum.tripleZero },
                { "res1", FunctionKeyTypeEnum.res1 },
                { "res2", FunctionKeyTypeEnum.res2 },
                { "res3", FunctionKeyTypeEnum.res3 },
                { "res4", FunctionKeyTypeEnum.res4 },
                { "res5", FunctionKeyTypeEnum.res5 },
                { "res6", FunctionKeyTypeEnum.res6 },
                { "res7", FunctionKeyTypeEnum.res7 },
                { "oem1", FunctionKeyTypeEnum.oem1 },
                { "oem2", FunctionKeyTypeEnum.oem2 },
                { "oem3", FunctionKeyTypeEnum.oem3 },
                { "oem4", FunctionKeyTypeEnum.oem4 },
                { "oem5", FunctionKeyTypeEnum.oem5 },
                { "oem6", FunctionKeyTypeEnum.oem6 },
                { "shift", FunctionKeyTypeEnum.shift },
                { "fdk01", FunctionKeyTypeEnum.fdk01 },
                { "fdk02", FunctionKeyTypeEnum.fdk02 },
                { "fdk03", FunctionKeyTypeEnum.fdk03 },
                { "fdk04", FunctionKeyTypeEnum.fdk04 },
                { "fdk05", FunctionKeyTypeEnum.fdk05 },
                { "fdk06", FunctionKeyTypeEnum.fdk06 },
                { "fdk07", FunctionKeyTypeEnum.fdk07 },
                { "fdk08", FunctionKeyTypeEnum.fdk08 },
                { "fdk09", FunctionKeyTypeEnum.fdk09 },
                { "fdk10", FunctionKeyTypeEnum.fdk10 },
                { "fdk11", FunctionKeyTypeEnum.fdk11 },
                { "fdk12", FunctionKeyTypeEnum.fdk12 },
                { "fdk13", FunctionKeyTypeEnum.fdk13 },
                { "fdk14", FunctionKeyTypeEnum.fdk14 },
                { "fdk15", FunctionKeyTypeEnum.fdk15 },
                { "fdk16", FunctionKeyTypeEnum.fdk16 },
                { "fdk17", FunctionKeyTypeEnum.fdk17 },
                { "fdk18", FunctionKeyTypeEnum.fdk18 },
                { "fdk19", FunctionKeyTypeEnum.fdk19 },
                { "fdk20", FunctionKeyTypeEnum.fdk20 },
                { "fdk21", FunctionKeyTypeEnum.fdk21 },
                { "fdk22", FunctionKeyTypeEnum.fdk22 },
                { "fdk23", FunctionKeyTypeEnum.fdk23 },
                { "fdk24", FunctionKeyTypeEnum.fdk24 },
                { "fdk25", FunctionKeyTypeEnum.fdk25 },
                { "fdk26", FunctionKeyTypeEnum.fdk26 },
                { "fdk27", FunctionKeyTypeEnum.fdk27 },
                { "fdk28", FunctionKeyTypeEnum.fdk28 },
                { "fdk29", FunctionKeyTypeEnum.fdk29 },
                { "fdk30", FunctionKeyTypeEnum.fdk30 },
                { "fdk31", FunctionKeyTypeEnum.fdk31 },
                { "fdk32", FunctionKeyTypeEnum.fdk32 }
            };
        }

        /// <summary>
        /// Defining details of the keys in the keyboard.
        /// </summary>
        public List<FunctionKeyClass> FunctionKeys { get; init; }
    }
}

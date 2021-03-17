/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * DispLight.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.TextTerminal.Completions
{
    [DataContract]
    [Completion(Name = "TextTerminal.DispLight")]
    public sealed class DispLightCompletion : Completion<DispLightCompletion.PayloadData>
    {
        public DispLightCompletion(string RequestId, DispLightCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, bool? Mode = null)
                : base(CompletionCode, ErrorDescription)
            {
                ErrorDescription.IsNotNullOrWhitespace($"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(DispLightCompletion.PayloadData)}");

                this.Mode = Mode;
            }

            /// <summary>
            ///Specifies whether the lighting of the text terminal unit is switched on (TRUE) or off (FALSE).
            /// </summary>
            [DataMember(Name = "mode")] 
            public bool? Mode { get; private set; }

        }
    }
}
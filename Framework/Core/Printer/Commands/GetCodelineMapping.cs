/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * GetCodelineMapping.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{
    //Original name = GetCodelineMapping
    [DataContract]
    [Command(Name = "Printer.GetCodelineMapping")]
    public sealed class GetCodelineMappingCommand : Command<GetCodelineMappingCommand.PayloadData>
    {
        public GetCodelineMappingCommand(string RequestId, GetCodelineMappingCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            public enum CodelineFormatEnum
            {
                Cmc7,
                E13b,
            }


            public PayloadData(int Timeout, CodelineFormatEnum? CodelineFormat = null)
                : base(Timeout)
            {
                this.CodelineFormat = CodelineFormat;
            }

            /// <summary>
            ///Specifies the code-line format that the mapping for the special characters is required for. This field can be one of the following values:**cmc7**
            ////  Report the CMC7 mapping.**e13b**
            ////  Report the E13B mapping.
            /// </summary>
            [DataMember(Name = "codelineFormat")] 
            public CodelineFormatEnum? CodelineFormat { get; private set; }

        }
    }
}

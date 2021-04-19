/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * LoadDefinition_g.cs uses automatically generated parts. 
 * created at 4/19/2021 3:05:28 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.Printer.Commands
{
    //Original name = LoadDefinition
    [DataContract]
    [Command(Name = "Printer.LoadDefinition")]
    public sealed class LoadDefinitionCommand : Command<LoadDefinitionCommand.PayloadData>
    {
        public LoadDefinitionCommand(string RequestId, LoadDefinitionCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(int Timeout, string FileName = null, bool? Overwrite = null)
                : base(Timeout)
            {
                this.FileName = FileName;
                this.Overwrite = Overwrite;
            }

            /// <summary>
            ///This is the full path and file name of the file to be loaded. This value cannot contain UNICODEcharacters. The file contains the form (including sub-forms and frames) or media definition in textformat as described in[Form, Sub-Form, Field, Frame, Table and Media Definitions](#printer.generalinformation.formandmediadefinitions).Only one form or media definition can be defined in the file.
            /// </summary>
            [DataMember(Name = "fileName")] 
            public string FileName { get; private set; }
            /// <summary>
            ///Specifies if an existing form or media definition with the same name is to be replaced. If this flagis true then an existing form or media definition with the same name will be replaced, unless thecommand fails with an error, where the definition will remain unchanged. If this flag is false thiscommand will fail with an error if the form or media definition already exists.
            /// </summary>
            [DataMember(Name = "overwrite")] 
            public bool? Overwrite { get; private set; }

        }
    }
}

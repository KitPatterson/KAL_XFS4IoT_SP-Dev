/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * SetResolution.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.TextTerminal.Commands
{
    //Original name = SetResolution
    [DataContract]
    [Command(Name = "TextTerminal.SetResolution")]
    public sealed class SetResolutionCommand : Command<SetResolutionCommand.PayloadData>
    {
        public SetResolutionCommand(string RequestId, SetResolutionCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            /// <summary>
            ///Specifies the horizontal size of the display of the text terminal unit.
            /// </summary>
            public class ResolutionClass
            {
                [DataMember(Name = "sizeX")] 
                public int? SizeX { get; private set; }
                [DataMember(Name = "sizeY")] 
                public int? SizeY { get; private set; }

                public ResolutionClass (int? SizeX, int? SizeY)
                {
                    this.SizeX = SizeX;
                    this.SizeY = SizeY;
                }


            }


            public PayloadData(int Timeout, object Resolution = null)
                : base(Timeout)
            {
                this.Resolution = Resolution;
            }

            /// <summary>
            ///Specifies the horizontal size of the display of the text terminal unit.
            /// </summary>
            [DataMember(Name = "resolution")] 
            public object Resolution { get; private set; }

        }
    }
}

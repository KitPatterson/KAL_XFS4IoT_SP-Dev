/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ChipPower.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CardReader.Commands
{
    //Original name = ChipPower
    [DataContract]
    [Command(Name = "CardReader.ChipPower")]
    public sealed class ChipPowerCommand : Command<ChipPowerCommand.PayloadData>
    {
        public ChipPowerCommand(string RequestId, ChipPowerCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            public enum ChipPowerEnum
            {
                Cold,
                Warm,
                Off,
            }


            public PayloadData(int Timeout, ChipPowerEnum? ChipPower = null)
                : base(Timeout)
            {
                this.ChipPower = ChipPower;
            }

            /// <summary>
            ///Specifies the action to perform as one of the following:**cold*
            ////The chip is powered on and reset.**warm*
            ////The chip is reset.**off**
            ////The chip is powered off.
            /// </summary>
            [DataMember(Name = "chipPower")] 
            public ChipPowerEnum? ChipPower { get; private set; }

        }
    }
}

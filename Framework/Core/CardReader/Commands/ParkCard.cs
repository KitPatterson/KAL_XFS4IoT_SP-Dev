/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * ParkCard.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Commands;

namespace XFS4IoT.CardReader.Commands
{
    //Original name = ParkCard
    [DataContract]
    [Command(Name = "CardReader.ParkCard")]
    public sealed class ParkCardCommand : Command<ParkCardCommand.PayloadData>
    {
        public ParkCardCommand(string RequestId, ParkCardCommand.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            public enum DirectionEnum
            {
                In,
                Out,
            }


            public PayloadData(int Timeout, DirectionEnum? Direction = null)
                : base(Timeout)
            {
                this.Direction = Direction;
            }

            /// <summary>
            ///Specifies which way to move the card as one of the following values:**in**
            ////The card is moved to the parking station from the read/write, chip I/O or transport position.**out**
            ////The card is moved from the parking station to the read/write, chip I/O or transport  position. Once the card has been moved any command can be executed e.g. [CardReader.EjectCard](#command-CardReader.EjectCard) or [CardReader.ReadRawData](#command-CardReader.ReadRawData).
            /// </summary>
            [DataMember(Name = "direction")] 
            public DirectionEnum? Direction { get; private set; }

        }
    }
}

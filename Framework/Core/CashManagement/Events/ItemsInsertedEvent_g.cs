/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashManagement interface.
 * ItemsInsertedEvent_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.CashManagement.Events
{

    [DataContract]
    [Event(Name = "CashManagement.ItemsInsertedEvent")]
    public sealed class ItemsInsertedEvent : Event<ItemsInsertedEvent.PayloadData>
    {

        public ItemsInsertedEvent(int RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {

            public PayloadData(string Position = null)
                : base()
            {
                this.Position = Position;
            }

            /// <summary>
            /// Supplies the input or output position as one of the following values. If not specified, the default position
            /// applies.
            /// 
            /// * ```inDefault``` - Default input position.
            /// * ```inLeft``` - Left input position.
            /// * ```inRight``` - Right input position.
            /// * ```inCenter``` - Center input position.
            /// * ```inTop``` - Top input position.
            /// * ```inBottom``` - Bottom input position.
            /// * ```inFront``` - Front input position.
            /// * ```inRear``` - Rear input position.
            /// * ```outDefault``` - Default output position.
            /// * ```outLeft``` - Left output position.
            /// * ```outRight``` - Right output position.
            /// * ```outCenter``` - Center output position.
            /// * ```outTop``` - Top output position.
            /// * ```outBottom``` - Bottom output position.
            /// * ```outFront``` - Front output position.
            /// * ```outRear``` - Rear output position.
            /// <example>inLeft</example>
            /// </summary>
            [DataMember(Name = "position")]
            public string Position { get; init; }

        }

    }
}

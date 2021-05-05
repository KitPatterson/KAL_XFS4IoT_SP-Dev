/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashAcceptor interface.
 * GetPositionCapabilities_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CashAcceptor.Completions
{
    [DataContract]
    [Completion(Name = "CashAcceptor.GetPositionCapabilities")]
    public sealed class GetPositionCapabilitiesCompletion : Completion<GetPositionCapabilitiesCompletion.PayloadData>
    {
        public GetPositionCapabilitiesCompletion(string RequestId, GetPositionCapabilitiesCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            [DataContract]
            public sealed class PosCapabilitiesClass
            {
                /// <summary>
                /// Specifies one of the input or output positions as one of the following values:
                /// 
                /// \"inLeft\": Left input position.
                /// 
                /// \"inRight\": Right input position.
                /// 
                /// \"inCenter\": Center input position.
                /// 
                /// \"inTop\": Top input position.
                /// 
                /// \"inBottom\": Bottom input position.
                /// 
                /// \"inFront\": Front input position.
                /// 
                /// \"inRear\": Rear input position.
                /// 
                /// \"outLeft\": Left output position.
                /// 
                /// \"outRight\": Right output position.
                /// 
                /// \"outCenter\": Center output position.
                /// 
                /// \"outTop\": Top output position.
                /// 
                /// \"outBottom\": Bottom output position.
                /// 
                /// \"outFront\": Front output position.
                /// 
                /// \"outRear\": Rear output position.
                /// </summary>
                public enum PositionEnum
                {
                    InLeft,
                    InRight,
                    InCenter,
                    InTop,
                    InBottom,
                    InFront,
                    InRear,
                    OutLeft,
                    OutRight,
                    OutCenter,
                    OutTop,
                    OutBottom,
                    OutFront,
                    OutRear,
                }

                public PosCapabilitiesClass(PositionEnum? Position = null, object Usage = null, bool? ShutterControl = null, bool? ItemsTakenSensor = null, bool? ItemsInsertedSensor = null, object RetractAreas = null, bool? PresentControl = null, bool? PreparePresent = null)
                    : base()
                {
                    this.Position = Position;
                    this.Usage = Usage;
                    this.ShutterControl = ShutterControl;
                    this.ItemsTakenSensor = ItemsTakenSensor;
                    this.ItemsInsertedSensor = ItemsInsertedSensor;
                    this.RetractAreas = RetractAreas;
                    this.PresentControl = PresentControl;
                    this.PreparePresent = PreparePresent;
                }

                /// <summary>
                /// Specifies one of the input or output positions as one of the following values:
                /// 
                /// \"inLeft\": Left input position.
                /// 
                /// \"inRight\": Right input position.
                /// 
                /// \"inCenter\": Center input position.
                /// 
                /// \"inTop\": Top input position.
                /// 
                /// \"inBottom\": Bottom input position.
                /// 
                /// \"inFront\": Front input position.
                /// 
                /// \"inRear\": Rear input position.
                /// 
                /// \"outLeft\": Left output position.
                /// 
                /// \"outRight\": Right output position.
                /// 
                /// \"outCenter\": Center output position.
                /// 
                /// \"outTop\": Top output position.
                /// 
                /// \"outBottom\": Bottom output position.
                /// 
                /// \"outFront\": Front output position.
                /// 
                /// \"outRear\": Rear output position.
                /// </summary>
                [DataMember(Name = "position")] 
                public PositionEnum? Position { get; private set; }

                /// <summary>
                /// Indicates if an output position is used to reject or rollback.
                /// </summary>
                [DataMember(Name = "usage")] 
                public object Usage { get; private set; }

                /// <summary>
                /// If set to TRUE the shutter is controlled implicitly by the Service. If set to FALSE the shutter must be controlled explicitly by the application using the OpenShutter and the CloseShutter commands. In either case the CashAcceptor.PresentMedia command may be used if the presentControl field is reported as FALSE. The shutterControl field is always set to TRUE if the described position has no shutter.
                /// </summary>
                [DataMember(Name = "shutterControl")] 
                public bool? ShutterControl { get; private set; }

                /// <summary>
                /// Specifies whether or not the described position can detect when items at the exit position are taken by the user. If set to TRUE the Service generates an accompanying CashAcceptor.ItemsTaken event. If set to FALSE this event is not generated. This field relates to output and refused positions.
                /// </summary>
                [DataMember(Name = "itemsTakenSensor")] 
                public bool? ItemsTakenSensor { get; private set; }

                /// <summary>
                /// Specifies whether the described position has the ability to detect when items have been inserted by the user. If set to TRUE the Service Provider generates an accompanying CashAcceptor.ItemsInserted event. If set to FALSE this event is not generated. This field relates to all input positions.
                /// </summary>
                [DataMember(Name = "itemsInsertedSensor")] 
                public bool? ItemsInsertedSensor { get; private set; }

                /// <summary>
                /// Specifies the areas to which items may be retracted from this position. If the device does not have a retract capability all values will be FALSE.
                /// </summary>
                [DataMember(Name = "retractAreas")] 
                public object RetractAreas { get; private set; }

                /// <summary>
                /// Specifies how the presenting of media items is controlled. If presentControl is TRUE then the CashAcceptor.PresentMedia command is not supported and items are moved to the output position for removal as part of the relevant command, e.g. CashAcceptor.CashIn or CashAcceptor.CashInRollback where there is implicit shutter control. If presentControl is FALSE then items returned or rejected can be moved to the output position using the CashAcceptor.PresentMedia command, this includes items returned or rejected as part of a CashAcceptor.CashIn or CashAcceptor.CashInRollback operation. The CashAcceptor.PresentMedia command will open and close the shutter implicitly.
                /// </summary>
                [DataMember(Name = "presentControl")] 
                public bool? PresentControl { get; private set; }

                /// <summary>
                /// Specifies how the presenting of items is controlled. If preparePresent is FALSE then items to be removed are moved to the output position as part of the relevant command e.g. CashAcceptor.OpenShutter or CashAcceptor.PresentMedia or CashAcceptor.CashInRollback. If preparePresent is TRUE then items are moved to the output position using the CashAcceptor.PreparePresent command.
                /// </summary>
                [DataMember(Name = "preparePresent")] 
                public bool? PreparePresent { get; private set; }

            }


            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, List<PosCapabilitiesClass> PosCapabilities = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.PosCapabilities = PosCapabilities;
            }

            /// <summary>
            /// Array of position capabilities for all positions configured in this service.
            /// </summary>
            [DataMember(Name = "posCapabilities")] 
            public List<PosCapabilitiesClass> PosCapabilities{ get; private set; }

        }
    }
}

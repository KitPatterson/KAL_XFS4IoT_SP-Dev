/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Common interface.
 * Capabilities.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.Common.Completions
{
    [DataContract]
    [Completion(Name = "Common.Capabilities")]
    public sealed class CapabilitiesCompletion : Completion<CapabilitiesCompletion.PayloadData>
    {
        public CapabilitiesCompletion(string RequestId, CapabilitiesCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            [DataContract]
            public sealed class InterfacesClass
            {
                /// <summary>
                ///Name of supported XFS4IoT interface.
                /// </summary>
                public enum NameEnum
                {
                    Common,
                    CardReader,
                    CashAcceptor,
                    CashDispenser,
                    CashManagement,
                    PinPad,
                    Crypto,
                    KeyManagement,
                    Keyboard,
                    TextTerminal,
                    Printer,
                }

                public InterfacesClass(NameEnum? Name = null, List<string> Commands = null, List<string> Events = null, int? MaximumRequests = null, List<string> AuthenticationRequired = null)
                    : base()
                {
                    this.Name = Name;
                    this.Commands = Commands;
                    this.Events = Events;
                    this.MaximumRequests = MaximumRequests;
                    this.AuthenticationRequired = AuthenticationRequired;
                }

                /// <summary>
                ///Name of supported XFS4IoT interface.
                /// </summary>
                [DataMember(Name = "name")] 
                public NameEnum? Name { get; private set; }

                /// <summary>
                ///Full array of commands supported by this XFS4IoT interface.
                /// </summary>
                [DataMember(Name = "commands")] 
                public List<string> Commands { get; private set; }

                /// <summary>
                ///Full array of events supported by this XFS4IoT interface.
                /// </summary>
                [DataMember(Name = "events")] 
                public List<string> Events { get; private set; }

                /// <summary>
                ///Specifies the maximum number of requests which can be queued by the Service. This will be omitted if not reported. This will be zero if the maximum number of requests is unlimited.
                /// </summary>
                [DataMember(Name = "maximumRequests")] 
                public int? MaximumRequests { get; private set; }

                /// <summary>
                ///Array of commands, which need to be authenticated using the security interface.
                /// </summary>
                [DataMember(Name = "authenticationRequired")] 
                public List<string> AuthenticationRequired { get; private set; }

            }

            /// <summary>
            ///Capability information common to all XFS4IoT services.
            /// </summary>
            public class CommonClass
            {
                [DataMember(Name = "serviceVersion")] 
                public string ServiceVersion { get; private set; }
                
                /// <summary>
                ///Array of deviceInformation structures. If the service uses more than one device there will be on array element for each device.
                /// </summary>
                public class DeviceInformationClass 
                {
                    [DataMember(Name = "modelName")] 
                    public string ModelName { get; private set; }
                    [DataMember(Name = "serialNumber")] 
                    public string SerialNumber { get; private set; }
                    [DataMember(Name = "revisionNumber")] 
                    public string RevisionNumber { get; private set; }
                    [DataMember(Name = "modelDescription")] 
                    public string ModelDescription { get; private set; }
                    
                    /// <summary>
                    ///Array of firmware structures specifying the names and version numbers of the firmware that is present.Single or multiple firmware versions can be reported. If the firmware versions are not reported, then this property is omitted.
                    /// </summary>
                    public class FirmwareClass 
                    {
                        [DataMember(Name = "firmwareName")] 
                        public string FirmwareName { get; private set; }
                        [DataMember(Name = "firmwareVersion")] 
                        public string FirmwareVersion { get; private set; }
                        [DataMember(Name = "hardwareRevision")] 
                        public string HardwareRevision { get; private set; }

                        public FirmwareClass (string FirmwareName, string FirmwareVersion, string HardwareRevision)
                        {
                            this.FirmwareName = FirmwareName;
                            this.FirmwareVersion = FirmwareVersion;
                            this.HardwareRevision = HardwareRevision;
                        }
                    }
                    [DataMember(Name = "firmware")] 
                    public FirmwareClass Firmware { get; private set; }
                    
                    /// <summary>
                    ///Array of software structures specifying the names and version numbers of the software components that are present.Single or multiple software versions can be reported. If the software versions are not reported, then this property is omitted.
                    /// </summary>
                    public class SoftwareClass 
                    {
                        [DataMember(Name = "firmwareName")] 
                        public string FirmwareName { get; private set; }
                        [DataMember(Name = "firmwareVersion")] 
                        public string FirmwareVersion { get; private set; }
                        [DataMember(Name = "hardwareRevision")] 
                        public string HardwareRevision { get; private set; }

                        public SoftwareClass (string FirmwareName, string FirmwareVersion, string HardwareRevision)
                        {
                            this.FirmwareName = FirmwareName;
                            this.FirmwareVersion = FirmwareVersion;
                            this.HardwareRevision = HardwareRevision;
                        }
                    }
                    [DataMember(Name = "software")] 
                    public SoftwareClass Software { get; private set; }

                    public DeviceInformationClass (string ModelName, string SerialNumber, string RevisionNumber, string ModelDescription, FirmwareClass Firmware, SoftwareClass Software)
                    {
                        this.ModelName = ModelName;
                        this.SerialNumber = SerialNumber;
                        this.RevisionNumber = RevisionNumber;
                        this.ModelDescription = ModelDescription;
                        this.Firmware = Firmware;
                        this.Software = Software;
                    }
                }
                [DataMember(Name = "deviceInformation")] 
                public DeviceInformationClass DeviceInformation { get; private set; }
                
                /// <summary>
                ///Specifies additional information about the Service while in Vendor Dependent Mode. If omitted, all sessions must be closed before entry to VDM.
                /// </summary>
                public class VendorModeIformationClass 
                {
                    [DataMember(Name = "allowOpenSessions")] 
                    public bool? AllowOpenSessions { get; private set; }
                    [DataMember(Name = "allowedExecuteCommands")] 
                    public List<string> AllowedExecuteCommands { get; private set; }

                    public VendorModeIformationClass (bool? AllowOpenSessions, List<string> AllowedExecuteCommands)
                    {
                        this.AllowOpenSessions = AllowOpenSessions;
                        this.AllowedExecuteCommands = AllowedExecuteCommands;
                    }
                }
                [DataMember(Name = "vendorModeIformation")] 
                public VendorModeIformationClass VendorModeIformation { get; private set; }
                [DataMember(Name = "extra")] 
                public List<string> Extra { get; private set; }
                
                /// <summary>
                ///Specifies which guidance lights are available
                /// </summary>
                public class GuideLightsClass 
                {
                    
                    /// <summary>
                    ///Indicates which flash rates are supported by the guidelight.
                    /// </summary>
                    public class FlashRateClass 
                    {
                        [DataMember(Name = "slow")] 
                        public bool? Slow { get; private set; }
                        [DataMember(Name = "medium")] 
                        public bool? Medium { get; private set; }
                        [DataMember(Name = "quick")] 
                        public bool? Quick { get; private set; }
                        [DataMember(Name = "continuous")] 
                        public bool? Continuous { get; private set; }

                        public FlashRateClass (bool? Slow, bool? Medium, bool? Quick, bool? Continuous)
                        {
                            this.Slow = Slow;
                            this.Medium = Medium;
                            this.Quick = Quick;
                            this.Continuous = Continuous;
                        }
                    }
                    [DataMember(Name = "flashRate")] 
                    public FlashRateClass FlashRate { get; private set; }
                    
                    /// <summary>
                    ///Indicates which colors are supported by the guidelight.
                    /// </summary>
                    public class ColorClass 
                    {
                        [DataMember(Name = "red")] 
                        public bool? Red { get; private set; }
                        [DataMember(Name = "green")] 
                        public bool? Green { get; private set; }
                        [DataMember(Name = "yellow")] 
                        public bool? Yellow { get; private set; }
                        [DataMember(Name = "blue")] 
                        public bool? Blue { get; private set; }
                        [DataMember(Name = "cyan")] 
                        public bool? Cyan { get; private set; }
                        [DataMember(Name = "magenta")] 
                        public bool? Magenta { get; private set; }
                        [DataMember(Name = "white")] 
                        public bool? White { get; private set; }

                        public ColorClass (bool? Red, bool? Green, bool? Yellow, bool? Blue, bool? Cyan, bool? Magenta, bool? White)
                        {
                            this.Red = Red;
                            this.Green = Green;
                            this.Yellow = Yellow;
                            this.Blue = Blue;
                            this.Cyan = Cyan;
                            this.Magenta = Magenta;
                            this.White = White;
                        }
                    }
                    [DataMember(Name = "color")] 
                    public ColorClass Color { get; private set; }
                    
                    /// <summary>
                    ///Indicates which directions are supported by the guidelight.
                    /// </summary>
                    public class DirectionClass 
                    {
                        [DataMember(Name = "entry")] 
                        public bool? Entry { get; private set; }
                        [DataMember(Name = "exit")] 
                        public bool? Exit { get; private set; }

                        public DirectionClass (bool? Entry, bool? Exit)
                        {
                            this.Entry = Entry;
                            this.Exit = Exit;
                        }
                    }
                    [DataMember(Name = "direction")] 
                    public DirectionClass Direction { get; private set; }

                    public GuideLightsClass (FlashRateClass FlashRate, ColorClass Color, DirectionClass Direction)
                    {
                        this.FlashRate = FlashRate;
                        this.Color = Color;
                        this.Direction = Direction;
                    }
                }
                [DataMember(Name = "guideLights")] 
                public GuideLightsClass GuideLights { get; private set; }
                [DataMember(Name = "powerSaveControl")] 
                public bool? PowerSaveControl { get; private set; }
                [DataMember(Name = "antiFraudModule")] 
                public bool? AntiFraudModule { get; private set; }
                [DataMember(Name = "synchronizableCommands")] 
                public List<string> SynchronizableCommands { get; private set; }
                [DataMember(Name = "endToEndSecurity")] 
                public bool? EndToEndSecurity { get; private set; }
                [DataMember(Name = "hardwareSecurityElement")] 
                public bool? HardwareSecurityElement { get; private set; }
                [DataMember(Name = "responseSecurityEnabled")] 
                public bool? ResponseSecurityEnabled { get; private set; }

                public CommonClass (string ServiceVersion, DeviceInformationClass DeviceInformation, VendorModeIformationClass VendorModeIformation, List<string> Extra, GuideLightsClass GuideLights, bool? PowerSaveControl, bool? AntiFraudModule, List<string> SynchronizableCommands, bool? EndToEndSecurity, bool? HardwareSecurityElement, bool? ResponseSecurityEnabled)
                {
                    this.ServiceVersion = ServiceVersion;
                    this.DeviceInformation = DeviceInformation;
                    this.VendorModeIformation = VendorModeIformation;
                    this.Extra = Extra;
                    this.GuideLights = GuideLights;
                    this.PowerSaveControl = PowerSaveControl;
                    this.AntiFraudModule = AntiFraudModule;
                    this.SynchronizableCommands = SynchronizableCommands;
                    this.EndToEndSecurity = EndToEndSecurity;
                    this.HardwareSecurityElement = HardwareSecurityElement;
                    this.ResponseSecurityEnabled = ResponseSecurityEnabled;
                }


            }

            /// <summary>
            ///Capability information for XFS4IoT services implementing the CardReader interface. This will be omitted if the CardReader interface is not supported.
            /// </summary>
            public class CardReaderClass
            {
                public enum TypeEnum
                {
                    Motor,
                    Swipe,
                    Dip,
                    LatchedDip,
                    Contactless,
                    IntelligentContactless,
                    Permanent,
                }
                [DataMember(Name = "type")] 
                public TypeEnum? Type { get; private set; }
                
                /// <summary>
                ///Specifies the tracks that can be read by the card reader.
                /// </summary>
                public class ReadTracksClass 
                {
                    [DataMember(Name = "track1")] 
                    public bool? Track1 { get; private set; }
                    [DataMember(Name = "track2")] 
                    public bool? Track2 { get; private set; }
                    [DataMember(Name = "track3")] 
                    public bool? Track3 { get; private set; }
                    [DataMember(Name = "watermark")] 
                    public bool? Watermark { get; private set; }
                    [DataMember(Name = "frontTrack1")] 
                    public bool? FrontTrack1 { get; private set; }
                    [DataMember(Name = "frontImage")] 
                    public bool? FrontImage { get; private set; }
                    [DataMember(Name = "backImage")] 
                    public bool? BackImage { get; private set; }
                    [DataMember(Name = "track1JIS")] 
                    public bool? Track1JIS { get; private set; }
                    [DataMember(Name = "track3JIS")] 
                    public bool? Track3JIS { get; private set; }
                    [DataMember(Name = "ddi")] 
                    public bool? Ddi { get; private set; }

                    public ReadTracksClass (bool? Track1, bool? Track2, bool? Track3, bool? Watermark, bool? FrontTrack1, bool? FrontImage, bool? BackImage, bool? Track1JIS, bool? Track3JIS, bool? Ddi)
                    {
                        this.Track1 = Track1;
                        this.Track2 = Track2;
                        this.Track3 = Track3;
                        this.Watermark = Watermark;
                        this.FrontTrack1 = FrontTrack1;
                        this.FrontImage = FrontImage;
                        this.BackImage = BackImage;
                        this.Track1JIS = Track1JIS;
                        this.Track3JIS = Track3JIS;
                        this.Ddi = Ddi;
                    }
                }
                [DataMember(Name = "readTracks")] 
                public ReadTracksClass ReadTracks { get; private set; }
                
                /// <summary>
                ///Specifies the tracks that can be read by the card reader.
                /// </summary>
                public class WriteTracksClass 
                {
                    [DataMember(Name = "track1")] 
                    public bool? Track1 { get; private set; }
                    [DataMember(Name = "track2")] 
                    public bool? Track2 { get; private set; }
                    [DataMember(Name = "track3")] 
                    public bool? Track3 { get; private set; }
                    [DataMember(Name = "frontTrack1")] 
                    public bool? FrontTrack1 { get; private set; }
                    [DataMember(Name = "track1JIS")] 
                    public bool? Track1JIS { get; private set; }
                    [DataMember(Name = "track3JIS")] 
                    public bool? Track3JIS { get; private set; }

                    public WriteTracksClass (bool? Track1, bool? Track2, bool? Track3, bool? FrontTrack1, bool? Track1JIS, bool? Track3JIS)
                    {
                        this.Track1 = Track1;
                        this.Track2 = Track2;
                        this.Track3 = Track3;
                        this.FrontTrack1 = FrontTrack1;
                        this.Track1JIS = Track1JIS;
                        this.Track3JIS = Track3JIS;
                    }
                }
                [DataMember(Name = "writeTracks")] 
                public WriteTracksClass WriteTracks { get; private set; }
                
                /// <summary>
                ///Specifies the chip card protocols that are supported by the card reader.
                /// </summary>
                public class ChipProtocolsClass 
                {
                    [DataMember(Name = "chipT0")] 
                    public bool? ChipT0 { get; private set; }
                    [DataMember(Name = "chipT1")] 
                    public bool? ChipT1 { get; private set; }
                    [DataMember(Name = "chipProtocolNotRequired")] 
                    public bool? ChipProtocolNotRequired { get; private set; }
                    [DataMember(Name = "chipTypeAPart3")] 
                    public bool? ChipTypeAPart3 { get; private set; }
                    [DataMember(Name = "chipTypeAPart4")] 
                    public bool? ChipTypeAPart4 { get; private set; }
                    [DataMember(Name = "chipTypeB")] 
                    public bool? ChipTypeB { get; private set; }
                    [DataMember(Name = "chipTypeNFC")] 
                    public bool? ChipTypeNFC { get; private set; }

                    public ChipProtocolsClass (bool? ChipT0, bool? ChipT1, bool? ChipProtocolNotRequired, bool? ChipTypeAPart3, bool? ChipTypeAPart4, bool? ChipTypeB, bool? ChipTypeNFC)
                    {
                        this.ChipT0 = ChipT0;
                        this.ChipT1 = ChipT1;
                        this.ChipProtocolNotRequired = ChipProtocolNotRequired;
                        this.ChipTypeAPart3 = ChipTypeAPart3;
                        this.ChipTypeAPart4 = ChipTypeAPart4;
                        this.ChipTypeB = ChipTypeB;
                        this.ChipTypeNFC = ChipTypeNFC;
                    }
                }
                [DataMember(Name = "chipProtocols")] 
                public ChipProtocolsClass ChipProtocols { get; private set; }
                [DataMember(Name = "maxCardCount")] 
                public double? MaxCardCount { get; private set; }
                public enum SecurityTypeEnum
                {
                    NotSupported,
                    Mm,
                    Cim86,
                }
                [DataMember(Name = "securityType")] 
                public SecurityTypeEnum? SecurityType { get; private set; }
                public enum PowerOnOptionEnum
                {
                    NoAction,
                    Eject,
                    Retain,
                    EjectThenRetain,
                    ReadPosition,
                }
                [DataMember(Name = "powerOnOption")] 
                public PowerOnOptionEnum? PowerOnOption { get; private set; }
                public enum PowerOffOptionEnum
                {
                    NoAction,
                    Eject,
                    Retain,
                    EjectThenRetain,
                    ReadPosition,
                }
                [DataMember(Name = "powerOffOption")] 
                public PowerOffOptionEnum? PowerOffOption { get; private set; }

                public CardReaderClass (TypeEnum? Type, ReadTracksClass ReadTracks, WriteTracksClass WriteTracks, ChipProtocolsClass ChipProtocols, double? MaxCardCount, SecurityTypeEnum? SecurityType, PowerOnOptionEnum? PowerOnOption, PowerOffOptionEnum? PowerOffOption)
                {
                    this.Type = Type;
                    this.ReadTracks = ReadTracks;
                    this.WriteTracks = WriteTracks;
                    this.ChipProtocols = ChipProtocols;
                    this.MaxCardCount = MaxCardCount;
                    this.SecurityType = SecurityType;
                    this.PowerOnOption = PowerOnOption;
                    this.PowerOffOption = PowerOffOption;
                }


            }

            /// <summary>
            ///Capability information for XFS4IoT services implementing the CashAcceptor interface. This will be omitted if the CashAcceptor interface is not supported.
            /// </summary>
            public class CashAcceptorClass
            {
                public enum TypeEnum
                {
                    TellerBill,
                    SelfServiceBill,
                    TellerCoin,
                    SelfServiceCoin,
                }
                [DataMember(Name = "type")] 
                public TypeEnum? Type { get; private set; }
                [DataMember(Name = "maxCashInItems")] 
                public int? MaxCashInItems { get; private set; }
                [DataMember(Name = "shutter")] 
                public bool? Shutter { get; private set; }
                [DataMember(Name = "shutterControl")] 
                public bool? ShutterControl { get; private set; }
                [DataMember(Name = "intermediateStacker")] 
                public int? IntermediateStacker { get; private set; }
                [DataMember(Name = "itemsTakenSensor")] 
                public bool? ItemsTakenSensor { get; private set; }
                [DataMember(Name = "itemsInsertedSensor")] 
                public bool? ItemsInsertedSensor { get; private set; }
                
                /// <summary>
                ///Specifies the CashAcceptor input and output positions which are available.
                /// </summary>
                public class PositionsClass 
                {
                    [DataMember(Name = "inLeft")] 
                    public bool? InLeft { get; private set; }
                    [DataMember(Name = "inRight")] 
                    public bool? InRight { get; private set; }
                    [DataMember(Name = "inCenter")] 
                    public bool? InCenter { get; private set; }
                    [DataMember(Name = "inTop")] 
                    public bool? InTop { get; private set; }
                    [DataMember(Name = "inBottom")] 
                    public bool? InBottom { get; private set; }
                    [DataMember(Name = "inFront")] 
                    public bool? InFront { get; private set; }
                    [DataMember(Name = "inRear")] 
                    public bool? InRear { get; private set; }
                    [DataMember(Name = "outLeft")] 
                    public bool? OutLeft { get; private set; }
                    [DataMember(Name = "outRight")] 
                    public bool? OutRight { get; private set; }
                    [DataMember(Name = "outCenter")] 
                    public bool? OutCenter { get; private set; }
                    [DataMember(Name = "outTop")] 
                    public bool? OutTop { get; private set; }
                    [DataMember(Name = "outBottom")] 
                    public bool? OutBottom { get; private set; }
                    [DataMember(Name = "outFront")] 
                    public bool? OutFront { get; private set; }
                    [DataMember(Name = "outRear")] 
                    public bool? OutRear { get; private set; }

                    public PositionsClass (bool? InLeft, bool? InRight, bool? InCenter, bool? InTop, bool? InBottom, bool? InFront, bool? InRear, bool? OutLeft, bool? OutRight, bool? OutCenter, bool? OutTop, bool? OutBottom, bool? OutFront, bool? OutRear)
                    {
                        this.InLeft = InLeft;
                        this.InRight = InRight;
                        this.InCenter = InCenter;
                        this.InTop = InTop;
                        this.InBottom = InBottom;
                        this.InFront = InFront;
                        this.InRear = InRear;
                        this.OutLeft = OutLeft;
                        this.OutRight = OutRight;
                        this.OutCenter = OutCenter;
                        this.OutTop = OutTop;
                        this.OutBottom = OutBottom;
                        this.OutFront = OutFront;
                        this.OutRear = OutRear;
                    }
                }
                [DataMember(Name = "positions")] 
                public PositionsClass Positions { get; private set; }
                
                /// <summary>
                ///Specifies the area to which items may be retracted. If the device does not have a retract capability all flags will be set to false.
                /// </summary>
                public class RetractAreasClass 
                {
                    [DataMember(Name = "retract")] 
                    public bool? Retract { get; private set; }
                    [DataMember(Name = "transport")] 
                    public bool? Transport { get; private set; }
                    [DataMember(Name = "stacker")] 
                    public bool? Stacker { get; private set; }
                    [DataMember(Name = "reject")] 
                    public bool? Reject { get; private set; }
                    [DataMember(Name = "billCassette")] 
                    public bool? BillCassette { get; private set; }
                    [DataMember(Name = "cashIn")] 
                    public bool? CashIn { get; private set; }

                    public RetractAreasClass (bool? Retract, bool? Transport, bool? Stacker, bool? Reject, bool? BillCassette, bool? CashIn)
                    {
                        this.Retract = Retract;
                        this.Transport = Transport;
                        this.Stacker = Stacker;
                        this.Reject = Reject;
                        this.BillCassette = BillCassette;
                        this.CashIn = CashIn;
                    }
                }
                [DataMember(Name = "retractAreas")] 
                public RetractAreasClass RetractAreas { get; private set; }
                
                /// <summary>
                ///Specifies the actions which may be performed on items which have been retracted to the transport. If the device does not have the capability to retract items to the transport or move items from the transport all flags will be set to false.
                /// </summary>
                public class RetractTransportActionsClass 
                {
                    [DataMember(Name = "present")] 
                    public bool? Present { get; private set; }
                    [DataMember(Name = "retract")] 
                    public bool? Retract { get; private set; }
                    [DataMember(Name = "reject")] 
                    public bool? Reject { get; private set; }
                    [DataMember(Name = "billCassette")] 
                    public bool? BillCassette { get; private set; }
                    [DataMember(Name = "cashIn")] 
                    public bool? CashIn { get; private set; }

                    public RetractTransportActionsClass (bool? Present, bool? Retract, bool? Reject, bool? BillCassette, bool? CashIn)
                    {
                        this.Present = Present;
                        this.Retract = Retract;
                        this.Reject = Reject;
                        this.BillCassette = BillCassette;
                        this.CashIn = CashIn;
                    }
                }
                [DataMember(Name = "retractTransportActions")] 
                public RetractTransportActionsClass RetractTransportActions { get; private set; }
                
                /// <summary>
                ///Specifies the actions which may be performed on items which have been retracted to the stacker. If the device does not have the capability to retract items to the stacker or move items from the stacker all flags will be set to false.
                /// </summary>
                public class RetractStackerActionsClass 
                {
                    [DataMember(Name = "present")] 
                    public bool? Present { get; private set; }
                    [DataMember(Name = "retract")] 
                    public bool? Retract { get; private set; }
                    [DataMember(Name = "reject")] 
                    public bool? Reject { get; private set; }
                    [DataMember(Name = "billCassette")] 
                    public bool? BillCassette { get; private set; }
                    [DataMember(Name = "cashIn")] 
                    public bool? CashIn { get; private set; }

                    public RetractStackerActionsClass (bool? Present, bool? Retract, bool? Reject, bool? BillCassette, bool? CashIn)
                    {
                        this.Present = Present;
                        this.Retract = Retract;
                        this.Reject = Reject;
                        this.BillCassette = BillCassette;
                        this.CashIn = CashIn;
                    }
                }
                [DataMember(Name = "retractStackerActions")] 
                public RetractStackerActionsClass RetractStackerActions { get; private set; }
                [DataMember(Name = "compareSignatures")] 
                public bool? CompareSignatures { get; private set; }
                [DataMember(Name = "replenish")] 
                public bool? Replenish { get; private set; }
                
                /// <summary>
                ///Specifies whether the cash-in limitation is supported or not for the CashAcceptor.SetCashInLimit command. If the device does not have the capability to limit the amount or the number of items during cash-in operationsall flags will be set to false.
                /// </summary>
                public class CashInLimitClass 
                {
                    [DataMember(Name = "byTotalItems")] 
                    public bool? ByTotalItems { get; private set; }
                    [DataMember(Name = "byAmount")] 
                    public bool? ByAmount { get; private set; }
                    [DataMember(Name = "multiple")] 
                    public bool? Multiple { get; private set; }
                    [DataMember(Name = "refuseOther")] 
                    public bool? RefuseOther { get; private set; }

                    public CashInLimitClass (bool? ByTotalItems, bool? ByAmount, bool? Multiple, bool? RefuseOther)
                    {
                        this.ByTotalItems = ByTotalItems;
                        this.ByAmount = ByAmount;
                        this.Multiple = Multiple;
                        this.RefuseOther = RefuseOther;
                    }
                }
                [DataMember(Name = "cashInLimit")] 
                public CashInLimitClass CashInLimit { get; private set; }
                
                /// <summary>
                ///Specifies the count action supported by the CashAcceptor.CashUnitCount command. If the device does not support counting then all flags will be set to false.
                /// </summary>
                public class CountActionsClass 
                {
                    [DataMember(Name = "individual")] 
                    public bool? Individual { get; private set; }
                    [DataMember(Name = "all")] 
                    public bool? All { get; private set; }

                    public CountActionsClass (bool? Individual, bool? All)
                    {
                        this.Individual = Individual;
                        this.All = All;
                    }
                }
                [DataMember(Name = "countActions")] 
                public CountActionsClass CountActions { get; private set; }
                [DataMember(Name = "deviceLockControl")] 
                public bool? DeviceLockControl { get; private set; }
                [DataMember(Name = "mixedMode")] 
                public bool? MixedMode { get; private set; }
                [DataMember(Name = "mixedDepositAndRollback")] 
                public bool? MixedDepositAndRollback { get; private set; }
                [DataMember(Name = "deplete")] 
                public bool? Deplete { get; private set; }

                public CashAcceptorClass (TypeEnum? Type, int? MaxCashInItems, bool? Shutter, bool? ShutterControl, int? IntermediateStacker, bool? ItemsTakenSensor, bool? ItemsInsertedSensor, PositionsClass Positions, RetractAreasClass RetractAreas, RetractTransportActionsClass RetractTransportActions, RetractStackerActionsClass RetractStackerActions, bool? CompareSignatures, bool? Replenish, CashInLimitClass CashInLimit, CountActionsClass CountActions, bool? DeviceLockControl, bool? MixedMode, bool? MixedDepositAndRollback, bool? Deplete)
                {
                    this.Type = Type;
                    this.MaxCashInItems = MaxCashInItems;
                    this.Shutter = Shutter;
                    this.ShutterControl = ShutterControl;
                    this.IntermediateStacker = IntermediateStacker;
                    this.ItemsTakenSensor = ItemsTakenSensor;
                    this.ItemsInsertedSensor = ItemsInsertedSensor;
                    this.Positions = Positions;
                    this.RetractAreas = RetractAreas;
                    this.RetractTransportActions = RetractTransportActions;
                    this.RetractStackerActions = RetractStackerActions;
                    this.CompareSignatures = CompareSignatures;
                    this.Replenish = Replenish;
                    this.CashInLimit = CashInLimit;
                    this.CountActions = CountActions;
                    this.DeviceLockControl = DeviceLockControl;
                    this.MixedMode = MixedMode;
                    this.MixedDepositAndRollback = MixedDepositAndRollback;
                    this.Deplete = Deplete;
                }


            }

            /// <summary>
            ///Capability information for XFS4IoT services implementing the CashDispenser interface. This will be omitted if the CashDispenser interface is not supported.
            /// </summary>
            public class CashDispenserClass
            {
                public enum TypeEnum
                {
                    TellerBill,
                    SelfServiceBill,
                    TellerCoin,
                    SelfServiceCoin,
                }
                [DataMember(Name = "type")] 
                public TypeEnum? Type { get; private set; }
                [DataMember(Name = "maxDispenseItems")] 
                public int? MaxDispenseItems { get; private set; }
                [DataMember(Name = "shutter")] 
                public bool? Shutter { get; private set; }
                [DataMember(Name = "shutterControl")] 
                public bool? ShutterControl { get; private set; }
                
                /// <summary>
                ///Specifies the area to which items may be retracted. If the device does not have a retract capability all flags will be set to false.
                /// </summary>
                public class RetractAreasClass 
                {
                    [DataMember(Name = "retract")] 
                    public bool? Retract { get; private set; }
                    [DataMember(Name = "transport")] 
                    public bool? Transport { get; private set; }
                    [DataMember(Name = "stacker")] 
                    public bool? Stacker { get; private set; }
                    [DataMember(Name = "reject")] 
                    public bool? Reject { get; private set; }
                    [DataMember(Name = "itemCassette")] 
                    public bool? ItemCassette { get; private set; }

                    public RetractAreasClass (bool? Retract, bool? Transport, bool? Stacker, bool? Reject, bool? ItemCassette)
                    {
                        this.Retract = Retract;
                        this.Transport = Transport;
                        this.Stacker = Stacker;
                        this.Reject = Reject;
                        this.ItemCassette = ItemCassette;
                    }
                }
                [DataMember(Name = "retractAreas")] 
                public RetractAreasClass RetractAreas { get; private set; }
                
                /// <summary>
                ///Specifies the actions which may be performed on items which have been retracted to the transport. If the device does not have the capability to retract items to the transport or move items from the transport all flags will be set to false.
                /// </summary>
                public class RetractTransportActionsClass 
                {
                    [DataMember(Name = "present")] 
                    public bool? Present { get; private set; }
                    [DataMember(Name = "retract")] 
                    public bool? Retract { get; private set; }
                    [DataMember(Name = "reject")] 
                    public bool? Reject { get; private set; }
                    [DataMember(Name = "itemCassette")] 
                    public bool? ItemCassette { get; private set; }

                    public RetractTransportActionsClass (bool? Present, bool? Retract, bool? Reject, bool? ItemCassette)
                    {
                        this.Present = Present;
                        this.Retract = Retract;
                        this.Reject = Reject;
                        this.ItemCassette = ItemCassette;
                    }
                }
                [DataMember(Name = "retractTransportActions")] 
                public RetractTransportActionsClass RetractTransportActions { get; private set; }
                
                /// <summary>
                ///Specifies the actions which may be performed on items which have been retracted to the stacker. If the device does not have the capability to retract items to the stacker or move items from the stacker all flags will be set to false.
                /// </summary>
                public class RetractStackerActionsClass 
                {
                    [DataMember(Name = "present")] 
                    public bool? Present { get; private set; }
                    [DataMember(Name = "retract")] 
                    public bool? Retract { get; private set; }
                    [DataMember(Name = "reject")] 
                    public bool? Reject { get; private set; }
                    [DataMember(Name = "itemCassette")] 
                    public bool? ItemCassette { get; private set; }

                    public RetractStackerActionsClass (bool? Present, bool? Retract, bool? Reject, bool? ItemCassette)
                    {
                        this.Present = Present;
                        this.Retract = Retract;
                        this.Reject = Reject;
                        this.ItemCassette = ItemCassette;
                    }
                }
                [DataMember(Name = "retractStackerActions")] 
                public RetractStackerActionsClass RetractStackerActions { get; private set; }
                [DataMember(Name = "intermediateStacker")] 
                public bool? IntermediateStacker { get; private set; }
                [DataMember(Name = "itemsTakenSensor")] 
                public bool? ItemsTakenSensor { get; private set; }
                
                /// <summary>
                ///Specifies the Dispenser output positions which are available.
                /// </summary>
                public class PositionsClass 
                {
                    [DataMember(Name = "left")] 
                    public bool? Left { get; private set; }
                    [DataMember(Name = "right")] 
                    public bool? Right { get; private set; }
                    [DataMember(Name = "center")] 
                    public bool? Center { get; private set; }
                    [DataMember(Name = "top")] 
                    public bool? Top { get; private set; }
                    [DataMember(Name = "bottom")] 
                    public bool? Bottom { get; private set; }
                    [DataMember(Name = "front")] 
                    public bool? Front { get; private set; }
                    [DataMember(Name = "rear")] 
                    public bool? Rear { get; private set; }

                    public PositionsClass (bool? Left, bool? Right, bool? Center, bool? Top, bool? Bottom, bool? Front, bool? Rear)
                    {
                        this.Left = Left;
                        this.Right = Right;
                        this.Center = Center;
                        this.Top = Top;
                        this.Bottom = Bottom;
                        this.Front = Front;
                        this.Rear = Rear;
                    }
                }
                [DataMember(Name = "positions")] 
                public PositionsClass Positions { get; private set; }
                
                /// <summary>
                ///Specifies the Dispenser move item options which are available.
                /// </summary>
                public class MoveItemsClass 
                {
                    [DataMember(Name = "fromCashUnit")] 
                    public bool? FromCashUnit { get; private set; }
                    [DataMember(Name = "toCashUnit")] 
                    public bool? ToCashUnit { get; private set; }
                    [DataMember(Name = "toTransport")] 
                    public bool? ToTransport { get; private set; }
                    [DataMember(Name = "toStacker")] 
                    public bool? ToStacker { get; private set; }

                    public MoveItemsClass (bool? FromCashUnit, bool? ToCashUnit, bool? ToTransport, bool? ToStacker)
                    {
                        this.FromCashUnit = FromCashUnit;
                        this.ToCashUnit = ToCashUnit;
                        this.ToTransport = ToTransport;
                        this.ToStacker = ToStacker;
                    }
                }
                [DataMember(Name = "moveItems")] 
                public MoveItemsClass MoveItems { get; private set; }
                [DataMember(Name = "prepareDispense")] 
                public bool? PrepareDispense { get; private set; }

                public CashDispenserClass (TypeEnum? Type, int? MaxDispenseItems, bool? Shutter, bool? ShutterControl, RetractAreasClass RetractAreas, RetractTransportActionsClass RetractTransportActions, RetractStackerActionsClass RetractStackerActions, bool? IntermediateStacker, bool? ItemsTakenSensor, PositionsClass Positions, MoveItemsClass MoveItems, bool? PrepareDispense)
                {
                    this.Type = Type;
                    this.MaxDispenseItems = MaxDispenseItems;
                    this.Shutter = Shutter;
                    this.ShutterControl = ShutterControl;
                    this.RetractAreas = RetractAreas;
                    this.RetractTransportActions = RetractTransportActions;
                    this.RetractStackerActions = RetractStackerActions;
                    this.IntermediateStacker = IntermediateStacker;
                    this.ItemsTakenSensor = ItemsTakenSensor;
                    this.Positions = Positions;
                    this.MoveItems = MoveItems;
                    this.PrepareDispense = PrepareDispense;
                }


            }

            /// <summary>
            ///Capability information for XFS4IoT services implementing the CashManagement interface. This will be omitted if the CashManagement interface is not supported.
            /// </summary>
            public class CashManagementClass
            {
                [DataMember(Name = "safeDoor")] 
                public bool? SafeDoor { get; private set; }
                [DataMember(Name = "cashBox")] 
                public bool? CashBox { get; private set; }
                
                /// <summary>
                ///Specifies the type of cash unit exchange operations supported by the device.
                /// </summary>
                public class ExchangeTypeClass 
                {
                    [DataMember(Name = "byHand")] 
                    public bool? ByHand { get; private set; }
                    [DataMember(Name = "toCassettes")] 
                    public bool? ToCassettes { get; private set; }
                    [DataMember(Name = "clearRecycler")] 
                    public bool? ClearRecycler { get; private set; }
                    [DataMember(Name = "depositInto")] 
                    public bool? DepositInto { get; private set; }

                    public ExchangeTypeClass (bool? ByHand, bool? ToCassettes, bool? ClearRecycler, bool? DepositInto)
                    {
                        this.ByHand = ByHand;
                        this.ToCassettes = ToCassettes;
                        this.ClearRecycler = ClearRecycler;
                        this.DepositInto = DepositInto;
                    }
                }
                [DataMember(Name = "exchangeType")] 
                public ExchangeTypeClass ExchangeType { get; private set; }
                
                /// <summary>
                ///Specifies the types of information that can be retrieved through the CashManagement.GetItemInfo command.
                /// </summary>
                public class ItemInfoTypesClass 
                {
                    [DataMember(Name = "serialNumber")] 
                    public bool? SerialNumber { get; private set; }
                    [DataMember(Name = "signature")] 
                    public bool? Signature { get; private set; }
                    [DataMember(Name = "imageFile")] 
                    public bool? ImageFile { get; private set; }

                    public ItemInfoTypesClass (bool? SerialNumber, bool? Signature, bool? ImageFile)
                    {
                        this.SerialNumber = SerialNumber;
                        this.Signature = Signature;
                        this.ImageFile = ImageFile;
                    }
                }
                [DataMember(Name = "itemInfoTypes")] 
                public ItemInfoTypesClass ItemInfoTypes { get; private set; }
                [DataMember(Name = "classificationList")] 
                public bool? ClassificationList { get; private set; }
                [DataMember(Name = "physicalNoteList")] 
                public bool? PhysicalNoteList { get; private set; }

                public CashManagementClass (bool? SafeDoor, bool? CashBox, ExchangeTypeClass ExchangeType, ItemInfoTypesClass ItemInfoTypes, bool? ClassificationList, bool? PhysicalNoteList)
                {
                    this.SafeDoor = SafeDoor;
                    this.CashBox = CashBox;
                    this.ExchangeType = ExchangeType;
                    this.ItemInfoTypes = ItemInfoTypes;
                    this.ClassificationList = ClassificationList;
                    this.PhysicalNoteList = PhysicalNoteList;
                }


            }

            /// <summary>
            ///Capability information for XFS4IoT services implementing the PinPad interface. This will be omitted if the PinPad interface is not supported.
            /// </summary>
            public class PinPadClass
            {
                
                /// <summary>
                ///Supported PIN format
                /// </summary>
                public class PinFormatsClass 
                {
                    [DataMember(Name = "3624")] 
                    public bool? Number3624 { get; private set; }
                    [DataMember(Name = "ansi")] 
                    public bool? Ansi { get; private set; }
                    [DataMember(Name = "iso0")] 
                    public bool? Iso0 { get; private set; }
                    [DataMember(Name = "iso1")] 
                    public bool? Iso1 { get; private set; }
                    [DataMember(Name = "eci2")] 
                    public bool? Eci2 { get; private set; }
                    [DataMember(Name = "eci3")] 
                    public bool? Eci3 { get; private set; }
                    [DataMember(Name = "visa")] 
                    public bool? Visa { get; private set; }
                    [DataMember(Name = "diebold")] 
                    public bool? Diebold { get; private set; }
                    [DataMember(Name = "dieboldCo")] 
                    public bool? DieboldCo { get; private set; }
                    [DataMember(Name = "visa3")] 
                    public bool? Visa3 { get; private set; }
                    [DataMember(Name = "emv")] 
                    public bool? Emv { get; private set; }
                    [DataMember(Name = "iso3")] 
                    public bool? Iso3 { get; private set; }
                    [DataMember(Name = "ap")] 
                    public bool? Ap { get; private set; }

                    public PinFormatsClass (bool? Number3624, bool? Ansi, bool? Iso0, bool? Iso1, bool? Eci2, bool? Eci3, bool? Visa, bool? Diebold, bool? DieboldCo, bool? Visa3, bool? Emv, bool? Iso3, bool? Ap)
                    {
                        this.Number3624 = Number3624;
                        this.Ansi = Ansi;
                        this.Iso0 = Iso0;
                        this.Iso1 = Iso1;
                        this.Eci2 = Eci2;
                        this.Eci3 = Eci3;
                        this.Visa = Visa;
                        this.Diebold = Diebold;
                        this.DieboldCo = DieboldCo;
                        this.Visa3 = Visa3;
                        this.Emv = Emv;
                        this.Iso3 = Iso3;
                        this.Ap = Ap;
                    }
                }
                [DataMember(Name = "pinFormats")] 
                public PinFormatsClass PinFormats { get; private set; }
                
                /// <summary>
                ///Supported presentation algorithms
                /// </summary>
                public class PresentationAlgorithmsClass 
                {
                    [DataMember(Name = "presentClear")] 
                    public bool? PresentClear { get; private set; }

                    public PresentationAlgorithmsClass (bool? PresentClear)
                    {
                        this.PresentClear = PresentClear;
                    }
                }
                [DataMember(Name = "presentationAlgorithms")] 
                public PresentationAlgorithmsClass PresentationAlgorithms { get; private set; }
                
                /// <summary>
                ///Specifies the type of the display used in the PIN pad module
                /// </summary>
                public class DisplayClass 
                {
                    [DataMember(Name = "none")] 
                    public bool? None { get; private set; }
                    [DataMember(Name = "ledThrough")] 
                    public bool? LedThrough { get; private set; }
                    [DataMember(Name = "display")] 
                    public bool? Display { get; private set; }

                    public DisplayClass (bool? None, bool? LedThrough, bool? Display)
                    {
                        this.None = None;
                        this.LedThrough = LedThrough;
                        this.Display = Display;
                    }
                }
                [DataMember(Name = "display")] 
                public DisplayClass Display { get; private set; }
                [DataMember(Name = "idConnect")] 
                public bool? IdConnect { get; private set; }
                
                /// <summary>
                ///Specifies the algorithms for PIN validation supported by the service
                /// </summary>
                public class ValidationAlgorithmsClass 
                {
                    [DataMember(Name = "des")] 
                    public bool? Des { get; private set; }
                    [DataMember(Name = "visa")] 
                    public bool? Visa { get; private set; }

                    public ValidationAlgorithmsClass (bool? Des, bool? Visa)
                    {
                        this.Des = Des;
                        this.Visa = Visa;
                    }
                }
                [DataMember(Name = "validationAlgorithms")] 
                public ValidationAlgorithmsClass ValidationAlgorithms { get; private set; }
                [DataMember(Name = "pinCanPersistAfterUse")] 
                public bool? PinCanPersistAfterUse { get; private set; }
                [DataMember(Name = "typeCombined")] 
                public bool? TypeCombined { get; private set; }
                [DataMember(Name = "setPinblockDataRequired")] 
                public bool? SetPinblockDataRequired { get; private set; }
                
                /// <summary>
                ///Array of attributes supported by the PinBlock command.
                /// </summary>
                public class PinBlockAttributesClass 
                {
                    public enum KeyUsageEnum
                    {
                        P0,
                    }
                    [DataMember(Name = "keyUsage")] 
                    public KeyUsageEnum? KeyUsage { get; private set; }
                    public enum AlgorithmEnum
                    {
                        A,
                        D,
                        R,
                        T,
                    }
                    [DataMember(Name = "algorithm")] 
                    public AlgorithmEnum? Algorithm { get; private set; }
                    public enum ModeOfUseEnum
                    {
                        E,
                    }
                    [DataMember(Name = "modeOfUse")] 
                    public ModeOfUseEnum? ModeOfUse { get; private set; }
                    public enum CryptoMethodEnum
                    {
                        Ecb,
                        Cbc,
                        Cfb,
                        Ofb,
                        Ctr,
                        Xts,
                        RsaesPkcs1V15,
                        RsaesOaep,
                    }
                    [DataMember(Name = "cryptoMethod")] 
                    public CryptoMethodEnum? CryptoMethod { get; private set; }

                    public PinBlockAttributesClass (KeyUsageEnum? KeyUsage, AlgorithmEnum? Algorithm, ModeOfUseEnum? ModeOfUse, CryptoMethodEnum? CryptoMethod)
                    {
                        this.KeyUsage = KeyUsage;
                        this.Algorithm = Algorithm;
                        this.ModeOfUse = ModeOfUse;
                        this.CryptoMethod = CryptoMethod;
                    }
                }
                [DataMember(Name = "pinBlockAttributes")] 
                public PinBlockAttributesClass PinBlockAttributes { get; private set; }
                
                /// <summary>
                ///Specified capabilites of German specific protocol supports
                /// </summary>
                public class countrySpecificDKClass 
                {
                    [DataMember(Name = "protocolSupported")] 
                    public bool? ProtocolSupported { get; private set; }
                    [DataMember(Name = "hsmVendor")] 
                    public string HsmVendor { get; private set; }
                    [DataMember(Name = "hsmJournaling")] 
                    public bool? HsmJournaling { get; private set; }
                    
                    /// <summary>
                    ///Supported derivation algorithms
                    /// </summary>
                    public class DerivationAlgorithmsClass 
                    {
                        [DataMember(Name = "chipZka")] 
                        public bool? ChipZka { get; private set; }

                        public DerivationAlgorithmsClass (bool? ChipZka)
                        {
                            this.ChipZka = ChipZka;
                        }
                    }
                    [DataMember(Name = "derivationAlgorithms")] 
                    public DerivationAlgorithmsClass DerivationAlgorithms { get; private set; }

                    public countrySpecificDKClass (bool? ProtocolSupported, string HsmVendor, bool? HsmJournaling, DerivationAlgorithmsClass DerivationAlgorithms)
                    {
                        this.ProtocolSupported = ProtocolSupported;
                        this.HsmVendor = HsmVendor;
                        this.HsmJournaling = HsmJournaling;
                        this.DerivationAlgorithms = DerivationAlgorithms;
                    }
                }
                [DataMember(Name = "countrySpecific.DK")] 
                public countrySpecificDKClass countrySpecificDK { get; private set; }
                
                /// <summary>
                ///Specified capabilites of Chinese specific PBOC3.0 protocol supports
                /// </summary>
                public class countrySpecificCHNClass 
                {
                    [DataMember(Name = "protocolSupported")] 
                    public bool? ProtocolSupported { get; private set; }

                    public countrySpecificCHNClass (bool? ProtocolSupported)
                    {
                        this.ProtocolSupported = ProtocolSupported;
                    }
                }
                [DataMember(Name = "countrySpecific.CHN")] 
                public countrySpecificCHNClass countrySpecificCHN { get; private set; }
                
                /// <summary>
                ///Specified capabilites of Luxemburg specific protocol supports
                /// </summary>
                public class countrySpecificLUXClass 
                {
                    [DataMember(Name = "protocolSupported")] 
                    public bool? ProtocolSupported { get; private set; }

                    public countrySpecificLUXClass (bool? ProtocolSupported)
                    {
                        this.ProtocolSupported = ProtocolSupported;
                    }
                }
                [DataMember(Name = "countrySpecific.LUX")] 
                public countrySpecificLUXClass countrySpecificLUX { get; private set; }

                public PinPadClass (PinFormatsClass PinFormats, PresentationAlgorithmsClass PresentationAlgorithms, DisplayClass Display, bool? IdConnect, ValidationAlgorithmsClass ValidationAlgorithms, bool? PinCanPersistAfterUse, bool? TypeCombined, bool? SetPinblockDataRequired, PinBlockAttributesClass PinBlockAttributes, countrySpecificDKClass countrySpecificDK, countrySpecificCHNClass countrySpecificCHN, countrySpecificLUXClass countrySpecificLUX)
                {
                    this.PinFormats = PinFormats;
                    this.PresentationAlgorithms = PresentationAlgorithms;
                    this.Display = Display;
                    this.IdConnect = IdConnect;
                    this.ValidationAlgorithms = ValidationAlgorithms;
                    this.PinCanPersistAfterUse = PinCanPersistAfterUse;
                    this.TypeCombined = TypeCombined;
                    this.SetPinblockDataRequired = SetPinblockDataRequired;
                    this.PinBlockAttributes = PinBlockAttributes;
                    this.countrySpecificDK = countrySpecificDK;
                    this.countrySpecificCHN = countrySpecificCHN;
                    this.countrySpecificLUX = countrySpecificLUX;
                }


            }

            /// <summary>
            ///Capability information for XFS4IoT services implementing the Crypto interface. This will be omitted if the Crypto interface is not supported.
            /// </summary>
            public class CryptoClass
            {
                
                /// <summary>
                ///Supported encryption modes.
                /// </summary>
                public class AlgorithmsClass 
                {
                    [DataMember(Name = "ecb")] 
                    public bool? Ecb { get; private set; }
                    [DataMember(Name = "cbc")] 
                    public bool? Cbc { get; private set; }
                    [DataMember(Name = "cfb")] 
                    public bool? Cfb { get; private set; }
                    [DataMember(Name = "rsa")] 
                    public bool? Rsa { get; private set; }
                    [DataMember(Name = "cma")] 
                    public bool? Cma { get; private set; }
                    [DataMember(Name = "desMac")] 
                    public bool? DesMac { get; private set; }
                    [DataMember(Name = "triDesEcb")] 
                    public bool? TriDesEcb { get; private set; }
                    [DataMember(Name = "triDesCbc")] 
                    public bool? TriDesCbc { get; private set; }
                    [DataMember(Name = "triDesCfb")] 
                    public bool? TriDesCfb { get; private set; }
                    [DataMember(Name = "triDesMac")] 
                    public bool? TriDesMac { get; private set; }
                    [DataMember(Name = "maaMac")] 
                    public bool? MaaMac { get; private set; }
                    [DataMember(Name = "triDesMac2805")] 
                    public bool? TriDesMac2805 { get; private set; }
                    [DataMember(Name = "sm4")] 
                    public bool? Sm4 { get; private set; }
                    [DataMember(Name = "sm4Mac")] 
                    public bool? Sm4Mac { get; private set; }

                    public AlgorithmsClass (bool? Ecb, bool? Cbc, bool? Cfb, bool? Rsa, bool? Cma, bool? DesMac, bool? TriDesEcb, bool? TriDesCbc, bool? TriDesCfb, bool? TriDesMac, bool? MaaMac, bool? TriDesMac2805, bool? Sm4, bool? Sm4Mac)
                    {
                        this.Ecb = Ecb;
                        this.Cbc = Cbc;
                        this.Cfb = Cfb;
                        this.Rsa = Rsa;
                        this.Cma = Cma;
                        this.DesMac = DesMac;
                        this.TriDesEcb = TriDesEcb;
                        this.TriDesCbc = TriDesCbc;
                        this.TriDesCfb = TriDesCfb;
                        this.TriDesMac = TriDesMac;
                        this.MaaMac = MaaMac;
                        this.TriDesMac2805 = TriDesMac2805;
                        this.Sm4 = Sm4;
                        this.Sm4Mac = Sm4Mac;
                    }
                }
                [DataMember(Name = "algorithms")] 
                public AlgorithmsClass Algorithms { get; private set; }
                
                /// <summary>
                ///Specifies which hash algorithm is supported for the calculation of the HASH.
                /// </summary>
                public class EmvHashAlgorithmClass 
                {
                    [DataMember(Name = "sha1Digest")] 
                    public bool? Sha1Digest { get; private set; }
                    [DataMember(Name = "sha256Digest")] 
                    public bool? Sha256Digest { get; private set; }

                    public EmvHashAlgorithmClass (bool? Sha1Digest, bool? Sha256Digest)
                    {
                        this.Sha1Digest = Sha1Digest;
                        this.Sha256Digest = Sha256Digest;
                    }
                }
                [DataMember(Name = "emvHashAlgorithm")] 
                public EmvHashAlgorithmClass EmvHashAlgorithm { get; private set; }
                
                /// <summary>
                ///Array of attributes supported by the CryptoData command.
                /// </summary>
                public class CryptoAttributesClass 
                {
                    public enum KeyUsageEnum
                    {
                        D0,
                        D1,
                    }
                    [DataMember(Name = "keyUsage")] 
                    public KeyUsageEnum? KeyUsage { get; private set; }
                    public enum AlgorithmEnum
                    {
                        A,
                        D,
                        R,
                        T,
                    }
                    [DataMember(Name = "algorithm")] 
                    public AlgorithmEnum? Algorithm { get; private set; }
                    public enum ModeOfUseEnum
                    {
                        D,
                        E,
                    }
                    [DataMember(Name = "modeOfUse")] 
                    public ModeOfUseEnum? ModeOfUse { get; private set; }
                    public enum CryptoMethodEnum
                    {
                        Ecb,
                        Cbc,
                        Cfb,
                        Ofb,
                        Otr,
                        Xts,
                        RsaesPkcs1V15,
                        RsaesOaep,
                    }
                    [DataMember(Name = "cryptoMethod")] 
                    public CryptoMethodEnum? CryptoMethod { get; private set; }

                    public CryptoAttributesClass (KeyUsageEnum? KeyUsage, AlgorithmEnum? Algorithm, ModeOfUseEnum? ModeOfUse, CryptoMethodEnum? CryptoMethod)
                    {
                        this.KeyUsage = KeyUsage;
                        this.Algorithm = Algorithm;
                        this.ModeOfUse = ModeOfUse;
                        this.CryptoMethod = CryptoMethod;
                    }
                }
                [DataMember(Name = "cryptoAttributes")] 
                public CryptoAttributesClass CryptoAttributes { get; private set; }
                
                /// <summary>
                ///Array of attributes supported by the GenerateAuthentication command.
                /// </summary>
                public class SignatureAttributesClass 
                {
                    public enum KeyUsageEnum
                    {
                        M0,
                        M1,
                        M2,
                        M3,
                        M4,
                        M5,
                        M6,
                        M7,
                        M8,
                        S0,
                        S1,
                        S2,
                    }
                    [DataMember(Name = "keyUsage")] 
                    public KeyUsageEnum? KeyUsage { get; private set; }
                    public enum AlgorithmEnum
                    {
                        A,
                        D,
                        R,
                        T,
                    }
                    [DataMember(Name = "algorithm")] 
                    public AlgorithmEnum? Algorithm { get; private set; }
                    public enum ModeOfUseEnum
                    {
                        G,
                        S,
                        V,
                    }
                    [DataMember(Name = "modeOfUse")] 
                    public ModeOfUseEnum? ModeOfUse { get; private set; }
                    public enum CryptoMethodEnum
                    {
                        RsassaPkcs1V15,
                        RsassaPss,
                    }
                    [DataMember(Name = "cryptoMethod")] 
                    public CryptoMethodEnum? CryptoMethod { get; private set; }
                    
                    /// <summary>
                    ///For asymmetric signature verification methods (keyUsage is ‘S0’, ‘S1’, or ‘S2’), this can be one of the following values to be used.If keyUsage is specified as any of the MAC usages (i.e. ‘m1’), then this proeprty should not be not set.
                    /// </summary>
                    public class HashAlgorithmClass 
                    {
                        [DataMember(Name = "sha1")] 
                        public bool? Sha1 { get; private set; }
                        [DataMember(Name = "sha256")] 
                        public bool? Sha256 { get; private set; }

                        public HashAlgorithmClass (bool? Sha1, bool? Sha256)
                        {
                            this.Sha1 = Sha1;
                            this.Sha256 = Sha256;
                        }
                    }
                    [DataMember(Name = "hashAlgorithm")] 
                    public HashAlgorithmClass HashAlgorithm { get; private set; }

                    public SignatureAttributesClass (KeyUsageEnum? KeyUsage, AlgorithmEnum? Algorithm, ModeOfUseEnum? ModeOfUse, CryptoMethodEnum? CryptoMethod, HashAlgorithmClass HashAlgorithm)
                    {
                        this.KeyUsage = KeyUsage;
                        this.Algorithm = Algorithm;
                        this.ModeOfUse = ModeOfUse;
                        this.CryptoMethod = CryptoMethod;
                        this.HashAlgorithm = HashAlgorithm;
                    }
                }
                [DataMember(Name = "signatureAttributes")] 
                public SignatureAttributesClass SignatureAttributes { get; private set; }

                public CryptoClass (AlgorithmsClass Algorithms, EmvHashAlgorithmClass EmvHashAlgorithm, CryptoAttributesClass CryptoAttributes, SignatureAttributesClass SignatureAttributes)
                {
                    this.Algorithms = Algorithms;
                    this.EmvHashAlgorithm = EmvHashAlgorithm;
                    this.CryptoAttributes = CryptoAttributes;
                    this.SignatureAttributes = SignatureAttributes;
                }


            }

            /// <summary>
            ///Capability information for XFS4IoT services implementing the KeyManagement interface. This will be omitted if the KeyManagement interface is not supported.
            /// </summary>
            public class KeyManagementClass
            {
                [DataMember(Name = "keyNum")] 
                public int? KeyNum { get; private set; }
                
                /// <summary>
                ///Specifies if key owner identification (in commands referenced as lpxIdent), which authorizes access to the encryption module, is required.A zero value is returned if the encryption module does not support this capability.
                /// </summary>
                public class IdKeyClass 
                {
                    [DataMember(Name = "initialization")] 
                    public bool? Initialization { get; private set; }
                    [DataMember(Name = "import")] 
                    public bool? Import { get; private set; }

                    public IdKeyClass (bool? Initialization, bool? Import)
                    {
                        this.Initialization = Initialization;
                        this.Import = Import;
                    }
                }
                [DataMember(Name = "idKey")] 
                public IdKeyClass IdKey { get; private set; }
                
                /// <summary>
                ///Specifies the key check modes that are supported to check the correctness of an imported key value
                /// </summary>
                public class KeyCheckModesClass 
                {
                    [DataMember(Name = "self")] 
                    public bool? Self { get; private set; }
                    [DataMember(Name = "zero")] 
                    public bool? Zero { get; private set; }

                    public KeyCheckModesClass (bool? Self, bool? Zero)
                    {
                        this.Self = Self;
                        this.Zero = Zero;
                    }
                }
                [DataMember(Name = "keyCheckModes")] 
                public KeyCheckModesClass KeyCheckModes { get; private set; }
                [DataMember(Name = "hsmVendor")] 
                public string HsmVendor { get; private set; }
                [DataMember(Name = "rsaAuthenticationScheme")] 
                public bool? RsaAuthenticationScheme { get; private set; }
                
                /// <summary>
                ///Specifies which type of rsa Signature Algorithm
                /// </summary>
                public class RsaSignatureAlgorithmClass 
                {
                    [DataMember(Name = "pkcs1V15")] 
                    public bool? Pkcs1V15 { get; private set; }
                    [DataMember(Name = "pss")] 
                    public bool? Pss { get; private set; }

                    public RsaSignatureAlgorithmClass (bool? Pkcs1V15, bool? Pss)
                    {
                        this.Pkcs1V15 = Pkcs1V15;
                        this.Pss = Pss;
                    }
                }
                [DataMember(Name = "rsaSignatureAlgorithm")] 
                public RsaSignatureAlgorithmClass RsaSignatureAlgorithm { get; private set; }
                
                /// <summary>
                ///Specifies which type of rsa Encipherment Algorith
                /// </summary>
                public class RsaCryptAlgorithmClass 
                {
                    [DataMember(Name = "pkcs1V15")] 
                    public bool? Pkcs1V15 { get; private set; }
                    [DataMember(Name = "oaep")] 
                    public bool? Oaep { get; private set; }

                    public RsaCryptAlgorithmClass (bool? Pkcs1V15, bool? Oaep)
                    {
                        this.Pkcs1V15 = Pkcs1V15;
                        this.Oaep = Oaep;
                    }
                }
                [DataMember(Name = "rsaCryptAlgorithm")] 
                public RsaCryptAlgorithmClass RsaCryptAlgorithm { get; private set; }
                
                /// <summary>
                ///Specifies which algorithm/method used to generate the public key check value/thumb print
                /// </summary>
                public class RsaKeyCheckModeClass 
                {
                    [DataMember(Name = "sha1")] 
                    public bool? Sha1 { get; private set; }
                    [DataMember(Name = "sha256")] 
                    public bool? Sha256 { get; private set; }

                    public RsaKeyCheckModeClass (bool? Sha1, bool? Sha256)
                    {
                        this.Sha1 = Sha1;
                        this.Sha256 = Sha256;
                    }
                }
                [DataMember(Name = "rsaKeyCheckMode")] 
                public RsaKeyCheckModeClass RsaKeyCheckMode { get; private set; }
                
                /// <summary>
                ///Specifies which capabilities are supported by the Signature scheme 
                /// </summary>
                public class SignatureSchemeClass 
                {
                    [DataMember(Name = "genRsaKeyPair")] 
                    public bool? GenRsaKeyPair { get; private set; }
                    [DataMember(Name = "randomNumber")] 
                    public bool? RandomNumber { get; private set; }
                    [DataMember(Name = "exportEppId")] 
                    public bool? ExportEppId { get; private set; }
                    [DataMember(Name = "enhancedRkl")] 
                    public bool? EnhancedRkl { get; private set; }

                    public SignatureSchemeClass (bool? GenRsaKeyPair, bool? RandomNumber, bool? ExportEppId, bool? EnhancedRkl)
                    {
                        this.GenRsaKeyPair = GenRsaKeyPair;
                        this.RandomNumber = RandomNumber;
                        this.ExportEppId = ExportEppId;
                        this.EnhancedRkl = EnhancedRkl;
                    }
                }
                [DataMember(Name = "signatureScheme")] 
                public SignatureSchemeClass SignatureScheme { get; private set; }
                
                /// <summary>
                ///Identifies the supported emv Import Scheme(s)
                /// </summary>
                public class EmvImportSchemesClass 
                {
                    [DataMember(Name = "plainCA")] 
                    public bool? PlainCA { get; private set; }
                    [DataMember(Name = "chksumCA")] 
                    public bool? ChksumCA { get; private set; }
                    [DataMember(Name = "epiCA")] 
                    public bool? EpiCA { get; private set; }
                    [DataMember(Name = "issuer")] 
                    public bool? Issuer { get; private set; }
                    [DataMember(Name = "icc")] 
                    public bool? Icc { get; private set; }
                    [DataMember(Name = "iccPin")] 
                    public bool? IccPin { get; private set; }
                    [DataMember(Name = "pkcsv15CA")] 
                    public bool? Pkcsv15CA { get; private set; }

                    public EmvImportSchemesClass (bool? PlainCA, bool? ChksumCA, bool? EpiCA, bool? Issuer, bool? Icc, bool? IccPin, bool? Pkcsv15CA)
                    {
                        this.PlainCA = PlainCA;
                        this.ChksumCA = ChksumCA;
                        this.EpiCA = EpiCA;
                        this.Issuer = Issuer;
                        this.Icc = Icc;
                        this.IccPin = IccPin;
                        this.Pkcsv15CA = Pkcsv15CA;
                    }
                }
                [DataMember(Name = "emvImportSchemes")] 
                public EmvImportSchemesClass EmvImportSchemes { get; private set; }
                
                /// <summary>
                ///Supported key block formats
                /// </summary>
                public class KeyBlockImportFormatsClass 
                {
                    [DataMember(Name = "ansTr31KeyBlock")] 
                    public bool? AnsTr31KeyBlock { get; private set; }
                    [DataMember(Name = "ansTr31KeyBlockB")] 
                    public bool? AnsTr31KeyBlockB { get; private set; }
                    [DataMember(Name = "ansTr31KeyBlockC")] 
                    public bool? AnsTr31KeyBlockC { get; private set; }

                    public KeyBlockImportFormatsClass (bool? AnsTr31KeyBlock, bool? AnsTr31KeyBlockB, bool? AnsTr31KeyBlockC)
                    {
                        this.AnsTr31KeyBlock = AnsTr31KeyBlock;
                        this.AnsTr31KeyBlockB = AnsTr31KeyBlockB;
                        this.AnsTr31KeyBlockC = AnsTr31KeyBlockC;
                    }
                }
                [DataMember(Name = "keyBlockImportFormats")] 
                public KeyBlockImportFormatsClass KeyBlockImportFormats { get; private set; }
                [DataMember(Name = "keyImportThroughParts")] 
                public bool? KeyImportThroughParts { get; private set; }
                
                /// <summary>
                ///Specifies which length of DES keys are supported.
                /// </summary>
                public class DesKeyLengthClass 
                {
                    [DataMember(Name = "single")] 
                    public bool? Single { get; private set; }
                    [DataMember(Name = "double")] 
                    public bool? Double { get; private set; }
                    [DataMember(Name = "triple")] 
                    public bool? Triple { get; private set; }

                    public DesKeyLengthClass (bool? Single, bool? Double, bool? Triple)
                    {
                        this.Single = Single;
                        this.Double = Double;
                        this.Triple = Triple;
                    }
                }
                [DataMember(Name = "desKeyLength")] 
                public DesKeyLengthClass DesKeyLength { get; private set; }
                
                /// <summary>
                ///Specifies supported certificate types 
                /// </summary>
                public class CertificateTypesClass 
                {
                    [DataMember(Name = "encKey")] 
                    public bool? EncKey { get; private set; }
                    [DataMember(Name = "verificationKey")] 
                    public bool? VerificationKey { get; private set; }
                    [DataMember(Name = "hostKey")] 
                    public bool? HostKey { get; private set; }

                    public CertificateTypesClass (bool? EncKey, bool? VerificationKey, bool? HostKey)
                    {
                        this.EncKey = EncKey;
                        this.VerificationKey = VerificationKey;
                        this.HostKey = HostKey;
                    }
                }
                [DataMember(Name = "certificateTypes")] 
                public CertificateTypesClass CertificateTypes { get; private set; }
                
                /// <summary>
                ///Specifying the options supported by the LoadCertificate command
                /// </summary>
                public class LoadCertOptionsClass 
                {
                    public enum SignerEnum
                    {
                        CertHost,
                        SigHost,
                        Ca,
                        Hl,
                        CertHostTr34,
                        CaTr34,
                        HlTr34,
                    }
                    [DataMember(Name = "signer")] 
                    public SignerEnum? Signer { get; private set; }
                    
                    /// <summary>
                    ///Specifies the load options supported by the LoadCertificate command 
                    /// </summary>
                    public class OptionClass 
                    {
                        [DataMember(Name = "newHost")] 
                        public bool? NewHost { get; private set; }
                        [DataMember(Name = "replaceHost")] 
                        public bool? ReplaceHost { get; private set; }

                        public OptionClass (bool? NewHost, bool? ReplaceHost)
                        {
                            this.NewHost = NewHost;
                            this.ReplaceHost = ReplaceHost;
                        }
                    }
                    [DataMember(Name = "option")] 
                    public OptionClass Option { get; private set; }

                    public LoadCertOptionsClass (SignerEnum? Signer, OptionClass Option)
                    {
                        this.Signer = Signer;
                        this.Option = Option;
                    }
                }
                [DataMember(Name = "loadCertOptions")] 
                public LoadCertOptionsClass LoadCertOptions { get; private set; }
                
                /// <summary>
                ///Supported options to load the Key Transport Key using the Certificate Remote Key Loading protocol.
                /// </summary>
                public class CrklLoadOptionsClass 
                {
                    [DataMember(Name = "noRandom")] 
                    public bool? NoRandom { get; private set; }
                    [DataMember(Name = "noRandomCrl")] 
                    public bool? NoRandomCrl { get; private set; }
                    [DataMember(Name = "random")] 
                    public bool? Random { get; private set; }
                    [DataMember(Name = "randomCrl")] 
                    public bool? RandomCrl { get; private set; }

                    public CrklLoadOptionsClass (bool? NoRandom, bool? NoRandomCrl, bool? Random, bool? RandomCrl)
                    {
                        this.NoRandom = NoRandom;
                        this.NoRandomCrl = NoRandomCrl;
                        this.Random = Random;
                        this.RandomCrl = RandomCrl;
                    }
                }
                [DataMember(Name = "crklLoadOptions")] 
                public CrklLoadOptionsClass CrklLoadOptions { get; private set; }
                
                /// <summary>
                ///A array of object specifying the loading methods that support the RestrictedKeyEncKey usage flag and the allowable usage flag combinations.
                /// </summary>
                public class RestrictedKeyEncKeySupportClass 
                {
                    public enum LoadingMethodEnum
                    {
                        RsaAuth2partySig,
                        RsaAuth3partyCert,
                        RsaAuth3partyCertTr34,
                        RestrictedSecurekeyentry,
                    }
                    [DataMember(Name = "loadingMethod")] 
                    public LoadingMethodEnum? LoadingMethod { get; private set; }
                    
                    /// <summary>
                    ///Specifies one or more usage flags that can be used in combination with the RestrictedKeyEncKey
                    /// </summary>
                    public class UsesClass 
                    {
                        [DataMember(Name = "crypt")] 
                        public bool? Crypt { get; private set; }
                        [DataMember(Name = "function")] 
                        public bool? Function { get; private set; }
                        [DataMember(Name = "macing")] 
                        public bool? Macing { get; private set; }
                        [DataMember(Name = "pinlocal")] 
                        public bool? Pinlocal { get; private set; }
                        [DataMember(Name = "svenckey")] 
                        public bool? Svenckey { get; private set; }
                        [DataMember(Name = "pinremote")] 
                        public bool? Pinremote { get; private set; }

                        public UsesClass (bool? Crypt, bool? Function, bool? Macing, bool? Pinlocal, bool? Svenckey, bool? Pinremote)
                        {
                            this.Crypt = Crypt;
                            this.Function = Function;
                            this.Macing = Macing;
                            this.Pinlocal = Pinlocal;
                            this.Svenckey = Svenckey;
                            this.Pinremote = Pinremote;
                        }
                    }
                    [DataMember(Name = "uses")] 
                    public UsesClass Uses { get; private set; }

                    public RestrictedKeyEncKeySupportClass (LoadingMethodEnum? LoadingMethod, UsesClass Uses)
                    {
                        this.LoadingMethod = LoadingMethod;
                        this.Uses = Uses;
                    }
                }
                [DataMember(Name = "restrictedKeyEncKeySupport")] 
                public RestrictedKeyEncKeySupportClass RestrictedKeyEncKeySupport { get; private set; }
                
                /// <summary>
                ///Specifies the Symmentric Key Management modes
                /// </summary>
                public class SymmetricKeyManagementMethodsClass 
                {
                    [DataMember(Name = "fixedKey")] 
                    public bool? FixedKey { get; private set; }
                    [DataMember(Name = "masterKey")] 
                    public bool? MasterKey { get; private set; }
                    [DataMember(Name = "tdesDukpt")] 
                    public bool? TdesDukpt { get; private set; }

                    public SymmetricKeyManagementMethodsClass (bool? FixedKey, bool? MasterKey, bool? TdesDukpt)
                    {
                        this.FixedKey = FixedKey;
                        this.MasterKey = MasterKey;
                        this.TdesDukpt = TdesDukpt;
                    }
                }
                [DataMember(Name = "symmetricKeyManagementMethods")] 
                public SymmetricKeyManagementMethodsClass SymmetricKeyManagementMethods { get; private set; }
                
                /// <summary>
                ///Array of attributes supported by ImportKey command for the key to be loaded.
                /// </summary>
                public class KeyAttributesClass 
                {
                    [DataMember(Name = "keyUsage")] 
                    public string KeyUsage { get; private set; }
                    [DataMember(Name = "propkeyUsage")] 
                    public int? PropkeyUsage { get; private set; }
                    [DataMember(Name = "algorithm")] 
                    public string Algorithm { get; private set; }
                    [DataMember(Name = "propAlgorithm")] 
                    public int? PropAlgorithm { get; private set; }
                    [DataMember(Name = "modeOfUse")] 
                    public string ModeOfUse { get; private set; }
                    [DataMember(Name = "propmodeOfUse")] 
                    public int? PropmodeOfUse { get; private set; }
                    [DataMember(Name = "cryptoMethod")] 
                    public string CryptoMethod { get; private set; }

                    public KeyAttributesClass (string KeyUsage, int? PropkeyUsage, string Algorithm, int? PropAlgorithm, string ModeOfUse, int? PropmodeOfUse, string CryptoMethod)
                    {
                        this.KeyUsage = KeyUsage;
                        this.PropkeyUsage = PropkeyUsage;
                        this.Algorithm = Algorithm;
                        this.PropAlgorithm = PropAlgorithm;
                        this.ModeOfUse = ModeOfUse;
                        this.PropmodeOfUse = PropmodeOfUse;
                        this.CryptoMethod = CryptoMethod;
                    }
                }
                [DataMember(Name = "keyAttributes")] 
                public KeyAttributesClass KeyAttributes { get; private set; }
                
                /// <summary>
                ///Array of attributes supported by the Import command for the key used to decrypt or unwrap the key being imported.
                /// </summary>
                public class DecryptAttributesClass 
                {
                    [DataMember(Name = "algorithm")] 
                    public string Algorithm { get; private set; }
                    [DataMember(Name = "propAlgorithm")] 
                    public int? PropAlgorithm { get; private set; }
                    public enum CryptoMethodEnum
                    {
                        Ecb,
                        Cbc,
                        Cfb,
                        Ofb,
                        Ctr,
                        Xts,
                        RsaesPkcs1V15,
                        RsaesOaep,
                    }
                    [DataMember(Name = "cryptoMethod")] 
                    public CryptoMethodEnum? CryptoMethod { get; private set; }

                    public DecryptAttributesClass (string Algorithm, int? PropAlgorithm, CryptoMethodEnum? CryptoMethod)
                    {
                        this.Algorithm = Algorithm;
                        this.PropAlgorithm = PropAlgorithm;
                        this.CryptoMethod = CryptoMethod;
                    }
                }
                [DataMember(Name = "decryptAttributes")] 
                public DecryptAttributesClass DecryptAttributes { get; private set; }
                
                /// <summary>
                ///Array of attributes supported by Import command for the key used for verification before importing the key.
                /// </summary>
                public class VerifyAttributesClass 
                {
                    [DataMember(Name = "keyUsage")] 
                    public string KeyUsage { get; private set; }
                    [DataMember(Name = "propkeyUsage")] 
                    public int? PropkeyUsage { get; private set; }
                    [DataMember(Name = "algorithm")] 
                    public string Algorithm { get; private set; }
                    [DataMember(Name = "propAlgorithm")] 
                    public int? PropAlgorithm { get; private set; }
                    [DataMember(Name = "modeOfUse")] 
                    public string ModeOfUse { get; private set; }
                    [DataMember(Name = "propmodeOfUse")] 
                    public int? PropmodeOfUse { get; private set; }
                    public enum CryptoMethodEnum
                    {
                        None,
                        Self,
                        Zero,
                        RsassaPkcs1V15,
                        RsassaPss,
                        Sha1,
                        Sha256,
                    }
                    [DataMember(Name = "cryptoMethod")] 
                    public CryptoMethodEnum? CryptoMethod { get; private set; }

                    public VerifyAttributesClass (string KeyUsage, int? PropkeyUsage, string Algorithm, int? PropAlgorithm, string ModeOfUse, int? PropmodeOfUse, CryptoMethodEnum? CryptoMethod)
                    {
                        this.KeyUsage = KeyUsage;
                        this.PropkeyUsage = PropkeyUsage;
                        this.Algorithm = Algorithm;
                        this.PropAlgorithm = PropAlgorithm;
                        this.ModeOfUse = ModeOfUse;
                        this.PropmodeOfUse = PropmodeOfUse;
                        this.CryptoMethod = CryptoMethod;
                    }
                }
                [DataMember(Name = "verifyAttributes")] 
                public VerifyAttributesClass VerifyAttributes { get; private set; }

                public KeyManagementClass (int? KeyNum, IdKeyClass IdKey, KeyCheckModesClass KeyCheckModes, string HsmVendor, bool? RsaAuthenticationScheme, RsaSignatureAlgorithmClass RsaSignatureAlgorithm, RsaCryptAlgorithmClass RsaCryptAlgorithm, RsaKeyCheckModeClass RsaKeyCheckMode, SignatureSchemeClass SignatureScheme, EmvImportSchemesClass EmvImportSchemes, KeyBlockImportFormatsClass KeyBlockImportFormats, bool? KeyImportThroughParts, DesKeyLengthClass DesKeyLength, CertificateTypesClass CertificateTypes, LoadCertOptionsClass LoadCertOptions, CrklLoadOptionsClass CrklLoadOptions, RestrictedKeyEncKeySupportClass RestrictedKeyEncKeySupport, SymmetricKeyManagementMethodsClass SymmetricKeyManagementMethods, KeyAttributesClass KeyAttributes, DecryptAttributesClass DecryptAttributes, VerifyAttributesClass VerifyAttributes)
                {
                    this.KeyNum = KeyNum;
                    this.IdKey = IdKey;
                    this.KeyCheckModes = KeyCheckModes;
                    this.HsmVendor = HsmVendor;
                    this.RsaAuthenticationScheme = RsaAuthenticationScheme;
                    this.RsaSignatureAlgorithm = RsaSignatureAlgorithm;
                    this.RsaCryptAlgorithm = RsaCryptAlgorithm;
                    this.RsaKeyCheckMode = RsaKeyCheckMode;
                    this.SignatureScheme = SignatureScheme;
                    this.EmvImportSchemes = EmvImportSchemes;
                    this.KeyBlockImportFormats = KeyBlockImportFormats;
                    this.KeyImportThroughParts = KeyImportThroughParts;
                    this.DesKeyLength = DesKeyLength;
                    this.CertificateTypes = CertificateTypes;
                    this.LoadCertOptions = LoadCertOptions;
                    this.CrklLoadOptions = CrklLoadOptions;
                    this.RestrictedKeyEncKeySupport = RestrictedKeyEncKeySupport;
                    this.SymmetricKeyManagementMethods = SymmetricKeyManagementMethods;
                    this.KeyAttributes = KeyAttributes;
                    this.DecryptAttributes = DecryptAttributes;
                    this.VerifyAttributes = VerifyAttributes;
                }


            }

            /// <summary>
            ///Capability information for XFS4IoT services implementing the Keyboard interface. This will be omitted if the Keyboard interface is not supported.
            /// </summary>
            public class KeyboardClass
            {
                
                /// <summary>
                ///Specifies whether the device will emit a key beep tone on key presses of active keys or inactive keys, and if so, which mode it supports
                /// </summary>
                public class AutoBeepClass 
                {
                    [DataMember(Name = "activeAvailable")] 
                    public bool? ActiveAvailable { get; private set; }
                    [DataMember(Name = "activeSelectable")] 
                    public bool? ActiveSelectable { get; private set; }
                    [DataMember(Name = "inactiveAvailable")] 
                    public bool? InactiveAvailable { get; private set; }
                    [DataMember(Name = "inactiveSelectable")] 
                    public bool? InactiveSelectable { get; private set; }

                    public AutoBeepClass (bool? ActiveAvailable, bool? ActiveSelectable, bool? InactiveAvailable, bool? InactiveSelectable)
                    {
                        this.ActiveAvailable = ActiveAvailable;
                        this.ActiveSelectable = ActiveSelectable;
                        this.InactiveAvailable = InactiveAvailable;
                        this.InactiveSelectable = InactiveSelectable;
                    }
                }
                [DataMember(Name = "autoBeep")] 
                public AutoBeepClass AutoBeep { get; private set; }
                
                /// <summary>
                ///Specifies the capabilities of the ets device.
                /// </summary>
                public class EtsCapsClass 
                {
                    [DataMember(Name = "xPos")] 
                    public int? XPos { get; private set; }
                    [DataMember(Name = "yPos")] 
                    public int? YPos { get; private set; }
                    [DataMember(Name = "xSize")] 
                    public int? XSize { get; private set; }
                    [DataMember(Name = "ySize")] 
                    public int? YSize { get; private set; }
                    [DataMember(Name = "maximumTouchFrames")] 
                    public int? MaximumTouchFrames { get; private set; }
                    [DataMember(Name = "maximumTouchKeys")] 
                    public int? MaximumTouchKeys { get; private set; }
                    
                    /// <summary>
                    ///Specifies if the device can float the touch keyboards. FloatNone if the PIN device cannot randomly shift the layout.
                    /// </summary>
                    public class FloatFlagsClass 
                    {
                        [DataMember(Name = "x")] 
                        public bool? X { get; private set; }
                        [DataMember(Name = "y")] 
                        public bool? Y { get; private set; }

                        public FloatFlagsClass (bool? X, bool? Y)
                        {
                            this.X = X;
                            this.Y = Y;
                        }
                    }
                    [DataMember(Name = "floatFlags")] 
                    public FloatFlagsClass FloatFlags { get; private set; }

                    public EtsCapsClass (int? XPos, int? YPos, int? XSize, int? YSize, int? MaximumTouchFrames, int? MaximumTouchKeys, FloatFlagsClass FloatFlags)
                    {
                        this.XPos = XPos;
                        this.YPos = YPos;
                        this.XSize = XSize;
                        this.YSize = YSize;
                        this.MaximumTouchFrames = MaximumTouchFrames;
                        this.MaximumTouchKeys = MaximumTouchKeys;
                        this.FloatFlags = FloatFlags;
                    }
                }
                [DataMember(Name = "etsCaps")] 
                public EtsCapsClass EtsCaps { get; private set; }

                public KeyboardClass (AutoBeepClass AutoBeep, EtsCapsClass EtsCaps)
                {
                    this.AutoBeep = AutoBeep;
                    this.EtsCaps = EtsCaps;
                }


            }

            /// <summary>
            ///Capability information for XFS4IoT services implementing the TextTerminal interface. This will be omitted if the TextTerminal interface is not supported.
            /// </summary>
            public class TextTerminalClass
            {
                public enum TypeEnum
                {
                    Fixed,
                    Removable,
                }
                [DataMember(Name = "type")] 
                public TypeEnum? Type { get; private set; }
                
                /// <summary>
                ///Array specifies the resolutions supported by the physical display device.The resolution indicated in the first position is the default resolution and the device will be placed in this resolution when the Service Provider is initialized or reset through the reset command.
                /// </summary>
                public class ResolutionsClass 
                {
                    [DataMember(Name = "sizeX")] 
                    public int? SizeX { get; private set; }
                    [DataMember(Name = "sizeY")] 
                    public int? SizeY { get; private set; }

                    public ResolutionsClass (int? SizeX, int? SizeY)
                    {
                        this.SizeX = SizeX;
                        this.SizeY = SizeY;
                    }
                }
                [DataMember(Name = "resolutions")] 
                public ResolutionsClass Resolutions { get; private set; }
                [DataMember(Name = "keyLock")] 
                public bool? KeyLock { get; private set; }
                [DataMember(Name = "displayLight")] 
                public bool? DisplayLight { get; private set; }
                [DataMember(Name = "cursor")] 
                public bool? Cursor { get; private set; }
                [DataMember(Name = "forms")] 
                public bool? Forms { get; private set; }
                
                /// <summary>
                ///For charSupport, a Service Provider can support ONLY ascii forms or can support BOTH ascii and unicode forms. A Service Provider can not support UNICODE forms without also supporting ASCII forms.\
                /// </summary>
                public class CharSupportClass 
                {
                    [DataMember(Name = "ascii")] 
                    public bool? Ascii { get; private set; }
                    [DataMember(Name = "unicode")] 
                    public bool? Unicode { get; private set; }

                    public CharSupportClass (bool? Ascii, bool? Unicode)
                    {
                        this.Ascii = Ascii;
                        this.Unicode = Unicode;
                    }
                }
                [DataMember(Name = "charSupport")] 
                public CharSupportClass CharSupport { get; private set; }
                
                /// <summary>
                ///Specifies which LEDs are available.The elements of this array are specified as a combination of the following flags and indicate all of the possible flash rates (type B) and colors (type C) that the LED is capable of handling. If the LED only supports one color then no value of type C is returned.
                /// </summary>
                public class LedsClass 
                {
                    [DataMember(Name = "off")] 
                    public bool? Off { get; private set; }
                    [DataMember(Name = "slowFlash")] 
                    public bool? SlowFlash { get; private set; }
                    [DataMember(Name = "mediumFlash")] 
                    public bool? MediumFlash { get; private set; }
                    [DataMember(Name = "quickFlash")] 
                    public bool? QuickFlash { get; private set; }
                    [DataMember(Name = "continuous")] 
                    public bool? Continuous { get; private set; }
                    [DataMember(Name = "red")] 
                    public bool? Red { get; private set; }
                    [DataMember(Name = "green")] 
                    public bool? Green { get; private set; }
                    [DataMember(Name = "yellow")] 
                    public bool? Yellow { get; private set; }
                    [DataMember(Name = "blue")] 
                    public bool? Blue { get; private set; }
                    [DataMember(Name = "cyan")] 
                    public bool? Cyan { get; private set; }
                    [DataMember(Name = "magenta")] 
                    public bool? Magenta { get; private set; }
                    [DataMember(Name = "white")] 
                    public bool? White { get; private set; }

                    public LedsClass (bool? Off, bool? SlowFlash, bool? MediumFlash, bool? QuickFlash, bool? Continuous, bool? Red, bool? Green, bool? Yellow, bool? Blue, bool? Cyan, bool? Magenta, bool? White)
                    {
                        this.Off = Off;
                        this.SlowFlash = SlowFlash;
                        this.MediumFlash = MediumFlash;
                        this.QuickFlash = QuickFlash;
                        this.Continuous = Continuous;
                        this.Red = Red;
                        this.Green = Green;
                        this.Yellow = Yellow;
                        this.Blue = Blue;
                        this.Cyan = Cyan;
                        this.Magenta = Magenta;
                        this.White = White;
                    }
                }
                [DataMember(Name = "leds")] 
                public LedsClass Leds { get; private set; }

                public TextTerminalClass (TypeEnum? Type, ResolutionsClass Resolutions, bool? KeyLock, bool? DisplayLight, bool? Cursor, bool? Forms, CharSupportClass CharSupport, LedsClass Leds)
                {
                    this.Type = Type;
                    this.Resolutions = Resolutions;
                    this.KeyLock = KeyLock;
                    this.DisplayLight = DisplayLight;
                    this.Cursor = Cursor;
                    this.Forms = Forms;
                    this.CharSupport = CharSupport;
                    this.Leds = Leds;
                }


            }

            /// <summary>
            ///Capability information for XFS4IoT services implementing the Printer interface. This will be omitted if the Printer interface is not supported.
            /// </summary>
            public class PrinterClass
            {
                
                /// <summary>
                ///Specifies the type(s) of the physical device driven by the logical service.
                /// </summary>
                public class TypeClass 
                {
                    [DataMember(Name = "receipt")] 
                    public bool? Receipt { get; private set; }
                    [DataMember(Name = "passbook")] 
                    public bool? Passbook { get; private set; }
                    [DataMember(Name = "journal")] 
                    public bool? Journal { get; private set; }
                    [DataMember(Name = "document")] 
                    public bool? Document { get; private set; }
                    [DataMember(Name = "scanner")] 
                    public bool? Scanner { get; private set; }

                    public TypeClass (bool? Receipt, bool? Passbook, bool? Journal, bool? Document, bool? Scanner)
                    {
                        this.Receipt = Receipt;
                        this.Passbook = Passbook;
                        this.Journal = Journal;
                        this.Document = Document;
                        this.Scanner = Scanner;
                    }
                }
                [DataMember(Name = "type")] 
                public TypeClass Type { get; private set; }
                
                /// <summary>
                ///Specifies at which resolution(s) the physical device can print. Used by the application to select the level of print quality desired; does not imply any absolute level of resolution, only relative.
                /// </summary>
                public class ResolutionClass 
                {
                    [DataMember(Name = "low")] 
                    public bool? Low { get; private set; }
                    [DataMember(Name = "medium")] 
                    public bool? Medium { get; private set; }
                    [DataMember(Name = "high")] 
                    public bool? High { get; private set; }
                    [DataMember(Name = "veryHigh")] 
                    public bool? VeryHigh { get; private set; }

                    public ResolutionClass (bool? Low, bool? Medium, bool? High, bool? VeryHigh)
                    {
                        this.Low = Low;
                        this.Medium = Medium;
                        this.High = High;
                        this.VeryHigh = VeryHigh;
                    }
                }
                [DataMember(Name = "resolution")] 
                public ResolutionClass Resolution { get; private set; }
                
                /// <summary>
                ///Specifies whether the device can read data from media, as a combination of the following flags.
                /// </summary>
                public class ReadFormClass 
                {
                    [DataMember(Name = "ocr")] 
                    public bool? Ocr { get; private set; }
                    [DataMember(Name = "micr")] 
                    public bool? Micr { get; private set; }
                    [DataMember(Name = "msf")] 
                    public bool? Msf { get; private set; }
                    [DataMember(Name = "barcode")] 
                    public bool? Barcode { get; private set; }
                    [DataMember(Name = "pageMark")] 
                    public bool? PageMark { get; private set; }
                    [DataMember(Name = "readImage")] 
                    public bool? ReadImage { get; private set; }
                    [DataMember(Name = "readEmptyLine")] 
                    public bool? ReadEmptyLine { get; private set; }

                    public ReadFormClass (bool? Ocr, bool? Micr, bool? Msf, bool? Barcode, bool? PageMark, bool? ReadImage, bool? ReadEmptyLine)
                    {
                        this.Ocr = Ocr;
                        this.Micr = Micr;
                        this.Msf = Msf;
                        this.Barcode = Barcode;
                        this.PageMark = PageMark;
                        this.ReadImage = ReadImage;
                        this.ReadEmptyLine = ReadEmptyLine;
                    }
                }
                [DataMember(Name = "readForm")] 
                public ReadFormClass ReadForm { get; private set; }
                
                /// <summary>
                ///Specifies whether the device can write data to the media, as a combination of the following flags.
                /// </summary>
                public class WriteFormClass 
                {
                    [DataMember(Name = "text")] 
                    public bool? Text { get; private set; }
                    [DataMember(Name = "graphics")] 
                    public bool? Graphics { get; private set; }
                    [DataMember(Name = "ocr")] 
                    public bool? Ocr { get; private set; }
                    [DataMember(Name = "micr")] 
                    public bool? Micr { get; private set; }
                    [DataMember(Name = "msf")] 
                    public bool? Msf { get; private set; }
                    [DataMember(Name = "barcode")] 
                    public bool? Barcode { get; private set; }
                    [DataMember(Name = "stamp")] 
                    public bool? Stamp { get; private set; }

                    public WriteFormClass (bool? Text, bool? Graphics, bool? Ocr, bool? Micr, bool? Msf, bool? Barcode, bool? Stamp)
                    {
                        this.Text = Text;
                        this.Graphics = Graphics;
                        this.Ocr = Ocr;
                        this.Micr = Micr;
                        this.Msf = Msf;
                        this.Barcode = Barcode;
                        this.Stamp = Stamp;
                    }
                }
                [DataMember(Name = "writeForm")] 
                public WriteFormClass WriteForm { get; private set; }
                
                /// <summary>
                ///Specifies whether the device is able to measure the inserted media, as a combination of the following flags.
                /// </summary>
                public class ExtentsClass 
                {
                    [DataMember(Name = "horizontal")] 
                    public bool? Horizontal { get; private set; }
                    [DataMember(Name = "vertical")] 
                    public bool? Vertical { get; private set; }

                    public ExtentsClass (bool? Horizontal, bool? Vertical)
                    {
                        this.Horizontal = Horizontal;
                        this.Vertical = Vertical;
                    }
                }
                [DataMember(Name = "extents")] 
                public ExtentsClass Extents { get; private set; }
                
                /// <summary>
                ///Specifies the manner in which media can be controlled, as a combination of the following flags.
                /// </summary>
                public class ControlClass 
                {
                    [DataMember(Name = "eject")] 
                    public bool? Eject { get; private set; }
                    [DataMember(Name = "perforate")] 
                    public bool? Perforate { get; private set; }
                    [DataMember(Name = "cut")] 
                    public bool? Cut { get; private set; }
                    [DataMember(Name = "skip")] 
                    public bool? Skip { get; private set; }
                    [DataMember(Name = "flush")] 
                    public bool? Flush { get; private set; }
                    [DataMember(Name = "retract")] 
                    public bool? Retract { get; private set; }
                    [DataMember(Name = "stack")] 
                    public bool? Stack { get; private set; }
                    [DataMember(Name = "partialCut")] 
                    public bool? PartialCut { get; private set; }
                    [DataMember(Name = "alarm")] 
                    public bool? Alarm { get; private set; }
                    [DataMember(Name = "pageForward")] 
                    public bool? PageForward { get; private set; }
                    [DataMember(Name = "pageBackward")] 
                    public bool? PageBackward { get; private set; }
                    [DataMember(Name = "turnMedia")] 
                    public bool? TurnMedia { get; private set; }
                    [DataMember(Name = "stamp")] 
                    public bool? Stamp { get; private set; }
                    [DataMember(Name = "park")] 
                    public bool? Park { get; private set; }
                    [DataMember(Name = "expel")] 
                    public bool? Expel { get; private set; }
                    [DataMember(Name = "ejectToTransport")] 
                    public bool? EjectToTransport { get; private set; }
                    [DataMember(Name = "rotate180")] 
                    public bool? Rotate180 { get; private set; }
                    [DataMember(Name = "clearBuffer")] 
                    public bool? ClearBuffer { get; private set; }

                    public ControlClass (bool? Eject, bool? Perforate, bool? Cut, bool? Skip, bool? Flush, bool? Retract, bool? Stack, bool? PartialCut, bool? Alarm, bool? PageForward, bool? PageBackward, bool? TurnMedia, bool? Stamp, bool? Park, bool? Expel, bool? EjectToTransport, bool? Rotate180, bool? ClearBuffer)
                    {
                        this.Eject = Eject;
                        this.Perforate = Perforate;
                        this.Cut = Cut;
                        this.Skip = Skip;
                        this.Flush = Flush;
                        this.Retract = Retract;
                        this.Stack = Stack;
                        this.PartialCut = PartialCut;
                        this.Alarm = Alarm;
                        this.PageForward = PageForward;
                        this.PageBackward = PageBackward;
                        this.TurnMedia = TurnMedia;
                        this.Stamp = Stamp;
                        this.Park = Park;
                        this.Expel = Expel;
                        this.EjectToTransport = EjectToTransport;
                        this.Rotate180 = Rotate180;
                        this.ClearBuffer = ClearBuffer;
                    }
                }
                [DataMember(Name = "control")] 
                public ControlClass Control { get; private set; }
                [DataMember(Name = "maxMediaOnStacker")] 
                public int? MaxMediaOnStacker { get; private set; }
                [DataMember(Name = "acceptMedia")] 
                public bool? AcceptMedia { get; private set; }
                [DataMember(Name = "multiPage")] 
                public bool? MultiPage { get; private set; }
                
                /// <summary>
                ///Specifies the Paper sources available for this printer as a combination of the following flags
                /// </summary>
                public class PaperSourcesClass 
                {
                    [DataMember(Name = "upper")] 
                    public bool? Upper { get; private set; }
                    [DataMember(Name = "lower")] 
                    public bool? Lower { get; private set; }
                    [DataMember(Name = "external")] 
                    public bool? External { get; private set; }
                    [DataMember(Name = "aux")] 
                    public bool? Aux { get; private set; }
                    [DataMember(Name = "aux2")] 
                    public bool? Aux2 { get; private set; }
                    [DataMember(Name = "park")] 
                    public bool? Park { get; private set; }

                    public PaperSourcesClass (bool? Upper, bool? Lower, bool? External, bool? Aux, bool? Aux2, bool? Park)
                    {
                        this.Upper = Upper;
                        this.Lower = Lower;
                        this.External = External;
                        this.Aux = Aux;
                        this.Aux2 = Aux2;
                        this.Park = Park;
                    }
                }
                [DataMember(Name = "paperSources")] 
                public PaperSourcesClass PaperSources { get; private set; }
                [DataMember(Name = "mediaTaken")] 
                public bool? MediaTaken { get; private set; }
                [DataMember(Name = "retractBins")] 
                public int? RetractBins { get; private set; }
                [DataMember(Name = "maxRetract")] 
                public List<int?> MaxRetract { get; private set; }
                
                /// <summary>
                ///Specifies the image format supported by this device, as a combination of following flags.
                /// </summary>
                public class ImageTypeClass 
                {
                    [DataMember(Name = "tif")] 
                    public bool? Tif { get; private set; }
                    [DataMember(Name = "wmf")] 
                    public bool? Wmf { get; private set; }
                    [DataMember(Name = "bmp")] 
                    public bool? Bmp { get; private set; }
                    [DataMember(Name = "jpg")] 
                    public bool? Jpg { get; private set; }

                    public ImageTypeClass (bool? Tif, bool? Wmf, bool? Bmp, bool? Jpg)
                    {
                        this.Tif = Tif;
                        this.Wmf = Wmf;
                        this.Bmp = Bmp;
                        this.Jpg = Jpg;
                    }
                }
                [DataMember(Name = "imageType")] 
                public ImageTypeClass ImageType { get; private set; }
                
                /// <summary>
                ///Specifies the front image color formats supported by this device, as a combination of following flags.
                /// </summary>
                public class FrontImageColorFormatClass 
                {
                    [DataMember(Name = "binary")] 
                    public bool? Binary { get; private set; }
                    [DataMember(Name = "grayscale")] 
                    public bool? Grayscale { get; private set; }
                    [DataMember(Name = "full")] 
                    public bool? Full { get; private set; }

                    public FrontImageColorFormatClass (bool? Binary, bool? Grayscale, bool? Full)
                    {
                        this.Binary = Binary;
                        this.Grayscale = Grayscale;
                        this.Full = Full;
                    }
                }
                [DataMember(Name = "frontImageColorFormat")] 
                public FrontImageColorFormatClass FrontImageColorFormat { get; private set; }
                
                /// <summary>
                ///Specifies the back image color formats supported by this device, as a combination of following flags.
                /// </summary>
                public class BackImageColorFormatClass 
                {
                    [DataMember(Name = "binary")] 
                    public bool? Binary { get; private set; }
                    [DataMember(Name = "grayScale")] 
                    public bool? GrayScale { get; private set; }
                    [DataMember(Name = "full")] 
                    public bool? Full { get; private set; }

                    public BackImageColorFormatClass (bool? Binary, bool? GrayScale, bool? Full)
                    {
                        this.Binary = Binary;
                        this.GrayScale = GrayScale;
                        this.Full = Full;
                    }
                }
                [DataMember(Name = "backImageColorFormat")] 
                public BackImageColorFormatClass BackImageColorFormat { get; private set; }
                
                /// <summary>
                ///Specifies the code line (MICR data) formats supported by this device, as a combination of following flags.
                /// </summary>
                public class CodelineFormatClass 
                {
                    [DataMember(Name = "cmc7")] 
                    public bool? Cmc7 { get; private set; }
                    [DataMember(Name = "e13b")] 
                    public bool? E13b { get; private set; }
                    [DataMember(Name = "ocr")] 
                    public bool? Ocr { get; private set; }

                    public CodelineFormatClass (bool? Cmc7, bool? E13b, bool? Ocr)
                    {
                        this.Cmc7 = Cmc7;
                        this.E13b = E13b;
                        this.Ocr = Ocr;
                    }
                }
                [DataMember(Name = "codelineFormat")] 
                public CodelineFormatClass CodelineFormat { get; private set; }
                
                /// <summary>
                ///Specifies the source for the read image command supported by this device, as a combination of the following flags.
                /// </summary>
                public class ImageSourceClass 
                {
                    [DataMember(Name = "imageFront")] 
                    public bool? ImageFront { get; private set; }
                    [DataMember(Name = "imageBack")] 
                    public bool? ImageBack { get; private set; }
                    [DataMember(Name = "codeLine")] 
                    public bool? CodeLine { get; private set; }

                    public ImageSourceClass (bool? ImageFront, bool? ImageBack, bool? CodeLine)
                    {
                        this.ImageFront = ImageFront;
                        this.ImageBack = ImageBack;
                        this.CodeLine = CodeLine;
                    }
                }
                [DataMember(Name = "imageSource")] 
                public ImageSourceClass ImageSource { get; private set; }
                [DataMember(Name = "dispensePaper")] 
                public bool? DispensePaper { get; private set; }
                [DataMember(Name = "osPrinter")] 
                public string OsPrinter { get; private set; }
                [DataMember(Name = "mediaPresented")] 
                public bool? MediaPresented { get; private set; }
                [DataMember(Name = "autoRetractPeriod")] 
                public int? AutoRetractPeriod { get; private set; }
                [DataMember(Name = "retractToTransport")] 
                public bool? RetractToTransport { get; private set; }
                
                /// <summary>
                ///Specifies the form write modes supported by this device, as a combination of the following flags.
                /// </summary>
                public class CoercivityTypeClass 
                {
                    [DataMember(Name = "low")] 
                    public bool? Low { get; private set; }
                    [DataMember(Name = "high")] 
                    public bool? High { get; private set; }
                    [DataMember(Name = "auto")] 
                    public bool? Auto { get; private set; }

                    public CoercivityTypeClass (bool? Low, bool? High, bool? Auto)
                    {
                        this.Low = Low;
                        this.High = High;
                        this.Auto = Auto;
                    }
                }
                [DataMember(Name = "coercivityType")] 
                public CoercivityTypeClass CoercivityType { get; private set; }
                
                /// <summary>
                ///<a name=\"printer-capability-controlpassbook\"></a>Specifies how the passbook can be controlled with the  [Printer.ControlPassbook](#command-Printer.ControlPassbook) command, as a combination of the following flags.
                /// </summary>
                public class ControlPassbookClass 
                {
                    [DataMember(Name = "turnForward")] 
                    public bool? TurnForward { get; private set; }
                    [DataMember(Name = "turnBackward")] 
                    public bool? TurnBackward { get; private set; }
                    [DataMember(Name = "closeForward")] 
                    public bool? CloseForward { get; private set; }
                    [DataMember(Name = "closeBackward")] 
                    public bool? CloseBackward { get; private set; }

                    public ControlPassbookClass (bool? TurnForward, bool? TurnBackward, bool? CloseForward, bool? CloseBackward)
                    {
                        this.TurnForward = TurnForward;
                        this.TurnBackward = TurnBackward;
                        this.CloseForward = CloseForward;
                        this.CloseBackward = CloseBackward;
                    }
                }
                [DataMember(Name = "controlPassbook")] 
                public ControlPassbookClass ControlPassbook { get; private set; }
                public enum PrintSidesEnum
                {
                    NotSupp,
                    Single,
                    Dual,
                }
                [DataMember(Name = "printSides")] 
                public PrintSidesEnum? PrintSides { get; private set; }

                public PrinterClass (TypeClass Type, ResolutionClass Resolution, ReadFormClass ReadForm, WriteFormClass WriteForm, ExtentsClass Extents, ControlClass Control, int? MaxMediaOnStacker, bool? AcceptMedia, bool? MultiPage, PaperSourcesClass PaperSources, bool? MediaTaken, int? RetractBins, List<int?> MaxRetract, ImageTypeClass ImageType, FrontImageColorFormatClass FrontImageColorFormat, BackImageColorFormatClass BackImageColorFormat, CodelineFormatClass CodelineFormat, ImageSourceClass ImageSource, bool? DispensePaper, string OsPrinter, bool? MediaPresented, int? AutoRetractPeriod, bool? RetractToTransport, CoercivityTypeClass CoercivityType, ControlPassbookClass ControlPassbook, PrintSidesEnum? PrintSides)
                {
                    this.Type = Type;
                    this.Resolution = Resolution;
                    this.ReadForm = ReadForm;
                    this.WriteForm = WriteForm;
                    this.Extents = Extents;
                    this.Control = Control;
                    this.MaxMediaOnStacker = MaxMediaOnStacker;
                    this.AcceptMedia = AcceptMedia;
                    this.MultiPage = MultiPage;
                    this.PaperSources = PaperSources;
                    this.MediaTaken = MediaTaken;
                    this.RetractBins = RetractBins;
                    this.MaxRetract = MaxRetract;
                    this.ImageType = ImageType;
                    this.FrontImageColorFormat = FrontImageColorFormat;
                    this.BackImageColorFormat = BackImageColorFormat;
                    this.CodelineFormat = CodelineFormat;
                    this.ImageSource = ImageSource;
                    this.DispensePaper = DispensePaper;
                    this.OsPrinter = OsPrinter;
                    this.MediaPresented = MediaPresented;
                    this.AutoRetractPeriod = AutoRetractPeriod;
                    this.RetractToTransport = RetractToTransport;
                    this.CoercivityType = CoercivityType;
                    this.ControlPassbook = ControlPassbook;
                    this.PrintSides = PrintSides;
                }


            }


            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, List<InterfacesClass> Interfaces = null, CommonClass Common = null, CardReaderClass CardReader = null, CashAcceptorClass CashAcceptor = null, CashDispenserClass CashDispenser = null, CashManagementClass CashManagement = null, PinPadClass PinPad = null, CryptoClass Crypto = null, KeyManagementClass KeyManagement = null, KeyboardClass Keyboard = null, TextTerminalClass TextTerminal = null, PrinterClass Printer = null)
                : base(CompletionCode, ErrorDescription)
            {
                ErrorDescription.IsNotNullOrWhitespace($"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(CapabilitiesCompletion.PayloadData)}");

                this.Interfaces = Interfaces;
                this.Common = Common;
                this.CardReader = CardReader;
                this.CashAcceptor = CashAcceptor;
                this.CashDispenser = CashDispenser;
                this.CashManagement = CashManagement;
                this.PinPad = PinPad;
                this.Crypto = Crypto;
                this.KeyManagement = KeyManagement;
                this.Keyboard = Keyboard;
                this.TextTerminal = TextTerminal;
                this.Printer = Printer;
            }

            /// <summary>
            ///Array of interfaces supported by this XFS4IoT service.
            /// </summary>
            [DataMember(Name = "interfaces")] 
            public List<InterfacesClass> Interfaces{ get; private set; }
            /// <summary>
            ///Capability information common to all XFS4IoT services.
            /// </summary>
            [DataMember(Name = "common")] 
            public CommonClass Common { get; private set; }
            /// <summary>
            ///Capability information for XFS4IoT services implementing the CardReader interface. This will be omitted if the CardReader interface is not supported.
            /// </summary>
            [DataMember(Name = "cardReader")] 
            public CardReaderClass CardReader { get; private set; }
            /// <summary>
            ///Capability information for XFS4IoT services implementing the CashAcceptor interface. This will be omitted if the CashAcceptor interface is not supported.
            /// </summary>
            [DataMember(Name = "cashAcceptor")] 
            public CashAcceptorClass CashAcceptor { get; private set; }
            /// <summary>
            ///Capability information for XFS4IoT services implementing the CashDispenser interface. This will be omitted if the CashDispenser interface is not supported.
            /// </summary>
            [DataMember(Name = "cashDispenser")] 
            public CashDispenserClass CashDispenser { get; private set; }
            /// <summary>
            ///Capability information for XFS4IoT services implementing the CashManagement interface. This will be omitted if the CashManagement interface is not supported.
            /// </summary>
            [DataMember(Name = "cashManagement")] 
            public CashManagementClass CashManagement { get; private set; }
            /// <summary>
            ///Capability information for XFS4IoT services implementing the PinPad interface. This will be omitted if the PinPad interface is not supported.
            /// </summary>
            [DataMember(Name = "pinPad")] 
            public PinPadClass PinPad { get; private set; }
            /// <summary>
            ///Capability information for XFS4IoT services implementing the Crypto interface. This will be omitted if the Crypto interface is not supported.
            /// </summary>
            [DataMember(Name = "crypto")] 
            public CryptoClass Crypto { get; private set; }
            /// <summary>
            ///Capability information for XFS4IoT services implementing the KeyManagement interface. This will be omitted if the KeyManagement interface is not supported.
            /// </summary>
            [DataMember(Name = "keyManagement")] 
            public KeyManagementClass KeyManagement { get; private set; }
            /// <summary>
            ///Capability information for XFS4IoT services implementing the Keyboard interface. This will be omitted if the Keyboard interface is not supported.
            /// </summary>
            [DataMember(Name = "keyboard")] 
            public KeyboardClass Keyboard { get; private set; }
            /// <summary>
            ///Capability information for XFS4IoT services implementing the TextTerminal interface. This will be omitted if the TextTerminal interface is not supported.
            /// </summary>
            [DataMember(Name = "textTerminal")] 
            public TextTerminalClass TextTerminal { get; private set; }
            /// <summary>
            ///Capability information for XFS4IoT services implementing the Printer interface. This will be omitted if the Printer interface is not supported.
            /// </summary>
            [DataMember(Name = "printer")] 
            public PrinterClass Printer { get; private set; }

        }
    }
}

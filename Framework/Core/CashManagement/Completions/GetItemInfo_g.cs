/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashManagement interface.
 * GetItemInfo_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CashManagement.Completions
{
    [DataContract]
    [Completion(Name = "CashManagement.GetItemInfo")]
    public sealed class GetItemInfoCompletion : Completion<GetItemInfoCompletion.PayloadData>
    {
        public GetItemInfoCompletion(string RequestId, GetItemInfoCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            [DataContract]
            public sealed class ItemsListClass
            {
                /// <summary>
                /// Defines the note level. Following values are possible:
                /// 
                /// * ```level1``` - A level 1 banknote.
                /// * ```level2``` - A level 2 banknote.
                /// * ```level3``` - A level 3 banknote.
                /// * ```level4Fit``` - A fit level 4 banknote.
                /// * ```level4Unfit``` - An unfit level 4 banknote.
                /// </summary>
                public enum LevelEnum
                {
                    Level1,
                    Level2,
                    Level3,
                    Level4Fit,
                    Level4Unfit,
                }

                /// <summary>
                /// Specifies if the serial number reported in the *serialNumber* field is on the classification list. 
                /// If the classification list reporting capability is not supported this field will be omitted.
                /// Following values are possible:
                /// 
                /// * ```onClassificationList``` - The serial number of the items is on the classification list.
                /// * ```notOnClassificationList``` - The serial number of the items is not on the classification list.
                /// * ```classificationListUnknown``` - It is unknown if the serial number of the item is on the classification list.
                /// </summary>
                public enum OnClassificationListEnum
                {
                    OnClassificationList,
                    NotOnClassificationList,
                    ClassificationListUnknown,
                }

                /// <summary>
                /// Specifies the location of the item. Following values are possible:
                /// 
                /// * ```device``` - The item is inside the device in some position other than a cash unit.
                /// * ```cashUnit``` - The item is in a cash unit. The cash unit is defined by 
                /// [cashunit](#cashmanagement.getiteminfo.completion.properties.itemslist.cashunit).
                /// * ```customer``` - The item has been dispensed to the customer.
                /// * ```unknown``` - The item location is unknown.
                /// </summary>
                public enum ItemLocationEnum
                {
                    Device,
                    CashUnit,
                    Customer,
                    Unknown,
                }

                /// <summary>
                /// If [itemLocation](#cashmanagement.getiteminfo.completion.properties.itemslist.itemlocation) is 
                /// ```device``` this parameter specifies where the item is in the device. 
                /// If *itemLocation* is not ```device``` then *itemDeviceLocation* will be omitted.
                /// Following values are possible:
                /// 
                /// * ```stacker``` - The item is in the intermediate stacker.
                /// * ```output``` - The item is at the output position. The items have not been in customer access.
                /// * ```transport``` - The item is at another location in the device.
                /// * ```unknown``` - The item is in the device but its location is unknown.
                /// </summary>
                public enum ItemDeviceLocationEnum
                {
                    Stacker,
                    Output,
                    Transport,
                    Unknown,
                }

                public ItemsListClass(LevelEnum? Level = null, string SerialNumber = null, object Orientation = null, string P6Signature = null, string ImageFile = null, OnClassificationListEnum? OnClassificationList = null, ItemLocationEnum? ItemLocation = null, string Cashunit = null, ItemDeviceLocationEnum? ItemDeviceLocation = null)
                    : base()
                {
                    this.Level = Level;
                    this.SerialNumber = SerialNumber;
                    this.Orientation = Orientation;
                    this.P6Signature = P6Signature;
                    this.ImageFile = ImageFile;
                    this.OnClassificationList = OnClassificationList;
                    this.ItemLocation = ItemLocation;
                    this.Cashunit = Cashunit;
                    this.ItemDeviceLocation = ItemDeviceLocation;
                }

                /// <summary>
                /// Defines the note level. Following values are possible:
                /// 
                /// * ```level1``` - A level 1 banknote.
                /// * ```level2``` - A level 2 banknote.
                /// * ```level3``` - A level 3 banknote.
                /// * ```level4Fit``` - A fit level 4 banknote.
                /// * ```level4Unfit``` - An unfit level 4 banknote.
                /// </summary>
                [DataMember(Name = "level")] 
                public LevelEnum? Level { get; private set; }

                /// <summary>
                /// This field contains the serial number of the item as a string. A '?' character (0x003F) is used 
                /// to represent any serial number character that cannot be recognized. If no serial number is available or 
                /// has not been requested then *serialNumber* is omitted.
                /// </summary>
                [DataMember(Name = "serialNumber")] 
                public string SerialNumber { get; private set; }

                /// <summary>
                /// Orientation of the entered banknote.
                /// </summary>
                [DataMember(Name = "orientation")] 
                public object Orientation { get; private set; }

                /// <summary>
                /// Base64 encoded binary file containing only the vendor specific P6 signature data. 
                /// If no P6 signature is available then this field is omitted.
                /// </summary>
                [DataMember(Name = "p6Signature")] 
                public string P6Signature { get; private set; }

                /// <summary>
                /// Base64 encoded binary image data. If the Service does not support this function or the image file has 
                /// not been requested then imageFile is omitted.
                /// </summary>
                [DataMember(Name = "imageFile")] 
                public string ImageFile { get; private set; }

                /// <summary>
                /// Specifies if the serial number reported in the *serialNumber* field is on the classification list. 
                /// If the classification list reporting capability is not supported this field will be omitted.
                /// Following values are possible:
                /// 
                /// * ```onClassificationList``` - The serial number of the items is on the classification list.
                /// * ```notOnClassificationList``` - The serial number of the items is not on the classification list.
                /// * ```classificationListUnknown``` - It is unknown if the serial number of the item is on the classification list.
                /// </summary>
                [DataMember(Name = "onClassificationList")] 
                public OnClassificationListEnum? OnClassificationList { get; private set; }

                /// <summary>
                /// Specifies the location of the item. Following values are possible:
                /// 
                /// * ```device``` - The item is inside the device in some position other than a cash unit.
                /// * ```cashUnit``` - The item is in a cash unit. The cash unit is defined by 
                /// [cashunit](#cashmanagement.getiteminfo.completion.properties.itemslist.cashunit).
                /// * ```customer``` - The item has been dispensed to the customer.
                /// * ```unknown``` - The item location is unknown.
                /// </summary>
                [DataMember(Name = "itemLocation")] 
                public ItemLocationEnum? ItemLocation { get; private set; }

                /// <summary>
                /// If [itemLocation](#cashmanagement.getiteminfo.completion.properties.itemslist.itemlocation) is 
                /// ```cashUnit``` this parameter specifies the object name of the cash unit 
                /// which received the item as stated by the [CashManagement.GetCashUnitInfo](#cashmanagement.getcashunitinfo) 
                /// command. 
                /// If *itemLocation* is not ```cashUnit``` *cashunit* will be omitted.
                /// </summary>
                [DataMember(Name = "cashunit")] 
                public string Cashunit { get; private set; }

                /// <summary>
                /// If [itemLocation](#cashmanagement.getiteminfo.completion.properties.itemslist.itemlocation) is 
                /// ```device``` this parameter specifies where the item is in the device. 
                /// If *itemLocation* is not ```device``` then *itemDeviceLocation* will be omitted.
                /// Following values are possible:
                /// 
                /// * ```stacker``` - The item is in the intermediate stacker.
                /// * ```output``` - The item is at the output position. The items have not been in customer access.
                /// * ```transport``` - The item is at another location in the device.
                /// * ```unknown``` - The item is in the device but its location is unknown.
                /// </summary>
                [DataMember(Name = "itemDeviceLocation")] 
                public ItemDeviceLocationEnum? ItemDeviceLocation { get; private set; }

            }


            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, List<ItemsListClass> ItemsList = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ItemsList = ItemsList;
            }

            /// <summary>
            /// Array of "item info" objects.
            /// </summary>
            [DataMember(Name = "itemsList")] 
            public List<ItemsListClass> ItemsList{ get; private set; }

        }
    }
}

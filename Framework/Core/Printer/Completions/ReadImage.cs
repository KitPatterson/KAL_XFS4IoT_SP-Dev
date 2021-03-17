/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * 
 * This file was created automatically as part of the XFS4IoT Printer interface.
 * ReadImage.cs uses automatically generated parts. 
 * created at 3/16/2021 6:52:32 PM
\***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.Printer.Completions
{
    [DataContract]
    [Completion(Name = "Printer.ReadImage")]
    public sealed class ReadImageCompletion : Completion<ReadImageCompletion.PayloadData>
    {
        public ReadImageCompletion(string RequestId, ReadImageCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            /// <summary>
            ///The status and data for each of the requested images.
            /// </summary>
            public class ImagesClass
            {
                
                /// <summary>
                ///The front image status and data.
                /// </summary>
                public class FrontClass 
                {
                    public enum StatusEnum
                    {
                        Ok,
                        NotSupp,
                        Missing,
                    }
                    [DataMember(Name = "status")] 
                    public StatusEnum? Status { get; private set; }
                    [DataMember(Name = "data")] 
                    public string Data { get; private set; }

                    public FrontClass (StatusEnum? Status, string Data)
                    {
                        this.Status = Status;
                        this.Data = Data;
                    }
                }
                [DataMember(Name = "front")] 
                public FrontClass Front { get; private set; }
                
                /// <summary>
                ///The back image status and data.
                /// </summary>
                public class BackClass 
                {
                    public enum StatusEnum
                    {
                        Ok,
                        NotSupp,
                        Missing,
                    }
                    [DataMember(Name = "status")] 
                    public StatusEnum? Status { get; private set; }
                    [DataMember(Name = "data")] 
                    public string Data { get; private set; }

                    public BackClass (StatusEnum? Status, string Data)
                    {
                        this.Status = Status;
                        this.Data = Data;
                    }
                }
                [DataMember(Name = "back")] 
                public BackClass Back { get; private set; }
                
                /// <summary>
                ///The codeline status and data.
                /// </summary>
                public class CodelineClass 
                {
                    public enum StatusEnum
                    {
                        Ok,
                        NotSupp,
                        Missing,
                    }
                    [DataMember(Name = "status")] 
                    public StatusEnum? Status { get; private set; }
                    [DataMember(Name = "data")] 
                    public string Data { get; private set; }

                    public CodelineClass (StatusEnum? Status, string Data)
                    {
                        this.Status = Status;
                        this.Data = Data;
                    }
                }
                [DataMember(Name = "codeline")] 
                public CodelineClass Codeline { get; private set; }

                public ImagesClass (FrontClass Front, BackClass Back, CodelineClass Codeline)
                {
                    this.Front = Front;
                    this.Back = Back;
                    this.Codeline = Codeline;
                }


            }


            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ImagesClass Images = null)
                : base(CompletionCode, ErrorDescription)
            {
                ErrorDescription.IsNotNullOrWhitespace($"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(ReadImageCompletion.PayloadData)}");

                this.Images = Images;
            }

            /// <summary>
            ///The status and data for each of the requested images.
            /// </summary>
            [DataMember(Name = "images")] 
            public ImagesClass Images { get; private set; }

        }
    }
}

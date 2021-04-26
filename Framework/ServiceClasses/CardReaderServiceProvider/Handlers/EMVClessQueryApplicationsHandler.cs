/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * EMVClessQueryApplicationsHandler.cs uses automatically generated parts. 
 * created at 4/20/2021 12:28:05 PM
\***********************************************************************************************/


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoTServer;
using XFS4IoT.Completions;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    public partial class EMVClessQueryApplicationsHandler
    {
        /// <summary>
        /// EMVApplication
        /// Provide chip application and kernel identifier supported
        /// </summary>
        public sealed class EMVApplication
        {
            public EMVApplication(List<byte> ApplicationIdentifier,
                                  List<byte> KernelIdentifier)
            {
                this.ApplicationIdentifier = ApplicationIdentifier;
                this.KernelIdentifier = KernelIdentifier;
            }

            /// <summary>
            /// Chip application identifier
            /// </summary>
            public List<byte> ApplicationIdentifier { get; private set; }
            /// <summary>
            /// The kernel identifier certified
            /// </summary>
            public List<byte> KernelIdentifier { get; private set; }
        }

        /// <summary>
        /// QueryEMVApplicationResult
        /// Return information for supported EMV applications by the device
        /// </summary>
        public sealed class QueryEMVApplicationResult : DeviceResult
        {
            public QueryEMVApplicationResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                            ResetCompletion.PayloadData.ErrorCodeEnum? ErrorCode = null,
                                            string ErrorDescription = null,
                                            List<EMVApplication> EMVApplications = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.EMVApplications = EMVApplications;
            }

            public QueryEMVApplicationResult(MessagePayload.CompletionCodeEnum CompletionCode,
                                             List<EMVApplication> EMVApplications = null)
                : base(CompletionCode, null)
            {
                this.EMVApplications = EMVApplications;
            }

            /// <summary>
            /// List of EMV applications and kernels information
            /// </summary>
            public List<EMVApplication> EMVApplications { get; private set; }
        }

        private async Task<EMVClessQueryApplicationsCompletion.PayloadData> HandleEMVClessQueryApplications(IEMVClessQueryApplicationsEvents events, EMVClessQueryApplicationsCommand eMVClessQueryApplications, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CardReaderDev.EMVClessQueryApplications()");
            var result = await Device.EMVClessQueryApplications();
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.EMVClessQueryApplications() -> {result.CompletionCode}");

            List<EMVClessQueryApplicationsCompletion.PayloadData.AppDataClass> appData = new();
            foreach (EMVApplication app in result.EMVApplications)
            {
                if (app.ApplicationIdentifier.Count == 0)
                    continue;
                appData.Add(new EMVClessQueryApplicationsCompletion.PayloadData.AppDataClass(Convert.ToBase64String(app.ApplicationIdentifier.ToArray()),
                                                                                             app.KernelIdentifier.Count == 0 ? null : Convert.ToBase64String(app.KernelIdentifier.ToArray())));
            }

            return new EMVClessQueryApplicationsCompletion.PayloadData(result.CompletionCode,
                                                                       result.ErrorDescription,
                                                                       appData);
        }
    }
}

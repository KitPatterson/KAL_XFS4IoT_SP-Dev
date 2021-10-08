/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Storage interface.
 * IStorageDevice.cs uses automatically generated parts.
\***********************************************************************************************/


using System.Threading;
using System.Threading.Tasks;
using XFS4IoTServer;

// KAL specific implementation of storage. 
namespace XFS4IoTFramework.Storage
{
    public interface IStorageDevice : IDevice
    {

        /// <summary>
        /// This command is used to obtain information regarding the status, capabilities and contents of storage units. Thecapabilities of the storage unit can be used to dynamically configure the storage unit using[Storage.SetStorage](#storage.setstorage).
        /// </summary>
        Task<XFS4IoT.Storage.Completions.GetStorageCompletion.PayloadData> GetStorage(IGetStorageEvents events, 
                                                                                      XFS4IoT.Storage.Commands.GetStorageCommand.PayloadData payload, 
                                                                                      CancellationToken cancellation);

        /// <summary>
        /// This command is used to adjust information about the configuration and contents of the device's storage units.Only fields that are to be changed need to be set in the payload of this command; values that are not meant to change can be omitted.This command generates the [Storage.StorageChangedEvent](#storage.storagechangedevent) to inform applications that storage unit information has been changed.Only a subset of the information reported by [Storage.GetStorage](#storage.getstorage) may be modified by this command therefore the payload is a subset of the GetStorage output. In addition, if the service supports an exchange state, only a subset of the information which may be modified by this command can be modified unless the service is in an exchange state. The descriptions of each field list which can be modified at any point using this command; any other changes must be performed while in an exchange state.The values set by this command are persistent.
        /// </summary>
        Task<XFS4IoT.Storage.Completions.SetStorageCompletion.PayloadData> SetStorage(ISetStorageEvents events, 
                                                                                      XFS4IoT.Storage.Commands.SetStorageCommand.PayloadData payload, 
                                                                                      CancellationToken cancellation);

        /// <summary>
        /// $ref: ../Docs/StartExchangeDescription.md
        /// </summary>
        Task<XFS4IoT.Storage.Completions.StartExchangeCompletion.PayloadData> StartExchange(IStartExchangeEvents events, 
                                                                                            XFS4IoT.Storage.Commands.StartExchangeCommand.PayloadData payload, 
                                                                                            CancellationToken cancellation);

        /// <summary>
        /// $ref: ../Docs/EndExchangeDescription.md
        /// </summary>
        Task<XFS4IoT.Storage.Completions.EndExchangeCompletion.PayloadData> EndExchange(IEndExchangeEvents events, 
                                                                                        XFS4IoT.Storage.Commands.EndExchangeCommand.PayloadData payload, 
                                                                                        CancellationToken cancellation);

    }
}

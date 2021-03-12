// (C) KAL ATM Software GmbH, 2021

using System.Threading;
using System.Threading.Tasks;

// KAL specific implementation of cardreader. 
namespace XFS4IoTFramework.Common
{
    public interface ICommonDevice
    {
        /// <summary>
        /// This command is used to obtain the overall status of any XFS4IoT service. The status includes common status information and can include zero or more interface specific status objects, depending on the implemented interfaces of the service. It may also return vendor-specific status information.
        /// </summary>
        Task<XFS4IoT.Common.Responses.StatusPayload> Status(ICommonConnection connection, XFS4IoT.Common.Commands.StatusPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command retrieves the capabilities of the device. It may also return vendor specific capability information.
        /// </summary>
        Task<XFS4IoT.Common.Responses.CapabilitiesPayload> Capabilities(ICommonConnection connection, XFS4IoT.Common.Commands.CapabilitiesPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to set the status of the devices guidance lights. This includes defining the flash rate, the color and the direction. When an application tries to use a color or direction that is not supported then the Service Provider will return the generic error WFS\_ERR\_UNSUPP\_DATA.
        /// </summary>
        Task<XFS4IoT.Common.Responses.SetGuidanceLightPayload> SetGuidanceLight(ICommonConnection connection, XFS4IoT.Common.Commands.SetGuidanceLightPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command activates or deactivates the power-saving mode.If the Service Provider receives another execute command while in power saving mode, the Service Provider automatically exits the power saving mode, and executes the requested command. If the Service Provider receives an information command while in power saving mode, the Service Provider will not exit the power saving mode.
        /// </summary>
        Task<XFS4IoT.Common.Responses.PowerSaveControlPayload> PowerSaveControl(ICommonConnection connection, XFS4IoT.Common.Commands.PowerSaveControlPayload payload, CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.Common.Responses.SynchronizeCommandPayload> SynchronizeCommand(ICommonConnection connection, XFS4IoT.Common.Commands.SynchronizeCommandPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command allows the application to specify the transaction state, which the Service Provider can then utilize in order to optimize performance. After receiving this command, this Service Provider can perform the necessary processing to start or end the customer transaction. This command should be called for every Service Provider that could be used in a customer transaction. The transaction state applies to every session.
        /// </summary>
        Task<XFS4IoT.Common.Responses.SetTransactionStatePayload> SetTransactionState(ICommonConnection connection, XFS4IoT.Common.Commands.SetTransactionStatePayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command can be used to get the transaction state.
        /// </summary>
        Task<XFS4IoT.Common.Responses.GetTransactionStatePayload> GetTransactionState(ICommonConnection connection, XFS4IoT.Common.Commands.GetTransactionStatePayload payload, CancellationToken cancellation);

    }
}

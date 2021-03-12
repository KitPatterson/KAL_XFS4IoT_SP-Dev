// (C) KAL ATM Software GmbH, 2021

using TextTerminal;
using System.Threading;
using System.Threading.Tasks;
using XFS4IoTFramework.Common;

// KAL specific implementation of textterminal. 
namespace XFS4IoTFramework.TextTerminal
{
    public interface ITextTerminalDevice : ICommonDevice
    {

        /// <summary>
        /// This command is used to retrieve the  list of forms available on the device.
        /// </summary>
        Task<XFS4IoT.TextTerminal.Responses.GetFormListPayload> GetFormList(ITextTerminalConnection connection, XFS4IoT.TextTerminal.Commands.GetFormListPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to retrieve details of the definition of a specified form.
        /// </summary>
        Task<XFS4IoT.TextTerminal.Responses.GetQueryFormPayload> GetQueryForm(ITextTerminalConnection connection, XFS4IoT.TextTerminal.Commands.GetQueryFormPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to retrieve details of the definition of a single or all fields on a specified form.
        /// </summary>
        Task<XFS4IoT.TextTerminal.Responses.GetQueryFieldPayload> GetQueryField(ITextTerminalConnection connection, XFS4IoT.TextTerminal.Commands.GetQueryFieldPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command returns information about the Keys (buttons) supported by the device.This command should be issued to determine which Keys are available.
        /// </summary>
        Task<XFS4IoT.TextTerminal.Responses.GetKeyDetailPayload> GetKeyDetail(ITextTerminalConnection connection, XFS4IoT.TextTerminal.Commands.GetKeyDetailPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to beep at the text terminal unit.
        /// </summary>
        Task<XFS4IoT.TextTerminal.Responses.BeepPayload> Beep(ITextTerminalConnection connection, XFS4IoT.TextTerminal.Commands.BeepPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command clears the specified area of the text terminal unit screen.The cursor is positioned to the upper left corner of the cleared area.
        /// </summary>
        Task<XFS4IoT.TextTerminal.Responses.ClearScreenPayload> ClearScreen(ITextTerminalConnection connection, XFS4IoT.TextTerminal.Commands.ClearScreenPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to switch the lighting of the text terminal unit on or off.
        /// </summary>
        Task<XFS4IoT.TextTerminal.Responses.DispLightPayload> DispLight(ITextTerminalConnection connection, XFS4IoT.TextTerminal.Commands.DispLightPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to set the status of the LEDs.
        /// </summary>
        Task<XFS4IoT.TextTerminal.Responses.SetLedPayload> SetLed(ITextTerminalConnection connection, XFS4IoT.TextTerminal.Commands.SetLedPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to set the resolution of the display.The screen is cleared and the cursor is positioned at the upper left position.
        /// </summary>
        Task<XFS4IoT.TextTerminal.Responses.SetResolutionPayload> SetResolution(ITextTerminalConnection connection, XFS4IoT.TextTerminal.Commands.SetResolutionPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to display a form by merging the supplied variable field data with the defined form and field data specified in the form.
        /// </summary>
        Task<XFS4IoT.TextTerminal.Responses.WriteFormPayload> WriteForm(ITextTerminalConnection connection, XFS4IoT.TextTerminal.Commands.WriteFormPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to read data from input fields on the specified form.
        /// </summary>
        Task<XFS4IoT.TextTerminal.Responses.ReadFormPayload> ReadForm(ITextTerminalConnection connection, XFS4IoT.TextTerminal.Commands.ReadFormPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command displays the specified text on the display of the text terminal unit. The specified text may include the control characters CR (Carriage Return) and LF (Line Feed). The control characters can be included in the text as CR, or LF, or CR LF, or LF CR and all combinations will perform the function of relocating the cursor position to the left hand side of the display on the next line down. If the text will overwrite the display area then the display will scroll.
        /// </summary>
        Task<XFS4IoT.TextTerminal.Responses.WritePayload> Write(ITextTerminalConnection connection, XFS4IoT.TextTerminal.Commands.WritePayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command activates the keyboard of the text terminal unit for input of the specified number of characters. Depending on the specified flush mode the input buffer is cleared.During this command, pressing an active key results in a Key event containing the key details. On completion of the command (when the maximum number of keys have been pressed or a terminator key is pressed), the entered string, as interpreted by the Service Provider, is returned. The Service Provider takes command keys into account when interpreting the data.
        /// </summary>
        Task<XFS4IoT.TextTerminal.Responses.ReadPayload> Read(ITextTerminalConnection connection, XFS4IoT.TextTerminal.Commands.ReadPayload payload, CancellationToken cancellation);

        /// <summary>
        /// Sends a service reset to the Service Provider. This command clears the screen, clears the keyboard buffer, sets the default resolution and sets the cursor position to the upper left.
        /// </summary>
        Task<XFS4IoT.TextTerminal.Responses.ResetPayload> Reset(ITextTerminalConnection connection, XFS4IoT.TextTerminal.Commands.ResetPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command defines the keys that will be active during the next ReadForm command.The configured set will be active until the next ReadForm command ends, at which point the default values are restored.
        /// </summary>
        Task<XFS4IoT.TextTerminal.Responses.DefineKeysPayload> DefineKeys(ITextTerminalConnection connection, XFS4IoT.TextTerminal.Commands.DefineKeysPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to obtain the overall status of any XFS4IoT service. The status includes common status information and can include zero or more interface specific status objects, depending on the implemented interfaces of the service. It may also return vendor-specific status information.
        /// </summary>
        Task<XFS4IoT.Common.Responses.StatusPayload> Status(ITextTerminalConnection connection, XFS4IoT.Common.Commands.StatusPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command retrieves the capabilities of the device. It may also return vendor specific capability information.
        /// </summary>
        Task<XFS4IoT.Common.Responses.CapabilitiesPayload> Capabilities(ITextTerminalConnection connection, XFS4IoT.Common.Commands.CapabilitiesPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to set the status of the devices guidance lights. This includes defining the flash rate, the color and the direction. When an application tries to use a color or direction that is not supported then the Service Provider will return the generic error WFS\_ERR\_UNSUPP\_DATA.
        /// </summary>
        Task<XFS4IoT.Common.Responses.SetGuidanceLightPayload> SetGuidanceLight(ITextTerminalConnection connection, XFS4IoT.Common.Commands.SetGuidanceLightPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command activates or deactivates the power-saving mode.If the Service Provider receives another execute command while in power saving mode, the Service Provider automatically exits the power saving mode, and executes the requested command. If the Service Provider receives an information command while in power saving mode, the Service Provider will not exit the power saving mode.
        /// </summary>
        Task<XFS4IoT.Common.Responses.PowerSaveControlPayload> PowerSaveControl(ITextTerminalConnection connection, XFS4IoT.Common.Commands.PowerSaveControlPayload payload, CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.Common.Responses.SynchronizeCommandPayload> SynchronizeCommand(ITextTerminalConnection connection, XFS4IoT.Common.Commands.SynchronizeCommandPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command allows the application to specify the transaction state, which the Service Provider can then utilize in order to optimize performance. After receiving this command, this Service Provider can perform the necessary processing to start or end the customer transaction. This command should be called for every Service Provider that could be used in a customer transaction. The transaction state applies to every session.
        /// </summary>
        Task<XFS4IoT.Common.Responses.SetTransactionStatePayload> SetTransactionState(ITextTerminalConnection connection, XFS4IoT.Common.Commands.SetTransactionStatePayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command can be used to get the transaction state.
        /// </summary>
        Task<XFS4IoT.Common.Responses.GetTransactionStatePayload> GetTransactionState(ITextTerminalConnection connection, XFS4IoT.Common.Commands.GetTransactionStatePayload payload, CancellationToken cancellation);

    }
}

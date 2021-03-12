// (C) KAL ATM Software GmbH, 2021

using Printer;
using System.Threading;
using System.Threading.Tasks;
using XFS4IoTFramework.Common;

// KAL specific implementation of printer. 
namespace XFS4IoTFramework.Printer
{
    public interface IPrinterDevice : ICommonDevice
    {

        /// <summary>
        /// This command is used to retrieve the list of forms available on the device.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.GetFormListPayload> GetFormList(IPrinterConnection connection, XFS4IoT.Printer.Commands.GetFormListPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to retrieve the list of media definitions available on the device.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.GetMediaListPayload> GetMediaList(IPrinterConnection connection, XFS4IoT.Printer.Commands.GetMediaListPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to retrieve details of the definition of a specified form.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.GetQueryFormPayload> GetQueryForm(IPrinterConnection connection, XFS4IoT.Printer.Commands.GetQueryFormPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to retrieve details of the definition of a specified media.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.GetQueryMediaPayload> GetQueryMedia(IPrinterConnection connection, XFS4IoT.Printer.Commands.GetQueryMediaPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to retrieve details of the definition of a single or all fields on a specified form.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.GetQueryFieldPayload> GetQueryField(IPrinterConnection connection, XFS4IoT.Printer.Commands.GetQueryFieldPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to retrieve the byte code mapping for the special banking symbols defined for image processing (e.g. check processing). This mapping must be reported as there is no standard for the fonts defined below.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.GetCodelineMappingPayload> GetCodelineMapping(IPrinterConnection connection, XFS4IoT.Printer.Commands.GetCodelineMappingPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to control media.If an eject operation is specified, it completes when the media is moved to the exit slot. A service event is generated when the media has been taken by the user (only if the [mediaTaken](#printer-capability-mediaTaken) capability is true.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.ControlMediaPayload> ControlMedia(IPrinterConnection connection, XFS4IoT.Printer.Commands.ControlMediaPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to print a form by merging the supplied variable field data with the defined form and field data specified in the form. If no media is present, the device waits, for the period of time specified by the *timeout* parameter, for media to be inserted from the external paper source.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.PrintFormPayload> PrintForm(IPrinterConnection connection, XFS4IoT.Printer.Commands.PrintFormPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to read data from input fields on the specified form. These input fields may consist of MICR, OCR, MSF, BARCODE, or PAGEMARK input fields. These input fields may also consist of TEXT fields for purposes of detecting available passbook print lines with passbook printers supporting such capability. If no media is present, the device waits, for the timeout specified, for media to be inserted.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.ReadFormPayload> ReadForm(IPrinterConnection connection, XFS4IoT.Printer.Commands.ReadFormPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to send raw data (a byte string of device dependent data) to the physical device.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.RawDataPayload> RawData(IPrinterConnection connection, XFS4IoT.Printer.Commands.RawDataPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to get the extents of the media inserted in the physical device. The input parameter specifies the base unit and fractions in which the media extent values will be returned. If no media is present, the device waits, for the timeout specified, for media to be inserted.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.MediaExtentsPayload> MediaExtents(IPrinterConnection connection, XFS4IoT.Printer.Commands.MediaExtentsPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This function resets the present value for number of media items retracted to zero. The function is possible only for printers with retract capability.The number of media items retracted is controlled by the service and can be requested before resetting using the info Printer.Status command.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.ResetCountPayload> ResetCount(IPrinterConnection connection, XFS4IoT.Printer.Commands.ResetCountPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This function returns image data from the current media. If no media is present, the device waits for the timeout specified, for media to be inserted.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.ReadImagePayload> ReadImage(IPrinterConnection connection, XFS4IoT.Printer.Commands.ReadImagePayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used by the application to perform a hardware reset which will attempt to return the device to a known good state.The device will attempt to retract or eject any items found anywhere within the device. This may not always be possible because of hardware problems. The [Printer.MediaDetectedEvent](#message-Printer.MediaDetectedEvent) event will inform the application where items were actually moved to.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.ResetPayload> Reset(IPrinterConnection connection, XFS4IoT.Printer.Commands.ResetPayload payload, CancellationToken cancellation);

        /// <summary>
        /// The media is removed from its present position (media inserted into device, media entering, unknown position) and stored in one of the retract bins. An event is sent if the storage capacity of the specified retract bin is reached. If the bin is already full and the command cannot be executed, an error is returned and the media remains in its present position.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.RetractMediaPayload> RetractMedia(IPrinterConnection connection, XFS4IoT.Printer.Commands.RetractMediaPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to move paper (which can also be a new passbook) from a paper source into the print position.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.DispensePaperPayload> DispensePaper(IPrinterConnection connection, XFS4IoT.Printer.Commands.DispensePaperPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to print a file that contains a complete print job in the native printer language. The creation and content of this file are both Operating System and printer specific and outwith the scope of this specification.If no media is present, the device waits, for the timeout specified, for media to be inserted from the external paper source.This command must not complete until all pages have been presented to the customer.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.PrintRawFilePayload> PrintRawFile(IPrinterConnection connection, XFS4IoT.Printer.Commands.PrintRawFilePayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to load a form (including sub-forms and frames) or media definition into the list of available forms. Once a form or media definition has been loaded through this command it can be used by any of the other form/media definition processing commands. Forms and media definitions loaded through this command are persistent. When a form or media definition is loaded a [Printer.DefinitionLoadedEvent](#message-Printer.DefinitionLoadedEvent) event is generated to inform applications that a form or media definition has been added or replaced.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.LoadDefinitionPayload> LoadDefinition(IPrinterConnection connection, XFS4IoT.Printer.Commands.LoadDefinitionPayload payload, CancellationToken cancellation);

        /// <summary>
        /// After the supplies have been replenished, this command is used to indicate that one or more supplies have been replenished and are expected to be in a healthy state.Hardware that cannot detect the level of a supply and reports on the supply's status using metrics (or some other means), must assume the supply has been fully replenished after this command is issued. The appropriate threshold event must be broadcast.Hardware that can detect the level of a supply must update its status based on its sensors, generate a threshold event if appropriate, and succeed the command even if the supply has not been replenished. If it has already detected the level and reported the threshold before this command was issued, the command must succeed and no threshold event is required.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.SupplyReplenishPayload> SupplyReplenish(IPrinterConnection connection, XFS4IoT.Printer.Commands.SupplyReplenishPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command can turn the pages of a passbook inserted in the printer by a specified number of pages in a specified direction and it can close the passbook. The [controlPassbook](#printer-capability-controlpassbook) field returned by [Common.Capabilities](#command-Common.Capabilities) specifies which functionality is supported. This command flushes the data before the pages are turned or the passbook is closed.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.ControlPassbookPayload> ControlPassbook(IPrinterConnection connection, XFS4IoT.Printer.Commands.ControlPassbookPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command switches the black mark detection mode and associated functionality on or off. The black mark detection mode is persistent. If the selected mode is already active this command will complete with success.
        /// </summary>
        Task<XFS4IoT.Printer.Responses.SetBlackMarkModePayload> SetBlackMarkMode(IPrinterConnection connection, XFS4IoT.Printer.Commands.SetBlackMarkModePayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to obtain the overall status of any XFS4IoT service. The status includes common status information and can include zero or more interface specific status objects, depending on the implemented interfaces of the service. It may also return vendor-specific status information.
        /// </summary>
        Task<XFS4IoT.Common.Responses.StatusPayload> Status(IPrinterConnection connection, XFS4IoT.Common.Commands.StatusPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command retrieves the capabilities of the device. It may also return vendor specific capability information.
        /// </summary>
        Task<XFS4IoT.Common.Responses.CapabilitiesPayload> Capabilities(IPrinterConnection connection, XFS4IoT.Common.Commands.CapabilitiesPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command is used to set the status of the devices guidance lights. This includes defining the flash rate, the color and the direction. When an application tries to use a color or direction that is not supported then the Service Provider will return the generic error WFS\_ERR\_UNSUPP\_DATA.
        /// </summary>
        Task<XFS4IoT.Common.Responses.SetGuidanceLightPayload> SetGuidanceLight(IPrinterConnection connection, XFS4IoT.Common.Commands.SetGuidanceLightPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command activates or deactivates the power-saving mode.If the Service Provider receives another execute command while in power saving mode, the Service Provider automatically exits the power saving mode, and executes the requested command. If the Service Provider receives an information command while in power saving mode, the Service Provider will not exit the power saving mode.
        /// </summary>
        Task<XFS4IoT.Common.Responses.PowerSaveControlPayload> PowerSaveControl(IPrinterConnection connection, XFS4IoT.Common.Commands.PowerSaveControlPayload payload, CancellationToken cancellation);

        /// <summary>
        /// {}
        /// </summary>
        Task<XFS4IoT.Common.Responses.SynchronizeCommandPayload> SynchronizeCommand(IPrinterConnection connection, XFS4IoT.Common.Commands.SynchronizeCommandPayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command allows the application to specify the transaction state, which the Service Provider can then utilize in order to optimize performance. After receiving this command, this Service Provider can perform the necessary processing to start or end the customer transaction. This command should be called for every Service Provider that could be used in a customer transaction. The transaction state applies to every session.
        /// </summary>
        Task<XFS4IoT.Common.Responses.SetTransactionStatePayload> SetTransactionState(IPrinterConnection connection, XFS4IoT.Common.Commands.SetTransactionStatePayload payload, CancellationToken cancellation);

        /// <summary>
        /// This command can be used to get the transaction state.
        /// </summary>
        Task<XFS4IoT.Common.Responses.GetTransactionStatePayload> GetTransactionState(IPrinterConnection connection, XFS4IoT.Common.Commands.GetTransactionStatePayload payload, CancellationToken cancellation);

    }
}

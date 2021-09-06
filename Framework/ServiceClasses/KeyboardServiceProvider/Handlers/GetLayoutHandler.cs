/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.Keyboard.Commands;
using XFS4IoT.Keyboard.Completions;
using XFS4IoT.Completions;
using XFS4IoT.Keyboard;

namespace XFS4IoTFramework.Keyboard
{
    public partial class GetLayoutHandler
    {
        private Task<GetLayoutCompletion.PayloadData> HandleGetLayout(IGetLayoutEvents events, GetLayoutCommand getLayout, CancellationToken cancel)
        {
            if (Keyboard.FirstGetLayoutCommand)
            {
                Logger.Log(Constants.DeviceClass, "KeyboardDev.GetLayoutInfo()");

                Keyboard.KeyboardLayouts = Device.GetLayoutInfo();

                Logger.Log(Constants.DeviceClass, "KeyboardDev.GetLayoutInfo()->");

                Keyboard.FirstGetLayoutCommand = false;

                // Update internal variables
                Keyboard.SupportedFunctionKeys.Clear();
                Keyboard.SupportedFunctionKeysWithShift.Clear();

                foreach (var entryType in Keyboard.KeyboardLayouts)
                {
                    List<string> fks = null;
                    List<string> shiftFks = null;

                    foreach (var frame in entryType.Value)
                    {
                        foreach (var key in frame.FunctionKeys)
                        {
                            if (!string.IsNullOrEmpty(key.FK))
                                fks.Add(key.FK);
                            if (!string.IsNullOrEmpty(key.ShiftFK))
                                fks.Add(key.ShiftFK);
                        }
                    }

                    if (fks.Count != 0)
                        Keyboard.SupportedFunctionKeys.Add(entryType.Key, fks);
                    if (shiftFks.Count != 0)
                        Keyboard.SupportedFunctionKeysWithShift.Add(entryType.Key, shiftFks);
                }
            }

            if (Keyboard.KeyboardLayouts is null)
            {
                // nothing to report, not keys for the keyboard
                Task.FromResult(new GetLayoutCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success, string.Empty));
            }
         
            EntryModeEnum? inquiry = null;
            if (getLayout.Payload.EntryMode is not null)
            {
                inquiry = getLayout.Payload.EntryMode switch
                {
                    GetLayoutCommand.PayloadData.EntryModeEnum.Data => EntryModeEnum.Data,
                    GetLayoutCommand.PayloadData.EntryModeEnum.Pin => EntryModeEnum.Pin,
                    _ => EntryModeEnum.Secure,
                };

                if (!Keyboard.KeyboardLayouts.ContainsKey((EntryModeEnum)inquiry))
                {
                    Task.FromResult(new GetLayoutCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode, 
                                                                        $"Specified mode is not supported by the device. {getLayout.Payload.EntryMode}",
                                                                        GetLayoutCompletion.PayloadData.ErrorCodeEnum.ModeNotSupported));
                }
            }

            List<LayoutClass> layoutResult = new();

            foreach (var entryType in Keyboard.KeyboardLayouts)
            {
                LayoutClass.EntryModeEnum mode = entryType.Key switch
                {
                    EntryModeEnum.Data => LayoutClass.EntryModeEnum.Data,
                    EntryModeEnum.Pin => LayoutClass.EntryModeEnum.Pin,
                    _ => LayoutClass.EntryModeEnum.Secure,
                };

                List<LayoutClass.FramesClass> resultFrames = new();
                foreach (var frame in entryType.Value)
                {
                    List<LayoutClass.FramesClass.FksClass> functionKeys = new();
                    foreach (var functionKey in frame.FunctionKeys)
                    {
                        functionKeys.Add(new LayoutClass.FramesClass.FksClass(functionKey.XPos,
                                                                              functionKey.YPos,
                                                                              functionKey.XSize,
                                                                              functionKey.YSize,
                                                                              functionKey.KeyType switch
                                                                              {
                                                                                  FrameClass.FunctionKeyClass.KeyTypeEnum.FK => LayoutClass.FramesClass.FksClass.KeyTypeEnum.Fk,
                                                                                  _ => LayoutClass.FramesClass.FksClass.KeyTypeEnum.Fdk
                                                                              },
                                                                              functionKey.FK,
                                                                              functionKey.ShiftFK));
                    }

                    resultFrames.Add(new(frame.XPos, 
                                         frame.YPos, 
                                         frame.XSize, 
                                         frame.YSize,
                                         frame.FloatAction != FrameClass.FloatActionEnum.NotSupported ? new LayoutClass.FramesClass.FloatActionClass(frame.FloatAction.HasFlag(FrameClass.FloatActionEnum.FloatX), frame.FloatAction.HasFlag(FrameClass.FloatActionEnum.FloatY)) : null,
                                         functionKeys));
                }

                if (inquiry is null ||
                    inquiry == entryType.Key)
                {
                    layoutResult.Add(new LayoutClass(mode, resultFrames));

                    if (inquiry is not null)
                        break;
                }
            }
            
            return Task.FromResult(new GetLayoutCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success,
                                                                       null, 
                                                                       Layout: layoutResult));
        }
    }
}

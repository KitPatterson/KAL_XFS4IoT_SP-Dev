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
using XFS4IoT.Keyboard;
using XFS4IoT.Keyboard.Commands;
using XFS4IoT.Keyboard.Completions;
using XFS4IoT.Completions;

namespace XFS4IoTFramework.Keyboard
{
    public partial class DefineLayoutHandler
    {
        private async Task<DefineLayoutCompletion.PayloadData> HandleDefineLayout(IDefineLayoutEvents events, DefineLayoutCommand defineLayout, CancellationToken cancel)
        {
            if (defineLayout.Payload.Layout is null)
            {
                return new DefineLayoutCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                              $"No key layout data specified.");
            }

            if (defineLayout.Payload.Layout is null)
            {
                return new DefineLayoutCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                              $"No key layout data specified.");
            }

            Dictionary<EntryModeEnum, List<FrameClass>> request = new();

            foreach (var entryMode in defineLayout.Payload.Layout)
            {
                if (entryMode.EntryMode is null)
                {
                    return new DefineLayoutCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                  $"No key mode specified.");
                }

                if (entryMode.Frames is null ||
                    entryMode.Frames.Count == 0)
                {
                    return new DefineLayoutCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                  $"No frames specified.");
                }

                List<FrameClass> frames = new();
                foreach (var frame in entryMode.Frames)
                {
                    FrameClass.FloatActionEnum action = FrameClass.FloatActionEnum.NotSupported;
                    if (frame.FloatAction is not null)
                    {
                        if (frame.FloatAction.FloatX is not null && (bool)frame.FloatAction.FloatX)
                            action |= FrameClass.FloatActionEnum.FloatX;
                        if (frame.FloatAction.FloatY is not null && (bool)frame.FloatAction.FloatY)
                            action |= FrameClass.FloatActionEnum.FloatY;
                    }

                    if (frame.XPos is null ||
                        frame.YPos is null ||
                        frame.XSize is null ||
                        frame.YSize is null)
                    {
                        return new DefineLayoutCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                      $"XPos, YPos, XSize, YSize are not specified in the frame. {entryMode.EntryMode}");
                    }

                    if (frame.Fks is null ||
                        frame.Fks.Count == 0)
                    {
                        return new DefineLayoutCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                      $"No function keys are specified in the frame. {entryMode.EntryMode}");
                    }

                    List<FrameClass.FunctionKeyClass> functionKeys = new();
                    foreach (var key in frame.Fks)
                    {
                        if (key.XPos is null ||
                            key.YPos is null ||
                            key.XSize is null ||
                            key.YSize is null)
                        {
                            return new DefineLayoutCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                          $"XPos, YPos, XSize, YSize are not specified in function keys. {entryMode.EntryMode}");
                        }

                        if (key.KeyType is null)
                        {
                            return new DefineLayoutCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                          $"KeyType is not specified in function keys. {entryMode.EntryMode}");
                        }

                        functionKeys.Add(new FrameClass.FunctionKeyClass((int)key.XPos,
                                                                         (int)key.YPos,
                                                                         (int)key.XSize,
                                                                         (int)key.YSize,
                                                                         key.KeyType switch
                                                                         {
                                                                             LayoutClass.FramesClass.FksClass.KeyTypeEnum.Fk => FrameClass.FunctionKeyClass.KeyTypeEnum.FK,
                                                                             _ => FrameClass.FunctionKeyClass.KeyTypeEnum.FDK
                                                                         },
                                                                         !string.IsNullOrEmpty(key.Fk) && FrameClass.FunctionKeyClass.StringFunctionKeyTypeMap.ContainsKey(key.Fk) ? FrameClass.FunctionKeyClass.StringFunctionKeyTypeMap[key.Fk] : FrameClass.FunctionKeyClass.FunctionKeyTypeEnum.unused,
                                                                         !string.IsNullOrEmpty(key.ShiftFK) && FrameClass.FunctionKeyClass.StringFunctionKeyTypeMap.ContainsKey(key.ShiftFK) ? FrameClass.FunctionKeyClass.StringFunctionKeyTypeMap[key.ShiftFK] : FrameClass.FunctionKeyClass.FunctionKeyTypeEnum.unused));
                    }

                    frames.Add(new FrameClass((int)frame.XPos, 
                                              (int)frame.YPos, 
                                              (int)frame.XSize, 
                                              (int)frame.YSize, 
                                              action, 
                                              functionKeys));
                }

                request.Add(entryMode.EntryMode switch
                            {
                                LayoutClass.EntryModeEnum.Data => EntryModeEnum.Data,
                                LayoutClass.EntryModeEnum.Pin => EntryModeEnum.Pin,
                                _ => EntryModeEnum.Secure,
                            },
                            frames);
            }

            Logger.Log(Constants.DeviceClass, "KeyboardDev.DefineLayout()");

            var result = await Device.DefineLayout(request, cancel);

            Logger.Log(Constants.DeviceClass, $"KeyboardDev.DefineLayout() -> {result.CompletionCode}, {result.ErrorCode}");

            if (result.CompletionCode == MessagePayload.CompletionCodeEnum.Success)
            {
                // Update internal layout

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
                            if (key.FK != FrameClass.FunctionKeyClass.FunctionKeyTypeEnum.unused)
                            {
                                fks.Add(key.FK.ToString());
                            }

                            if (key.ShiftFK != FrameClass.FunctionKeyClass.FunctionKeyTypeEnum.unused)
                            {
                                fks.Add(key.ShiftFK.ToString());
                            }
                        }
                    }

                    if (fks.Count != 0)
                        Keyboard.SupportedFunctionKeys.Add(entryType.Key, fks);
                    if (shiftFks.Count != 0)
                        Keyboard.SupportedFunctionKeysWithShift.Add(entryType.Key, shiftFks);
                }
            }
            
            return new DefineLayoutCompletion.PayloadData(result.CompletionCode,
                                                          result.ErrorDescription,
                                                          result.ErrorCode);
        }
    }
}

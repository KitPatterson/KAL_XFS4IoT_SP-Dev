﻿/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using XFS4IoTFramework.Keyboard;
using XFS4IoTFramework.Common;

namespace XFS4IoTServer
{
    public interface IKeyboardService : ICommonService
    {
    }

    public interface IKeyboardServiceClass : IKeyboardService, IKeyboardUnsolicitedEvents
    {
    }
}

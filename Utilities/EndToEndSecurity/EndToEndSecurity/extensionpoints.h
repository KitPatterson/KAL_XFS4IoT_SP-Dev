/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/
#pragma once

// This file defines the functions that must be implemented to complete the end to end security 
// support. These will be implemented for each hardware device. 


/// <summary>
/// A programatic error has occured
/// </summary>
/// <details> 
/// Something unexpected and seriously wrong has happened. The system is likely to be unstable and 
/// may not continue to run. This could include errors up to and including memory corruption. 
/// </details> 
/// <param name="Message">A text message explaining what has happened</param>
extern void FatalError(char const* const Message);

/// <summary>
/// Log a text message for later debugging.
/// </summary>
/// <param name="Message">Text message for debugging</param>
extern void Log(char const* const Message);


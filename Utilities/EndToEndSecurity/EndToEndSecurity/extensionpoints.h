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
/// <remarks> 
/// Something unexpected and seriously wrong has happened. The system is likely to be unstable and 
/// may not continue to run. This could include errors up to and including memory corruption. 
/// </remarks> 
/// <param name="Message">A text message explaining what has happened</param>
extern void FatalError(char const* const Message);

/// <summary>
/// Log a text message for later debugging.
/// </summary>
/// <param name="Message">Text message for debugging</param>
extern void Log(char const* const Message);

/// <summary>
/// Create a new Nonce value. 
/// </summary>
/// <remarks>
/// The firmware must create and track one nonce for use in command tokens. 
/// Note that there is no special handling needed for the responce token nonce - that's handled
/// by the host. 
/// The current nonce does not need to be tracked across power-cycles. A power cycle should clear 
/// the current nonce. However, the new nonce after the powercycle must be different to the previous 
/// values. 
/// Must return a pointer to a null terminated string containing a new nonce value. 
/// The nonce must be different to any proceeding nonce values, including across power-cycles. 
/// This could be done by persistently tracking an integer value and incrementing it. 
/// Alternatively it can be done by creating a long random number, using a hardware 
/// random number generator. 
/// The returned nonce pointer should remain valid for as long as the nonce is valid - 
/// i.e. until the nonce is cleared (or the machine is restarted.) 
/// </remarks>
/// <param name="Nonce">Output parameter pointing to the null terminated nonce string</param>
extern void NewNonce( char const ** Nonce );

// Compare given nonce string to the current stored nonce value. 
// input nonce is _not_ null terminated
extern bool CompareNonce(char const* const CommandNonce, unsigned int NonceLength);

// clear the current nonce value. 
extern void ClearNonce();

// TokenHMAC is a 32 byte buffer. 
extern bool CheckHMAC(char const *const Token, unsigned int TokenLength, char const* const TokenHMAC);

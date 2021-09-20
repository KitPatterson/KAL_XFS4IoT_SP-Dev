/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

#include "pch.h"
#include "framework.h"
#include "tokens.h"
#include "extensionpoints.h"

// Utility functions
char LogVBuffer[4096];
void LogV(char const *const Message, ...)
{
    va_list vl;
    va_start(vl, Message);
    vsprintf_s(LogVBuffer, sizeof(LogVBuffer), Message, vl);
    va_end(vl);

    Log(LogVBuffer);
}

/// <summary>
/// Validate that a token has a valid format.
/// </summary>
/// <description> 
/// Takes a token string and reports if it is valid or not. If the token is valid then it is safe 
/// to proceed with the operation protected by the token. If not, the operation should fail with an 
/// error. 
/// The token still is assumed to be unsafe and may have been passed by an attacker. Everything 
/// possible will be done to avoid invalid behaviour due to a hostile token. 
/// </description>
/// 
/// <param name="Token">Null terminated token string</param>
/// <param name="TokenSize">Token buffer size, including null</param>
/// <returns>true or false</returns>
bool ValidateToken( char const *const Token, size_t TokenSize )
{
    LogV("ValidateToken:");

    // Parameter checking. 
    // Consider the token to be an untrusted value, so maximum validity checking. 
    // Null token
    if (Token == NULL)
    {
        Log("ValidateToken: Null token => false");
        return false;
    }

    LogV("ValidateToken: Token=%s", Token);
   
    unsigned int TokenStringLength = strlen(Token)+1;       // Plus null
    if (TokenStringLength == 1) return false;               // Zero length string.
    if (TokenStringLength != TokenSize) return false;       // Buffer length and string size don't match
    if (TokenStringLength < MinTokenLength) return false;   // Token is too short to be valid
    if (TokenStringLength > MaxTokenLength) return false;   // Token is too long (and might cause buffer overflows.) 

    // Parse
    // Check format
    // Find Nonce
    // Find HMAC
    // Find other keys
    // 
    // Check 
    // Check Token Nonce matches current nonce
    // Check HMAC matches calculated HMAC

    LogV("ValidateToken: => true");
    return true;
}

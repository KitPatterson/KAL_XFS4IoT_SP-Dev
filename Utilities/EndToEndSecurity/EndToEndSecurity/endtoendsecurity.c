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
void LogV(char const* const Message, ...);

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
bool ValidateToken(char const* const Token, size_t TokenSize)
{
    LogV("ValidateToken( Token=\"%.1024s\", TokenSize=%d )", Token, TokenSize);

    // Parameter checking. 
    // Consider the token to be an untrusted value, so maximum validity checking. 
    // Null token
    if (Token == NULL)
    {
        Log("ValidateToken: Null token => false");
        return false;
    }

    unsigned int TokenStringLength = strlen(Token) + 1;       // Plus null
    // Zero length string.
    if (TokenStringLength == 1)
    {
        Log("ValidateToken: Empty token => false");
        return false;
    }
    // Buffer length and string size don't match
    if (TokenStringLength != TokenSize)
    {
        Log("ValidateToken: TokenSize didn't match token length => false");
        return false;
    }
    // Token is too short to be valid
    if (TokenStringLength < MinTokenLength)
    {
        Log("ValidateToken: Token is too short => false");
        return false;
    }
    // Token is too long (and might cause buffer overflows.) 
    if (TokenStringLength > MaxTokenLength)
    {
        Log("ValidateToken: Token is too long. Max length=1024 bytes => false");
        return false;
    }

    // Parse
    // Check format
    bool inKeyName = true; // true for key name, false for value
    int symbolLength = 0; 
    for (char const* offset = Token; offset < Token + TokenStringLength - 1; offset++)
    {
        char thisChar = *offset;
        if (inKeyName)
        {
            if (thisChar == '=')
            { 
                if (symbolLength == 0)
                {
                    Log("ValidateToken: Missing key name");
                    return false; 
                }
                inKeyName = false; 
                symbolLength = 0; 
            }
            else if (!isalnum(thisChar))
            {
                Log("ValidateToken: Invalid character in key name");
                return false;
            }
            else
                symbolLength++;
        }
        else
        {
            if (thisChar == ',')
            {
                if (symbolLength == 0)
                {
                    Log("ValidateToken: Missing value");
                    return false;
                }
                inKeyName = true;
                symbolLength = 0; 
            }
            else if (!isalnum(thisChar))
            {
                Log("ValidateToken: Invalid character in value");
                return false;
            }
            else
                symbolLength++;
        }
    }
    if (inKeyName || symbolLength == 0)
    {
        Log("ValidateToken: Missing value");
        return false; 
    }

    // Find Nonce
    char const *const nonceStrOffset = strstr(Token, NonceStr);
    if (nonceStrOffset != Token)
    {
        Log("ValidateToken: First key must be NONCE => false");
        return false;
    }
    // Find HMAC
    char const* const HMACStrOffset = strstr(Token, HMACSHA256Str);
    if (HMACStrOffset == 0)
    {
        Log("ValidateToken: No HMAC key found => false");
        return false;
    }
    // HMAC must be 64 characters
    int HMACLen = TokenStringLength - (HMACStrOffset - Token);
    if ( HMACLen != 64 + sizeof(HMACSHA256Str) + 1)
    {
        LogV("ValidateToken: HMACSHA256 value is too short. %d bytes, should be 64 => false", (HMACLen-/*HMACSHA256=*/11 -1));
        return false; 
    }
    // Find other keys
    // 
    // Check 
    // Check Token Nonce matches current nonce
    // Check HMAC matches calculated HMAC
    LogV("ValidateToken: => true");
    return true;
}

/// <summary>
/// VArg version of Log()
/// </summary>
char LogVBuffer[2048];
void LogV(char const* const Message, ...)
{
    va_list vl;
    va_start(vl, Message);
    vsprintf_s(LogVBuffer, sizeof(LogVBuffer), Message, vl);
    va_end(vl);

    Log(LogVBuffer);
}


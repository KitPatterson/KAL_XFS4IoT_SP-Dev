/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

#include "pch.h"
#include "framework.h"

// Customisation points
extern void FatalError(char const* const Message);
extern void Log(char const* const Message);

// Token details
char const NonceStr[] = "NONCE";
char const HMACSHA256Str[] = "HMACSHA258";
#define HMACSHA256Len (64U)
char const TokenFormatStr[] = "TOKENFORMAT";
char const TokenLengthStr[] = "TOKENLENGTH";

// Absolute minimum token length, including null. 
// NONCE=1,TOKENFORMAT=1,TOKENLENGTH=0164,HMACSHA256=CB735612FD6141213C2827FB5A6A4F4846D7A7347B15434916FEA6AC16F3D2F2
unsigned int const MinTokenLength = sizeof(NonceStr)-1 + 2 + 1 +          // NONCE=1,
                                    sizeof(TokenFormatStr)-1 + 2 + 1 +    // TOKENFORMAT=1,
                                    sizeof(TokenLengthStr)-1 + 2 + 4 +    // TOKENLENGTH=0164,
                                    sizeof(HMACSHA256Str)-1 + 1 + HMACSHA256Len + //HMACSHA256=1234567890123456789012345678901234567890123456789012345678901234
                                    1;                                    // null terminated

// Max permitted token length, as defined in XFS Spec. (In bytes, plus null)
unsigned int const MaxTokenLength = 1024 + 1; 

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
    // Parameter checking. 
    // Consider the token to be an untrusted value, so maximum validity checking. 
    if (Token == NULL) return false;                                // Null token
    Log(Token);
   
    unsigned int TokenStringLength = strlen(Token)+1;       // Plus null
    if (TokenStringLength == 1) return false;               // Zero length string.
    if (TokenStringLength != TokenSize) return false;       // Buffer length and string size don't match
    if (TokenStringLength < MinTokenLength) return false;   // Token is too short to be valid
    if (TokenStringLength > MaxTokenLength) return false;   // Token is too long (and might cause buffer overflows.) 

    return true;
}

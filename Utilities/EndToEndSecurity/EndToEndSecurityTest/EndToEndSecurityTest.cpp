#include "pch.h"
#include "CppUnitTest.h"
#include "EndToEndSecurity.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

extern "C" void Log(char const* const Message)
{
	Logger::WriteMessage( Message );
};

extern "C" void FatalError(char const* const Message)
{
	using namespace Microsoft::VisualStudio::CppUnitTestFramework;
	Assert::Fail(ToString(Message).c_str());
}

namespace EndToEndSecurityTest
{
	TEST_CLASS(EndToEndSecurityTest)
	{
	public:
		
		TEST_METHOD(ValidateTokenMinLength)
		{
			char const testToken[] = "NONCE=1,TOKENFORMAT=1,TOKENLENGTH=0164,HMACSHA256=CB735612FD6141213C2827FB5A6A4F4846D7A7347B15434916FEA6AC16F3D2F2";
			auto result = ValidateToken( testToken, sizeof(testToken)  );
			Assert::AreEqual(result, true);
		}
		TEST_METHOD(ValidateTokenTooShort)
		{
			// HMAC one character too short. 
			char const testToken[] = "NONCE=1,TOKENFORMAT=1,TOKENLENGTH=0164,HMACSHA256=CB735612FD6141213C2827FB5A6A4F4846D7A7347B15434916FEA6AC16F3D2F";
			auto result = ValidateToken( testToken, sizeof(testToken)  );
			Assert::AreEqual(result, false);
		}
		TEST_METHOD(ValidateTokenEmptyString)
		{
			char const testToken[] = "";
			auto result = ValidateToken( testToken, sizeof(testToken)  );
			Assert::AreEqual(result, false);
		}
		TEST_METHOD(ValidateTokenNull)
		{
			char const *const testToken = nullptr;
			auto result = ValidateToken( testToken, sizeof(testToken)  );
			Assert::AreEqual(result, false);
		}
	};
}

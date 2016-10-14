// CodeSamples.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "UserTypeToString.h"
#include <cstdio>
#include <string.h>
#include <crtdbg.h>


/* Print function, file, and line number when a CRT error occurs.
*/
void _invalid_parameter(const wchar_t * expression,
    const wchar_t * function,
    const wchar_t * file,
    unsigned int line,
    uintptr_t pReserved
    )
{
    wprintf(L"Invalid parameter detected in function %s."
        L" File: %s Line: %d\n", function, file, line);
    wprintf(L"Expression: %s\n", expression);
}

void TestSecureCrtFunctions()
{
    _invalid_parameter_handler oldHandler, newHandler;
    newHandler = _invalid_parameter;
    oldHandler = _set_invalid_parameter_handler(newHandler);
    // Disable the message box for assertions.
    _CrtSetReportMode(_CRT_ASSERT, 0);

    // Experimentation with C-Run time functions
    wchar_t* src = L"this is a string that is too big to copy to the destination";
    wchar_t destination[10] = { 0 };
    try
    {
        auto result = wcscpy_s(destination, 10, src);
        if (result != 0)
        {
            wprintf(L"copy failed!\n");
        }
        wprintf(destination);
    }
    catch (...)
    {
        wprintf(L"copy failed!\n");
    }
}

int main(int argc, wchar_t* argv[])
{
	// Uncomment the code below to test streaming a user defined type to an output stream
	/*UserTypeToString obj("Shawn Fox", 25, 14000000.35);
	std::cout << obj << std::endl;*/

    // Uncomment the below call to test how errors work for CRT run time functions
    //TestSecureCrtFunctions();
	return 0;
}


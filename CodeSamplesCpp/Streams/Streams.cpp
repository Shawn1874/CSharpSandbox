// Streams.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <string>
#include "UserTypeToString.h"

void TestStreamUserType()
{
	UserTypeToString instance("Shawn Williams", 43, 227000);
	std::cout << instance << std::endl;
}

template < class T >
void GetInput(T& value, std::string& message)
{
	while ((std::cout << message)
		&& !(std::cin >> value)) {
		std::cout << "Invalid entry! ";
		std::cin.clear();
		std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
	}
}

int main()
{
	int input = 0;
	GetInput(input, std::string("Enter a number: "));
	std::cout << "You entered: " << input << std::endl;

	double fp = 0;
	GetInput(fp, std::string("Enter a number: "));
	std::cout << "You entered: " << fp << std::endl;

	char character = ' ';
	GetInput(character, std::string("Enter a character: "));
	std::cout << "You entered: " << character << std::endl;
    return 0;
}


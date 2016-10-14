#pragma once

#include <iostream>
#include <string>

class UserTypeToString
{
public:
	UserTypeToString(std::string name, int age, double netWorth);
	~UserTypeToString();
	friend std::ostream& operator<<(std::ostream& os, const UserTypeToString& obj);

	std::string name;
	int age;
	double netWorth;
};

// Must be inline or defined in the .cpp and can't be a member of the class
std::ostream& operator<<(std::ostream& os, const UserTypeToString& obj);


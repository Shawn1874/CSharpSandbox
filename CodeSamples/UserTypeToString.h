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

std::ostream& operator<<(std::ostream& os, const UserTypeToString& obj);
//{
//	// write obj to stream
//	os << "name - " << obj.name.c_str() << "age - " << obj.age << "netWorth - " << obj.netWorth << std::endl;
//	return os;
//}


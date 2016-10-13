#include "stdafx.h"
#include "UserTypeToString.h"

UserTypeToString::UserTypeToString(std::string name, int age, double netWorth) : name(name), age(age), netWorth(netWorth)
{
}


UserTypeToString::~UserTypeToString()
{
}

std::ostream& operator<<(std::ostream& os, const UserTypeToString& obj)
{
	// write obj to stream
	os << "name - " << obj.name.c_str() << " age - " << obj.age << " netWorth - " << obj.netWorth;
	return os;
}

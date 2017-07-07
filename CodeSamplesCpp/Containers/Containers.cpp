// Containers.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <vector>
#include <iostream>

int main(int argc, char* argv[])
{
    std::vector<int> test = { 0, 1, 2, 3 };
    for(auto value : test)
    {
        std::cout << value << std::endl;
    }
    return 0;
}


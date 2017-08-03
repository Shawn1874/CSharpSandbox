#include "stdafx.h"
#include "TestArray.h"
#include <array>
#include <cmath>

TestArray::TestArray()
{
}


TestArray::~TestArray()
{
}

TEST_F(TestArray, TestAccessors)
{
	const unsigned int COUNT = 5;
	std::array<int, COUNT> values = { 0 };
	EXPECT_EQ(values.size(), COUNT);
	EXPECT_FALSE(values.empty());
	for (unsigned int i : values)
	{
		EXPECT_EQ(i, 0);
	}

	int series = 1;
	for (auto current = values.begin(); current != values.end(); ++current)
	{
		*current = std::pow<int, int>(series, 2);
		++series;
	}
	EXPECT_EQ(values.front(), 1);
	EXPECT_EQ(values.back(), 25);
	EXPECT_EQ(values.at(1), 4);
	EXPECT_EQ(values[3], 16);

	std::array<int, COUNT> squares = { 1, 4, 9, 16, 25 };
	EXPECT_EQ(values, squares);

}

TEST_F(TestArray, TestFillandSwap)
{
	const unsigned int COUNT = 5;
	std::array<int, COUNT> values;
	values.fill(1);

	std::array<int, COUNT> numbers;
	numbers.fill(5);

	numbers.swap(values); // must be the same length
	EXPECT_EQ(numbers.front(), 1);
	EXPECT_EQ(values.front(), 5);
}

#include "stdafx.h"
#include "TestRemove.h"


TestRemove::TestRemove()
{
}


TestRemove::~TestRemove()
{
}

TEST_F(TestRemove, RemoveTest)
{
	std::vector<int> values = { 1, 2, 1, 3, 4, 1, 5 };

	// Remove doesn't remove, but erase does. Remove all ones.
	EXPECT_EQ(values.size(), 7);
	auto logicalEnd = std::remove(values.begin(), values.end(), 1);
	EXPECT_EQ(values.size(), 7);

	values.erase(logicalEnd,
		values.end());
	EXPECT_EQ(values.size(), 4);  
	EXPECT_EQ(values.capacity(), 7); // erasing doesn't change capacity
}

TEST_F(TestRemove, RemoveIfTest)
{
	std::vector<int> values = { 1, 2, 1, 3, 4, 1, 5 };

	// Remove doesn't remove, but erase does.  Remove odd numbers
	EXPECT_EQ(values.size(), 7);

	auto logicalEnd = std::remove_if(values.begin(), values.end(), [](int val) { return val % 2; });

	EXPECT_EQ(values.size(), 7);

	values.erase(logicalEnd,
				 values.end());

	EXPECT_EQ(values.size(), 2);
	EXPECT_EQ(values.capacity(), 7); // erasing doesn't change capacity
	EXPECT_EQ(values[0], 2);
	EXPECT_EQ(values[1], 4);
}
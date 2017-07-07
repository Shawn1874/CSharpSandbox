#include "stdafx.h"
#include "TestVector.h"


TestVector::TestVector()
{
}


TestVector::~TestVector()
{
}

TEST_F(TestVector, VectorReserveMemoryTest)
{
	std::vector<int> values;
	std::vector<int>::size_type count = 5;
	values.reserve(count);

	EXPECT_EQ(count, values.capacity());
	EXPECT_TRUE(values.empty());
	EXPECT_EQ(0, values.size());

	// Capacity remains == count until more than count numbers are added
	values.push_back(0);
	EXPECT_EQ(count, values.capacity());
	EXPECT_EQ(1, values.size());

	for (int i = 1; i < 6; ++i)
	{
		values.push_back(i);
	}

	EXPECT_GT(values.capacity(), (count));
}

TEST_F(TestVector, VectorAssignTest)
{
	std::vector<int> values;
	EXPECT_TRUE(values.empty());

	values.assign({ 1, 2 });
	EXPECT_FALSE(values.empty());
	EXPECT_EQ(2, values.size());

	values.assign(3, 0);
	EXPECT_EQ(3, values.size());
	EXPECT_EQ(values[0], 0);
	EXPECT_EQ(values[1], 0);
	EXPECT_EQ(values[2], 0);

	const unsigned int count = 4;
	int valuesArray[count] = { 10, 20, 30, 40 };
	values.assign(valuesArray, valuesArray + count);
	EXPECT_EQ(count, values.size());
	EXPECT_EQ(values[0], 10);
	EXPECT_EQ(values[3], 40);


}

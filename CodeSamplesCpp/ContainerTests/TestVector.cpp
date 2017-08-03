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

TEST_F(TestVector, ExceptionsTest)
{
	std::vector<int> values = { 1, 2, 3, 4, 5 };
	EXPECT_THROW(values.at(5), std::out_of_range);
	EXPECT_NO_THROW(values.at(4));
}

TEST_F(TestVector, Iterators)
{
	std::vector<std::string> words = { "the", "cow", "jumped", "over", "the", "moon" };
	EXPECT_EQ("the", *(words.begin()));
	EXPECT_EQ("moon", *(words.end() - 1));
	EXPECT_EQ("the", *(words.rend() - 1));
	EXPECT_EQ("moon", *(words.rbegin()));

	std::string expected = "the cow jumped over the moon";
	std::string actual;
	for (auto str : words)
	{
		actual += str;
		actual += " ";
	}
	actual.erase(actual.end() - 1);
	EXPECT_EQ(actual, expected);
}

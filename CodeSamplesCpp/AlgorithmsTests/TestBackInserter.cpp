#include "stdafx.h"
#include "TestBackInserter.h"
#include <vector>
#include <iterator>

TestBackInserter::TestBackInserter()
{
}


TestBackInserter::~TestBackInserter()
{
}

TEST_F(TestBackInserter, AppendTest)
{
  std::vector <int> values = { 1, 2, 3 };
  auto backInserter = std::back_inserter(values);

  *backInserter = 4;

  EXPECT_EQ(values.size(), 4);
  EXPECT_EQ(values.at(3), 4);

  *backInserter = 5;

  EXPECT_EQ(values.size(), 5);
  EXPECT_EQ(values.at(4), 5);

}

TEST_F(TestBackInserter, MergeTest)
{
  std::vector <int> values = { 1, 2, 3 };
  std::vector <int> moreValues = { 5, 8, 13 };

  auto backInserter = std::back_inserter(values);
  std::copy(moreValues.begin(), moreValues.end(), backInserter);

  EXPECT_EQ(values.size(), 6);
  EXPECT_EQ(values.at(3), 5);
  EXPECT_EQ(values.at(4), 8);
  EXPECT_EQ(values.at(5), 13);
}



TEST_F(TestBackInserter, LoopTest)
{
  std::vector <int> values = { 1, 2, 3 };

  auto backInserter = std::back_inserter(values);

  EXPECT_EQ(values.size(), 3);

  for(int i = 0, j = 1; i < 5; ++i, ++j)
  {
    *backInserter = values[j] + values[j + 1];
  }

  EXPECT_EQ(values.size(), 8);
  EXPECT_EQ(values.at(3), 5);
  EXPECT_EQ(values.at(4), 8);
  EXPECT_EQ(values.at(5), 13);
  EXPECT_EQ(values.at(6), 21);
  EXPECT_EQ(values.at(7), 34);
}

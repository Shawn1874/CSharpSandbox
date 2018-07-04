#include "stdafx.h"
#include "TestCustomAlgorithms.h"
#include "Algorithms.h"
#include <vector>
#include <deque>

TestCustomAlgorithms::TestCustomAlgorithms()
{
}


TestCustomAlgorithms::~TestCustomAlgorithms()
{
}

TEST_F(TestCustomAlgorithms, TestReverseString)
{
  std::string name = "nwahs";
  std::string result = "shawn";

  Algorithms::Reverse(name.begin(), name.end());

  EXPECT_EQ(name, result);

}

TEST_F(TestCustomAlgorithms, TestReverseVector)
{
  std::vector<int> original = { 9, 7, 5, 3, 1 };
  std::vector<int> expected = { 1, 3, 5, 7, 9 };

  Algorithms::Reverse(original.begin(), original.end());

  EXPECT_EQ(original, expected);

}

TEST_F(TestCustomAlgorithms, TestMergeSortedRange)
{
  std::vector<int> a = { 0, 1, 7, 11, 0, 0, 0, 0};
  std::vector<int> b = { 2, 3, 9, 12 };

  Algorithms::MergeSortedRanges(std::begin(b), std::end(b), std::begin(a), std::begin(a) + 4);
  EXPECT_EQ(a[2], 2);
  EXPECT_EQ(a[3], 3);
  EXPECT_EQ(a[5], 9);
  EXPECT_EQ(a[7], 12);
  EXPECT_EQ(a.size(), 8);


  std::deque<int> c = { 1, 1, 7, 11, 0, 0, 0, 0 };
  std::deque<int> d = { 2, 3, 9, 12 };

  Algorithms::MergeSortedRanges(d.begin(), d.end(), c.begin(), c.end() + 4);
  EXPECT_EQ(c[0], 1);
  EXPECT_EQ(c[1], 1);
  EXPECT_EQ(c[2], 2);
  EXPECT_EQ(c[3], 3);
  EXPECT_EQ(c[4], 7);
  EXPECT_EQ(c[5], 9);
  EXPECT_EQ(c[6], 11);
  EXPECT_EQ(c[7], 12);
  EXPECT_EQ(c.size(), 8);

  EXPECT_EQ(d[0], 2);
  EXPECT_EQ(d[1], 3);
  EXPECT_EQ(d[2], 9);
  EXPECT_EQ(d[3], 12);
}

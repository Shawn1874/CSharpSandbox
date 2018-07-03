#include "stdafx.h"
#include "TestCustomAlgorithms.h"
#include "Algorithms.h"

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
  //std::reverse(name.begin(), name.end());

  EXPECT_EQ(name, result);

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
}

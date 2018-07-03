#include "stdafx.h"
#include "TestMathFunctions.h"


TestMathFunctions::TestMathFunctions()
{
}


TestMathFunctions::~TestMathFunctions()
{
}

TEST_F(TestMathFunctions, TestStdRound)
{
  auto result = std::round(3.9);
  EXPECT_EQ(result, 4);

  result = std::round(321761.0 / 100.0);
  EXPECT_EQ(result, 3218);

  result = std::round(321761 / (double) 100);
  EXPECT_EQ(result, 3218);

  result = std::round(321761 / 100);
  EXPECT_EQ(result, 3217);
}

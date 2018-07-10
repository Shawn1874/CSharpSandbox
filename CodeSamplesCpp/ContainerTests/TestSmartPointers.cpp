#include "stdafx.h"
#include "TestSmartPointers.h"
#include <memory>

TestSmartPointers::TestSmartPointers()
{
}


TestSmartPointers::~TestSmartPointers()
{
}

TEST_F(TestSmartPointers, UniquePointerTest)
{
  auto p = std::make_unique<int>(0);
  *p = 5;

  EXPECT_NE(*p, NULL);
  EXPECT_EQ(*p, 5);

  auto temp = p.release();
  GTEST_ASSERT_NE(temp, nullptr);
  GTEST_ASSERT_EQ(p, nullptr);
}

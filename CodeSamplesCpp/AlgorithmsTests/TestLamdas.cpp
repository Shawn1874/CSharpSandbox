#include "stdafx.h"
#include "TestLamdas.h"
#include <vector>

TestLamdas::TestLamdas()
{
}


TestLamdas::~TestLamdas()
{
}

TEST_F(TestLamdas, BasicCaptureExpressionTest)
{
  int x(0), y(0);

  std::vector<int> values = { 1, 2, 3, 4, 5 };

  // test capturing by value
  std::for_each(values.begin(), values.end(), [=](int element) mutable {
    x += element;
    y += element;
  });

  EXPECT_EQ(x, 0);
  EXPECT_EQ(y, 0);

  // test capturing x by value
  std::for_each(values.begin(), values.end(), [=, &x](int element) mutable {
    x += element;
    y += element;
  });

  EXPECT_EQ(x, 15);
  EXPECT_EQ(y, 0);

  // test capturing by reference
  std::for_each(values.begin(), values.end(), [&](int element) mutable {
    x += element;
    if(element % 2)
    {
      y += element;
    }
  });

  EXPECT_EQ(x, 30);
  EXPECT_EQ(y, 9);
}

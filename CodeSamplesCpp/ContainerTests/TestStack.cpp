#include "stdafx.h"
#include "TestStack.h"
#include <stack>
#include <deque>
#include <list>
TestStack::TestStack()
{
}


TestStack::~TestStack()
{
}

// By default stack is implemented in terms of a deque
TEST_F(TestStack, ConstructorsTest)
{
  std::deque<int> values = { 1, 2, 3, 4 };

  // Construct using a deque
  std::stack<int> aStack(values);
  EXPECT_EQ(values.size(), aStack.size());

  // copy constructor
  std::stack<int> anotherStack(aStack);
  //EXPECT_EQ(anotherStack.size(), aStack.size());
  EXPECT_EQ(anotherStack, aStack);

}

TEST_F(TestStack, ConditionaOperatorTests)
{
  std::deque<int> smaller = { 1, 2, 3, 4 }; // lexicographically less since it is smaller
  std::deque<int> greater = { 1, 2, 3, 4, 5 };

  std::stack<int> smallerStack(smaller);
  std::stack<int> greaterStack(greater);
  EXPECT_LT(smaller, greater);
  EXPECT_LE(smaller, greater);
  EXPECT_NE(smaller, greater);
  EXPECT_GT(greater, smaller);
  EXPECT_GE(greater, smaller);

  // It is still lexicographically less since the last value to compare (4) < (5)
  smallerStack.push(4);
  EXPECT_LT(smaller, greater);
  EXPECT_LE(smaller, greater);
  EXPECT_NE(smaller, greater);
  EXPECT_GT(greater, smaller);
  EXPECT_GE(greater, smaller);
}

TEST_F(TestStack, PushAndPopTest)
{
  std::stack<std::string> strings;
  EXPECT_TRUE(strings.empty());

  strings.push("The");
  EXPECT_EQ(strings.top(), "The");
  EXPECT_FALSE(strings.empty());

  strings.push("cow");
  EXPECT_EQ(strings.top(), "cow");

  strings.push("jumped");
  EXPECT_EQ(strings.top(), "jumped");

  strings.push("over");
  EXPECT_EQ(strings.top(), "over");

  strings.push("the");
  EXPECT_EQ(strings.top(), "the");

  strings.push("moon");
  EXPECT_EQ(strings.top(), "moon");

  std::string sentence;

  while(strings.size())
  {
    if(!sentence.empty())
    {
      sentence.insert(0, " ");
    }

    sentence.insert(0, strings.top());
    strings.pop();
  }

  EXPECT_EQ(sentence, "The cow jumped over the moon");
}

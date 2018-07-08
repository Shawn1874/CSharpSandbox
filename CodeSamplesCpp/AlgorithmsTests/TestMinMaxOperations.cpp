#include "stdafx.h"
#include "TestMinMaxOperations.h"
#include <algorithm>
#include <array>
#include <vector>

TestMinMaxOperations::TestMinMaxOperations()
{
}


TestMinMaxOperations::~TestMinMaxOperations()
{
}

TEST_F(TestMinMaxOperations, TestMin)
{
  int a = 5, b = 4;

  // std::min can return a non-const by value
  int c = std::min(a, b);
  EXPECT_EQ(c, b);
  c = 7;
  EXPECT_NE(c, b);  // c is a copy which is now changed

  // std::min can return a const reference too
  const int &g = std::min(a, b);
  EXPECT_EQ(g, b);
  EXPECT_EQ(&g, &b);

  // std::min can return a constexpr
  constexpr int f(7), d(10);
  std::array<int, std::min(f, d)> values;
  EXPECT_EQ(values.size(), f);

}

TEST_F(TestMinMaxOperations, TestMax)
{
  EXPECT_EQ(std::max(5, 4), 5);

  // like std::min, std::max can be used as a const expression
  std::array<int, std::max(5, 4)> values;
  EXPECT_EQ(values.size(), 5);

  // std::min, std::max also work with initiaalizer lists
  constexpr int size = std::max({ 55, 25, 35, 20, 10 });
  double fpNumbers[size];  
  EXPECT_EQ(size, 55);
}

TEST_F(TestMinMaxOperations, TestMinElement)
{
  std::array<int, 5> values = { 5, 15, 1, 3, 20 };

  // std::min_element returns an iterator not a copy of the value found
  EXPECT_EQ(*std::min_element(values.begin(), values.end()), 1);
}

TEST_F(TestMinMaxOperations, TestMaxElement)
{
  auto values = { 25, 15, 11, 6, 20 };

  // std::min_element returns an iterator not a copy of the value found
  EXPECT_EQ(*std::max_element(values.begin(), values.end()), 25);

  struct Person
  {
    Person(std::string name, unsigned int age) : name(name), age(age) {}

    std::string name;
    unsigned int age;
  };

  std::vector<Person> people = { Person("Shawn Fox", 45), Person("Steve Meyers", 25 ), Person("Sarah Jenkins", 23 ) };

  // use a custom lambda to find the youngest or oldest person
  auto oldest = std::max_element(people.begin(), people.end(), [](const Person &p1, const Person &p2) { return p1.age < p2.age; });
  EXPECT_EQ(oldest->name, "Shawn Fox");
}

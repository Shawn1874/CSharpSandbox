#pragma once
#include "gtest/gtest.h"

class TestFileIOApis : public ::testing::Test
{
public:
  TestFileIOApis();
  virtual ~TestFileIOApis();

  enum Modes
  {
    F_EXISTS    = 0,
    F_WRITEABLE = 2,
    F_READABLE  = 4,
    F_READWRITE = 6
  };

protected:
  const char* testFileName;
};


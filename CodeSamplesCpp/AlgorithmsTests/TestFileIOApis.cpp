#include "stdafx.h"
#include "TestFileIOApis.h"
#include  <io.h>

TEST_F(TestFileIOApis, FileExistsTest)
{
  auto result = _access("nonexistentfile.txt", F_EXISTS);
  EXPECT_EQ(result, -1);
  EXPECT_EQ(errno, ENOENT);

  // A non-existent file can't be readable or writeable either
  result = _access("nonexistentfile.txt", F_READABLE);
  EXPECT_EQ(result, -1);
  EXPECT_EQ(errno, ENOENT);

  result = _access("nonexistentfile.txt", F_WRITEABLE);
  EXPECT_EQ(result, -1);
  EXPECT_EQ(errno, ENOENT);

  result = _access(testFileName, F_EXISTS);
}

TEST_F(TestFileIOApis, FileReadWriteTest)
{
  // It's rather strange to me that the read-only and write-only tests work this way
  // for a read-write file.
  auto result = _access(testFileName, F_READABLE);
  EXPECT_EQ(result, 0);
  EXPECT_EQ(errno, EINVAL);

  result = _access(testFileName, F_WRITEABLE);
  EXPECT_EQ(result, 0);
  EXPECT_EQ(errno, EINVAL);
  
  result = _access(testFileName, F_EXISTS);
  EXPECT_EQ(result, 0);
  EXPECT_EQ(errno, EINVAL);

  result = _access(testFileName, F_READWRITE);
  EXPECT_EQ(result, 0);
  EXPECT_EQ(errno, EINVAL);
}

TEST_F(TestFileIOApis, ReadOnlyTest)
{
  errno = 0;
  auto result = _chmod(testFileName, _S_IREAD);
  EXPECT_EQ(result, 0);
  EXPECT_EQ(errno, 0);

  errno = 0;
  result = _access(testFileName, F_READWRITE);
  EXPECT_EQ(result, -1);
  EXPECT_EQ(errno, EACCES);

  errno = 0;
  result = _access(testFileName, F_WRITEABLE);
  EXPECT_EQ(result, -1);
  EXPECT_EQ(errno, EACCES);
}

TEST_F(TestFileIOApis, WriteOnlyTest)
{
  errno = 0;
  auto result = _chmod(testFileName, _S_IWRITE);
  EXPECT_EQ(result, 0);
  EXPECT_EQ(errno, 0);

  result = _access(testFileName, F_WRITEABLE);
  EXPECT_EQ(result, 0);
  EXPECT_EQ(errno, 0);

  result = _access(testFileName, F_READWRITE);
  EXPECT_EQ(result, 0);
  EXPECT_EQ(errno, 0);

  result = _access(testFileName, F_READABLE);
  EXPECT_EQ(result, 0);
  EXPECT_EQ(errno, 0);
}

TestFileIOApis::TestFileIOApis()
  : testFileName ("test.txt")
{
  FILE *fp = fopen(testFileName, "w+");
  const char* msg = "hello world";
  if (fp)
  {
    fwrite(msg, sizeof(char), strlen(msg), fp);
    fclose(fp);
  }
}

TestFileIOApis::~TestFileIOApis()
{
  remove(testFileName);
}

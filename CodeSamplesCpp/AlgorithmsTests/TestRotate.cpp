#include "stdafx.h"
#include "TestRotate.h"
#include <string>
#include <array>
#include <vector>
#include <algorithm>

TestRotate::TestRotate()
{
}


TestRotate::~TestRotate()
{
}

TEST_F(TestRotate, TestRotateContainer)
{
	std::array<std::string, 5>  phrase = { "going", "to", "the", "store", "I'm" };
	auto iter = std::rotate(phrase.begin(), phrase.begin() + 4, phrase.end());
	EXPECT_EQ(*(phrase.begin()), "I'm");
}

TEST_F(TestRotate, TestRotateCopyContainer)
{
	std::vector<std::string>  phrase = { "going", "to", "the", "store", "I'm" };
	std::vector<std::string> correctedPhrase(5);

	std::rotate_copy(phrase.begin(), 
				     phrase.begin() + 4,   // first element to copy
					 phrase.end(), 
					 correctedPhrase.begin());  // beginning of destination range

	EXPECT_EQ(*(correctedPhrase.begin()), "I'm");
	EXPECT_EQ(*(correctedPhrase.end() - 1), "store");
}

#pragma once
#include <algorithm>
class Algorithms
{
public:
  Algorithms();
  ~Algorithms();

  template <typename BidirIter>
  static void Reverse(BidirIter begin, BidirIter end)
  {
    // 0, 1, 2, 3, end
    for( ; begin != --end && begin != end; ++begin)
    {
      std::swap(*begin, *end);
    }
  }

  /*
  * Merges sorted range source into the sorted range destination.  If ranges are not sorted, the behavior
  * is undefined.  Caller must ensure that destination is large enough to contain the merged ranges or
  * the behavior is undefined.  This method will not allocate memory.
  */
  template <typename FwdIter>
  static void MergeSortedRanges(FwdIter sourceFirst, FwdIter sourceLast, FwdIter destFirst, FwdIter destLast)
  {
    // iterate over the source range
    // determine the location within the destination
    // swap element from source with element from destination
    // iterate over the remainder of the destination swapping the temporary until end of range reached
    FwdIter tempIter;
    bool found = false;

    while(sourceFirst != sourceLast)
    {
      for(; destFirst != destLast; ++destFirst)
      {
        if(*sourceFirst < *destFirst)
        {
          found = true;
          break;
        }
      }

      // *destFirst must be replaced with value from source but values must be shifted

      auto tempValue = *sourceFirst;
      *destLast = 0;
      destLast++;
      tempIter = destFirst + 1;

      for( ; destFirst != destLast; ++destFirst)
      {
        std::swap(tempValue, *destFirst);
      }

      ++sourceFirst;
      destFirst = tempIter;

      found = false;
    }
  }
};


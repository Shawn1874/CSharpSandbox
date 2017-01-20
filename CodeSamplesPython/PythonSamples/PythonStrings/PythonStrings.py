#!/usr/bin/env python3
"""Placeholder module in case I want to try more complex concepts
"""
def split(input, delimiter, numSplits):
    return input.split(delimiter, numSplits)


if __name__ == '__main__': 
    """ Entry point of the program when this file is run as the main script.
    """
    result = split("C:\\first\\second\\third", "\\", 3)
    print(len(result))
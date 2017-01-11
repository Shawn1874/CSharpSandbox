#!/usr/bin/env python3
import unittest

class UnitTests(unittest.TestCase):
    """description of class"""

    def test_split(self):
        test = "C:\\first\\second\\third"
        parts = test.split("\\", 3)
        self.assertEqual(len(parts), 4)
        self.assertEqual("C:", parts[0])
        self.assertEqual("first", parts[1])
        self.assertEqual("second", parts[2])
        self.assertEqual("third", parts[3])


if __name__ == '__main__':
    unittest.main()



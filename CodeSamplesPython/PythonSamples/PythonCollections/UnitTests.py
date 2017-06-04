#!/usr/bin/env python3
import unittest

class CollectionTests(unittest.TestCase):
    """Test of string methods"""

    def test_lists(self):
        value = ["hello", "world"]
        self.assertEqual(len(value), 2)

    def test_append(self):
        anotherFruit = "peaches"
        container = ["grapes", "apples", "pears"]
        self.assertNotIn(anotherFruit, container)
        container.append(anotherFruit)
        self.assertIn(anotherFruit, container)

if __name__ == '__main__':
    unittest.main()
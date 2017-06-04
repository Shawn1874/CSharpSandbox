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

    def test_extend(self):
        container = [1, 3, 5]
        self.assertNotIn(7, container)
        self.assertNotIn(9, container)
        container += [7, 9]
        self.assertIn(7, container)
        self.assertIn(9, container)
        self.assertNotIn(11, container)
        self.assertNotIn(13, container)
        container.extend([11, 13])
        self.assertIn(11, container)
        self.assertIn(13, container)

    def test_remove(self):
        container = [1, 3, 6, 5, 7, 9, 11]
        self.assertTrue(6 in container)
        container.remove(6)
        self.assertNotIn(6, container)
        self.assertTrue(3 in container)
        value = container.pop(1)
        self.assertEqual(value, 3)
        self.assertNotIn(3, container)

if __name__ == '__main__':
    unittest.main()
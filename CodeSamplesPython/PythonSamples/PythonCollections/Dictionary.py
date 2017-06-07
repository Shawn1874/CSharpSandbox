import unittest

class Test_Dictionary(unittest.TestCase):
    def test_key(self):
        d = { 1 : 'one', 2 : 'two', 3 : 'three' }
        self.assertEqual(len(d), 3)
        v = d.get(4)
        self.assertIsNone(v)
        v = d.get(3)
        self.assertIsNotNone(v)
        self.assertEqual(v, 'three')
        self.assertTrue(2 in d)
        self.assertFalse(5 in d)

    def test_pop(self):
        d = { 1 : 'one', 2 : 'two', 3 : 'three' }
        v = d.pop(1)
        self.assertEqual(len(d), 2)
        self.assertEqual(v, 'one')

if __name__ == '__main__':
    unittest.main()

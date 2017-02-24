#!/usr/bin/env python3
import unittest

class StringTests(unittest.TestCase):
    """Test of string methods"""

    def test_split(self):
        test = "C:\\first\\second\\third"
        parts = test.split("\\", 3)
        self.assertEqual(len(parts), 4)
        self.assertEqual("C:", parts[0])
        self.assertEqual("first", parts[1])
        self.assertEqual("second", parts[2])
        self.assertEqual("third", parts[3])

    def test_slicing(self):
        test = "the cow jumped over the moon"

        result = test[:-4]
        self.assertEqual(result, "the cow jumped over the ")

        result = test[:3]
        self.assertEqual(result, "the")

        result = test[3:]
        self.assertEqual(result, " cow jumped over the moon")

        result = test[24:]
        self.assertEqual(result, "moon")

        result = test[-4:]
        self.assertEqual(result, "moon")

        result = test[8:14]
        self.assertEqual(result, "jumped")

    def test_is_xxx_functions(self):
        # test of str.isalnum
        self.assertFalse (''.isalnum())
        self.assertTrue('hello'.isalnum())
        self.assertTrue('123xyz'.isalnum())
        self.assertTrue('xyz123'.isalnum())
        self.assertFalse('1#*&%$#@!~x'.isalnum())

        # test of str.isalpha
        self.assertTrue('hello'.isalpha())
        self.assertFalse('123xyz'.isalpha())
        self.assertFalse('xyz123'.isalpha())
        self.assertFalse(''.isalpha())

        #test of str.isdecimal,  str.isdigit, and str.isnumeric
        # if a string is decimal or digit then it must be numeric
        # if a string is decimal then it is also a digit
        value = '123.456'
        self.assertTrue(value.isdecimal)
        self.assertTrue(value.isdigit)
        self.assertTrue(value.isnumeric)

        value = '\u00BC' # fraction 1/4
        print(value)
        self.assertFalse(value.isdecimal())
        self.assertFalse(value.isdigit())
        self.assertTrue(value.isnumeric())

        value = '\u2155'  # fraction 1/5
        self.assertFalse(value.isdecimal())
        self.assertFalse(value.isdigit())
        self.assertTrue(value.isnumeric())

        value = '\u06F3'  # extended arabic-indic digit 3
        self.assertTrue(value.isdecimal())
        self.assertTrue(value.isdigit())
        self.assertTrue(value.isnumeric())

        value = '\u2168'  # Roman Numeral IX
        self.assertFalse(value.isdecimal())
        self.assertFalse(value.isdigit())
        self.assertTrue(value.isnumeric())

        value = '\u2080'  # subscript 0
        self.assertFalse(value.isdecimal())
        self.assertTrue(value.isdigit())
        self.assertTrue(value.isnumeric())

        # test of str.isidentifier
        value = 'if'
        self.assertTrue(value.islower())

        value = 'FOR'
        self.assertTrue(value.isupper())

        value = 'TheTitle'
        self.assertFalse(value.istitle())
        #test of str.islower

if __name__ == '__main__':
    unittest.main()



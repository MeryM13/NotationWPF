using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NotationLibrary
{
	public class Calculate
	{
		public static string Calc(string str, int notation_from, int notation_to)
		{
			string ret;
			string[] number = StringSplit(str);
			try
            {
				string[] result = new string[] {"", ""};

				decimal sum_decimal = Math.Round(ConvertIn10_decimal(number[1], notation_from), 5);
				result[1] = ConvertFrom10_decimal(sum_decimal, notation_to);

				int sum_int = ConvertIn10_int(number[0], notation_from);
				result[0] = ConvertFrom10_int(sum_int, notation_to);

				ret = result[0] + '.' + result[1];
			}
			catch (IndexOutOfRangeException)
			{
				string result = new string("");

				int sum_int = ConvertIn10_int(number[0], notation_from);
				result = ConvertFrom10_int(sum_int, notation_to);

				ret = result;
			}
			return ret;
		}

		private static string[] StringSplit(string str)
		{
			if (str == null)
				throw new Exception("Number string was empty");
			else
				return str.Split(' ', '.', ',');
		}

		private static int CharToInt(char ch)
		{
			if (ch >= '0' && ch <= '9')
				return (int)ch - '0';
			else
				if (ch >= 'A' && ch <= 'Z')
				return (int)ch - 'A' + 10;
			else
					if (ch >= 'a' && ch <= 'z')
				return (int)ch - 'a' + 10;
			else
				throw new Exception("Char was not recognised");
		}

		private static char IntToChar(int a)
		{
			if (a >= 0 && a <= 9)
				return (char)(a + '0');
			else
				return (char)(a + 'A' - 10);
		}

		private static bool Check(int numb, int notation)
		{
			if (numb >= notation || numb < 0)
				return false;
			else
				return true;
		}

		private static int ConvertIn10_int(string number, int notation)
		{
			if (notation == 10)
			{
				return Int32.Parse(number);
			}
			int sum = 0;
			int len = number.Length;
			for (int i = 0; i < len; i++)
			{
				int a = CharToInt(number[i]);
				if (!Check(a, notation))
					throw new Exception("Цифра в записи: " + a.ToString() + " была больше системы счисления: " + notation.ToString());
				else
				{
					sum += a;
					sum *= notation;
				}
			}
			return sum / notation;
		}

		private static decimal ConvertIn10_decimal(string number, int notation)
        {
			if (notation == 10)
            {
				return decimal.Parse(number);
            }
			decimal sum = 0;
			int len = number.Length;
			for (int i = len - 1; i >= 0; i--)
			{
				int a = CharToInt(number[i]);
				if (!Check(a, notation))
					throw new Exception("Цифра в записи: " + a.ToString() + " была больше системы счисления: " + notation.ToString());
				else
				{
					sum = (a + sum) / notation;
				}
			}
			return sum;
		}
		private static void Swap(ref char a, ref char b)
		{
			char c = a;
			a = b;
			b = c;
		}

		private static string ConvertFrom10_int(int number, int notation)
		{
			if (notation == 10)
            {
				return number.ToString();
            }
			string result = "";
			while (number != 0)
			{
				int a = number % notation;
				result += IntToChar(a);
				number /= notation;
			}
			int n = result.Length;
			char[] str = result.ToCharArray();
			for (int i = 0; i < n / 2; i++)
				Swap(ref str[i], ref str[n - i - 1]);
			return new string(str);
		}

		private static string ConvertFrom10_decimal(decimal number, int notation)
		{
			if (notation == 10)
			{
				return number.ToString();
			}
			string result = "";
			int i = 0;
			while (number != 0 && i < 10)
			{
				int a = (int)(number * notation);
				result += IntToChar(a);
				number = number * notation - a;
				i++;
			}
			return result;
		}

	}
}

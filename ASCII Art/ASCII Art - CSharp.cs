using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
	/// <summary>
	/// Length of each letter [Includes the space between letters]
	/// </summary>
	static int L;
	/// <summary>
	/// Height of each letter
	/// </summary>
	static int H;
	/// <summary>
	/// The alphabet in ASCII 
	/// </summary>
	static List<string> lines = new List<string>(); // Alphabet in ASCII

	static void Main(string[] args)
	{
		L = int.Parse(Console.ReadLine());
		H = int.Parse(Console.ReadLine());
		string T = Console.ReadLine();
		for (int i = 0; i < H; i++)
		{
			lines.Add(Console.ReadLine());
		}

		// Write an action using Console.WriteLine()
		// To debug: Console.Error.WriteLine("Debug messages...");
		PrintASCII(T);

		Console.WriteLine();
		Console.ReadKey();
	}

	static List<StringBuilder> GetASCIILetter(char letter)
	{
		letter = char.ToUpper(letter);
		List<StringBuilder> rstr = new List<StringBuilder>();
		if (letter >= 'A' && letter <= 'Z' || letter == '?')
		{
			if (letter == '?')
			{
				lines.ForEach(t => rstr.Add(
				(new StringBuilder(
					t.Substring(('Z' - 'A' + 1) * L, L)
					)
					))); // Where 'Lines' = ASCII Alphabet
			}
			else
			{
				lines.ForEach(t => rstr.Add(
				(new StringBuilder(
					t.Substring((char.ToUpper(letter) - 'A') * L, L)
					)
					))); // Where 'Lines' = ASCII Alphabet
			}
			return rstr;
		}
		else return GetASCIILetter('?');

	}

	static List<StringBuilder> GetASCIIWord(string s)
	{
		List<StringBuilder> rstr = new List<StringBuilder>();
		s.ToList().ForEach(let =>
		{
			if (rstr.Count == 0)
			{
				rstr.AddRange(GetASCIILetter(let));
			}
			else
			{
				IEnumerator ascii_enum = GetASCIILetter(let).GetEnumerator();

				rstr.ForEach(element =>
				{
					ascii_enum.MoveNext();
					element.Append(ascii_enum.Current);

				});

			}

		});

		return rstr;
	}

	static void PrintASCII (List<StringBuilder> list)
	{
		list.ForEach(t => Console.WriteLine(t));
	}

	static void PrintASCII(string s)
	{
		PrintASCII(GetASCIIWord(s));
	}
}


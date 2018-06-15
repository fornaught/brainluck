using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class Kata
{
    public static string BrainLuck(string code, string input)
    {
        Console.WriteLine(code);
        Console.WriteLine(input);
        int index = 0;
        char[] input2 = input.ToCharArray();
        byte[] test2 = Encoding.ASCII.GetBytes("ÿ?");
        byte[] asciiBytes = Encoding.ASCII.GetBytes(input);
        List<byte> inputlist = new List<byte>(asciiBytes);
        List<byte> bl = new List<byte>(asciiBytes);
        for (int i = 0; i < 1000; i++) {
            bl.Add(byte.MinValue);
        }
        List<int> lbracket = new List<int>();
        for (int i = 0; i < code.Length; i++)
        {
            if (code[i] == '[')
            {
                lbracket.Add(i);
            }
        }
        List<int> rbracket = new List<int>();
        for (int i = code.Length -1; i >= 0; i--)
        {
            if (code[i] == ']')
            {
                rbracket.Add(i);
            }
        }

        List<int> rbracket2 = new List<int>();
        List<int> lbracket2 = new List<int>();

        for (int i = 0; i < code.Length; i++)
        {
            if (code[i] == ']')
            {
                int afstand = code.Length;
                int b = 0;
                for (int j = 0; j < lbracket.Count; j++)
                {
                    if (i - lbracket[j] < afstand && i - lbracket[j] > 0)
                    {
                        Console.WriteLine("afstand " + (i - lbracket[j]));
                        afstand = i - lbracket[j];
                        b = i;
                    }
                }
                Console.WriteLine("min as = " + afstand);
                lbracket2.Add(i - afstand);
                lbracket.Remove(i - afstand);
                rbracket2.Add(b);
            }
        }

        for (int i = 0; i < lbracket2.Count; i++)
        {
            Console.WriteLine("brackets " + lbracket2[i] + " " + rbracket2[i]);
        }

        string output = "";
        for (int i = 0; i < code.Length; i++)
        {
            //Console.WriteLine("code[i] " + code[i] +  ", bl[index] " + bl?[index]);
            switch (code[i])
            {
                case '>':
                    if (index < code.Length - 1)
                        index += 1;
                    else
                        index = 0;
                    break;
                case '<':
                    if (index > 0)
                        index -= 1;
                    else
                        index = code.Length - 1;
                    break;
                case '+':
                    if (bl[index] < 255)
                        bl[index] += 1;
                    else
                        bl[index] = 0;
                    break;
                case '-':
                    if (bl[index] > 0)
                        bl[index] -= 1;
                    else
                        bl[index] = 255;
                    break;
                case '.':
                    Console.WriteLine(bl[index] + " " + (char)bl[index]);
                    output += (char) bl[index];
                    break;
                case ',':
                    if (inputlist.Count > 0)
                    {
                        Console.WriteLine(inputlist[0] + " " + (char)inputlist[0]);
                        bl[index] = inputlist[0];
                        inputlist.RemoveAt(0);
                        break;
                    }
                    else
                        return output;
                case '[':
                    if (bl[index] == 0)
                    {
                        int a = lbracket2.IndexOf(i);
                        i = rbracket2[a];
                        //Console.WriteLine("hoi " + a);
                        //Match m = Regex.Match(code.Substring(i, code.Length - i), "]");
                        //i = m.Index + 1;
                    }
                    break;
                case ']':
                    if (bl[index] > 0)
                    {
                        int a = rbracket2.IndexOf(i);
                        i = lbracket2[a];
                    }
                    else
                        Console.WriteLine("mjoah " + (char) bl[index] + ".");
                    break;
            }
        }
        Console.WriteLine("output " + output);
        return output;
    }
}
using UnityEngine;
using System.Text.RegularExpressions;

public class StringSplitter
{
    public static string SpaceSentence(string text)
    {
        return Regex.Replace(text, "(\\B[A-Z])", " $1");
    }
}

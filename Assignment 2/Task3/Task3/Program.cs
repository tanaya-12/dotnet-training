using System;
class Program
{
    static void Main()
    {
        string word1 = "madam";
        string word2 = "hello";

        Console.WriteLine(word1.IsPalindrome()); 
        Console.WriteLine(word2.IsPalindrome()); 
    }
}

public static class StringExtensions
{
    public static bool IsPalindrome(this string s)
    {
        if (string.IsNullOrWhiteSpace(s))
            return false;

        s = s.Replace(" ", "").ToLower();

        int left = 0;
        int right = s.Length - 1;

        while (left < right)
        {
            if (s[left] != s[right])
                return false;

            left++;
            right--;
        }

        return true;
    }
}

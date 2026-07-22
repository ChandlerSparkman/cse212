using System;

public class Program
{
    static void Main(string[] args)
    {
        bool check = IsAnagram("CAT", "TAC");
        Console.WriteLine(check);
    }

    public static bool IsAnagram(string word1, string word2)
    {
        word1 = word1.ToLower().Replace(" ","");
        char[] chars1 = word1.ToCharArray();
        word2 = word2.ToLower().Replace(" ","");
        char[] chars2 = word2.ToCharArray();

        Array.Sort(chars1);
        word1 = new string(chars1);
        Array.Sort(chars2);
        word2 = new string(chars2);

        if (word1 == word2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
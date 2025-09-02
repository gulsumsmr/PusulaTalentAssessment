using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public static class LongestVowelSubsequence
{
    private static readonly HashSet<char> Vowels =
        new HashSet<char>("aeiouAEIOU".ToCharArray());

    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
        if (words == null || words.Count == 0)
            return JsonSerializer.Serialize(new List<object>());

        var list = new List<object>();

        foreach (var w in words)
        {
            var word = w ?? string.Empty;

            int bestStart = -1, bestLen = 0;
            int currStart = -1, currLen = 0;

            for (int i = 0; i < word.Length; i++)
            {
                if (Vowels.Contains(word[i]))
                {
                    if (currLen == 0) currStart = i;
                    currLen++;
                    if (currLen > bestLen)
                    {
                        bestLen = currLen;
                        bestStart = currStart;
                    }
                }
                else
                {
                    currLen = 0;
                }
            }

            string seq = bestLen > 0 ? word.Substring(bestStart, bestLen) : string.Empty;

            list.Add(new
            {
                word = word,
                sequence = seq,
                length = bestLen
            });
        }

        return JsonSerializer.Serialize(list);
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public static class MaxIncreasingSubArray
{
    public static string MaxIncreasingSubArrayAsJson(List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0)
            return JsonSerializer.Serialize(new List<int>());

        int bestStart = 0, bestLen = 1, bestSum = numbers[0];
        int currStart = 0, currLen = 1, currSum = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] > numbers[i - 1]) 
            {
                currLen++;
                currSum += numbers[i];
            }
            else
            {
                if (currSum > bestSum || (currSum == bestSum && currLen > bestLen))
                {
                    bestSum = currSum;
                    bestLen = currLen;
                    bestStart = currStart;
                }
                currStart = i;
                currLen = 1;
                currSum = numbers[i];
            }
        }

        if (currSum > bestSum || (currSum == bestSum && currLen > bestLen))
        {
            bestSum = currSum;
            bestLen = currLen;
            bestStart = currStart;
        }

        var result = numbers.GetRange(bestStart, bestLen);
        return JsonSerializer.Serialize(result);
    }
}

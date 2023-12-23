using System;
using System.Collections.Generic;

public static class Extensions
{
    public static T Random<T>(this List<T> list)
    {
        int count = list.Count;
        Random random = new Random();
        var randomNum = random.Next(0, count);
        return list[randomNum];
    }
}

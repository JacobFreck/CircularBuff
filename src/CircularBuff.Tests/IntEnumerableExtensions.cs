using System;
using System.Collections.Generic;
using System.Linq;

namespace CircularBuff.Tests;

public static class IntEnumerableExtensions
{

    public static IEnumerable<int> To(this int from, int to)
    {
        if (from < to)
        {
            while (from <= to)
            {
                yield return from++;
            }
        }
        else
        {
            while (from >= to)
            {
                yield return from--;
            }
        }
    }

    public static IEnumerable<T> Step<T>(this IEnumerable<T> source, int step)
    {
        if (step == 0)
        {
            throw new ArgumentOutOfRangeException("step", "Param cannot be zero.");
        }

        return source.Where((x, i) => (i % step) == 0);
    }
}


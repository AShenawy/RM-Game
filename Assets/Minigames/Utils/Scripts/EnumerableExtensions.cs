using System;
using System.Collections.Generic;
using System.Linq;

public static class EnumerableExtensions
{
    public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current)
    {
        return current.Next ?? current.List.First;
    }

    public static LinkedListNode<T> PreviousOrLast<T>(this LinkedListNode<T> current)
    {
        return current.Previous ?? current.List.Last;
    }

    public static HashSet<T> GetHashSet<T>(this IEnumerable<T> source)
    {
        return new HashSet<T>(source);
    }

    public static T GetRandomElement<T>(this List<T> source) {
        var rand = new Random();
        return source[rand.Next(source.Count)];
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.Shuffle(new Random());
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random random)
    {
        if (source == null) throw new ArgumentNullException("source");
        if (random == null) throw new ArgumentNullException("random");

        return source.ShuffleIterator(random);
    }

    private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source, Random rng)
    {
        var buffer = source.ToList();
        for (int i = 0; i < buffer.Count; i++)
        {
            int j = rng.Next(i, buffer.Count);
            yield return buffer[j];

            buffer[j] = buffer[i];
        }
    }
}
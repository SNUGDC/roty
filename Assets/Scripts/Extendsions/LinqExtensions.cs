using System.Collections.Generic;
using System.Linq;
using System;

namespace ExtensionMethods
{
	public static class LinqExtensions
	{
		public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> func)
		{
			var firstIterator = first.GetEnumerator();
			var secondIterator = second.GetEnumerator();
			
			while (firstIterator.MoveNext() && secondIterator.MoveNext())
			{
				yield return func(firstIterator.Current, secondIterator.Current);
			}
		}

		public static IEnumerable<T> Sample<T>(this IEnumerable<T> source, int count = 1)
		{
			var random = new Random ();
			
			var randomOrder = from element in source
				orderby random.Next ()
					select element;
		
			if (randomOrder.Count () < count) {
				throw new ArgumentOutOfRangeException ("Sample's count is over source's count.");
			}
		
			return randomOrder.Take (count);
		}
	}
}

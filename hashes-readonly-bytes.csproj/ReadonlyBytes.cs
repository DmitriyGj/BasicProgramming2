using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using NUnit.Framework;

namespace hashes
{
	public class ReadonlyBytes : IEnumerable<byte>
	{
		private readonly byte[] items;
		public int Length {get => items.Length; }
		static int prime = 16777619;
		static int offsetBasis = unchecked((int)2166136261);
		private int hashCode = 0;

		public byte this[int index]
		{
			get => index >= 0 && index < Length ? items[index] : throw new IndexOutOfRangeException();
		}
		public IEnumerator<byte> GetEnumerator()
		{
			for (int i = 0; i != Length; i++)
				yield return items[i];
		}

		public override int GetHashCode()
		{
			if (hashCode == 0)
			{
				unchecked
				{
					hashCode = offsetBasis;
					foreach (var value in items)
						hashCode = (hashCode ^ value.GetHashCode()) * prime;
				}
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			if (obj == null
			|| obj.GetType() != typeof(ReadonlyBytes))
				return false;
			return GetHashCode() == obj.GetHashCode() ;
		}

		public ReadonlyBytes(params byte[] a) => items = a != null ? a : throw new ArgumentNullException();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public override string ToString() => $"[{string.Join(", ",items)}]" ;
	}
}
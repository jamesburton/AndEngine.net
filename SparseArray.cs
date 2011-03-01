using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Collections
{
	/// <summary>
	/// Summary description for SparseArray.
	/// </summary>
	public class SparseArray : IList
	{
		protected int dimensions = 1;
		protected Hashtable hashtable;
		protected int[] lowerBounds, upperBounds;

		public SparseArray()
		{
			hashtable = new Hashtable();
			lowerBounds = new int[dimensions];
			upperBounds = new int[dimensions];
		}
		
		public SparseArray(int dimensions)
		{
			this.dimensions = dimensions;
			hashtable = new Hashtable();
			lowerBounds = new int[dimensions];
			upperBounds = new int[dimensions];
		}

		protected string IndexToHash(int[] indices)
		{
			if (indices.Length != dimensions)
				throw new ArgumentException("The number of indices must match the number of dimensions");

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < indices.Length; i++)
			{
				sb.Append(indices[i].ToString());
				if (i < (indices.Length-1))
					sb.Append(',');
			}
			return sb.ToString();
		}

		protected int[] HashToIndex(string hash)
		{
			string[] subs = hash.Split(',');
			if (subs.Length != dimensions)
				throw new ArgumentException("The number of indices must match the number of dimensions");

			int[] ret = new int[dimensions];
			for (int i = 0; i < dimensions; i++)
				ret[i] = int.Parse(subs[i]);

			return ret;
		}

		public bool IsFixedSize { get { return false; } }
		public bool IsReadOnly { get { return false; } }
		public bool IsSynchronized { get { return false; } }
		public int Count { get { return hashtable.Count; } }
		public int Rank { get { return dimensions; } }
		public object SyncRoot { get { return null; } }

		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		public int GetLowerBound(int dimension)
		{
			if (dimension > dimensions)
				throw new ArgumentOutOfRangeException("dimension");
			return lowerBounds[dimension];
		}

		public int GetUpperBound(int dimension)
		{
			if (dimension > dimensions)
				throw new ArgumentOutOfRangeException("dimension");
			return upperBounds[dimension];
		}

		public object GetValue(int[] indices)
		{
			string key = IndexToHash(indices);
			if (hashtable.Contains(key))
				return hashtable[key];
			return null;
		}

		public object GetValue(int index)
		{
			return GetValue(new int[] { index });
		}

		public object GetValue(int index1, int index2)
		{
			return GetValue(new int[] { index1, index2 });
		}

		public void SetValue(object value, int[] indices)
		{
			hashtable.Add(IndexToHash(indices), value);
			for (int i = 0; i < dimensions; i++)
			{
				if (lowerBounds[i] > indices[i])
					lowerBounds[i] = indices[i];
				if (upperBounds[i] < indices[i])
					upperBounds[i] = indices[i];
			}
		}

		public void SetValue(object value, int index)
		{
			SetValue(value, new int[] { index });
		}

		public void SetValue(object value, int index1, int index2)
		{
			SetValue(value, new int[] { index1, index2 });
		}

		private class SparseArrayEnumerator : IEnumerator
		{
			private IDictionaryEnumerator dict;
			private SparseArray parent;
			public SparseArrayEnumerator(SparseArray array) { parent = array; dict = array.hashtable.GetEnumerator(); }
			public void Reset() { dict.Reset(); }
			public bool MoveNext() { return dict.MoveNext(); }
			public object Current { get { return dict.Value; } }
			public int[] Index { get { return parent.HashToIndex((string)dict.Key); } }
		}

		public System.Collections.IEnumerator GetEnumerator()
		{
			return new SparseArrayEnumerator(this);
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, object value)
		{
			throw new NotImplementedException();
		}

		public void Remove(object value)
		{
			throw new NotImplementedException();
		}

		public bool Contains(object value)
		{
			return hashtable.ContainsValue(value);
		}

		public void Clear()
		{
			hashtable.Clear();
		}

		public int IndexOf(object value)
		{
			if (dimensions != 1)
				throw new RankException();
			return 0;
		}

		public int Add(object value)
		{
			throw new NotImplementedException();
		}

		public object this[int[] indicies]
		{
			get { return GetValue(indicies); } 
			set { SetValue(value, indicies); }
		}

		public object this[int index]
		{
			get { return GetValue(index); } 
			set { SetValue(value, index); }
		}

		public object this[int index1, int index2]
		{
			get { return GetValue(index1, index2); } 
			set { SetValue(value, index1, index2); }
		}

	}
}


namespace System.Collections.Generic
{

    /// <summary>
    /// Summary description for SparseArray.
    /// </summary>
    public class SparseArray<T> : IList<T>
    {
        protected int dimensions = 1;
        protected Hashtable hashtable;
        protected int[] lowerBounds, upperBounds;

        public SparseArray()
        {
            hashtable = new Hashtable();
            lowerBounds = new int[dimensions];
            upperBounds = new int[dimensions];
        }

        public SparseArray(int dimensions)
        {
            this.dimensions = dimensions;
            hashtable = new Hashtable();
            lowerBounds = new int[dimensions];
            upperBounds = new int[dimensions];
        }

        protected string IndexToHash(int[] indices)
        {
            if (indices.Length != dimensions)
                throw new ArgumentException("The number of indices must match the number of dimensions");

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < indices.Length; i++)
            {
                sb.Append(indices[i].ToString());
                if (i < (indices.Length - 1))
                    sb.Append(',');
            }
            return sb.ToString();
        }

        protected int[] HashToIndex(string hash)
        {
            string[] subs = hash.Split(',');
            if (subs.Length != dimensions)
                throw new ArgumentException("The number of indices must match the number of dimensions");

            int[] ret = new int[dimensions];
            for (int i = 0; i < dimensions; i++)
                ret[i] = int.Parse(subs[i]);

            return ret;
        }

        public bool IsFixedSize { get { return false; } }
        public bool IsReadOnly { get { return false; } }
        public bool IsSynchronized { get { return false; } }
        public int Count { get { return hashtable.Count; } }
        public int Rank { get { return dimensions; } }
        public object SyncRoot { get { return null; } }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int GetLowerBound(int dimension)
        {
            if (dimension > dimensions)
                throw new ArgumentOutOfRangeException("dimension");
            return lowerBounds[dimension];
        }

        public int GetUpperBound(int dimension)
        {
            if (dimension > dimensions)
                throw new ArgumentOutOfRangeException("dimension");
            return upperBounds[dimension];
        }

        public T GetValue(int[] indices)
        {
            string key = IndexToHash(indices);
            if (hashtable.Contains(key))
                return (T)hashtable[key];
            return (T)null;
        }

        public T GetValue(int index)
        {
            return GetValue(new int[] { index });
        }

        public T GetValue(int index1, int index2)
        {
            return GetValue(new int[] { index1, index2 });
        }

        public void SetValue(T value, int[] indices)
        {
            hashtable.Add(IndexToHash(indices), value);
            for (int i = 0; i < dimensions; i++)
            {
                if (lowerBounds[i] > indices[i])
                    lowerBounds[i] = indices[i];
                if (upperBounds[i] < indices[i])
                    upperBounds[i] = indices[i];
            }
        }

        public void SetValue(T value, int index)
        {
            SetValue(value, new int[] { index });
        }

        public void SetValue(T value, int index1, int index2)
        {
            SetValue(value, new int[] { index1, index2 });
        }

        private class SparseArrayEnumerator<T2> : IEnumerator<T2>
        {
            private IDictionaryEnumerator dict;
            private SparseArray<T2> parent;
            public SparseArrayEnumerator(SparseArray<T2> array) { parent = array; dict = array.hashtable.GetEnumerator(); }
            public void Reset() { dict.Reset(); }
            public bool MoveNext() { return dict.MoveNext(); }
            public T2 Current { get { return (T2)dict.Value; } }
            public int[] Index { get { return parent.HashToIndex((string)dict.Key); } }
            public void Dispose() { }
        }

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            return new SparseArrayEnumerator<T>(this);
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T value)
        {
            throw new NotImplementedException();
        }

        public void Remove(T value)
        {
            throw new NotImplementedException();
        }

        public bool Contains(T value)
        {
            return hashtable.ContainsValue(value);
        }

        public void Clear()
        {
            hashtable.Clear();
        }

        public int IndexOf(T value)
        {
            if (dimensions != 1)
                throw new RankException();
            return 0;
        }

        public int Add(T value)
        {
            throw new NotImplementedException();
        }

        public T this[int[] indicies]
        {
            get { return GetValue(indicies); }
            set { SetValue(value, indicies); }
        }

        public T this[int index]
        {
            get { return GetValue(index); }
            set { SetValue(value, index); }
        }

        public T this[int index1, int index2]
        {
            get { return GetValue(index1, index2); }
            set { SetValue(value, index1, index2); }
        }

    }
}

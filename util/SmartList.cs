using System.Collections.Generic;

namespace andengine.util
{

    /**
     * @author Nicolas Gramlich
     * @since 22:20:08 - 27.12.2010
     */
    public class SmartList<T> : List<T>
    {
        // ===========================================================
        // Constants
        // ===========================================================

        private static long serialVersionUID = -8335986399182700102L;

        // ===========================================================
        // Fields
        // ===========================================================

        // ===========================================================
        // Constructors
        // ===========================================================

        public SmartList()
        {

        }

        public SmartList(int pCapacity)
            : base(pCapacity)
        {
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        /**
         * @param pItem the item to remove.
         * @param pParameterCallable to be called with the removed item, if it was removed.
         */
        public bool Remove(T pItem, ParameterCallable<T> pParameterCallable)
        {
            bool removed = this.Remove(pItem);
            if (removed)
            {
                pParameterCallable.call(pItem);
            }
            return removed;
        }

        public T Remove(IMatcher<T> pMatcher)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (pMatcher.Matches(this[i]))
                {
                    T removed = this[i];
                    this.RemoveAt(i);
                    return removed;
                }
            }
            return default(T);
        }

        public bool RemoveAll(IMatcher<T> pMatcher)
        {
            bool result = false;
            for (int i = this.Count - 1; i >= 0; i--)
            {
                if (pMatcher.Matches(this[i]))
                {
                    this.RemoveAt(i);
                    result = true;
                }
            }
            return result;
        }

        /**
         * @param pMatcher to find the items.
         * @param pParameterCallable to be called with each matched item after it was removed.
         */
        public bool RemoveAll(IMatcher<T> pMatcher, ParameterCallable<T> pParameterCallable)
        {
            bool result = false;
            for (int i = this.Count - 1; i >= 0; i--)
            {
                if (pMatcher.Matches(this[i]))
                {
                    T removed = this[i];
                    this.Remove(removed);
                    pParameterCallable.call(removed);
                    result = true;
                }
            }
            return result;
        }

        public void Clear(ParameterCallable<T> pParameterCallable)
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                T removed = this[i];
                this.Remove(removed);
                pParameterCallable.call(removed);
            }
        }

        public T Find(IMatcher<T> pMatcher)
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                T item = this[i];
                if (pMatcher.Matches(item))
                {
                    return item;
                }
            }
            return default(T);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
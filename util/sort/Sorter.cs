namespace andengine.util.sort
{

    //using java.util.Comparator;
    //import java.util.List;
    using System.Collections.Generic;

    /**
     * @author Nicolas Gramlich
     * @since 14:14:39 - 06.08.2010
     * @param <T>
     */
    public abstract class Sorter<T>
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public abstract void Sort(T[] pArray, int pStart, int pEnd, /* Comparator<T> */ IComparer<T> pComparator);

        public abstract void Sort(List<T> pList, int pStart, int pEnd, /* Comparator<T> */ IComparer<T> pComparator);

        // ===========================================================
        // Methods
        // ===========================================================

        public void Sort(T[] pArray, /* Comparator<T> */ IComparer<T> pComparator)
        {
            Sort(pArray, 0, pArray.Length, pComparator);
        }

        public void Sort(List<T> pList, /* Comparator<T> */ IComparer<T> pComparator)
        {
            Sort(pList, 0, pList.Count, pComparator);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
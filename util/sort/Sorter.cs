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

        public abstract void sort(T[] pArray, int pStart, int pEnd, /* Comparator<T> */ IComparer<T> pComparator);

        public abstract void sort(List<T> pList, int pStart, int pEnd, /* Comparator<T> */ IComparer<T> pComparator);

        // ===========================================================
        // Methods
        // ===========================================================

        public void sort(T[] pArray, /* Comparator<T> */ IComparer<T> pComparator)
        {
            sort(pArray, 0, pArray.Length, pComparator);
        }

        public void sort(List<T> pList, /* Comparator<T> */ IComparer<T> pComparator)
        {
            sort(pList, 0, pList.Count, pComparator);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
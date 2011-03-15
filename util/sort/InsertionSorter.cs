namespace andengine.util.sort
{

    //import java.util.Comparator;
    //import java.util.List;
    using System.Collections.Generic;

    /**
     * @author Nicolas Gramlich
     * @since 14:14:31 - 06.08.2010
     * @param <T>
     */
    public class InsertionSorter<T> : Sorter<T>
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

        public override void Sort(T[] pArray, int pStart, int pEnd, /* Comparator<T> */ IComparer<T> pComparator)
        {
            for (int i = pStart + 1; i < pEnd; i++)
            {
                T current = pArray[i];
                T prev = pArray[i - 1];
                if (pComparator.Compare(current, prev) < 0)
                {
                    int j = i;
                    do
                    {
                        pArray[j--] = prev;
                    } while (j > pStart && pComparator.Compare(current, prev = pArray[j - 1]) < 0);
                    pArray[j] = current;
                }
            }
            return;
        }

        public override void Sort(List<T> pList, int pStart, int pEnd, /* Comparator<T> */ IComparer<T> pComparator)
        {
            for (int i = pStart + 1; i < pEnd; i++)
            {
                T current = pList[i];
                T prev = pList[i - 1];
                if (pComparator.Compare(current, prev) < 0)
                {
                    int j = i;
                    do
                    {
                        pList[j--] = prev;
                    } while (j > pStart && pComparator.Compare(current, prev = pList[j - 1]) < 0);
                    pList[j] = current;
                }
            }
            return;
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
namespace andengine.util
{

    //using SparseArray = Android.Util.SparseArray;
    using Android.Util;

    /**
     * @author Nicolas Gramlich
     * @since 11:51:29 - 20.08.2010
     * @param <T>
     */
    public class Library<T>
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        //protected readonly SparseArray<T> mItems;
        //protected readonly SparseArray mItems;
        //protected readonly TestSparseMatrix.SparseMatrix<T> mItems;
        protected readonly System.Collections.SparseArray mItems;

        // ===========================================================
        // Constructors
        // ===========================================================

        public Library()
        {
            //this.mItems = new SparseArray<T>();
            //this.mItems = new SparseArray();
            //this.mItems = new TestSparseMatrix.SparseMatrix<T>();
            this.mItems = new System.Collections.SparseArray(1);
        }

        public Library(int pInitialCapacity)
        {
            //this.mItems = new SparseArray<T>(pInitialCapacity);
            //this.mItems = new SparseArray(pInitialCapacity);
            //this.mItems = new TestSparseMatrix.SparseMatrix<T>();
            this.mItems = new System.Collections.SparseArray(1);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public void put(int pID, T pItem)
        {
            //T existingItem = this.mItems.get(pID);
            T existingItem = (T)this.mItems.GetValue(pID);
            if (existingItem == null)
            {
                this.mItems.SetValue(pItem, pID);
            }
            else
            {
                throw new IllegalArgumentException("ID: '" + pID + "' is already associated with item: '" + existingItem.toString() + "'.");
            }
        }

        public void remove(int pID)
        {
            this.mItems.RemoveAt(pID);
        }

        public T get(int pID)
        {
            return (T)this.mItems.GetValue(pID);
        }

        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
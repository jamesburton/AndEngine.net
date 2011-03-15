namespace andengine.entity.layer
{

    using Java.Util;
    //import java.util.Comparator;
    //import java.util.List;

    using IEntity = andengine.entity.IEntity;
    //using InsertionSorter = andengine.util.sort.InsertionSorter;
    using System.Collections.Generic;
using System;

    public class ZIndexSorter : andengine.util.sort.InsertionSorter<IEntity>
    {
        // ===========================================================
        // Constants
        // ===========================================================

        private static ZIndexSorter INSTANCE;

        // ===========================================================
        // Fields
        // ===========================================================

        /*
        public interface IComparator<T>
        {
            int compare(T pEntityA, T pEntityB);
        }
        //private final Comparator<IEntity> mZIndexComparator = new IComparator<IEntity>() {
        private sealed class ZIndexComparator : IComparator<IEntity>
        {
            public override int compare(IEntity pEntityA, IEntity pEntityB)
            {
                return pEntityA.getZIndex() - pEntityB.getZIndex();
            }
        };
        private static readonly IComparator<IEntity> mZIndexComparator = new ZIndexComparator();
        */
        private sealed class ZIndexComparator : IComparer<IEntity>
        {
            /*public  override  int Compare(Object pEntityA, Object pEntityB)
            {
                return ((IEntity)pEntityA).GetZIndex() - ((IEntity)pEntityB).GetZIndex();
            }
            public int Compare(Java.Lang.Object pEntityA, Java.Lang.Object pEntityB)
            {
                return ((IEntity)pEntityA).GetZIndex() - ((IEntity)pEntityB).GetZIndex();
            }*/

            // TODO: Work out correct Equals implemetation: public bool Equals(Java.Lang.Object pEntity) { return ???; }
            public int Compare(IEntity x, IEntity y)
            {
                return x.GetZIndex() - y.GetZIndex();
            }
        }
        private static readonly IComparer<IEntity> mZIndexComparator = new ZIndexComparator();

        // ===========================================================
        // Constructors
        // ===========================================================

        private ZIndexSorter()
        {

        }

        public static ZIndexSorter Instance { get { return GetInstance(); } }
        public static ZIndexSorter GetInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new ZIndexSorter();
            }
            return INSTANCE;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public void Sort(IEntity[] pEntities)
        {
            Sort(pEntities, mZIndexComparator);
        }

        public void Sort(IEntity[] pEntities, int pStart, int pEnd)
        {
            Sort(pEntities, pStart, pEnd, mZIndexComparator);
        }

        public void Sort(List<IEntity> pEntities)
        {
            Sort(pEntities, mZIndexComparator);
        }

        public void Sort(List<IEntity> pEntities, int pStart, int pEnd)
        {
            Sort(pEntities, pStart, pEnd, mZIndexComparator);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
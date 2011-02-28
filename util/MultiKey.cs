namespace andengine.util
{

    //import java.util.Arrays;
    using System.Collections;
    using System.Collections.Generic;
    using Java.Lang;

    public class MultiKey<K>
    {
        // ===========================================================
        // Constants
        // ===========================================================

        //private static /* final */ readonly long serialVersionUID = 4465448607415788805L;
        private static /* final */ readonly long serialVersionUID = 4565848617435788805L;

        // ===========================================================
        // Fields
        // ===========================================================

        private /* final */ readonly K[] mKeys;
        private /* final */ readonly int mCachedHashCode;

        // ===========================================================
        // Constructors
        // ===========================================================

        public MultiKey(/* final K... pKeys*/ params K[] pKeys)
        {
            this.mKeys = pKeys;

            this.mCachedHashCode = hash(pKeys);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public K[] getKeys()
        {
            return this.mKeys;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override bool equals(/* final */ object pOther)
        {
            if (pOther == this)
            {
                return true;
            }
            if (pOther is MultiKey<K>)
            {
                /* final */
                MultiKey<K> otherMultiKey = (MultiKey<K>)pOther;
                //return Arrays.equals(this.mKeys, otherMultiKey.mKeys);
                return this.mKeys.Equals(otherMultiKey.mKeys);
            }
            return false;
        }

        public static int hash(/*final Object ... pKeys*/ params object[] pKeys)
        {
            int hashCode = 0;
            //for(Object key : pKeys) {
            foreach (object key in pKeys)
            {
                if (key != null)
                {
                    hashCode ^= key.GetHashCode();
                }
            }
            return hashCode;
        }


        public override int hashCode()
        {
            return this.mCachedHashCode;
        }

        public override String ToString()
        {
            //return "MultiKey" + Arrays.asList(this.mKeys).toString();
            return "MultiKey" + new List<K>(this.mKeys).ToString();
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public K getKey(/*final*/ int pIndex)
        {
            return this.mKeys[pIndex];
        }

        public int size()
        {
            return this.mKeys.Length;
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
using System;
namespace andengine.util
{

    using andengine.util.constants/*.Constants*/;
    using Android.Util;
    //using Java.Lang;
    using String = System.String;
    using Throwable = Java.Lang.Throwable;
    using IllegalArgumentException = Java.Lang.IllegalArgumentException;

    //import android.util.Log;

    public class DebugLevel
    {
        public enum DebugValueEnum
        {
            NONE,
            ERROR,
            WARNING,
            INFO,
            DEBUG,
            VERBOSE
        }
        public static readonly DebugLevel NONE = new DebugLevel(DebugValueEnum.NONE);
        public static readonly DebugLevel ERROR = new DebugLevel(DebugValueEnum.ERROR);
        public static readonly DebugLevel WARNING = new DebugLevel(DebugValueEnum.WARNING);
        public static readonly DebugLevel INFO = new DebugLevel(DebugValueEnum.INFO);
        public static readonly DebugLevel DEBUG = new DebugLevel(DebugValueEnum.DEBUG);
        public static readonly DebugLevel VERBOSE = new DebugLevel(DebugValueEnum.VERBOSE);
        public DebugValueEnum DebugValue = DebugValueEnum.NONE;
        public DebugLevel()
        {
        }
        public DebugLevel(DebugLevel debugLevel)
        {
            Init(debugLevel.DebugValue);
        }
        public DebugLevel(DebugValueEnum DebugValue)
        {
            Init(DebugValue);
        }
        protected void Init(DebugValueEnum DebugValue)
        {
            this.DebugValue = DebugValue;
        }
        public bool IsSameOrLessThan(DebugLevel otherDebugLevel)
        {
            return IsSameOrLessThan(otherDebugLevel.DebugValue);
        }
        public bool IsSameOrLessThan(DebugValueEnum DebugValue)
        {
            return (this.DebugValue <= DebugValue);
        }
    }

    /**
     * @author Nicolas Gramlich
     * @since 13:29:16 - 08.03.2010
     */
    public class Debug : Constants
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private static DebugLevel DEBUGLEVEL = DebugLevel.VERBOSE;

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public static void SetDebugLevel(/* final */ DebugLevel pDebugLevel)
        {
            if (pDebugLevel == null)
            {
                throw new IllegalArgumentException("pDebugLevel must not be null!");
            }
            Debug.DEBUGLEVEL = pDebugLevel;
        }

        public static DebugLevel GetDebugLevel()
        {
            return Debug.DEBUGLEVEL;
        }

        public static DebugLevel DebugLevel { get { return GetDebugLevel(); } set { SetDebugLevel(value); } }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        //public static void V(System.String pMessage) { V(new Java.Lang.String(pMessage)); }
        public static void V(Java.Lang.String pMessage) { V(pMessage.ToString()); }
        public static void V(/* final */ String pMessage)
        {
            Debug.V(pMessage, null);
        }

        //public static void v(/* final */ System.String pMessage, /* final */ Throwable pThrowable) { v(new Java.Lang.String(pMessage), pThrowable); } 

        public static void V(/* final */ String pMessage, /* final */ Throwable pThrowable)
        {
            if (DEBUGLEVEL.IsSameOrLessThan(DebugLevel.VERBOSE))
            {
                //Log.Verbose(DEBUGTAG, pMessage, pThrowable);
                Log.Verbose(DEBUGTAG, pThrowable, pMessage);
            }
        }

        /*
        public static void d(System.String pMessage)
        {
            d(new Java.Lang.String(pMessage));
        } */

        public static void D(/* final */ String pMessage)
        {
            Debug.D(pMessage, null);
        }

        //public static void d(System.String pMessage, Throwable pThrowable) { Debug.d(new Java.Lang.String(pMessage), pThrowable); }
        public static void D(/* final */ String pMessage, /* final */ Throwable pThrowable)
        {
            if (DEBUGLEVEL.IsSameOrLessThan(DebugLevel.DEBUG))
            {
                Log.Debug(DEBUGTAG, pMessage, pThrowable);
            }
        }

        public static void I(/* final */ String pMessage)
        {
            Debug.I(pMessage, null);
        }

        public static void I(/* final */ String pMessage, /* final */ Throwable pThrowable)
        {
            if (DEBUGLEVEL.IsSameOrLessThan(DebugLevel.INFO))
            {
                Log.Info(DEBUGTAG, pMessage, pThrowable);
            }
        }

        public static void W(/* final */ String pMessage)
        {
            Debug.W(pMessage, null);
        }

        public static void W(/* final */ Throwable pThrowable)
        {
            Debug.W(DEBUGTAG, pThrowable);
        }

        public static void W(/* final */ String pMessage, /* final */ Throwable pThrowable)
        {
            if (DEBUGLEVEL.IsSameOrLessThan(DebugLevel.WARNING))
            {
                if (pThrowable == null)
                {
                    Log.Warn(DEBUGTAG, pMessage, new Exception());
                }
                else
                {
                    Log.Warn(DEBUGTAG, pMessage, pThrowable);
                }
            }
        }

        public static void E(/* final */ String pMessage)
        {
            Debug.E(pMessage, null);
        }

        public static void E(/* final */ Throwable pThrowable)
        {
            Debug.E(DEBUGTAG, pThrowable);
        }

        public static void E(/* final */ String pMessage, /* final */ Throwable pThrowable)
        {
            if (DEBUGLEVEL.IsSameOrLessThan(DebugLevel.ERROR))
            {
                if (pThrowable == null)
                {
                    Log.Error(DEBUGTAG, pMessage, new Exception());
                }
                else
                {
                    Log.Error(DEBUGTAG, pMessage, pThrowable);
                }
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        /*
            //public static enum DebugLevel : Comparable<DebugLevel> {
            public static enum DebugLevel : IComparable<DebugLevel> {
                NONE,
                ERROR,
                WARNING,
                INFO,
                DEBUG,
                VERBOSE;

                public static DebugLevel ALL = DebugLevel.VERBOSE;

                private bool isSameOrLessThan(final DebugLevel pDebugLevel) {
                    return this.compareTo(pDebugLevel) >= 0;
                }
            }
         //*/
    }
}
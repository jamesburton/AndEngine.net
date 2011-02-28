using System;
namespace andengine.util
{

    using andengine.util.constants/*.Constants*/;
    using Android.Util;
    using Java.Lang;

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
            init(debugLevel.DebugValue);
        }
        public DebugLevel(DebugValueEnum DebugValue)
        {
            init(DebugValue);
        }
        protected void init(DebugValueEnum DebugValue)
        {
            this.DebugValue = DebugValue;
        }
        public bool isSameOrLessThan(DebugLevel otherDebugLevel)
        {
            return isSameOrLessThan(otherDebugLevel.DebugValue);
        }
        public bool isSameOrLessThan(DebugValueEnum DebugValue)
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

        public static void setDebugLevel(/* final */ DebugLevel pDebugLevel)
        {
            if (pDebugLevel == null)
            {
                throw new IllegalArgumentException("pDebugLevel must not be null!");
            }
            Debug.DEBUGLEVEL = pDebugLevel;
        }

        public static DebugLevel getDebugLevel()
        {
            return Debug.DEBUGLEVEL;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public static void v(System.String pMessage) { v(new Java.Lang.String(pMessage)); }
        public static void v(/* final */ String pMessage)
        {
            Debug.v(pMessage, null);
        }

        public static void v(/* final */ System.String pMessage, /* final */ Throwable pThrowable)
        { v(new Java.Lang.String(pMessage), pThrowable); }
        public static void v(/* final */ String pMessage, /* final */ Throwable pThrowable)
        {
            if (DEBUGLEVEL.isSameOrLessThan(DebugLevel.VERBOSE))
            {
                //Log.Verbose(DEBUGTAG, pMessage, pThrowable);
                Log.Verbose(DEBUGTAG, pThrowable, pMessage);
            }
        }

        public static void d(System.String pMessage)
        {
            d(new Java.Lang.String(pMessage));
        }

        public static void d(/* final */ String pMessage)
        {
            Debug.d(pMessage, null);
        }

        public static void d(/* final */ String pMessage, /* final */ Throwable pThrowable)
        {
            if (DEBUGLEVEL.isSameOrLessThan(DebugLevel.DEBUG))
            {
                Log.Debug(DEBUGTAG, pMessage, pThrowable);
            }
        }

        public static void i(/* final */ String pMessage)
        {
            Debug.i(pMessage, null);
        }

        public static void i(/* final */ String pMessage, /* final */ Throwable pThrowable)
        {
            if (DEBUGLEVEL.isSameOrLessThan(DebugLevel.INFO))
            {
                Log.i(DEBUGTAG, pMessage, pThrowable);
            }
        }

        public static void w(/* final */ String pMessage)
        {
            Debug.w(pMessage, null);
        }

        public static void w(/* final */ Throwable pThrowable)
        {
            Debug.w(DEBUGTAG, pThrowable);
        }

        public static void w(/* final */ String pMessage, /* final */ Throwable pThrowable)
        {
            if (DEBUGLEVEL.isSameOrLessThan(DebugLevel.WARNING))
            {
                if (pThrowable == null)
                {
                    Log.w(DEBUGTAG, pMessage, new Exception());
                }
                else
                {
                    Log.w(DEBUGTAG, pMessage, pThrowable);
                }
            }
        }

        public static void e(/* final */ String pMessage)
        {
            Debug.e(pMessage, null);
        }

        public static void e(/* final */ Throwable pThrowable)
        {
            Debug.e(DEBUGTAG, pThrowable);
        }

        public static void e(/* final */ String pMessage, /* final */ Throwable pThrowable)
        {
            if (DEBUGLEVEL.isSameOrLessThan(DebugLevel.ERROR))
            {
                if (pThrowable == null)
                {
                    Log.e(DEBUGTAG, pMessage, new Exception());
                }
                else
                {
                    Log.e(DEBUGTAG, pMessage, pThrowable);
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
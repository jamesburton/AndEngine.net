namespace andengine.util.modifier.util
{

    //using IModifier = andengine.util.modifier.IModifier;
    using andengine.util.modifier;
    //using IModifier<T> = andengine.util.modifier.IModifier<T>;

    /**
     * @author Nicolas Gramlich
     * @since 11:16:36 - 03.09.2010
     */
    public class ModifierUtils
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

        // ===========================================================
        // Methods
        // ===========================================================

        public static IModifier<T> getModifierWithLongestDuration<T>(/* final */ IModifier<T>[] pModifiers)
        {
            IModifier<T> retVal = null;
            float longestDuration = float.MinValue;

            for (int i = pModifiers.Length - 1; i >= 0; i--)
            {
                /* final */
                float duration = pModifiers[i].Duration;
                if (duration > longestDuration)
                {
                    longestDuration = duration;
                    retVal = pModifiers[i];
                }
            }

            return retVal;
        }

        //public static float getSequenceDurationOfModifier(/* final */ IModifier<?>[] pModifiers){
        public static float getSequenceDurationOfModifier<T>(/* final */ IModifier<T>[] pModifiers)
        {
            float duration = float.MinValue;

            for (int i = pModifiers.Length - 1; i >= 0; i--)
            {
                duration += pModifiers[i].Duration;
            }

            return duration;
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
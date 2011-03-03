using Java.Lang;
namespace andengine.util.modifier
{


    /**
     * @author Nicolas Gramlich
     * @since 11:17:50 - 19.03.2010
     */
    public interface IModifier<T> : ICloneable
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        /* public */ void Reset();

        bool Finished { get; }
        //* public */ bool IsFinished();
        bool RemoveWhenFinished { get; set; }
        //* public */ bool isRemoveWhenFinished();
        //* public */ void setRemoveWhenFinished(/* final */ bool pRemoveWhenFinished);

        /* public */ IModifier<T> Clone();

        float Duration { get; }
        //* public */ float getDuration();

        /* public */ void OnUpdate(/* final */ float pSecondsElapsed, /* final */ T pItem);

        IModifierListener<T> ModifierListener { get; set; }
        //* public */ IModifierListener<T> getModifierListener();
        //* public */ void setModifierListener(/* final */ IModifierListener<T> pModifierListener);

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        /*
        public static interface IModifierListener<T>
        {
            // ===========================================================
            // Final Fields
            // ===========================================================

            // ===========================================================
            // Methods
            // ===========================================================

            public void onModifierFinished(final IModifier<T> pModifier, final T pItem);
        }
        */
    }

    // Non inner version of IModifierListener<T>:-
    public interface IModifierListener<T>
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        /* public */ void OnModifierFinished(/* final */ IModifier<T> pModifier, /* final */ T pItem);
    }
}
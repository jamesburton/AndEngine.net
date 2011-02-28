namespace andengine.util.modifier
{


    /**
     * @author Nicolas Gramlich
     * @since 10:47:23 - 03.09.2010
     * @param <T>
     */
    public abstract class BaseModifier<T> : IModifier<T>
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        protected bool mFinished;
        private bool mRemoveWhenFinished = true;
        protected IModifierListener<T> mModifierListener;

        // ===========================================================
        // Constructors
        // ===========================================================

        public BaseModifier()
        {
            this.mModifierListener = ((IModifierListener<T>)null);
        }

        public BaseModifier(IModifierListener<T> pModifierListener)
        {
            this.mModifierListener = pModifierListener;
        }

        protected BaseModifier(BaseModifier<T> pBaseModifier)
        {
            this.mModifierListener = pBaseModifier.mModifierListener;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override bool isFinished()
        {
            return this.mFinished;
        }

        public override sealed bool isRemoveWhenFinished()
        {
            return this.mRemoveWhenFinished;
        }

        public /* final */ void setRemoveWhenFinished(bool pRemoveWhenFinished)
        {
            this.mRemoveWhenFinished = pRemoveWhenFinished;
        }

        public IModifierListener<T> getModifierListener()
        {
            return this.mModifierListener;
        }

        public void setModifierListener(IModifierListener<T> pModifierListener)
        {
            this.mModifierListener = pModifierListener;
        }

        public override abstract IModifier<T> clone();

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
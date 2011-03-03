namespace andengine.util.modifier
{


    /**
     * @author Nicolas Gramlich
     * @since 10:47:23 - 03.09.2010
     * @param <T>
     */
    public abstract class BaseModifier<T> : IModifier<T>
    {
        // TODO: Check having add the line below to fulfill the interface requirements from Java.Runtime.IJavaObject
        //public System.IntPtr Handle;
        System.IntPtr Android.Runtime.IJavaObject.Handle
        {
            get
            {
                //return base.Android.Runtime.IJavaObject.Handle;
                return this.Handle;
            }
        }
        public System.IntPtr Handle;
        public abstract void Reset();
        void IModifier<T>.Reset() { this.Reset(); }
        public abstract float Duration { get; }
        float IModifier<T>.Duration { get { return this.Duration; } }
        public abstract void OnUpdate(float pSecondsElapsed, T pItem);
        void IModifier<T>.OnUpdate(float pSecondsElapsed, T pItem) { this.OnUpdate(pSecondsElapsed, pItem); }

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

        public bool Finished { get { return this.mFinished; } }
        /*
        public override bool IsFinished()
        {
            return this.mFinished;
        }
        //*/

        public bool RemoveWhenFinished { get { return this.mRemoveWhenFinished; } set { this.mRemoveWhenFinished = value; } }
        /*
        public override sealed bool IsRemoveWhenFinished()
        {
            return this.mRemoveWhenFinished;
        }

        public /* final * / void SetRemoveWhenFinished(bool pRemoveWhenFinished)
        {
            this.mRemoveWhenFinished = pRemoveWhenFinished;
        }
        //*/

        public IModifierListener<T> ModifierListener { get { return mModifierListener; } set { mModifierListener = value; } }
        /*
        public IModifierListener<T> GetModifierListener()
        {
            return this.mModifierListener;
        }

        public void setModifierListener(IModifierListener<T> pModifierListener)
        {
            this.mModifierListener = pModifierListener;
        }
        //*/

        public /* override */ abstract IModifier<T> Clone();

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
namespace andengine.entity.scene.background
{
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    //using andengine.util.modifier.IModifier;
    //using andengine.util.modifier.ModifierList;

    using andengine.util.modifier;

    /**
     * @author Nicolas Gramlich
     * @since 14:08:17 - 19.07.2010
     */
    public abstract class BaseBackground : IBackground
    {
        #region pass-thru abstract method stubs for interface support
        public abstract void SetColor(float pRed, float pGreen, float pBlue);
        public abstract void SetColor(float pRed, float pGreen, float pBlue, float pAlpha);

        public abstract void OnDraw(GL10 gl10, andengine.engine.camera.Camera camera);
        #endregion

        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        //private /* final */ readonly ModifierList<IBackground> mBackgroundModifiers = new ModifierList<IBackground>(this);
        private /* final */ readonly ModifierList<IBackground> mBackgroundModifiers;

        // ===========================================================
        // Constructors
        // ===========================================================
        public BaseBackground() { mBackgroundModifiers = new ModifierList<IBackground>(this); }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public /* override */ virtual void AddBackgroundModifier(IModifier<IBackground> pBackgroundModifier)
        {
            this.mBackgroundModifiers.Add(pBackgroundModifier);
        }

        public /* override */ virtual bool RemoveBackgroundModifier(IModifier<IBackground> pBackgroundModifier)
        {
            return this.mBackgroundModifiers.Remove(pBackgroundModifier);
        }

        public /* override */ virtual void ClearBackgroundModifiers()
        {
            this.mBackgroundModifiers.Clear();
        }

        public /* override */ virtual void OnUpdate(float pSecondsElapsed)
        {
            this.mBackgroundModifiers.OnUpdate(pSecondsElapsed);
        }

        public /* override */ virtual void Reset()
        {
            this.mBackgroundModifiers.Reset();
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
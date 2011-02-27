namespace andengine.entity.scene.background
{

    //using andengine.util.modifier.IModifier;
    //using andengine.util.modifier.ModifierList;

    using andengine.util.modifier;

    /**
     * @author Nicolas Gramlich
     * @since 14:08:17 - 19.07.2010
     */
    public abstract class BaseBackground : IBackground
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private /* final */ readonly ModifierList<IBackground> mBackgroundModifiers = new ModifierList<IBackground>(this);

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override void addBackgroundModifier(IModifier<IBackground> pBackgroundModifier)
        {
            this.mBackgroundModifiers.Add pBackgroundModifier);
        }

        public override bool removeBackgroundModifier(IModifier<IBackground> pBackgroundModifier)
        {
            return this.mBackgroundModifiers.Remove(pBackgroundModifier);
        }

        public override void clearBackgroundModifiers()
        {
            this.mBackgroundModifiers.Clear();
        }

        public override void onUpdate(float pSecondsElapsed)
        {
            this.mBackgroundModifiers.onUpdate(pSecondsElapsed);
        }

        public override void reset()
        {
            this.mBackgroundModifiers.reset();
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
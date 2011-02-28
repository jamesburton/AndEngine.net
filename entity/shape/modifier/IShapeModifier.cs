namespace andengine.entity.shape.modifier
{

    using IShape = andengine.entity.shape.IShape;
    //using IModifier = andengine.util.modifier.IModifier;

    /**
     * @author Nicolas Gramlich
     * @since 11:17:50 - 19.03.2010
     */
    public interface IShapeModifier : andengine.util.modifier.IModifier<IShape>
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        /*
        public static interface IShapeModifierListener : IModifierListener<IShape>{
            // ===========================================================
            // Final Fields
            // ===========================================================

            // ===========================================================
            // Methods
            // ===========================================================
        }
        */
    }

    // NB: Moved from inside above interface
    public /* static */ interface IShapeModifierListener : andengine.util.modifier.IModifierListener<IShape>
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================
    }
}
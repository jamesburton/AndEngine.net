namespace andengine.entity.shape.modifier
{

    using IShape = andengine.entity.shape.IShape;
    //using BaseDoubleValueSpanModifier = andengine.util.modifier.BaseDoubleValueSpanModifier;
    using IEaseFunction = andengine.util.modifier.ease.IEaseFunction;

    /**
     * @author Nicolas Gramlich
     * @since 23:29:22 - 19.03.2010
     */
    public abstract class DoubleValueSpanShapeModifier
        : andengine.util.modifier.BaseDoubleValueSpanModifier<IShape>, IShapeModifier
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

        public DoubleValueSpanShapeModifier(float pDuration, float pFromValueA, float pToValueA, float pFromValueB, float pToValueB)
            : base(pDuration, pFromValueA, pToValueA, pFromValueB, pToValueB)
        {
        }

        public DoubleValueSpanShapeModifier(float pDuration, float pFromValueA, float pToValueA, float pFromValueB, float pToValueB, IEaseFunction pEaseFunction)
            : base(pDuration, pFromValueA, pToValueA, pFromValueB, pToValueB, pEaseFunction)
        {
        }

        public DoubleValueSpanShapeModifier(float pDuration, float pFromValueA, float pToValueA, float pFromValueB, float pToValueB, IShapeModifierListener pShapeModifierListener)
            : base(pDuration, pFromValueA, pToValueA, pFromValueB, pToValueB, pShapeModifierListener)
        {
        }

        public DoubleValueSpanShapeModifier(float pDuration, float pFromValueA, float pToValueA, float pFromValueB, float pToValueB, IShapeModifierListener pShapeModifierListener, IEaseFunction pEaseFunction)
            : base(pDuration, pFromValueA, pToValueA, pFromValueB, pToValueB, pShapeModifierListener, pEaseFunction)
        {
        }

        protected DoubleValueSpanShapeModifier(DoubleValueSpanShapeModifier pDoubleValueSpanModifier)
            : base(pDoubleValueSpanModifier)
        {
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
namespace andengine.entity.shape.modifier
{

    using IShape = andengine.entity.shape.IShape;
    using IEaseFunction = andengine.util.modifier.ease.IEaseFunction;

    /**
     * @author Nicolas Gramlich
     * @since 23:37:53 - 19.03.2010
     */
    public class ScaleModifier : DoubleValueSpanShapeModifier
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

        public ScaleModifier(float pDuration, float pFromScale, float pToScale)
            : this(pDuration, pFromScale, pToScale, null, IEaseFunction.DEFAULT)
        {
        }

        public ScaleModifier(float pDuration, float pFromScale, float pToScale, IEaseFunction pEaseFunction)
            : this(pDuration, pFromScale, pToScale, null, pEaseFunction)
        {
        }

        public ScaleModifier(float pDuration, float pFromScale, float pToScale, IShapeModifierListener pShapeModifierListener)
            : this(pDuration, pFromScale, pToScale, pFromScale, pToScale, pShapeModifierListener, IEaseFunction.DEFAULT)
        {
        }

        public ScaleModifier(float pDuration, float pFromScale, float pToScale, IShapeModifierListener pShapeModifierListener, IEaseFunction pEaseFunction)
            : this(pDuration, pFromScale, pToScale, pFromScale, pToScale, pShapeModifierListener, pEaseFunction)
        {
        }

        public ScaleModifier(float pDuration, float pFromScaleX, float pToScaleX, float pFromScaleY, float pToScaleY)
            : this(pDuration, pFromScaleX, pToScaleX, pFromScaleY, pToScaleY, null, IEaseFunction.DEFAULT)
        {
        }

        public ScaleModifier(float pDuration, float pFromScaleX, float pToScaleX, float pFromScaleY, float pToScaleY, IEaseFunction pEaseFunction)
            : this(pDuration, pFromScaleX, pToScaleX, pFromScaleY, pToScaleY, null, pEaseFunction)
        {
        }

        public ScaleModifier(float pDuration, float pFromScaleX, float pToScaleX, float pFromScaleY, float pToScaleY, IShapeModifierListener pShapeModifierListener)
            : base(pDuration, pFromScaleX, pToScaleX, pFromScaleY, pToScaleY, pShapeModifierListener, IEaseFunction.DEFAULT)
        {
        }

        public ScaleModifier(float pDuration, float pFromScaleX, float pToScaleX, float pFromScaleY, float pToScaleY, IShapeModifierListener pShapeModifierListener, IEaseFunction pEaseFunction)
            : base(pDuration, pFromScaleX, pToScaleX, pFromScaleY, pToScaleY, pShapeModifierListener, pEaseFunction)
        {
        }

        protected ScaleModifier(ScaleModifier pScaleModifier)
            : base(pScaleModifier)
        {
        }

        public /* ScaleModifier */ override andengine.util.modifier.IModifier<IShape> Clone()
        {
            return new ScaleModifier(this);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected override void OnSetInitialValues(IShape pShape, float pScaleA, float pScaleB)
        {
            pShape.SetScale(pScaleA, pScaleB);
        }

        protected override void OnSetValues(IShape pShape, float pPercentageDone, float pScaleA, float pScaleB)
        {
            pShape.SetScale(pScaleA, pScaleB);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
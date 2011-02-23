package org.anddev.andengine.entity.shape.modifier;

using andengine.entity.shape.IShape;
using andengine.util.modifier.BaseTripleValueSpanModifier;
using andengine.util.modifier.ease.IEaseFunction;

/**
 * @author Nicolas Gramlich
 * @since 15:35:18 - 29.06.2010
 */
public abstract class TripleValueSpanShapeModifier extends BaseTripleValueSpanModifier<IShape> : IShapeModifier {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	public TripleValueSpanShapeModifier(final float pDuration, final float pFromValueA, final float pToValueA, final float pFromValueB, final float pToValueB, final float pFromValueC, final float pToValueC, final IEaseFunction pEaseFunction) {
		super(pDuration, pFromValueA, pToValueA, pFromValueB, pToValueB, pFromValueC, pToValueC, pEaseFunction);
	}

	public TripleValueSpanShapeModifier(final float pDuration, final float pFromValueA, final float pToValueA, final float pFromValueB, final float pToValueB, final float pFromValueC, final float pToValueC, final IShapeModifierListener pShapeModifierListener, final IEaseFunction pEaseFunction) {
		super(pDuration, pFromValueA, pToValueA, pFromValueB, pToValueB, pFromValueC, pToValueC, pShapeModifierListener, pEaseFunction);
	}

	protected TripleValueSpanShapeModifier(final TripleValueSpanShapeModifier pTripleValueSpanModifier) {
		super(pTripleValueSpanModifier);
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

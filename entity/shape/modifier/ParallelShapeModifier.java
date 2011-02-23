package org.anddev.andengine.entity.shape.modifier;

using andengine.entity.shape.IShape;
using andengine.util.modifier.ParallelModifier;

/**
 * @author Nicolas Gramlich
 * @since 12:40:31 - 03.09.2010
 */
public class ParallelShapeModifier extends ParallelModifier<IShape> : IShapeModifier {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	public ParallelShapeModifier(final IShapeModifier... pShapeModifiers) throws IllegalArgumentException {
		super(pShapeModifiers);
	}

	public ParallelShapeModifier(final IShapeModifierListener pShapeModifierListener, final IShapeModifier... pShapeModifiers) throws IllegalArgumentException {
		super(pShapeModifierListener, pShapeModifiers);
	}

	protected ParallelShapeModifier(final ParallelShapeModifier pParallelShapeModifier) {
		super(pParallelShapeModifier);
	}

	@Override
	public ParallelShapeModifier clone() {
		return new ParallelShapeModifier(this);
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

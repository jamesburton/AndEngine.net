package org.anddev.andengine.entity.shape.modifier;

using andengine.entity.shape.IShape;
using andengine.util.modifier.ease.IEaseFunction;


/**
 * @author Nicolas Gramlich
 * @since 21:59:38 - 06.07.2010
 */
public class RotationAtModifier extends RotationModifier {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	private final float mRotationCenterX;
	private final float mRotationCenterY;

	// ===========================================================
	// Constructors
	// ===========================================================

	public RotationAtModifier(final float pDuration, final float pFromRotation, final float pToRotation, final float pRotationCenterX, final float pRotationCenterY) {
		super(pDuration, pFromRotation, pToRotation, IEaseFunction.DEFAULT);
		this.mRotationCenterX = pRotationCenterX;
		this.mRotationCenterY = pRotationCenterY;
	}

	public RotationAtModifier(final float pDuration, final float pFromRotation, final float pToRotation, final float pRotationCenterX, final float pRotationCenterY, final IEaseFunction pEaseFunction) {
		super(pDuration, pFromRotation, pToRotation, pEaseFunction);
		this.mRotationCenterX = pRotationCenterX;
		this.mRotationCenterY = pRotationCenterY;
	}

	public RotationAtModifier(final float pDuration, final float pFromRotation, final float pToRotation, final float pRotationCenterX, final float pRotationCenterY, final IShapeModifierListener pShapeModifierListener) {
		super(pDuration, pFromRotation, pToRotation, pShapeModifierListener, IEaseFunction.DEFAULT);
		this.mRotationCenterX = pRotationCenterX;
		this.mRotationCenterY = pRotationCenterY;
	}

	public RotationAtModifier(final float pDuration, final float pFromRotation, final float pToRotation, final float pRotationCenterX, final float pRotationCenterY, final IShapeModifierListener pShapeModifierListener, final IEaseFunction pEaseFunction) {
		super(pDuration, pFromRotation, pToRotation, pShapeModifierListener, pEaseFunction);
		this.mRotationCenterX = pRotationCenterX;
		this.mRotationCenterY = pRotationCenterY;
	}

	protected RotationAtModifier(final RotationAtModifier pRotationAtModifier) {
		super(pRotationAtModifier);
		this.mRotationCenterX = pRotationAtModifier.mRotationCenterX;
		this.mRotationCenterY = pRotationAtModifier.mRotationCenterY;
	}

	@Override
	public RotationAtModifier clone(){
		return new RotationAtModifier(this);
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	protected void onManagedInitialize(final IShape pShape) {
		super.onManagedInitialize(pShape);
		pShape.setRotationCenter(this.mRotationCenterX, this.mRotationCenterY);
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}

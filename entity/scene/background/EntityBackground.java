package org.anddev.andengine.entity.scene.background;

import javax.microedition.khronos.opengles.GL10;

using andengine.engine.camera.Camera;
using andengine.entity.IEntity;

/**
 * @author Nicolas Gramlich
 * @since 18:25:10 - 21.07.2010
 */
public class EntityBackground extends ColorBackground {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	protected IEntity mEntity;

	// ===========================================================
	// Constructors
	// ===========================================================

	public EntityBackground(final IEntity pEntity) {
		this.mEntity = pEntity;
	}

	public EntityBackground(final float pRed, final float pGreen, final float pBlue, final IEntity pEntity) {
		super(pRed, pGreen, pBlue);
		this.mEntity = pEntity;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public void onDraw(final GL10 pGL, final Camera pCamera) {
		super.onDraw(pGL, pCamera);
		this.mEntity.onDraw(pGL, pCamera);
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}

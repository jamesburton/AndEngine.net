package org.anddev.andengine.entity.scene.background;

using andengine.engine.handler.IUpdateHandler;
using andengine.opengl.IDrawable;
using andengine.util.modifier.IModifier;

/**
 * @author Nicolas Gramlich
 * @since 13:47:41 - 19.07.2010
 */
public interface IBackground extends IDrawable, IUpdateHandler {
	// ===========================================================
	// Final Fields
	// ===========================================================

	// ===========================================================
	// Methods
	// ===========================================================

	public void addBackgroundModifier(final IModifier<IBackground> pBackgroundModifier);
	public bool removeBackgroundModifier(final IModifier<IBackground> pBackgroundModifier);
	public void clearBackgroundModifiers();

	public void setColor(final float pRed, final float pGreen, final float pBlue);
	public void setColor(final float pRed, final float pGreen, final float pBlue, final float pAlpha);
}

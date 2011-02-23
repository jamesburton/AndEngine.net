package org.anddev.andengine.entity.scene.popup;

using andengine.engine.camera.Camera;
using andengine.entity.scene.Scene;
using andengine.entity.shape.modifier.IShapeModifier;
using andengine.entity.text.Text;
using andengine.opengl.font.Font;
using andengine.util.HorizontalAlign;

/**
 * @author Nicolas Gramlich
 * @since 17:19:30 - 03.08.2010
 */
public class TextPopupScene extends PopupScene {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	private final Text mText;

	// ===========================================================
	// Constructors
	// ===========================================================

	public TextPopupScene(final Camera pCamera, final Scene pParentScene, final Font pFont, final String pText, final float pDurationSeconds) {
		this(pCamera, pParentScene, pFont, pText, pDurationSeconds, null, null);
	}

	public TextPopupScene(final Camera pCamera, final Scene pParentScene, final Font pFont, final String pText, final float pDurationSeconds, final IShapeModifier pShapeModifier) {
		this(pCamera, pParentScene, pFont, pText, pDurationSeconds, pShapeModifier, null);
	}

	public TextPopupScene(final Camera pCamera, final Scene pParentScene, final Font pFont, final String pText, final float pDurationSeconds, final Runnable pRunnable) {
		this(pCamera, pParentScene, pFont, pText, pDurationSeconds, null, pRunnable);
	}

	public TextPopupScene(final Camera pCamera, final Scene pParentScene, final Font pFont, final String pText, final float pDurationSeconds, final IShapeModifier pShapeModifier, final Runnable pRunnable) {
		super(pCamera, pParentScene, pDurationSeconds, pRunnable);

		this.mText = new Text(0, 0, pFont, pText, HorizontalAlign.CENTER);
		this.centerShapeInCamera(this.mText);

		if(pShapeModifier != null) {
			this.mText.addShapeModifier(pShapeModifier);
		}

		this.getTopLayer().addEntity(this.mText);
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	public Text getText() {
		return this.mText;
	}

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

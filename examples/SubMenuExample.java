namespace andengine.examples {

import org.anddev.andengine.entity.scene.menu.MenuScene;
import org.anddev.andengine.entity.scene.menu.animator.SlideMenuAnimator;
import org.anddev.andengine.entity.scene.menu.item.IMenuItem;
import org.anddev.andengine.entity.scene.menu.item.SpriteMenuItem;
using Texture = andengine.opengl.texture.Texture;
using TextureOptions = andengine.opengl.texture.TextureOptions;
using TextureRegion = andengine.opengl.texture.region.TextureRegion;
using TextureRegionFactory = andengine.opengl.texture.region.TextureRegionFactory;


/**
 * @author Nicolas Gramlich
 * @since 11:33:33 - 01.04.2010
 */
public class SubMenuExample : MenuExample {
	// ===========================================================
	// Constants
	// ===========================================================

	private static final int MENU_QUIT_OK = MenuExample.MENU_QUIT + 1;
	private static final int MENU_QUIT_BACK = MENU_QUIT_OK + 1;

	// ===========================================================
	// Fields
	// ===========================================================

	private MenuScene mSubMenuScene;

	private Texture mSubMenuTexture;
	private TextureRegion mMenuOkTextureRegion;
	private TextureRegion mMenuBackTextureRegion;

	// ===========================================================
	// Constructors
	// ===========================================================

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public void onLoadResources() {
		super.onLoadResources();
		this.mSubMenuTexture = new Texture(256, 128, TextureOptions.BILINEAR_PREMULTIPLYALPHA);

		this.mMenuOkTextureRegion = TextureRegionFactory.createFromAsset(this.mSubMenuTexture, this, "gfx/menu_ok.png", 0, 0);
		this.mMenuBackTextureRegion = TextureRegionFactory.createFromAsset(this.mSubMenuTexture, this, "gfx/menu_back.png", 0, 50);

		this.mEngine.getTextureManager().loadTexture(this.mSubMenuTexture);
	}

	@Override
	protected void createMenuScene() {
		super.createMenuScene();

		this.mSubMenuScene = new MenuScene(this.mCamera);
		this.mSubMenuScene.addMenuItem(new SpriteMenuItem(MENU_QUIT_OK, this.mMenuOkTextureRegion));
		this.mSubMenuScene.addMenuItem(new SpriteMenuItem(MENU_QUIT_BACK, this.mMenuBackTextureRegion));
		this.mSubMenuScene.setMenuAnimator(new SlideMenuAnimator());
		this.mSubMenuScene.buildAnimations();

		this.mSubMenuScene.setBackgroundEnabled(false);

		this.mSubMenuScene.setOnMenuItemClickListener(this);
	}

	@Override
	public boolean onMenuItemClicked(final MenuScene pMenuScene, final IMenuItem pMenuItem, final float pMenuItemLocalX, final float pMenuItemLocalY) {
		switch(pMenuItem.getID()) {
			case MENU_RESET:
				this.mMainScene.reset();

				this.mMenuScene.back();
				return true;
			case MENU_QUIT:
				pMenuScene.setChildSceneModal(this.mSubMenuScene);
				return true;
			case MENU_QUIT_BACK:
				this.mSubMenuScene.back();
				return true;
			case MENU_QUIT_OK:
				this.finish();
				return true;
			default:
				return false;
		}
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}

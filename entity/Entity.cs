using System;
using System.Collections.Generic;
using andengine.engine.handler;
using andengine.entity.layer;
using andengine.entity.modifier;
using andengine.util;
using andengine.util.constants;

namespace andengine.entity
{

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;

    using Camera = andengine.engine.camera.Camera;


    /**
     * @author Nicolas Gramlich
     * @since 12:00:48 - 08.03.2010
     */
    public abstract class Entity : IEntity
    {
	    // ===========================================================
	    // Constants
	    // ===========================================================

	    private static int CHILDREN_CAPACITY_DEFAULT = 4;
	    private static int ENTITYMODIFIERS_CAPACITY_DEFAULT = 4;
	    private static int UPDATEHANDLERS_CAPACITY_DEFAULT = 4;

	    private static float[] VERTICES_SCENE_TO_LOCAL_TMP = new float[2];
	    private static float[] VERTICES_LOCAL_TO_SCENE_TMP = new float[2];

        private static readonly ParameterCallable<IEntity> PARAMETERCALLABLE_DETACHCHILD = new ParameterCallableDetachChild();
        private class ParameterCallableDetachChild: ParameterCallable<IEntity> {
		    public void call(IEntity pEntity) {
			    pEntity.setParent(null);
			    pEntity.onDetached();
		    }
	    };

        // ===========================================================
        // Fields
        // ===========================================================

	    protected bool mVisible = true;
	    protected bool mIgnoreUpdate = false;

	    protected int mZIndex = 0;

	    private IEntity mParent;

	    protected SmartList<IEntity> mChildren;
	    private EntityModifierList mEntityModifiers;
	    private UpdateHandlerList mUpdateHandlers;

	    protected float mRed = 1f;
	    protected float mGreen = 1f;
	    protected float mBlue = 1f;
	    protected float mAlpha = 1f;

	    protected float mX;
	    protected float mY;

	    private float mInitialX;
	    private float mInitialY;

	    protected float mRotation = 0;

	    protected float mRotationCenterX = 0;
	    protected float mRotationCenterY = 0;

	    protected float mScaleX = 1f;
	    protected float mScaleY = 1f;

	    protected float mScaleCenterX = 0;
	    protected float mScaleCenterY = 0;

	    private Transformation mLocalToSceneTransformation = new Transformation();
	    private Transformation mSceneToLocalTransformation = new Transformation();

	    private Object mUserData;

        // ===========================================================
        // Constructors
        // ===========================================================

        public Entity()
        {

        }

        public Entity(/* final */ int pZIndex)
        {
            this.mZIndex = pZIndex;
        }

        public Entity(float pX, float pY)
        {
            this.mInitialX = pX;
            this.mInitialY = pY;

            this.mX = pX;
            this.mY = pY;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public bool IsVisible()
        {
            return this.mVisible;
        }

        public void SetVisible(/* final */ bool pVisible)
        {
            this.mVisible = pVisible;
        }

        public bool Visible { get { return IsVisible(); } set { SetVisible(value); } }

        public bool IsIgnoreUpdate()
        {
            return this.mIgnoreUpdate;
        }

        public void SetIgnoreUpdate(/* final */ bool pIgnoreUpdate)
        {
            this.mIgnoreUpdate = pIgnoreUpdate;
        }

        public bool IgnoreUpdate { get { return IsIgnoreUpdate(); } set { SetIgnoreUpdate(value); } }

        public IEntity getParent()
        {
            return this.mParent;
        }

        public void setParent(IEntity pEntity)
        {
            this.mParent = pEntity;
        }

        public IEntity Parent { get { return getParent(); } set { setParent(value);} }

        public /* override */ virtual int GetZIndex()
        {
            return this.mZIndex;
        }

        public /* override */ virtual void SetZIndex(/* final */ int pZIndex)
        {
            this.mZIndex = pZIndex;
        }

        public int ZIndex { get { return GetZIndex(); } set { SetZIndex(value); } }

	
	public float getX() {
		return this.mX;
	}


    public float X { get { return getX(); } }
    
        public float getY()
    {
		return this.mY;
	}

        public float Y { get { return getY(); } }

	public float getInitialX() {
		return this.mInitialX;
	}
    public float InitialX { get { return getInitialX(); } }
	
	public float getInitialY() {
		return this.mInitialY;
	}
    public float InitialY { get { return getInitialY(); } }
	
	public void setPosition(IEntity pOtherEntity) {
		this.setPosition(pOtherEntity.getX(), pOtherEntity.getY());
	}

	
	public void setPosition(float pX, float pY) {
		this.mX = pX;
		this.mY = pY;
	}

	
	public void setInitialPosition() {
		this.mX = this.mInitialX;
		this.mY = this.mInitialY;
	}

	
	public float getRotation() {
		return this.mRotation;
	}

	
	public void setRotation(float pRotation) {
		this.mRotation = pRotation;
	}
    public float Rotation { get { return getRotation(); } set { setRotation(value);} }
	
	public float getRotationCenterX() {
		return this.mRotationCenterX;
	}

	
	public float getRotationCenterY() {
		return this.mRotationCenterY;
	}

	
	public void setRotationCenterX(float pRotationCenterX) {
		this.mRotationCenterX = pRotationCenterX;
	}

	
	public void setRotationCenterY(float pRotationCenterY) {
		this.mRotationCenterY = pRotationCenterY;
	}

	
	public void setRotationCenter(float pRotationCenterX, float pRotationCenterY) {
		this.mRotationCenterX = pRotationCenterX;
		this.mRotationCenterY = pRotationCenterY;
	}

	
	public bool isScaled() {
		return this.mScaleX != 1 || this.mScaleY != 1;
	}

	
	public float getScaleX() {
		return this.mScaleX;
	}

	
	public float getScaleY() {
		return this.mScaleY;
	}

	
	public void setScaleX(float pScaleX) {
		this.mScaleX = pScaleX;
	}

	
	public void setScaleY(float pScaleY) {
		this.mScaleY = pScaleY;
	}

	
	public void setScale(float pScale) {
		this.mScaleX = pScale;
		this.mScaleY = pScale;
	}

	
	public void setScale(float pScaleX, float pScaleY) {
		this.mScaleX = pScaleX;
		this.mScaleY = pScaleY;
	}

	
	public float getScaleCenterX() {
		return this.mScaleCenterX;
	}

	
	public float getScaleCenterY() {
		return this.mScaleCenterY;
	}

	
	public void setScaleCenterX(float pScaleCenterX) {
		this.mScaleCenterX = pScaleCenterX;
	}

	
	public void setScaleCenterY(float pScaleCenterY) {
		this.mScaleCenterY = pScaleCenterY;
	}

	
	public void setScaleCenter(float pScaleCenterX, float pScaleCenterY) {
		this.mScaleCenterX = pScaleCenterX;
		this.mScaleCenterY = pScaleCenterY;
	}

	
	public float getRed() {
		return this.mRed;
	}

	
	public float getGreen() {
		return this.mGreen;
	}

	
	public float getBlue() {
		return this.mBlue;
	}

	
	public float getAlpha() {
		return this.mAlpha;
	}

	/**
	 * @param pAlpha from <code>0.0f</code> (transparent) to <code>1.0f</code> (opaque)
	 */
	
	public void setAlpha(float pAlpha) {
		this.mAlpha = pAlpha;
	}

	/**
	 * @param pRed from <code>0.0f</code> to <code>1.0f</code>
	 * @param pGreen from <code>0.0f</code> to <code>1.0f</code>
	 * @param pBlue from <code>0.0f</code> to <code>1.0f</code>
	 */
	
	public void setColor(float pRed, float pGreen, float pBlue) {
		this.mRed = pRed;
		this.mGreen = pGreen;
		this.mBlue = pBlue;
	}

	/**
	 * @param pRed from <code>0.0f</code> to <code>1.0f</code>
	 * @param pGreen from <code>0.0f</code> to <code>1.0f</code>
	 * @param pBlue from <code>0.0f</code> to <code>1.0f</code>
	 * @param pAlpha from <code>0.0f</code> (transparent) to <code>1.0f</code> (opaque)
	 */
	
	public void setColor(float pRed, float pGreen, float pBlue, float pAlpha) {
		this.mRed = pRed;
		this.mGreen = pGreen;
		this.mBlue = pBlue;
		this.mAlpha = pAlpha;
	}

	
	public int getChildCount() {
		if(this.mChildren == null) {
			return 0;
		}
		return this.mChildren.Count;
	}

	
	public IEntity getChild(int pIndex) {
		if(this.mChildren == null) {
			return null;
		}
		return this.mChildren[pIndex];
	}

	
	public IEntity getFirstChild() {
		if(this.mChildren == null) {
			return null;
		}
	    return this.mChildren[0];
	}

	
	public IEntity getLastChild() {
		if(this.mChildren == null) {
			return null;
		}
		return this.mChildren[this.mChildren.Count - 1];
	}

	
	public void detachChildren() {
		if(this.mChildren == null) {
			return;
		}
		this.mChildren.Clear(PARAMETERCALLABLE_DETACHCHILD);
	}

	
	public void attachChild(IEntity pEntity) {
		if(this.mChildren == null) {
			this.allocateChildren();
		}

		this.mChildren.Add(pEntity);
		pEntity.setParent(this);
		pEntity.onAttached();
	}

	
	public IEntity findChild(IEntityMatcher pEntityMatcher) {
		if(this.mChildren == null) {
			return null;
		}
		return this.mChildren.Find(pEntityMatcher);
	}

	
	public void sortChildren() {
		if(this.mChildren == null) {
			return;
		}
		ZIndexSorter.GetInstance().Sort(this.mChildren);
	}

	
	public void sortChildren(IComparer<IEntity> pEntityComparator) {
		if(this.mChildren == null) {
			return;
		}
		ZIndexSorter.GetInstance().Sort(this.mChildren, pEntityComparator);
	}

	
	public bool detachChild(IEntity pEntity) {
		if(this.mChildren == null) {
			return false;
		}
		return this.mChildren.Remove(pEntity, PARAMETERCALLABLE_DETACHCHILD);
	}

	
	public IEntity detachChild(IEntityMatcher pEntityMatcher) {
		if(this.mChildren == null) {
			return null;
		}
		return this.mChildren.Remove(pEntityMatcher);
	}

	
	public bool detachChildren(IEntityMatcher pEntityMatcher) {
		if(this.mChildren == null) {
			return false;
		}
		return this.mChildren.RemoveAll(pEntityMatcher, Entity.PARAMETERCALLABLE_DETACHCHILD);
	}

	
	public void registerUpdateHandler(IUpdateHandler pUpdateHandler) {
		if(this.mUpdateHandlers == null) {
			this.allocateUpdateHandlers();
		}
		this.mUpdateHandlers.Add(pUpdateHandler);
	}

	
	public bool unregisterUpdateHandler(IUpdateHandler pUpdateHandler) {
		if(this.mUpdateHandlers == null) {
			return false;
		}
		return this.mUpdateHandlers.Remove(pUpdateHandler);
	}

	
	public bool unregisterUpdateHandlers(IUpdateHandlerMatcher pUpdateHandlerMatcher) {
		if(this.mUpdateHandlers == null) {
			return false;
		}
	    return this.mUpdateHandlers.RemoveAll(pUpdateHandlerMatcher.Matches) > 0;
	}

	
	public void clearUpdateHandlers() {
		if(this.mUpdateHandlers == null) {
			return;
		}
		this.mUpdateHandlers.Clear();
	}

	
	public void registerEntityModifier(IEntityModifier pEntityModifier) {
		if(this.mEntityModifiers == null) {
			this.allocateEntityModifiers();
		}
		this.mEntityModifiers.Add(pEntityModifier);
	}

	
	public bool unregisterEntityModifier(IEntityModifier pEntityModifier) {
		if(this.mEntityModifiers == null) {
			return false;
		}
		return this.mEntityModifiers.Remove(pEntityModifier);
	}

	
	public bool unregisterEntityModifiers(IEntityModifierMatcher pEntityModifierMatcher) {
		if(this.mEntityModifiers == null) {
			return false;
		}
		return this.mEntityModifiers.RemoveAll(pEntityModifierMatcher.Matches) > 0;
	}

	
	public void clearEntityModifiers() {
		if(this.mEntityModifiers == null) {
			return;
		}
		this.mEntityModifiers.Clear();
	}

	
	public float[] getSceneCenterCoordinates() {
		return this.convertLocalToSceneCoordinates(0, 0);
	}

	
	public float[] convertLocalToSceneCoordinates(float pX, float pY) {
		Entity.VERTICES_LOCAL_TO_SCENE_TMP[Constants.VERTEX_INDEX_X] = pX;
		Entity.VERTICES_LOCAL_TO_SCENE_TMP[Constants.VERTEX_INDEX_Y] = pY;

		this.getLocalToSceneTransformation().transform(Entity.VERTICES_LOCAL_TO_SCENE_TMP);

		return Entity.VERTICES_LOCAL_TO_SCENE_TMP;
	}

	
	public float[] convertSceneToLocalCoordinates(float pX, float pY) {
		Entity.VERTICES_SCENE_TO_LOCAL_TMP[Constants.VERTEX_INDEX_X] = pX;
		Entity.VERTICES_SCENE_TO_LOCAL_TMP[Constants.VERTEX_INDEX_Y] = pY;

		this.getSceneToLocalTransformation().transform(Entity.VERTICES_SCENE_TO_LOCAL_TMP);

		return Entity.VERTICES_SCENE_TO_LOCAL_TMP;
	}

	
	public Transformation getLocalToSceneTransformation() {
		// TODO skip this calculation when the transformation is not "dirty"
		Transformation localToSceneTransformation = this.mLocalToSceneTransformation;
		localToSceneTransformation.setToIdentity();

		/* Scale. */
		float scaleX = this.mScaleX;
		float scaleY = this.mScaleY;
		if(scaleX != 1 || scaleY != 1) {
			float scaleCenterX = this.mScaleCenterX;
			float scaleCenterY = this.mScaleCenterY;

			/* TODO Check if it is worth to check for scaleCenterX == 0 && scaleCenterY == 0 as the two postTranslate can be saved.
			 * The same obviously applies for all similar occurrences of this pattern in this class. */

			localToSceneTransformation.postTranslate(-scaleCenterX, -scaleCenterY);
			localToSceneTransformation.postScale(scaleX, scaleY);
			localToSceneTransformation.postTranslate(scaleCenterX, scaleCenterY);
		}

		/* TODO There is a special, but very likely case when mRotationCenter and mScaleCenter are the same.
		 * In that case the last postTranslate of the scale and the first postTranslate of the rotation is superfluous. */

		/* Rotation. */
		float rotation = this.mRotation;
		if(rotation != 0) {
			float rotationCenterX = this.mRotationCenterX;
			float rotationCenterY = this.mRotationCenterY;

			localToSceneTransformation.postTranslate(-rotationCenterX, -rotationCenterY);
			localToSceneTransformation.postRotate(rotation);
			localToSceneTransformation.postTranslate(rotationCenterX, rotationCenterY);
		}

		/* Translation. */
		localToSceneTransformation.postTranslate(this.mX, this.mY);

		IEntity parent = this.mParent;
		if(parent != null) {
			localToSceneTransformation.postConcat(parent.getLocalToSceneTransformation());
		}

		return localToSceneTransformation;
	}

	
	public Transformation getSceneToLocalTransformation() {
		// TODO skip this calculation when the transformation is not "dirty"
		Transformation sceneToLocalTransformation = this.mSceneToLocalTransformation;
		sceneToLocalTransformation.setToIdentity();

		IEntity parent = this.mParent;
		if(parent != null) {
			sceneToLocalTransformation.postConcat(parent.getSceneToLocalTransformation());
		}

		/* Translation. */
		sceneToLocalTransformation.postTranslate(-this.mX, -this.mY);

		/* Rotation. */
		float rotation = this.mRotation;
		if(rotation != 0) {
			float rotationCenterX = this.mRotationCenterX;
			float rotationCenterY = this.mRotationCenterY;

			sceneToLocalTransformation.postTranslate(-rotationCenterX, -rotationCenterY);
			sceneToLocalTransformation.postRotate(-rotation);
			sceneToLocalTransformation.postTranslate(rotationCenterX, rotationCenterY);
		}

		/* TODO There is a special, but very likely case when mRotationCenter and mScaleCenter are the same.
		 * In that case the last postTranslate of the rotation and the first postTranslate of the scale is superfluous. */

		/* Scale. */
		float scaleX = this.mScaleX;
		float scaleY = this.mScaleY;
		if(scaleX != 1 || scaleY != 1) {
			float scaleCenterX = this.mScaleCenterX;
			float scaleCenterY = this.mScaleCenterY;

			sceneToLocalTransformation.postTranslate(-scaleCenterX, -scaleCenterY);
			sceneToLocalTransformation.postScale(1 / scaleX, 1 / scaleY);
			sceneToLocalTransformation.postTranslate(scaleCenterX, scaleCenterY);
		}

		return sceneToLocalTransformation;
	}

	
	public void onAttached() {

	}

	
	public void onDetached() {

	}

	
	public Object getUserData() {
		return this.mUserData;
	}

	
	public void setUserData(Object pUserData) {
		this.mUserData = pUserData;
	}

	
	public void onDraw(GL10 pGL, Camera pCamera) {
		if(this.mVisible) {
			this.OnManagedDraw(pGL, pCamera);
		}
	}

	
	public void onUpdate(float pSecondsElapsed) {
		if(!this.mIgnoreUpdate) {
			this.OnManagedUpdate(pSecondsElapsed);
		}
	}

	
	public void reset() {
		this.mVisible = true;
		this.mIgnoreUpdate = false;

		this.mX = this.mInitialX;
		this.mY = this.mInitialY;
		this.mRotation = 0;
		this.mScaleX = 1;
		this.mScaleY = 1;

		this.mRed = 1.0f;
		this.mGreen = 1.0f;
		this.mBlue = 1.0f;
		this.mAlpha = 1.0f;

		if(this.mEntityModifiers != null) {
			this.mEntityModifiers.Reset();
		}

		if(this.mChildren != null) {
			List<IEntity> entities = this.mChildren;
			for(int i = entities.Count - 1; i >= 0; i--) {
				entities[i].Reset();
			}
		}
	}

	// ===========================================================
	// Methods
	// ===========================================================

	protected void doDraw(GL10 pGL, Camera pCamera) {

	}

	private void allocateEntityModifiers() {
		this.mEntityModifiers = new EntityModifierList(this, Entity.ENTITYMODIFIERS_CAPACITY_DEFAULT);
	}

	private void allocateChildren() {
		this.mChildren = new SmartList<IEntity>(Entity.CHILDREN_CAPACITY_DEFAULT);
	}

	private void allocateUpdateHandlers() {
		this.mUpdateHandlers = new UpdateHandlerList(Entity.UPDATEHANDLERS_CAPACITY_DEFAULT);
	}

	protected void onApplyTransformations(GL10 pGL) {
		/* Translation. */
		this.applyTranslation(pGL);

		/* Rotation. */
		this.applyRotation(pGL);

		/* Scale. */
		this.applyScale(pGL);
	}

	protected void applyTranslation(GL10 pGL) {
		pGL.GlTranslatef(this.mX, this.mY, 0);
	}

	protected void applyRotation(GL10 pGL) {
		float rotation = this.mRotation;

		if(rotation != 0) {
			float rotationCenterX = this.mRotationCenterX;
			float rotationCenterY = this.mRotationCenterY;

			pGL.GlTranslatef(rotationCenterX, rotationCenterY, 0);
			pGL.GlRotatef(rotation, 0, 0, 1);
			pGL.GlTranslatef(-rotationCenterX, -rotationCenterY, 0);

			/* TODO There is a special, but very likely case when mRotationCenter and mScaleCenter are the same.
			 * In that case the last glTranslatef of the rotation and the first glTranslatef of the scale is superfluous.
			 * The problem is that applyRotation and applyScale would need to be "merged" in order to efficiently check for that condition.  */
		}
	}

	protected void applyScale(GL10 pGL) {
		float scaleX = this.mScaleX;
		float scaleY = this.mScaleY;

		if(scaleX != 1 || scaleY != 1) {
			float scaleCenterX = this.mScaleCenterX;
			float scaleCenterY = this.mScaleCenterY;

			pGL.GlTranslatef(scaleCenterX, scaleCenterY, 0);
			pGL.GlScalef(scaleX, scaleY, 1);
			pGL.GlTranslatef(-scaleCenterX, -scaleCenterY, 0);
		}
	}
        protected abstract void OnManagedDraw(/* final */ GL10 pGL, /* final */ Camera pCamera);

        public virtual /* override */ /* final */ /* sealed */ void OnDraw(/* final */ GL10 pGL, /* final */ Camera pCamera)
        {
            if (this.mVisible)
            {
                this.OnManagedDraw(pGL, pCamera);
            }
        }

        protected abstract void OnManagedUpdate(/* final */ float pSecondsElapsed);

        public /* override final sealed */ virtual void OnUpdate(/* final */ float pSecondsElapsed)
        {
            if (!this.mIgnoreUpdate)
            {
                this.OnManagedUpdate(pSecondsElapsed);
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public /* override */ virtual void Reset()
        {
            this.mVisible = true;
            this.mIgnoreUpdate = false;
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
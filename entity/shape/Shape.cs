namespace andengine.entity.shape
{

    //import javax.microedition.khronos.opengles.GL10;
    //TODO Check this conversion
    using OpenTK.Graphics.ES11;

    using andengine.engine.camera/*.Camera*/;
    using andengine.entity/*.Entity*/;
    using andengine.input.touch/*.TouchEvent*/;
    using andengine.opengl.util/*.GLHelper*/;
    using andengine.util.modifier/*.IModifier;
    using andengine.util.modifier.ModifierList*/;

    /**
     * @author Nicolas Gramlich
     * @since 11:51:27 - 13.03.2010
     */
    public abstract class Shape : Entity, IShape
    {
        // ===========================================================
        // Constants
        // ===========================================================

        public static /* final */ sealed int BLENDFUNCTION_SOURCE_DEFAULT = GL10.GL_SRC_ALPHA;
        public static /* final */ sealed int BLENDFUNCTION_DESTINATION_DEFAULT = GL10.GL_ONE_MINUS_SRC_ALPHA;

        public static /* final */ sealed int BLENDFUNCTION_SOURCE_PREMULTIPLYALPHA_DEFAULT = GL10.GL_ONE;
        public static /* final */ sealed int BLENDFUNCTION_DESTINATION_PREMULTIPLYALPHA_DEFAULT = GL10.GL_ONE_MINUS_SRC_ALPHA;

        // ===========================================================
        // Fields
        // ===========================================================

        protected float mRed = 1f;
        protected float mGreen = 1f;
        protected float mBlue = 1f;
        protected float mAlpha = 1f;

        private /* final */ sealed float mBaseX;
        private /* final */ sealed float mBaseY;

        protected float mX;
        protected float mY;

        protected float mAccelerationX = 0;
        protected float mAccelerationY = 0;

        protected float mVelocityX = 0;
        protected float mVelocityY = 0;

        protected float mRotation = 0;

        protected float mAngularVelocity = 0;

        protected float mRotationCenterX = 0;
        protected float mRotationCenterY = 0;

        protected float mScaleX = 1f;
        protected float mScaleY = 1f;

        protected float mScaleCenterX = 0;
        protected float mScaleCenterY = 0;

        private bool mUpdatePhysics = true;

        protected int mSourceBlendFunction = BLENDFUNCTION_SOURCE_DEFAULT;
        protected int mDestinationBlendFunction = BLENDFUNCTION_DESTINATION_DEFAULT;

        private /* final */ sealed ModifierList<IShape> mShapeModifiers = new ModifierList<IShape>(this);

        private bool mCullingEnabled = false;

        // ===========================================================
        // Constructors
        // ===========================================================

        public Shape(/* final */ float pX, /* final */ float pY)
        {
            this.mBaseX = pX;
            this.mBaseY = pY;

            this.mX = pX;
            this.mY = pY;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public override float getRed()
        {
            return this.mRed;
        }

        public override float getGreen()
        {
            return this.mGreen;
        }

        public override float getBlue()
        {
            return this.mBlue;
        }

        public override float getAlpha()
        {
            return this.mAlpha;
        }

        /**
         * @param pAlpha from <code>0.0f</code> (invisible) to <code>1.0f</code> (opaque)
         */
        public override void setAlpha(/* final */ float pAlpha)
        {
            this.mAlpha = pAlpha;
        }

        /**
         * @param pRed from <code>0.0f</code> to <code>1.0f</code>
         * @param pGreen from <code>0.0f</code> to <code>1.0f</code>
         * @param pBlue from <code>0.0f</code> to <code>1.0f</code>
         */
        public override void setColor(/* final */ float pRed, /* final */ float pGreen, /* final */ float pBlue)
        {
            this.mRed = pRed;
            this.mGreen = pGreen;
            this.mBlue = pBlue;
        }

        /**
         * @param pRed from <code>0.0f</code> to <code>1.0f</code>
         * @param pGreen from <code>0.0f</code> to <code>1.0f</code>
         * @param pBlue from <code>0.0f</code> to <code>1.0f</code>
         * @param pAlpha from <code>0.0f</code> (invisible) to <code>1.0f</code> (opaque)
         */
        public override void setColor(/* final */ float pRed, /* final */ float pGreen, /* final */ float pBlue, /* final */ float pAlpha)
        {
            this.mRed = pRed;
            this.mGreen = pGreen;
            this.mBlue = pBlue;
            this.mAlpha = pAlpha;
        }

        public override float getX()
        {
            return this.mX;
        }

        public override float getY()
        {
            return this.mY;
        }

        public override float getBaseX()
        {
            return this.mBaseX;
        }

        public override float getBaseY()
        {
            return this.mBaseY;
        }

        public override void setPosition(/* final */ IShape pOtherShape)
        {
            this.setPosition(pOtherShape.getX(), pOtherShape.getY());
        }

        public override void setPosition(/* final */ float pX, /* final */ float pY)
        {
            this.mX = pX;
            this.mY = pY;
            this.onPositionChanged();
        }

        public override void setBasePosition()
        {
            this.mX = this.mBaseX;
            this.mY = this.mBaseY;
            this.onPositionChanged();
        }

        public override float getVelocityX()
        {
            return this.mVelocityX;
        }

        public override float getVelocityY()
        {
            return this.mVelocityY;
        }

        public override void setVelocityX(/* final */ float pVelocityX)
        {
            this.mVelocityX = pVelocityX;
        }

        public override void setVelocityY(/* final */ float pVelocityY)
        {
            this.mVelocityY = pVelocityY;
        }

        public override void setVelocity(/* final */ float pVelocity)
        {
            this.mVelocityX = pVelocity;
            this.mVelocityY = pVelocity;
        }

        public override void setVelocity(/* final */ float pVelocityX, /* final */ float pVelocityY)
        {
            this.mVelocityX = pVelocityX;
            this.mVelocityY = pVelocityY;
        }

        public override float getAccelerationX()
        {
            return this.mAccelerationX;
        }

        public override float getAccelerationY()
        {
            return this.mAccelerationY;
        }

        public override void setAccelerationX(/* final */ float pAccelerationX)
        {
            this.mAccelerationX = pAccelerationX;
        }

        public override void setAccelerationY(/* final */ float pAccelerationY)
        {
            this.mAccelerationY = pAccelerationY;
        }

        public override void setAcceleration(/* final */ float pAccelerationX, /* final */ float pAccelerationY)
        {
            this.mAccelerationX = pAccelerationX;
            this.mAccelerationY = pAccelerationY;
        }

        public override void setAcceleration(/* final */ float pAcceleration)
        {
            this.mAccelerationX = pAcceleration;
            this.mAccelerationY = pAcceleration;
        }

        public override void accelerate(/* final */ float pAccelerationX, /* final */ float pAccelerationY)
        {
            this.mAccelerationX += pAccelerationX;
            this.mAccelerationY += pAccelerationY;
        }

        public override float getRotation()
        {
            return this.mRotation;
        }

        public override void setRotation(/* final */ float pRotation)
        {
            this.mRotation = pRotation;
        }

        public override float getAngularVelocity()
        {
            return this.mAngularVelocity;
        }

        public override void setAngularVelocity(/* final */ float pAngularVelocity)
        {
            this.mAngularVelocity = pAngularVelocity;
        }

        public override float getRotationCenterX()
        {
            return this.mRotationCenterX;
        }

        public override float getRotationCenterY()
        {
            return this.mRotationCenterY;
        }

        public override void setRotationCenterX(/* final */ float pRotationCenterX)
        {
            this.mRotationCenterX = pRotationCenterX;
        }

        public override void setRotationCenterY(/* final */ float pRotationCenterY)
        {
            this.mRotationCenterY = pRotationCenterY;
        }

        public override void setRotationCenter(/* final */ float pRotationCenterX, /* final */ float pRotationCenterY)
        {
            this.mRotationCenterX = pRotationCenterX;
            this.mRotationCenterY = pRotationCenterY;
        }

        public bool isScaled()
        {
            return this.mScaleX != 1 || this.mScaleY != 1;
        }

        public override float getScaleX()
        {
            return this.mScaleX;
        }

        public override float getScaleY()
        {
            return this.mScaleY;
        }

        public override void setScaleX(/* final */ float pScaleX)
        {
            this.mScaleX = pScaleX;
        }

        public override void setScaleY(/* final */ float pScaleY)
        {
            this.mScaleY = pScaleY;
        }

        public override void setScale(/* final */ float pScale)
        {
            this.mScaleX = pScale;
            this.mScaleY = pScale;
        }

        public override void setScale(/* final */ float pScaleX, /* final */ float pScaleY)
        {
            this.mScaleX = pScaleX;
            this.mScaleY = pScaleY;
        }

        public override float getScaleCenterX()
        {
            return this.mScaleCenterX;
        }

        public override float getScaleCenterY()
        {
            return this.mScaleCenterY;
        }

        public override void setScaleCenterX(/* final */ float pScaleCenterX)
        {
            this.mScaleCenterX = pScaleCenterX;
        }

        public override void setScaleCenterY(/* final */ float pScaleCenterY)
        {
            this.mScaleCenterY = pScaleCenterY;
        }

        public override void setScaleCenter(/* final */ float pScaleCenterX, /* final */ float pScaleCenterY)
        {
            this.mScaleCenterX = pScaleCenterX;
            this.mScaleCenterY = pScaleCenterY;
        }

        public override bool isUpdatePhysics()
        {
            return this.mUpdatePhysics;
        }

        /**
         * Enable or disable the AndEngine <b>internal</b> physics, you usually call this once you use the AndEnginePhysicsBox2dExtension.
         * @param pUpdatePhysics
         */
        public override void setUpdatePhysics(/* final */ bool pUpdatePhysics)
        {
            this.mUpdatePhysics = pUpdatePhysics;
        }

        public override bool isCullingEnabled()
        {
            return this.mCullingEnabled;
        }

        public override void setCullingEnabled(/* final */ bool pCullingEnabled)
        {
            this.mCullingEnabled = pCullingEnabled;
        }

        public override void setBlendFunction(/* final */ int pSourceBlendFunction, /* final */ int pDestinationBlendFunction)
        {
            this.mSourceBlendFunction = pSourceBlendFunction;
            this.mDestinationBlendFunction = pDestinationBlendFunction;
        }

        public override float getWidthScaled()
        {
            return this.getWidth() * this.mScaleX;
        }

        public override float getHeightScaled()
        {
            return this.getHeight() * this.mScaleY;
        }

        public override void addShapeModifier(/* final */ IModifier<IShape> pShapeModifier)
        {
            this.mShapeModifiers.Add(pShapeModifier);
        }

        public override bool removeShapeModifier(/* final */ IModifier<IShape> pShapeModifier)
        {
            return this.mShapeModifiers.Remove(pShapeModifier);
        }

        public override void clearShapeModifiers()
        {
            this.mShapeModifiers.Clear();
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override bool onAreaTouched(/* final */ TouchEvent pSceneTouchEvent, /* final */ float pTouchAreaLocalX, /* final */ float pTouchAreaLocalY)
        {
            return false;
        }

        /**
         * Will only be performed if {@link Shape#isCullingEnabled()} is true.
         * @param pCamera
         * @return <code>true</code> when this object is visible by the {@link Camera}, <code>false</code> otherwise.
         */
        protected abstract bool isCulled(/* final */ Camera pCamera);

        protected void onPositionChanged()
        {

        }

        protected abstract void onApplyVertices(/* final */ GL10 pGL);
        protected abstract void drawVertices(/* final */ GL10 pGL, /* final */ Camera pCamera);

        protected override void onManagedUpdate(/* final */ float pSecondsElapsed)
        {
            if (this.mUpdatePhysics)
            {
                /* Apply linear acceleration. */
                /* final */
                float accelerationX = this.mAccelerationX;
                /* final */
                float accelerationY = this.mAccelerationY;
                if (accelerationX != 0 || accelerationY != 0)
                {
                    this.mVelocityX += accelerationX * pSecondsElapsed;
                    this.mVelocityY += accelerationY * pSecondsElapsed;
                }

                /* Apply angular velocity. */
                /* final */
                float angularVelocity = this.mAngularVelocity;
                if (angularVelocity != 0)
                {
                    this.mRotation += angularVelocity * pSecondsElapsed;
                }

                /* Apply linear velocity. */
                /* final */
                float velocityX = this.mVelocityX;
                /* final */
                float velocityY = this.mVelocityY;
                if (velocityX != 0 || velocityY != 0)
                {
                    this.mX += velocityX * pSecondsElapsed;
                    this.mY += velocityY * pSecondsElapsed;
                    this.onPositionChanged();
                }
            }

            this.mShapeModifiers.onUpdate(pSecondsElapsed);
        }

        protected override void onManagedDraw(/* final */ GL10 pGL, /* final */ Camera pCamera)
        {
            if (this.mCullingEnabled == false || this.isCulled(pCamera) == false)
            {
                this.onInitDraw(pGL);

                pGL.glPushMatrix();
                {
                    this.onApplyVertices(pGL);
                    this.onApplyTransformations(pGL);
                    this.drawVertices(pGL, pCamera);
                }
                pGL.glPopMatrix();
            }
        }

        protected void onInitDraw(/* final */ GL10 pGL)
        {
            GLHelper.setColor(pGL, this.mRed, this.mGreen, this.mBlue, this.mAlpha);

            GLHelper.enableVertexArray(pGL);
            GLHelper.blendFunction(pGL, this.mSourceBlendFunction, this.mDestinationBlendFunction);
        }

        protected void onApplyTransformations(/* final */ GL10 pGL)
        {
            this.applyTranslation(pGL);

            this.applyRotation(pGL);

            this.applyScale(pGL);
        }

        protected void applyTranslation(/* final */ GL10 pGL)
        {
            pGL.glTranslatef(this.mX, this.mY, 0);
        }

        protected void applyRotation(/* final */ GL10 pGL)
        {
            /* final */
            float rotation = this.mRotation;

            if (rotation != 0)
            {
                /* final */
                float rotationCenterX = this.mRotationCenterX;
                /* final */
                float rotationCenterY = this.mRotationCenterY;

                pGL.glTranslatef(rotationCenterX, rotationCenterY, 0);
                pGL.glRotatef(rotation, 0, 0, 1);
                pGL.glTranslatef(-rotationCenterX, -rotationCenterY, 0);
            }
        }

        protected void applyScale(/* final */ GL10 pGL)
        {
            /* final */
            float scaleX = this.mScaleX;
            /* final */
            float scaleY = this.mScaleY;

            if (scaleX != 1 || scaleY != 1)
            {
                /* final */
                float scaleCenterX = this.mScaleCenterX;
                /* final */
                float scaleCenterY = this.mScaleCenterY;

                pGL.glTranslatef(scaleCenterX, scaleCenterY, 0);
                pGL.glScalef(scaleX, scaleY, 1);
                pGL.glTranslatef(-scaleCenterX, -scaleCenterY, 0);
            }
        }

        public override void reset()
        {
            //super.reset();
            base.reset();

            this.mX = this.mBaseX;
            this.mY = this.mBaseY;
            this.mAccelerationX = 0;
            this.mAccelerationY = 0;
            this.mVelocityX = 0;
            this.mVelocityY = 0;
            this.mRotation = 0;
            this.mAngularVelocity = 0;
            this.mScaleX = 1;
            this.mScaleY = 1;

            this.onPositionChanged();

            this.mRed = 1.0f;
            this.mGreen = 1.0f;
            this.mBlue = 1.0f;
            this.mAlpha = 1.0f;

            this.mSourceBlendFunction = BLENDFUNCTION_SOURCE_DEFAULT;
            this.mDestinationBlendFunction = BLENDFUNCTION_DESTINATION_DEFAULT;

            this.mShapeModifiers.reset();
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
namespace andengine.entity.shape
{

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;

    using Camera = andengine.engine.camera.Camera;
    using Entity = andengine.entity.Entity;
    using TouchEvent = andengine.input.touch.TouchEvent;
    using GLHelper = andengine.opengl.util.GLHelper;

    //using IModifier = andengine.util.modifier.IModifier;
    //using IModifier = andengine.util.modifier.IModifier;
    //using ModifierList = andengine.util.modifier.ModifierList;
    //using andengine.util.modifier;

    /**
     * @author Nicolas Gramlich
     * @since 11:51:27 - 13.03.2010
     */
    public abstract class Shape : Entity, IShape
    {
        // ===========================================================
        // Constants
        // ===========================================================

        public static /* final */ readonly int BLENDFUNCTION_SOURCE_DEFAULT = Javax.Microedition.Khronos.Opengles.GL10Consts.GlSrcAlpha;
        public static /* final */ readonly int BLENDFUNCTION_DESTINATION_DEFAULT = Javax.Microedition.Khronos.Opengles.GL10Consts.GlOneMinusSrcAlpha;

        public static /* final */ readonly int BLENDFUNCTION_SOURCE_PREMULTIPLYALPHA_DEFAULT = Javax.Microedition.Khronos.Opengles.GL10Consts.GlOne;
        public static /* final */ readonly int BLENDFUNCTION_DESTINATION_PREMULTIPLYALPHA_DEFAULT = Javax.Microedition.Khronos.Opengles.GL10Consts.GlOneMinusSrcAlpha;

        // ===========================================================
        // Fields
        // ===========================================================

        protected float mRed = 1f;
        protected float mGreen = 1f;
        protected float mBlue = 1f;
        protected float mAlpha = 1f;

        private /* final */ readonly float mBaseX;
        private /* final */ readonly float mBaseY;

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

        //private /* final */ readonly ModifierList<IShape> mShapeModifiers = new ModifierList<IShape>(this);
        private readonly andengine.util.modifier.ModifierList<IShape> mShapeModifiers;

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

            mShapeModifiers = new andengine.util.modifier.ModifierList<IShape>(this);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public float Red { get { return GetRed(); } }
        public float Green { get { return GetGreen(); } }
        public float Blue { get { return GetBlue(); } }
        public float Alpha { get { return GetAlpha(); } set { SetAlpha(value); } }

        public override float GetRed()
        {
            return this.mRed;
        }

        public override float GetGreen()
        {
            return this.mGreen;
        }

        public override float GetBlue()
        {
            return this.mBlue;
        }

        public override float GetAlpha()
        {
            return this.mAlpha;
        }

        /**
         * @param pAlpha from <code>0.0f</code> (invisible) to <code>1.0f</code> (opaque)
         */
        public override void SetAlpha(/* final */ float pAlpha)
        {
            this.mAlpha = pAlpha;
        }

        /**
         * @param pRed from <code>0.0f</code> to <code>1.0f</code>
         * @param pGreen from <code>0.0f</code> to <code>1.0f</code>
         * @param pBlue from <code>0.0f</code> to <code>1.0f</code>
         */
        public override void SetColor(/* final */ float pRed, /* final */ float pGreen, /* final */ float pBlue)
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
        public override void SetColor(/* final */ float pRed, /* final */ float pGreen, /* final */ float pBlue, /* final */ float pAlpha)
        {
            this.mRed = pRed;
            this.mGreen = pGreen;
            this.mBlue = pBlue;
            this.mAlpha = pAlpha;
        }

        public float X { get { return GetX(); } }
        public float Y { get { return GetY(); } }
        public float BaseX { get { return GetBaseX(); } }
        public float BaseY { get { return GetBaseY(); } }

        public override float GetX()
        {
            return this.mX;
        }

        public override float GetY()
        {
            return this.mY;
        }

        public override float GetBaseX()
        {
            return this.mBaseX;
        }

        public override float GetBaseY()
        {
            return this.mBaseY;
        }

        public override void SetPosition(/* final */ IShape pOtherShape)
        {
            this.SetPosition(pOtherShape.GetX(), pOtherShape.GetY());
        }

        public override void SetPosition(/* final */ float pX, /* final */ float pY)
        {
            this.mX = pX;
            this.mY = pY;
            this.OnPositionChanged();
        }

        public override void SetBasePosition()
        {
            this.mX = this.mBaseX;
            this.mY = this.mBaseY;
            this.OnPositionChanged();
        }

        public float VelocityX { get { return GetVelocityX(); } set { SetVelocityX(value); } }
        public float VelocityY { get { return GetVelocityY(); } set { SetVelocityY(value); } }

        public override float GetVelocityX()
        {
            return this.mVelocityX;
        }

        public override float GetVelocityY()
        {
            return this.mVelocityY;
        }

        public override void SetVelocityX(/* final */ float pVelocityX)
        {
            this.mVelocityX = pVelocityX;
        }

        public override void SetVelocityY(/* final */ float pVelocityY)
        {
            this.mVelocityY = pVelocityY;
        }

        public override void SetVelocity(/* final */ float pVelocity)
        {
            this.mVelocityX = pVelocity;
            this.mVelocityY = pVelocity;
        }

        public override void SetVelocity(/* final */ float pVelocityX, /* final */ float pVelocityY)
        {
            this.mVelocityX = pVelocityX;
            this.mVelocityY = pVelocityY;
        }

        public float AccelerationX { get { return GetAccelerationX(); } set { SetAccelerationX(value); } }
        public float AccelerationY { get { return GetAccelerationY(); } set { SetAccelerationY(value); } }

        public override float GetAccelerationX()
        {
            return this.mAccelerationX;
        }

        public override float GetAccelerationY()
        {
            return this.mAccelerationY;
        }

        public override void SetAccelerationX(/* final */ float pAccelerationX)
        {
            this.mAccelerationX = pAccelerationX;
        }

        public override void SetAccelerationY(/* final */ float pAccelerationY)
        {
            this.mAccelerationY = pAccelerationY;
        }

        public override void SetAcceleration(/* final */ float pAccelerationX, /* final */ float pAccelerationY)
        {
            this.mAccelerationX = pAccelerationX;
            this.mAccelerationY = pAccelerationY;
        }

        public override void SetAcceleration(/* final */ float pAcceleration)
        {
            this.mAccelerationX = pAcceleration;
            this.mAccelerationY = pAcceleration;
        }

        public override void Accelerate(/* final */ float pAccelerationX, /* final */ float pAccelerationY)
        {
            this.mAccelerationX += pAccelerationX;
            this.mAccelerationY += pAccelerationY;
        }

        public float Rotation { get { return getRotation(); } set { setRotation(value); } }

        public override float GetRotation()
        {
            return this.mRotation;
        }

        public override void SetRotation(/* final */ float pRotation)
        {
            this.mRotation = pRotation;
        }

        public float AngularVelocity { get { return getAngularVelocity(); } set { setAngularVelocity(value); } }

        public override float GetAngularVelocity()
        {
            return this.mAngularVelocity;
        }

        public override void SetAngularVelocity(/* final */ float pAngularVelocity)
        {
            this.mAngularVelocity = pAngularVelocity;
        }

        public float RotationCenterX { get { return GetRotationCenterX(); } set { SetRotationCenterX(value); } }
        public float RotationCenterY { get { return GetRotationCenterY(); } set { SetRotationCenterY(value); } }

        public override float GetRotationCenterX()
        {
            return this.mRotationCenterX;
        }

        public override float GetRotationCenterY()
        {
            return this.mRotationCenterY;
        }

        public override void SetRotationCenterX(/* final */ float pRotationCenterX)
        {
            this.mRotationCenterX = pRotationCenterX;
        }

        public override void SetRotationCenterY(/* final */ float pRotationCenterY)
        {
            this.mRotationCenterY = pRotationCenterY;
        }

        public override void SetRotationCenter(/* final */ float pRotationCenterX, /* final */ float pRotationCenterY)
        {
            this.mRotationCenterX = pRotationCenterX;
            this.mRotationCenterY = pRotationCenterY;
        }

        public bool IsScaled()
        {
            return this.mScaleX != 1 || this.mScaleY != 1;
        }

        public float ScaleX { get { return GetScaleX(); } set { SetScaleX(value); } }
        public float ScaleY { get { return GetScaleY(); } set { SetScaleY(value); } }

        public /* override */ float GetScaleX()
        {
            return this.mScaleX;
        }

        public /* override */ float GetScaleY()
        {
            return this.mScaleY;
        }

        public /* override */ void SetScaleX(/* final */ float pScaleX)
        {
            this.mScaleX = pScaleX;
        }

        public override void SetScaleY(/* final */ float pScaleY)
        {
            this.mScaleY = pScaleY;
        }

        public override void SetScale(/* final */ float pScale)
        {
            this.mScaleX = pScale;
            this.mScaleY = pScale;
        }

        public override void SetScale(/* final */ float pScaleX, /* final */ float pScaleY)
        {
            this.mScaleX = pScaleX;
            this.mScaleY = pScaleY;
        }

        public float ScaleCenterX { get { return GetScaleCenterX(); } set { SetScaleCenterX(value); } }
        public float ScaleCenterY { get { return GetScaleCenterY(); } set { SetScaleCenterY(value); } }

        public override float GetScaleCenterX()
        {
            return this.mScaleCenterX;
        }

        public override float GetScaleCenterY()
        {
            return this.mScaleCenterY;
        }

        public override void SetScaleCenterX(/* final */ float pScaleCenterX)
        {
            this.mScaleCenterX = pScaleCenterX;
        }

        public override void SetScaleCenterY(/* final */ float pScaleCenterY)
        {
            this.mScaleCenterY = pScaleCenterY;
        }

        public override void SetScaleCenter(/* final */ float pScaleCenterX, /* final */ float pScaleCenterY)
        {
            this.mScaleCenterX = pScaleCenterX;
            this.mScaleCenterY = pScaleCenterY;
        }

        public bool UpdatePhysics { get { return IsUpdatePhysics(); } set { SetUpdatePhysics(value); } }

        public override bool IsUpdatePhysics()
        {
            return this.mUpdatePhysics;
        }

        /**
         * Enable or disable the AndEngine <b>internal</b> physics, you usually call this once you use the AndEnginePhysicsBox2dExtension.
         * @param pUpdatePhysics
         */
        public void SetUpdatePhysics(/* final */ bool pUpdatePhysics)
        {
            this.mUpdatePhysics = pUpdatePhysics;
        }

        public bool CullingEnabled { get { return IsCullingEnabled(); } set { SetCullingEnabled(value); } }

        public override bool IsCullingEnabled()
        {
            return this.mCullingEnabled;
        }

        public override void SetCullingEnabled(/* final */ bool pCullingEnabled)
        {
            this.mCullingEnabled = pCullingEnabled;
        }

        public override void SetBlendFunction(/* final */ int pSourceBlendFunction, /* final */ int pDestinationBlendFunction)
        {
            this.mSourceBlendFunction = pSourceBlendFunction;
            this.mDestinationBlendFunction = pDestinationBlendFunction;
        }

        public float WidthScaled { get { return GetWidthScaled(); } }

        public override float GetWidthScaled()
        {
            return this./*getWidth()*/Width * this.mScaleX;
        }

        public float HeightScaled { get { return GetHeightScaled(); } }

        public override float GetHeightScaled()
        {
            return this./*getHeight()*/Height * this.mScaleY;
        }

        //public override void addShapeModifier(/* final */ IModifier<IShape> pShapeModifier)
        public override void AddShapeModifier(andengine.util.modifier.IModifier<IShape> pShapeModifier)
        {
            this.mShapeModifiers.Add(pShapeModifier);
        }

        //public override bool removeShapeModifier(/* final */ IModifier<IShape> pShapeModifier)
        public override bool RemoveShapeModifier(andengine.util.modifier.IModifier<IShape> pShapeModifier)
        {
            return this.mShapeModifiers.Remove(pShapeModifier);
        }

        public override void ClearShapeModifiers()
        {
            this.mShapeModifiers.Clear();
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override bool OnAreaTouched(/* final */ TouchEvent pSceneTouchEvent, /* final */ float pTouchAreaLocalX, /* final */ float pTouchAreaLocalY)
        {
            return false;
        }

        /**
         * Will only be performed if {@link Shape#isCullingEnabled()} is true.
         * @param pCamera
         * @return <code>true</code> when this object is visible by the {@link Camera}, <code>false</code> otherwise.
         */
        protected abstract bool IsCulled(/* final */ Camera pCamera);

        protected void OnPositionChanged()
        {

        }

        protected abstract void OnApplyVertices(/* final */ GL10 pGL);
        protected abstract void DrawVertices(/* final */ GL10 pGL, /* final */ Camera pCamera);

        protected override void OnManagedUpdate(/* final */ float pSecondsElapsed)
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
                    this.OnPositionChanged();
                }
            }

            this.mShapeModifiers.OnUpdate(pSecondsElapsed);
        }

        protected override void OnManagedDraw(/* final */ GL10 pGL, /* final */ Camera pCamera)
        {
            if (this.mCullingEnabled == false || this.IsCulled(pCamera) == false)
            {
                this.OnInitDraw(pGL);

                pGL.GlPushMatrix();
                {
                    this.OnApplyVertices(pGL);
                    this.OnApplyTransformations(pGL);
                    this.DrawVertices(pGL, pCamera);
                }
                pGL.GlPopMatrix();
            }
        }

        protected void OnInitDraw(/* final */ GL10 pGL)
        {
            GLHelper.SetColor(pGL, this.mRed, this.mGreen, this.mBlue, this.mAlpha);

            GLHelper.EnableVertexArray(pGL);
            GLHelper.BlendFunction(pGL, this.mSourceBlendFunction, this.mDestinationBlendFunction);
        }

        protected void OnApplyTransformations(/* final */ GL10 pGL)
        {
            this.ApplyTranslation(pGL);

            this.ApplyRotation(pGL);

            this.ApplyScale(pGL);
        }

        protected void ApplyTranslation(/* final */ GL10 pGL)
        {
            pGL.GlTranslatef(this.mX, this.mY, 0);
        }

        protected void ApplyRotation(/* final */ GL10 pGL)
        {
            /* final */
            float rotation = this.mRotation;

            if (rotation != 0)
            {
                /* final */
                float rotationCenterX = this.mRotationCenterX;
                /* final */
                float rotationCenterY = this.mRotationCenterY;

                pGL.GlTranslatef(rotationCenterX, rotationCenterY, 0);
                pGL.GlRotatef(rotation, 0, 0, 1);
                pGL.GlTranslatef(-rotationCenterX, -rotationCenterY, 0);
            }
        }

        protected void ApplyScale(/* final */ GL10 pGL)
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

                pGL.GlTranslatef(scaleCenterX, scaleCenterY, 0);
                pGL.GlScalef(scaleX, scaleY, 1);
                pGL.GlTranslatef(-scaleCenterX, -scaleCenterY, 0);
            }
        }

        public override void Reset()
        {
            //super.reset();
            base.Reset();

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

            this.OnPositionChanged();

            this.mRed = 1.0f;
            this.mGreen = 1.0f;
            this.mBlue = 1.0f;
            this.mAlpha = 1.0f;

            this.mSourceBlendFunction = BLENDFUNCTION_SOURCE_DEFAULT;
            this.mDestinationBlendFunction = BLENDFUNCTION_DESTINATION_DEFAULT;

            this.mShapeModifiers.Reset();
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
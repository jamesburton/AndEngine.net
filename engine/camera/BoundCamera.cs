namespace andengine.engine.camera
{

    /**
     * @author Nicolas Gramlich
     * @since 15:55:54 - 27.07.2010
     */
    public class BoundCamera : Camera
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        protected bool mBoundsEnabled;

        private float mBoundsMinX;
        private float mBoundsMaxX;
        private float mBoundsMinY;
        private float mBoundsMaxY;

        private float mBoundsCenterX;
        private float mBoundsCenterY;

        private float mBoundsWidth;
        private float mBoundsHeight;

        // ===========================================================
        // Constructors
        // ===========================================================

        public BoundCamera(float pX, float pY, float pWidth, float pHeight)
            : base(pX, pY, pWidth, pHeight)
        {
        }

        public BoundCamera(float pX, float pY, float pWidth, float pHeight, float pBoundMinX, float pBoundMaxX, float pBoundMinY, float pBoundMaxY)
            : base(pX, pY, pWidth, pHeight)
        {
            this.SetBounds(pBoundMinX, pBoundMaxX, pBoundMinY, pBoundMaxY);
            this.mBoundsEnabled = true;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public bool IsBoundsEnabled()
        {
            return this.mBoundsEnabled;
        }

        public void SetBoundsEnabled(bool pBoundsEnabled)
        {
            this.mBoundsEnabled = pBoundsEnabled;
        }

        public void setBounds(float pBoundMinX, float pBoundMaxX, float pBoundMinY, float pBoundMaxY)
        {
            this.mBoundsMinX = pBoundMinX;
            this.mBoundsMaxX = pBoundMaxX;
            this.mBoundsMinY = pBoundMinY;
            this.mBoundsMaxY = pBoundMaxY;

            this.mBoundsWidth = this.mBoundsMaxX - this.mBoundsMinX;
            this.mBoundsHeight = this.mBoundsMaxY - this.mBoundsMinY;

            this.mBoundsCenterX = this.mBoundsMinX + this.mBoundsWidth * 0.5f;
            this.mBoundsCenterY = this.mBoundsMinY + this.mBoundsHeight * 0.5f;
        }

        public float GetBoundsWidth()
        {
            return this.mBoundsWidth;
        }
        public float BoundsWidth { get { return GetBoundsWidth(); } }

        //public float getBoundsHeight() {
        public float GetBoundsHeight()
        {
            return this.mBoundsHeight;
        }

        public float BoundsHeight { get { return GetBoundsHeight(); } }

        public override /* or virtual */ void SetCenter(float pCenterX, float pCenterY)
        {
            base.SetCenter(pCenterX, pCenterY);

            if (this.mBoundsEnabled)
            {
                EnsureInBounds();
            }
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        protected void EnsureInBounds()
        {
            base.SetCenter(this.DetermineBoundedX(), this.DetermineBoundedY());
        }

        private float DetermineBoundedX()
        {
            //if(this.mBoundsWidth < this.getWidth()) {
            if (this.mBoundsWidth < this.Width)
            {
                return this.mBoundsCenterX;
            }
            else
            {
                //final float currentCenterX = this.getCenterX();
                float currentCenterX = this.CenterX;

                //final float minXBoundExceededAmount = this.mBoundsMinX - this.getMinX();
                float minXBoundExceededAmount = this.mBoundsMinX - this.MinX;
                bool minXBoundExceeded = minXBoundExceededAmount > 0;

                //float maxXBoundExceededAmount = this.getMaxX() - this.mBoundsMaxX;
                float maxXBoundExceededAmount = this.MaxX - this.mBoundsMaxX;
                bool maxXBoundExceeded = maxXBoundExceededAmount > 0;

                if (minXBoundExceeded)
                {
                    if (maxXBoundExceeded)
                    {
                        /* Min and max X exceeded. */
                        return currentCenterX - maxXBoundExceededAmount + minXBoundExceededAmount;
                    }
                    else
                    {
                        /* Only min X exceeded. */
                        return currentCenterX + minXBoundExceededAmount;
                    }
                }
                else
                {
                    if (maxXBoundExceeded)
                    {
                        /* Only max X exceeded. */
                        return currentCenterX - maxXBoundExceededAmount;
                    }
                    else
                    {
                        /* Nothing exceeded. */
                        return currentCenterX;
                    }
                }
            }
        }

        private float DetermineBoundedY()
        {
            //if(this.mBoundsHeight < this.getHeight()) {
            if (this.mBoundsHeight < this.Height)
            {
                return this.mBoundsCenterY;
            }
            else
            {
                //float currentCenterY = this.getCenterY();
                float currentCenterY = this.CenterY;

                //final float minYBoundExceededAmount = this.mBoundsMinY - this.getMinY();
                float minYBoundExceededAmount = this.mBoundsMinY - this.MinY;
                //final bool minYBoundExceeded = minYBoundExceededAmount > 0;
                bool minYBoundExceeded = minYBoundExceededAmount > 0;

                //final float maxYBoundExceededAmount = this.getMaxY() - this.mBoundsMaxY;
                float maxYBoundExceededAmount = this.MaxY - this.mBoundsMaxY;
                //final bool maxYBoundExceeded = maxYBoundExceededAmount > 0;
                bool maxYBoundExceeded = maxYBoundExceededAmount > 0;

                if (minYBoundExceeded)
                {
                    if (maxYBoundExceeded)
                    {
                        /* Min and max Y exceeded. */
                        return currentCenterY - maxYBoundExceededAmount + minYBoundExceededAmount;
                    }
                    else
                    {
                        /* Only min Y exceeded. */
                        return currentCenterY + minYBoundExceededAmount;
                    }
                }
                else
                {
                    if (maxYBoundExceeded)
                    {
                        /* Only max Y exceeded. */
                        return currentCenterY - maxYBoundExceededAmount;
                    }
                    else
                    {
                        /* Nothing exceeded. */
                        return currentCenterY;
                    }
                }
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
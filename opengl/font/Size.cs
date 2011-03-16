namespace andengine.opengl.font
{

    /**
     * @author Nicolas Gramlich
     * @since 10:29:21 - 03.04.2010
     */
    class Size
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private float mWidth;
        private float mHeight;

        // ===========================================================
        // Constructors
        // ===========================================================

        public Size()
        {
            Init(0, 0);
        }

        public Size(float pWidth, float pHeight)
        {
            Init(pWidth, pHeight);
        }

        protected void Init(float pWidth, float pHeight)
        {
            this.SetWidth(pWidth);
            this.SetHeight(pHeight);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public float Width { get { return GetWidth(); } set { SetWidth(value); } }

        public void SetWidth(float width)
        {
            this.mWidth = width;
        }

        public float GetWidth()
        {
            return this.mWidth;
        }

        public float Height { get { return GetHeight(); } set { SetHeight(value); } }

        public void SetHeight(float height)
        {
            this.mHeight = height;
        }

        public float GetHeight()
        {
            return this.mHeight;
        }

        public void Set(int pWidth, int pHeight)
        {
            this.SetWidth(pWidth);
            this.SetHeight(pHeight);
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
}
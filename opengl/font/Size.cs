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
            this.setWidth(pWidth);
            this.setHeight(pHeight);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public float Width { get { return getWidth(); } set { setWidth(value); } }

        public void setWidth(float width)
        {
            this.mWidth = width;
        }

        public float getWidth()
        {
            return this.mWidth;
        }

        public float Height { get { return getHeight(); } set { setHeight(value); } }

        public void setHeight(float height)
        {
            this.mHeight = height;
        }

        public float getHeight()
        {
            return this.mHeight;
        }

        public void set(int pWidth, int pHeight)
        {
            this.setWidth(pWidth);
            this.setHeight(pHeight);
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
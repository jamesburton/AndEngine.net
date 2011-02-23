namespace andengine.entity
{

    //import javax.microedition.khronos.opengles.GL10;
    // TODO: Verify mapping
    using OpenTK.Graphics.ES20;

    using andengine.engine.camera/*.Camera*/;


    /**
     * @author Nicolas Gramlich
     * @since 12:00:48 - 08.03.2010
     */
    public abstract class Entity : IEntity
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private bool mVisible = true;
        private bool mIgnoreUpdate;
        private int mZIndex = 0;

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

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public bool isVisible()
        {
            return this.mVisible;
        }

        public void setVisible(/* final */ bool pVisible)
        {
            this.mVisible = pVisible;
        }

        public bool isIgnoreUpdate()
        {
            return this.mIgnoreUpdate;
        }

        public void setIgnoreUpdate(/* final */ bool pIgnoreUpdate)
        {
            this.mIgnoreUpdate = pIgnoreUpdate;
        }

        public override int getZIndex()
        {
            return this.mZIndex;
        }

        public override void setZIndex(/* final */ int pZIndex)
        {
            this.mZIndex = pZIndex;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected abstract void onManagedDraw(/* final */ GL10 pGL, /* final */ Camera pCamera);

        public override /* final */ sealed void onDraw(/* final */ GL10 pGL, /* final */ Camera pCamera)
        {
            if (this.mVisible)
            {
                this.onManagedDraw(pGL, pCamera);
            }
        }

        protected abstract void onManagedUpdate(/* final */ float pSecondsElapsed);

        public override /* final */ sealed void onUpdate(/* final */ float pSecondsElapsed)
        {
            if (!this.mIgnoreUpdate)
            {
                this.onManagedUpdate(pSecondsElapsed);
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public override void reset()
        {
            this.mVisible = true;
            this.mIgnoreUpdate = false;
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
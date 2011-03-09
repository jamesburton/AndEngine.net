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

        public /* override */ virtual int GetZIndex()
        {
            return this.mZIndex;
        }

        public /* override */ virtual void SetZIndex(/* final */ int pZIndex)
        {
            this.mZIndex = pZIndex;
        }

        public int ZIndex { get { return GetZIndex(); } set { SetZIndex(value); } }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

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
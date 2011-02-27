namespace andengine.util.pool
{

    using IEntity = andengine.entity.IEntity;
    using ILayer = andengine.entity.layer.ILayer;

    /**
     * @author Nicolas Gramlich
     * @since 00:53:22 - 28.08.2010
     */
    public class EntityRemoveRunnablePoolItem : RunnablePoolItem
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        protected IEntity mEntity;
        protected ILayer mLayer;

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public void setEntity(IEntity pEntity)
        {
            this.mEntity = pEntity;
        }

        public void setLayer(ILayer pLayer)
        {
            this.mLayer = pLayer;
        }

        public void set(IEntity pEntity, ILayer pLayer)
        {
            this.mEntity = pEntity;
            this.mLayer = pLayer;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override void run()
        {
            this.mLayer.removeEntity(this.mEntity);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
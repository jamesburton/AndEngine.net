namespace andengine.opengl.buffer
{

    //import javax.microedition.khronos.opengles.GL11;
    using GL11 = Javax.Microedition.Khronos.Opengles.IGL11;

    using FastFloatBuffer = andengine.opengl.util.FastFloatBuffer;
    using GLHelper = andengine.opengl.util.GLHelper;

    /**
     * @author Nicolas Gramlich
     * @since 14:22:56 - 07.04.2010
     */
    public abstract class BufferObject
    {
        // ===========================================================
        // Constants
        // ===========================================================

        private static /* final */ readonly int[] HARDWAREBUFFERID_FETCHER = new int[1];

        // ===========================================================
        // Fields
        // ===========================================================

        protected /* final */ readonly int[] mBufferData;

        private /* final */ readonly int mDrawType;

        private /* final */ readonly FastFloatBuffer mFloatBuffer;

        private int mHardwareBufferID = -1;
        private bool mLoadedToHardware;
        private bool mHardwareBufferNeedsUpdate = true;

        // ===========================================================
        // Constructors
        // ===========================================================

        public BufferObject(int pCapacity, int pDrawType)
        {
            this.mDrawType = pDrawType;
            this.mBufferData = new int[pCapacity];
            this.mFloatBuffer = new FastFloatBuffer(pCapacity);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public FastFloatBuffer getFloatBuffer()
        {
            return this.mFloatBuffer;
        }

        public int getHardwareBufferID()
        {
            return this.mHardwareBufferID;
        }

        public bool isLoadedToHardware()
        {
            return this.mLoadedToHardware;
        }

        void setLoadedToHardware(bool pLoadedToHardware)
        {
            this.mLoadedToHardware = pLoadedToHardware;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public void setHardwareBufferNeedsUpdate()
        {
            this.mHardwareBufferNeedsUpdate = true;
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public void selectOnHardware(GL11 pGL11)
        {
            int hardwareBufferID = this.mHardwareBufferID;
            if (hardwareBufferID == -1)
            {
                return;
            }

            GLHelper.bindBuffer(pGL11, hardwareBufferID); // TODO Does this always need to be binded, or are just for buffers of the same 'type'(texture/vertex)?

            if (this.mHardwareBufferNeedsUpdate)
            {
                //			Debug.d("BufferObject.updating: ID = "  + this.mHardwareBufferID);
                this.mHardwareBufferNeedsUpdate = false;
                //synchronized(this) {
                object thisLock = new object();
                lock (thisLock)
                {
                    GLHelper.bufferData(pGL11, this.mFloatBuffer.mByteBuffer, this.mDrawType);
                }
            }
        }

        public void loadToHardware(GL11 pGL11)
        {
            this.mHardwareBufferID = this.generateHardwareBufferID(pGL11);
            //		Debug.d("BufferObject.loadToHardware(): ID = " + this.mHardwareBufferID);

            this.mLoadedToHardware = true;
        }

        public void unloadFromHardware(GL11 pGL11)
        {
            this.deleteBufferOnHardware(pGL11);

            this.mHardwareBufferID = -1;

            this.mLoadedToHardware = false;
        }

        private void deleteBufferOnHardware(GL11 pGL11)
        {
            GLHelper.deleteBuffer(pGL11, this.mHardwareBufferID);
        }

        private int generateHardwareBufferID(GL11 pGL11)
        {
            pGL11.GlGenBuffers(1, HARDWAREBUFFERID_FETCHER, 0);

            return HARDWAREBUFFERID_FETCHER[0];
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}

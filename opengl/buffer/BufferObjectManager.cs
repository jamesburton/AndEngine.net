namespace andengine.opengl.buffer
{

    //import java.util.ArrayList;
    using System.Collections.Generic;
    //import java.util.HashSet;
    // TODO: Match not found:- using HashSet = Java.Util.HashSet;

    //import javax.microedition.khronos.opengles.GL11;
    using GL11 = Javax.Microedition.Khronos.Opengles.IGL11;

    /**
     * @author Nicolas Gramlich
     * @since 17:48:46 - 08.03.2010
     */
    public class BufferObjectManager
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private static readonly HashSet<BufferObject> mBufferObjectsManaged = new HashSet<BufferObject>();

        private static readonly /*ArrayList*/List<BufferObject> mBufferObjectsLoaded = new /*ArrayList*/List<BufferObject>();

        private static /*final ArrayList*/ readonly List<BufferObject> mBufferObjectsToBeLoaded = new /*ArrayList*/List<BufferObject>();
        private static /*final ArrayList*/ readonly List<BufferObject> mBufferObjectsToBeUnloaded = new /*ArrayList*/List<BufferObject>();

        private static BufferObjectManager mActiveInstance = null;

        // ===========================================================
        // Constructors
        // ===========================================================

        public static BufferObjectManager GetActiveInstance()
        {
            return BufferObjectManager.mActiveInstance;
        }

        public static void SetActiveInstance(BufferObjectManager pActiveInstance)
        {
            BufferObjectManager.mActiveInstance = pActiveInstance;
        }

        public static BufferObjectManager ActiveInstance { get { return GetActiveInstance(); } set { SetActiveInstance(value); } }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public void Clear()
        {
            BufferObjectManager.mBufferObjectsToBeLoaded.Clear();
            BufferObjectManager.mBufferObjectsLoaded.Clear();
            BufferObjectManager.mBufferObjectsManaged.Clear();
        }

        public void LoadBufferObject(BufferObject pBufferObject)
        {
            if (pBufferObject == null)
            {
                return;
            }

            if (BufferObjectManager.mBufferObjectsManaged.Contains(pBufferObject))
            {
                /* Just make sure it doesn't get deleted. */
                BufferObjectManager.mBufferObjectsToBeUnloaded.Remove(pBufferObject);
            }
            else
            {
                BufferObjectManager.mBufferObjectsManaged.Add(pBufferObject);
                BufferObjectManager.mBufferObjectsToBeLoaded.Add(pBufferObject);
            }
        }

        public void UnloadBufferObject(BufferObject pBufferObject)
        {
            if (pBufferObject == null)
            {
                return;
            }
            if (BufferObjectManager.mBufferObjectsManaged.Contains(pBufferObject))
            {
                if (BufferObjectManager.mBufferObjectsLoaded.Contains(pBufferObject))
                {
                    BufferObjectManager.mBufferObjectsToBeUnloaded.Add(pBufferObject);
                }
                else if (BufferObjectManager.mBufferObjectsToBeLoaded.Remove(pBufferObject))
                {
                    BufferObjectManager.mBufferObjectsManaged.Remove(pBufferObject);
                }
            }
        }

        //public void loadBufferObjects(final BufferObject... pBufferObjects) {
        public void LoadBufferObjects(params BufferObject[] pBufferObjects)
        {
            //for(int i = pBufferObjects.length - 1; i >= 0; i--) {
            for (int i = pBufferObjects.Length - 1; i >= 0; i--)
            {
                this.LoadBufferObject(pBufferObjects[i]);
            }
        }

        //public void unloadBufferObjects(final BufferObject... pBufferObjects) {
        public void UnloadBufferObjects(params BufferObject[] pBufferObjects)
        {
            //for(int i = pBufferObjects.length - 1; i >= 0; i--) {
            for (int i = pBufferObjects.Length - 1; i >= 0; i--)
            {
                this.UnloadBufferObject(pBufferObjects[i]);
            }
        }

        public void ReloadBufferObjects()
        {
            /*final ArrayList*/
            List<BufferObject> loadedBufferObjects = BufferObjectManager.mBufferObjectsLoaded;
            for (int i = loadedBufferObjects.Count - 1; i >= 0; i--)
            {
                loadedBufferObjects[i].SetLoadedToHardware(false);
            }

            BufferObjectManager.mBufferObjectsToBeLoaded.AddRange(loadedBufferObjects);

            loadedBufferObjects.Clear();
        }

        public void UpdateBufferObjects(GL11 pGL11)
        {
            HashSet<BufferObject> bufferObjectsManaged = BufferObjectManager.mBufferObjectsManaged;
            List<BufferObject> bufferObjectsLoaded = BufferObjectManager.mBufferObjectsLoaded;
            List<BufferObject> bufferObjectsToBeLoaded = BufferObjectManager.mBufferObjectsToBeLoaded;
            List<BufferObject> bufferObjectsToBeUnloaded = BufferObjectManager.mBufferObjectsToBeUnloaded;

            /* First load pending BufferObjects. */
            int bufferObjectToBeLoadedCount = bufferObjectsToBeLoaded.Count;

            if (bufferObjectToBeLoadedCount > 0)
            {
                for (int i = bufferObjectToBeLoadedCount - 1; i >= 0; i--)
                {
                    BufferObject bufferObjectToBeLoaded = bufferObjectsToBeLoaded[i];
                    if (!bufferObjectToBeLoaded.IsLoadedToHardware())
                    {
                        bufferObjectToBeLoaded.LoadToHardware(pGL11);
                        bufferObjectToBeLoaded.SetHardwareBufferNeedsUpdate();
                    }
                    bufferObjectsLoaded.Add(bufferObjectToBeLoaded);
                }

                bufferObjectsToBeLoaded.Clear();
            }

            /* Then unload pending BufferObjects. */
            int bufferObjectsToBeUnloadedCount = bufferObjectsToBeUnloaded.Count;

            if (bufferObjectsToBeUnloadedCount > 0)
            {
                for (int i = bufferObjectsToBeUnloadedCount - 1; i >= 0; i--)
                {
                    //BufferObject bufferObjectToBeUnloaded = bufferObjectsToBeUnloaded.remove(i);
                    BufferObject bufferObjectToBeUnloaded = bufferObjectsToBeUnloaded[i];
                    bufferObjectsToBeUnloaded.Remove(bufferObjectToBeUnloaded);
                    if (bufferObjectToBeUnloaded.IsLoadedToHardware())
                    {
                        bufferObjectToBeUnloaded.UnloadFromHardware(pGL11);
                    }
                    bufferObjectsLoaded.Remove(bufferObjectToBeUnloaded);
                    bufferObjectsManaged.Remove(bufferObjectToBeUnloaded);
                }
            }

        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
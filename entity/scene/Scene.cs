namespace andengine.entity.scene
{

    using andengine.util.constants;
    //import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_X;
    //import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_Y;

    //import java.util.ArrayList;
    //using ArrayList = Android.Util.ArrayList;
    using System.Collections.Generic;

    //import javax.microedition.khronos.opengles.GL10;
    //using Android.Opengl;
    //using OpenTK.Graphics.ES20;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;

    //using andengine.engine.handler;
    //using andengine.engine.camera.Camera;
    using Camera = andengine.engine.camera.Camera;
    //using andengine.engine.handler.IUpdateHandler;
    using IUpdateHandler = andengine.engine.handler.IUpdateHandler;
    //using andengine.engine.handler.UpdateHandlerList;
    using UpdateHandlerList = andengine.engine.handler.UpdateHandlerList;
    //using andengine.engine.handler.runnable.RunnableHandler;
    using RunnableHandler = andengine.engine.handler.runnable.RunnableHandler;
    //using andengine.entity;
    //using andengine.entity.Entity;
    using Entity = andengine.entity.Entity;
    //using andengine.entity.layer;
    //using andengine.entity.layer.DynamicCapacityLayer;
    using DynamicCapacityLayer = andengine.entity.layer.DynamicCapacityLayer;
    //using andengine.entity.layer.FixedCapacityLayer;
    using FixedCapacityLayer = andengine.entity.layer.FixedCapacityLayer;
    //using andengine.entity.layer.ILayer;
    using ILayer = andengine.entity.layer.ILayer;
    //using andengine.entity.layer.ZIndexSorter;
    using IndexSorter = andengine.entity.layer.ZIndexSorter;
    //using andengine.entity.scene.background;
    //using andengine.entity.scene.background.ColorBackground;
    using ColorBackground = andengine.entity.scene.background.ColorBackground;
    //using andengine.entity.scene.background.IBackground;
    using IBackground = andengine.entity.scene.background.IBackground;
    //using andengine.entity.shape;
    //using andengine.entity.shape.Shape;
    using Shape = andengine.entity.shape.Shape;
    //using andengine.input.touch;
    //using andengine.input.touch.TouchEvent;
    using TouchEvent = andengine.input.touch.TouchEvent;
    //using andengine.opengl.util;
    //using andengine.opengl.util.GLHelper;
    using GLHelper = andengine.opengl.util.GLHelper;

    //import android.util.SparseArray;
    //using SparseArray = Android.Util.SparseArray;
    using Android.Util;
    //import android.view.MotionEvent;
    using MotionEvent = Android.Views.MotionEvent;

    /**
     * @author Nicolas Gramlich
     * @since 12:47:39 - 08.03.2010
     */
    public class Scene : Entity
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private float mSecondsElapsedTotal;

        protected Scene mParentScene;
        protected Scene mChildScene;
        private bool mChildSceneModalDraw;
        private bool mChildSceneModalUpdate;
        private bool mChildSceneModalTouch;

        private /* final */ readonly int mLayerCount;
        private /* final */ readonly ILayer[] mLayers;

        //private /* final */ readonly ArrayList<ITouchArea> mTouchAreas = new ArrayList<ITouchArea>();
        private /* final */ readonly List<ITouchArea> mTouchAreas = new List<ITouchArea>();

        private /* final */ readonly RunnableHandler mRunnableHandler = new RunnableHandler();

        private /* final */ readonly UpdateHandlerList mUpdateHandlers = new UpdateHandlerList();

        private IOnSceneTouchListener mOnSceneTouchListener;

        private IOnAreaTouchListener mOnAreaTouchListener;

        private IBackground mBackground = new ColorBackground(0, 0, 0); // Black
        private bool mBackgroundEnabled = true;

        private bool mOnAreaTouchTraversalBackToFront = true;

        private bool mTouchAreaBindingEnabled = false;
        private /* final */ readonly System.Collections.Generic.SparseArray<ITouchArea> mTouchAreaBindings = new SparseArray<ITouchArea>();

        // ===========================================================
        // Constructors
        // ===========================================================

        public Scene(/* final */ int pLayerCount)
        {
            this.mLayerCount = pLayerCount;
            this.mLayers = new ILayer[pLayerCount];
            this.createLayers();
        }

        public Scene(/* final */ int pLayerCount, /* final */ bool pFixedCapacityLayers, /* final */ params int[] pLayerCapacities) /* throws IllegalArgumentException */ {
            if (pLayerCount != pLayerCapacities.Length)
            {
                throw new IllegalArgumentException("pLayerCount must be the same as the length of pLayerCapacities.");
            }
            this.mLayerCount = pLayerCount;
            this.mLayers = new ILayer[pLayerCount];
            this.createLayers(pFixedCapacityLayers, pLayerCapacities);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public float getSecondsElapsedTotal()
        {
            return this.mSecondsElapsedTotal;
        }

        public IBackground getBackground()
        {
            return this.mBackground;
        }

        public void setBackground(/* final */ IBackground pBackground)
        {
            this.mBackground = pBackground;
        }

        public ILayer getLayer(/* final */ int pLayerIndex) /* throws ArrayIndexOutOfBoundsException */ {
            return this.mLayers[pLayerIndex];
        }

        public int getLayerCount()
        {
            return this.mLayers.Length;
        }

        public ILayer getBottomLayer()
        {
            return this.mLayers[0];
        }

        public ILayer getTopLayer()
        {
            return this.mLayers[this.mLayerCount - 1];
        }

        public void setLayer(/* final */ int pLayerIndex, /* final */ ILayer pLayer)
        {
            this.mLayers[pLayerIndex] = pLayer;
        }

        public void swapLayers(/* final */ int pLayerIndexA, /* final */ int pLayerIndexB)
        {
            /* final */
            ILayer[] layers = this.mLayers;
            /* final */
            ILayer tmp = layers[pLayerIndexA];
            layers[pLayerIndexA] = layers[pLayerIndexB];
            layers[pLayerIndexB] = tmp;
        }

        /**
         * Similar to {@link Scene#setLayer(int, ILayer)} but returns the {@link ILayer} that would be overwritten.
         * 
         * @param pLayerIndex
         * @param pLayer
         * @return the layer that has been replaced.
         */
        public ILayer replaceLayer(/* final */ int pLayerIndex, /* final */ ILayer pLayer)
        {
            /* final */
            ILayer[] layers = this.mLayers;
            /* final */
            ILayer oldLayer = layers[pLayerIndex];
            layers[pLayerIndex] = pLayer;
            return oldLayer;
        }

        /**
         * Sorts the {@link ILayer} based on their ZIndex. Sort is stable.
         */
        public void sortLayers()
        {
            ZIndexSorter.getInstance().sort(this.mLayers);
        }

        public bool isBackgroundEnabled()
        {
            return this.mBackgroundEnabled;
        }

        public void setBackgroundEnabled(/* final */ bool pEnabled)
        {
            this.mBackgroundEnabled = pEnabled;
        }

        public void clearTouchAreas()
        {
            this.mTouchAreas.clear();
        }

        public void registerTouchArea(/* final */ ITouchArea pTouchArea)
        {
            this.mTouchAreas.add(pTouchArea);
        }

        public void unregisterTouchArea(/* final */ ITouchArea pTouchArea)
        {
            this.mTouchAreas.remove(pTouchArea);
        }

        public void clearUpdateHandlers()
        {
            this.mUpdateHandlers.Clear();
        }

        public void registerUpdateHandler(/* final */ IUpdateHandler pUpdateHandler)
        {
            this.mUpdateHandlers.Add(pUpdateHandler);
        }

        public void unregisterUpdateHandler(/* final */ IUpdateHandler pUpdateHandler)
        {
            this.mUpdateHandlers.Remove(pUpdateHandler);
        }

        public void setOnSceneTouchListener(/* final */ IOnSceneTouchListener pOnSceneTouchListener)
        {
            this.mOnSceneTouchListener = pOnSceneTouchListener;
        }

        public IOnSceneTouchListener getOnSceneTouchListener()
        {
            return this.mOnSceneTouchListener;
        }

        public bool hasOnSceneTouchListener()
        {
            return this.mOnSceneTouchListener != null;
        }

        public void setOnAreaTouchListener(/* final */ IOnAreaTouchListener pOnAreaTouchListener)
        {
            this.mOnAreaTouchListener = pOnAreaTouchListener;
        }

        public IOnAreaTouchListener getOnAreaTouchListener()
        {
            return this.mOnAreaTouchListener;
        }

        public bool hasOnAreaTouchListener()
        {
            return this.mOnAreaTouchListener != null;
        }

        private void setParentScene(/* final */ Scene pParentScene)
        {
            this.mParentScene = pParentScene;
        }

        public bool hasChildScene()
        {
            return this.mChildScene != null;
        }

        public Scene getChildScene()
        {
            return this.mChildScene;
        }

        public void setChildSceneModal(/* final */ Scene pChildScene)
        {
            this.setChildScene(pChildScene, true, true, true);
        }

        public void setChildScene(/* final */ Scene pChildScene)
        {
            this.setChildScene(pChildScene, false, false, false);
        }

        public void setChildScene(/* final */ Scene pChildScene, /* final */ bool pModalDraw, /* final */ bool pModalUpdate, /* final */ bool pModalTouch)
        {
            pChildScene.setParentScene(this);
            this.mChildScene = pChildScene;
            this.mChildSceneModalDraw = pModalDraw;
            this.mChildSceneModalUpdate = pModalUpdate;
            this.mChildSceneModalTouch = pModalTouch;
        }

        public void clearChildScene()
        {
            this.mChildScene = null;
        }

        public void setOnAreaTouchTraversalBackToFront()
        {
            this.mOnAreaTouchTraversalBackToFront = true;
        }

        public void setOnAreaTouchTraversalFrontToBack()
        {
            this.mOnAreaTouchTraversalBackToFront = false;
        }

        /**
         * Enable or disable the binding of TouchAreas to PointerIDs (fingers).
         * When enabled: TouchAreas get bound to a PointerID (finger) when returning true in
         * {@link Shape#onAreaTouched(TouchEvent, float, float)} or
         * {@link IOnAreaTouchListener#onAreaTouched(TouchEvent, ITouchArea, float, float)}
         * with {@link MotionEvent#ACTION_DOWN}, they will receive all subsequent {@link TouchEvent}s
         * that are made with the same PointerID (finger)
         * <b>even if the {@link TouchEvent} is outside of the actual {@link ITouchArea}</b>!
         * 
         * @param pTouchAreaBindingEnabled
         */
        public void setTouchAreaBindingEnabled(/* final */ bool pTouchAreaBindingEnabled)
        {
            this.mTouchAreaBindingEnabled = pTouchAreaBindingEnabled;
            if (this.mTouchAreaBindingEnabled == false)
            {
                this.mTouchAreaBindings.clear();
            }
        }

        public bool isTouchAreaBindingEnabled()
        {
            return this.mTouchAreaBindingEnabled;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected override void onManagedDraw(/* final */ GL10 pGL, /* final */ Camera pCamera)
        {
            /* final */
            Scene childScene = this.mChildScene;
            if (childScene == null || !this.mChildSceneModalDraw)
            {
                if (this.mBackgroundEnabled)
                {
                    pCamera.onApplyPositionIndependentMatrix(pGL);
                    GLHelper.setModelViewIdentityMatrix(pGL);

                    this.mBackground.onDraw(pGL, pCamera);
                }

                pCamera.onApplyMatrix(pGL);
                GLHelper.setModelViewIdentityMatrix(pGL);

                this.drawLayers(pGL, pCamera);
            }
            if (childScene != null)
            {
                childScene.onDraw(pGL, pCamera);
            }
        }

        protected override void onManagedUpdate(/* final */ float pSecondsElapsed)
        {
            this.updateUpdateHandlers(pSecondsElapsed);

            this.mRunnableHandler.onUpdate(pSecondsElapsed);
            this.mSecondsElapsedTotal += pSecondsElapsed;

            /* final */
            Scene childScene = this.mChildScene;
            if (childScene == null || !this.mChildSceneModalUpdate)
            {
                this.mBackground.onUpdate(pSecondsElapsed);
                this.updateLayers(pSecondsElapsed);
            }

            if (childScene != null)
            {
                childScene.onUpdate(pSecondsElapsed);
            }
        }

        public bool onSceneTouchEvent(/* final */ TouchEvent pSceneTouchEvent)
        {
            /* final */
            int action = pSceneTouchEvent.getAction();
            /* final */
            bool isDownAction = action == MotionEvent.ACTION_DOWN;

            if (this.mTouchAreaBindingEnabled && !isDownAction)
            {
                /* final */
                SparseArray<ITouchArea> touchAreaBindings = this.mTouchAreaBindings;
                /* final */
                ITouchArea boundTouchArea = touchAreaBindings.get(pSceneTouchEvent.getPointerID());
                /* In the case a ITouchArea has been bound to this PointerID,
                 * we'll pass this this TouchEvent to the same ITouchArea. */
                if (boundTouchArea != null)
                {
                    /* final */
                    float sceneTouchEventX = pSceneTouchEvent.getX();
                    /* final */
                    float sceneTouchEventY = pSceneTouchEvent.getY();

                    /* Check if boundTouchArea needs to be removed. */
                    switch (action)
                    {
                        case MotionEvent.ActionPointer1Up: //ACTION_UP:
                        // TODO: Check Action mappings from MotionEvent ... missing values in MonoDroid?
                        //case MotionEvent.ACTION_CANCEL:
                            touchAreaBindings.remove(pSceneTouchEvent.getPointerID());
                    }
                    /* final */
                    bool handled = this.onAreaTouchEvent(pSceneTouchEvent, sceneTouchEventX, sceneTouchEventY, boundTouchArea);
                    if (handled != null && handled)
                    {
                        return true;
                    }
                }
            }

            /* final */
            Scene childScene = this.mChildScene;
            if (childScene != null)
            {
                /* final */
                bool handledByChild = this.onChildSceneTouchEvent(pSceneTouchEvent);
                if (handledByChild)
                {
                    return true;
                }
                else if (this.mChildSceneModalTouch)
                {
                    return false;
                }
            }

            /* final */
            float sceneTouchEventX = pSceneTouchEvent.getX();
            /* final */
            float sceneTouchEventY = pSceneTouchEvent.getY();

            /* First give the layers a chance to handle their TouchAreas. */
            {
                /* final */
                int layerCount = this.mLayerCount;
                /* final */
                ILayer[] layers = this.mLayers;
                if (this.mOnAreaTouchTraversalBackToFront)
                { /* Back to Front. */
                    for (int i = 0; i < layerCount; i++)
                    {
                        /* final */
                        ILayer layer = layers[i];
                        /* final */
                        //ArrayList<ITouchArea> layerTouchAreas = layer.getTouchAreas();
                        List<ITouchArea> layerTouchAreas = layer.getTouchAreas();
                        /* final */
                        int layerTouchAreaCount = layerTouchAreas.Count;
                        if (layerTouchAreaCount > 0)
                        {
                            for (int j = 0; j < layerTouchAreaCount; j++)
                            {
                                /* final */
                                ITouchArea layerTouchArea = layerTouchAreas[j];
                                if (layerTouchArea.contains(sceneTouchEventX, sceneTouchEventY))
                                {
                                    /* final */
                                    bool handled = this.onAreaTouchEvent(pSceneTouchEvent, sceneTouchEventX, sceneTouchEventY, layerTouchArea);
                                    if (handled != null && handled)
                                    {
                                        /* If binding of ITouchAreas is enabled and this is an ACTION_DOWN event,
                                         *  bind this ITouchArea to the PointerID. */
                                        if (this.mTouchAreaBindingEnabled && isDownAction)
                                        {
                                            this.mTouchAreaBindings[pSceneTouchEvent.getPointerID()] = layerTouchArea;
                                        }
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                { /* Front to back. */
                    for (int i = layerCount - 1; i >= 0; i--)
                    {
                        /* final */
                        ILayer layer = layers[i];
                        /* final */
                        //ArrayList<ITouchArea> layerTouchAreas = layer.getTouchAreas();
                        List<ITouchArea> layerTouchAreas = layer.getTouchAreas();
                        /* final */
                        int layerTouchAreaCount = layerTouchAreas.Count;
                        if (layerTouchAreaCount > 0)
                        {
                            for (int j = layerTouchAreaCount - 1; j >= 0; j--)
                            {
                                /* final */
                                ITouchArea layerTouchArea = layerTouchAreas[j];
                                if (layerTouchArea.contains(sceneTouchEventX, sceneTouchEventY))
                                {
                                    /* final */
                                    bool handled = this.onAreaTouchEvent(pSceneTouchEvent, sceneTouchEventX, sceneTouchEventY, layerTouchArea);
                                    if (handled != null && handled)
                                    {
                                        /* If binding of ITouchAreas is enabled and this is an ACTION_DOWN event,
                                         *  bind this ITouchArea to the PointerID. */
                                        if (this.mTouchAreaBindingEnabled && isDownAction)
                                        {
                                            this.mTouchAreaBindings[pSceneTouchEvent.getPointerID()] = layerTouchArea;
                                        }
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            /* final */
            //ArrayList<ITouchArea> touchAreas = this.mTouchAreas;
            /* final */
            int touchAreaCount = touchAreas.size();
            if (touchAreaCount > 0)
            {
                if (this.mOnAreaTouchTraversalBackToFront)
                { /* Back to Front. */
                    for (int i = 0; i < touchAreaCount; i++)
                    {
                        /* final */
                        ITouchArea touchArea = touchAreas.get(i);
                        if (touchArea.contains(sceneTouchEventX, sceneTouchEventY))
                        {
                            /* final */
                            bool handled = this.onAreaTouchEvent(pSceneTouchEvent, sceneTouchEventX, sceneTouchEventY, touchArea);
                            if (handled != null && handled)
                            {
                                /* If binding of ITouchAreas is enabled and this is an ACTION_DOWN event,
                                 *  bind this ITouchArea to the PointerID. */
                                if (this.mTouchAreaBindingEnabled && isDownAction)
                                {
                                    this.mTouchAreaBindings.put(pSceneTouchEvent.getPointerID(), touchArea);
                                }
                                return true;
                            }
                        }
                    }
                }
                else
                { /* Front to back. */
                    for (int i = touchAreaCount - 1; i >= 0; i--)
                    {
                        /* final */
                        ITouchArea touchArea = touchAreas.get(i);
                        if (touchArea.contains(sceneTouchEventX, sceneTouchEventY))
                        {
                            /* final */
                            bool handled = this.onAreaTouchEvent(pSceneTouchEvent, sceneTouchEventX, sceneTouchEventY, touchArea);
                            if (handled != null && handled)
                            {
                                /* If binding of ITouchAreas is enabled and this is an ACTION_DOWN event,
                                 *  bind this ITouchArea to the PointerID. */
                                if (this.mTouchAreaBindingEnabled && isDownAction)
                                {
                                    this.mTouchAreaBindings.put(pSceneTouchEvent.getPointerID(), touchArea);
                                }
                                return true;
                            }
                        }
                    }
                }
            }
            /* If no area was touched, the Scene itself was touched as a fallback. */
            if (this.mOnSceneTouchListener != null)
            {
                return this.mOnSceneTouchListener.onSceneTouchEvent(this, pSceneTouchEvent);
            }
            else
            {
                return false;
            }
        }

        private bool onAreaTouchEvent(/* final */ TouchEvent pSceneTouchEvent, /* final */ float sceneTouchEventX, /* final */ float sceneTouchEventY, /* final */ ITouchArea touchArea)
        {
            /* final */
            float[] touchAreaLocalCoordinates = touchArea.convertSceneToLocalCoordinates(sceneTouchEventX, sceneTouchEventY);
            /* final */
            float touchAreaLocalX = touchAreaLocalCoordinates[VERTEX_INDEX_X];
            /* final */
            float touchAreaLocalY = touchAreaLocalCoordinates[VERTEX_INDEX_Y];

            /* final */
            bool handledSelf = touchArea.onAreaTouched(pSceneTouchEvent, touchAreaLocalX, touchAreaLocalY);
            if (handledSelf)
            {
                return Boolean.TRUE;
            }
            else if (this.mOnAreaTouchListener != null)
            {
                return this.mOnAreaTouchListener.onAreaTouched(pSceneTouchEvent, touchArea, touchAreaLocalX, touchAreaLocalY);
            }
            else
            {
                return null;
            }
        }

        protected bool onChildSceneTouchEvent(/* final */ TouchEvent pSceneTouchEvent)
        {
            return this.mChildScene.onSceneTouchEvent(pSceneTouchEvent);
        }

        public override void reset()
        {
            //super.reset();
            base.reset();

            this.clearChildScene();

            /* final */
            ILayer[] layers = this.mLayers;
            for (int i = this.mLayerCount - 1; i >= 0; i--)
            {
                layers[i].reset();
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public void postRunnable(/* final */ Runnable pRunnable)
        {
            this.mRunnableHandler.postRunnable(pRunnable);
        }

        public void back()
        {
            this.clearChildScene();

            if (this.mParentScene != null)
            {
                this.mParentScene.clearChildScene();
                this.mParentScene = null;
            }
        }

        private void createLayers()
        {
            /* final */
            ILayer[] layers = this.mLayers;
            for (int i = this.mLayerCount - 1; i >= 0; i--)
            {
                layers[i] = new DynamicCapacityLayer();
            }
        }

        private void createLayers(/* final */ bool pFixedCapacityLayers, /* final */ int[] pLayerCapacities)
        {
            /* final */
            ILayer[] layers = this.mLayers;
            if (pFixedCapacityLayers)
            {
                for (int i = this.mLayerCount - 1; i >= 0; i--)
                {
                    layers[i] = new FixedCapacityLayer(pLayerCapacities[i]);
                }
            }
            else
            {
                for (int i = this.mLayerCount - 1; i >= 0; i--)
                {
                    layers[i] = new DynamicCapacityLayer(pLayerCapacities[i]);
                }
            }
        }

        private void updateLayers(/* final */ float pSecondsElapsed)
        {
            /* final */
            ILayer[] layers = this.mLayers;
            /* final */
            int layerCount = this.mLayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                layers[i].onUpdate(pSecondsElapsed);
            }
        }

        private void drawLayers(/* final */ GL10 pGL, /* final */ Camera pCamera)
        {
            /* final */
            ILayer[] layers = this.mLayers;
            /* final */
            int layerCount = this.mLayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                layers[i].onDraw(pGL, pCamera);
            }
        }

        private void updateUpdateHandlers(/* final */ float pSecondsElapsed)
        {
            if (this.mChildScene == null || !this.mChildSceneModalUpdate)
            {
                this.mUpdateHandlers.onUpdate(pSecondsElapsed);
            }

            if (this.mChildScene != null)
            {
                this.mChildScene.updateUpdateHandlers(pSecondsElapsed);
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        public /* static */ interface ITouchArea
        {
            // ===========================================================
            // Final Fields
            // ===========================================================

            // ===========================================================
            // Methods
            // ===========================================================

            /* public */ bool contains(/* final */ float pX, /* final */ float pY);

            /* public */ float[] convertSceneToLocalCoordinates(/* final */ float pX, /* final */ float pY);
            /* public */ float[] convertLocalToSceneCoordinates(/* final */ float pX, /* final */ float pY);

            /**
             * This method only fires if this {@link ITouchArea} is registered to the {@link Scene} via {@link Scene#registerTouchArea(ITouchArea)} or to a {@link ILayer} via {@link ILayer#registerTouchArea(ITouchArea)}.
             * @param pSceneTouchEvent
             * @return <code>true</code> if the event was handled (that means {@link IOnAreaTouchListener} of the {@link Scene} will not be fired!), otherwise <code>false</code>.
             */
            /* public */ bool onAreaTouched(/* final */ TouchEvent pSceneTouchEvent, /* final */ float pTouchAreaLocalX, /* final */ float pTouchAreaLocalY);
        }

        public /* static */ interface IOnAreaTouchListener
        {
            // ===========================================================
            // Constants
            // ===========================================================

            // ===========================================================
            // Methods
            // ===========================================================

            /* public */ bool onAreaTouched(/* final */ TouchEvent pSceneTouchEvent, /* final */ ITouchArea pTouchArea, /* final */ float pTouchAreaLocalX, /* final */ float pTouchAreaLocalY);
        }

        public /* static */ interface IOnSceneTouchListener
        {
            // ===========================================================
            // Final Fields
            // ===========================================================

            // ===========================================================
            // Methods
            // ===========================================================

            /* public */ bool onSceneTouchEvent(/* final */ Scene pScene, /* final */ TouchEvent pSceneTouchEvent);
        }

    }
}

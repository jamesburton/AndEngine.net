using Android.Views;
using Java.Lang;

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
    using ZIndexSorter = andengine.entity.layer.ZIndexSorter;
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
    using andengine.engine.handler.runnable;

    using IllegalArgumentException = Java.Lang.IllegalArgumentException;

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
            this.CreateLayers();
        }

        public Scene(/* final */ int pLayerCount, /* final */ bool pFixedCapacityLayers, /* final */ params int[] pLayerCapacities) /* throws IllegalArgumentException */ {
            if (pLayerCount != pLayerCapacities.Length)
            {
                throw new IllegalArgumentException("pLayerCount must be the same as the length of pLayerCapacities.");
            }
            this.mLayerCount = pLayerCount;
            this.mLayers = new ILayer[pLayerCount];
            this.CreateLayers(pFixedCapacityLayers, pLayerCapacities);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public float GetSecondsElapsedTotal()
        {
            return this.mSecondsElapsedTotal;
        }
        public float SecondsElapsedTotal { get { return GetSecondsElapsedTotal(); } }

        public IBackground GetBackground()
        {
            return this.mBackground;
        }

        public void SetBackground(/* final */ IBackground pBackground)
        {
            this.mBackground = pBackground;
        }

        public IBackground Background { get { return GetBackground(); } set { SetBackground(value); } }

        public ILayer GetLayer(/* final */ int pLayerIndex) /* throws ArrayIndexOutOfBoundsException */ {
            return this.mLayers[pLayerIndex];
        }

        public int GetLayerCount()
        {
            return this.mLayers.Length;
        }
        public int LayerCount { get { return GetLayerCount(); } }

        public ILayer GetBottomLayer()
        {
            return this.mLayers[0];
        }
        public ILayer BottomLayer { get { return GetBottomLayer(); } }

        public ILayer GetTopLayer()
        {
            return this.mLayers[this.mLayerCount - 1];
        }
        public ILayer TopLayer { get { return GetTopLayer(); } }

        public void SetLayer(/* final */ int pLayerIndex, /* final */ ILayer pLayer)
        {
            this.mLayers[pLayerIndex] = pLayer;
        }

        public void SwapLayers(/* final */ int pLayerIndexA, /* final */ int pLayerIndexB)
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
        public ILayer ReplaceLayer(/* final */ int pLayerIndex, /* final */ ILayer pLayer)
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
        public void SortLayers()
        {
            ZIndexSorter.getInstance().sort(this.mLayers);
        }

        public bool IsBackgroundEnabled()
        {
            return this.mBackgroundEnabled;
        }

        public void SetBackgroundEnabled(/* final */ bool pEnabled)
        {
            this.mBackgroundEnabled = pEnabled;
        }

        public bool BackgroundEnabled { get { return IsBackgroundEnabled(); } set { SetBackgroundEnabled(value); } }

        public void ClearTouchAreas()
        {
            this.mTouchAreas.Clear();
        }

        public void RegisterTouchArea(/* final */ ITouchArea pTouchArea)
        {
            this.mTouchAreas.Add(pTouchArea);
        }

        public void UnregisterTouchArea(/* final */ ITouchArea pTouchArea)
        {
            this.mTouchAreas.Remove(pTouchArea);
        }

        public void ClearUpdateHandlers()
        {
            this.mUpdateHandlers.Clear();
        }

        public void RegisterUpdateHandler(/* final */ IUpdateHandler pUpdateHandler)
        {
            this.mUpdateHandlers.Add(pUpdateHandler);
        }

        public void UnregisterUpdateHandler(/* final */ IUpdateHandler pUpdateHandler)
        {
            this.mUpdateHandlers.Remove(pUpdateHandler);
        }

        public void SetOnSceneTouchListener(/* final */ IOnSceneTouchListener pOnSceneTouchListener)
        {
            this.mOnSceneTouchListener = pOnSceneTouchListener;
        }

        public IOnSceneTouchListener GetOnSceneTouchListener()
        {
            return this.mOnSceneTouchListener;
        }

        public IOnSceneTouchListener OnSceneTouchListener { get { return GetOnSceneTouchListener(); } set { SetOnSceneTouchListener(value); } }

        public bool HasOnSceneTouchListener()
        {
            return this.mOnSceneTouchListener != null;
        }

        public void SetOnAreaTouchListener(/* final */ IOnAreaTouchListener pOnAreaTouchListener)
        {
            this.mOnAreaTouchListener = pOnAreaTouchListener;
        }

        public IOnAreaTouchListener GetOnAreaTouchListener()
        {
            return this.mOnAreaTouchListener;
        }

        public IOnAreaTouchListener OnAreaTouchListener { get { return GetOnAreaTouchListener(); } set { SetOnAreaTouchListener(value); } }

        public bool HasOnAreaTouchListener()
        {
            return this.mOnAreaTouchListener != null;
        }

        private void SetParentScene(/* final */ Scene pParentScene)
        {
            this.mParentScene = pParentScene;
        }

        public Scene ParentScene { set { SetParentScene(value); } }

        public bool HasChildScene()
        {
            return this.mChildScene != null;
        }

        public Scene GetChildScene()
        {
            return this.mChildScene;
        }

        public Scene ChildScene { get { return GetChildScene(); } }

        public void SetChildSceneModal(/* final */ Scene pChildScene)
        {
            this.SetChildScene(pChildScene, true, true, true);
        }

        public void SetChildScene(/* final */ Scene pChildScene)
        {
            this.SetChildScene(pChildScene, false, false, false);
        }

        public void SetChildScene(/* final */ Scene pChildScene, /* final */ bool pModalDraw, /* final */ bool pModalUpdate, /* final */ bool pModalTouch)
        {
            pChildScene.SetParentScene(this);
            this.mChildScene = pChildScene;
            this.mChildSceneModalDraw = pModalDraw;
            this.mChildSceneModalUpdate = pModalUpdate;
            this.mChildSceneModalTouch = pModalTouch;
        }

        public void ClearChildScene()
        {
            this.mChildScene = null;
        }

        public void SetOnAreaTouchTraversalBackToFront()
        {
            this.mOnAreaTouchTraversalBackToFront = true;
        }

        public void SetOnAreaTouchTraversalFrontToBack()
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
        public void SetTouchAreaBindingEnabled(/* final */ bool pTouchAreaBindingEnabled)
        {
            this.mTouchAreaBindingEnabled = pTouchAreaBindingEnabled;
            if (this.mTouchAreaBindingEnabled == false)
            {
                this.mTouchAreaBindings.Clear();
            }
        }

        public bool IsTouchAreaBindingEnabled()
        {
            return this.mTouchAreaBindingEnabled;
        }

        public bool TouchAreaBindingEnabled { get { return IsTouchAreaBindingEnabled(); } set { SetTouchAreaBindingEnabled(value); } }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected override void OnManagedDraw(/* final */ GL10 pGL, /* final */ Camera pCamera)
        {
            /* final */
            Scene childScene = this.mChildScene;
            if (childScene == null || !this.mChildSceneModalDraw)
            {
                if (this.mBackgroundEnabled)
                {
                    pCamera.OnApplyPositionIndependentMatrix(pGL);
                    GLHelper.SetModelViewIdentityMatrix(pGL);

                    this.mBackground.OnDraw(pGL, pCamera);
                }

                pCamera.OnApplyMatrix(pGL);
                GLHelper.SetModelViewIdentityMatrix(pGL);

                this.DrawLayers(pGL, pCamera);
            }
            if (childScene != null)
            {
                childScene.OnDraw(pGL, pCamera);
            }
        }

        protected override void OnManagedUpdate(/* final */ float pSecondsElapsed)
        {
            this.UpdateUpdateHandlers(pSecondsElapsed);

            this.mRunnableHandler.OnUpdate(pSecondsElapsed);
            this.mSecondsElapsedTotal += pSecondsElapsed;

            /* final */
            Scene childScene = this.mChildScene;
            if (childScene == null || !this.mChildSceneModalUpdate)
            {
                this.mBackground.OnUpdate(pSecondsElapsed);
                this.UpdateLayers(pSecondsElapsed);
            }

            if (childScene != null)
            {
                childScene.OnUpdate(pSecondsElapsed);
            }
        }

        public virtual bool OnSceneTouchEvent(/* final */ TouchEvent pSceneTouchEvent)
        {
            //* final */ int action = pSceneTouchEvent.GetAction();
            MotionEvent action = pSceneTouchEvent.GetMotionEvent();
            // final bool isDownAction = action == MotionEvent.ACTION_DOWN;
            bool isDownAction = (action == (MotionEvent) MotionEvent.ActionPointer1Down);

            // final float sceneTouchEventX = pSceneTouchEvent.getX();
            float sceneTouchEventX = pSceneTouchEvent.X;
            // final float sceneTouchEventY = pSceneTouchEvent.getY();
            float sceneTouchEventY = pSceneTouchEvent.Y;

            if (this.mTouchAreaBindingEnabled && !isDownAction)
            {
                /* final */
                SparseArray<ITouchArea> touchAreaBindings = this.mTouchAreaBindings;
                /* final */
                ITouchArea boundTouchArea = touchAreaBindings[pSceneTouchEvent.GetPointerID()];
                /* In the case a ITouchArea has been bound to this PointerID,
                 * we'll pass this this TouchEvent to the same ITouchArea. */
                if (boundTouchArea != null)
                {
                    /* Check if boundTouchArea needs to be removed. */
                    switch (action.Action)
                    {
                        //TODO: this value was MotionEvent.ActionPointer1Up in Java.  Is it important to be pointer 1?
                        case MotionEventActions.PointerUp:
                        case MotionEventActions.Cancel:
                            touchAreaBindings.RemoveAt(pSceneTouchEvent.GetPointerID());
                            break;
                    }
                    /* final */
                    bool? handled = this.OnAreaTouchEvent(pSceneTouchEvent, sceneTouchEventX, sceneTouchEventY, boundTouchArea);
                    if (handled != null && handled.Value)
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
                bool handledByChild = this.OnChildSceneTouchEvent(pSceneTouchEvent);
                if (handledByChild)
                {
                    return true;
                }
                else if (this.mChildSceneModalTouch)
                {
                    return false;
                }
            }

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
                        var layerTouchAreas = layer.GetTouchAreas();
                        /* final */
                        int layerTouchAreaCount = layerTouchAreas.Count;
                        if (layerTouchAreaCount > 0)
                        {
                            for (int j = 0; j < layerTouchAreaCount; j++)
                            {
                                /* final */
                                ITouchArea layerTouchArea = layerTouchAreas[j];
                                if (layerTouchArea.Contains(sceneTouchEventX, sceneTouchEventY))
                                {
                                    /* final */
                                    bool? handled = this.OnAreaTouchEvent(pSceneTouchEvent, sceneTouchEventX, sceneTouchEventY, layerTouchArea);
                                    if (handled != null && handled.Value)
                                    {
                                        /* If binding of ITouchAreas is enabled and this is an ACTION_DOWN event,
                                         *  bind this ITouchArea to the PointerID. */
                                        if (this.mTouchAreaBindingEnabled && isDownAction)
                                        {
                                            this.mTouchAreaBindings[pSceneTouchEvent.GetPointerID()] = layerTouchArea;
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
                        var layerTouchAreas = layer.GetTouchAreas();
                        /* final */
                        int layerTouchAreaCount = layerTouchAreas.Count;
                        if (layerTouchAreaCount > 0)
                        {
                            for (int j = layerTouchAreaCount - 1; j >= 0; j--)
                            {
                                /* final */
                                ITouchArea layerTouchArea = layerTouchAreas[j];
                                if (layerTouchArea.Contains(sceneTouchEventX, sceneTouchEventY))
                                {
                                    /* final */
                                    bool? handled = this.OnAreaTouchEvent(pSceneTouchEvent, sceneTouchEventX, sceneTouchEventY, layerTouchArea);
                                    if (handled != null && handled.Value)
                                    {
                                        /* If binding of ITouchAreas is enabled and this is an ACTION_DOWN event,
                                         *  bind this ITouchArea to the PointerID. */
                                        if (this.mTouchAreaBindingEnabled && isDownAction)
                                        {
                                            this.mTouchAreaBindings[pSceneTouchEvent.GetPointerID()] = layerTouchArea;
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
            List<ITouchArea> touchAreas = this.mTouchAreas;
            /* final */
            int touchAreaCount = touchAreas.Count;
            if (touchAreaCount > 0)
            {
                if (this.mOnAreaTouchTraversalBackToFront)
                { /* Back to Front. */
                    for (int i = 0; i < touchAreaCount; i++)
                    {
                        /* final */
                        ITouchArea touchArea = touchAreas[i];
                        if (touchArea.Contains(sceneTouchEventX, sceneTouchEventY))
                        {
                            /* final */
                            bool? handled = this.OnAreaTouchEvent(pSceneTouchEvent, sceneTouchEventX, sceneTouchEventY, touchArea);
                            if (handled != null && handled.Value)
                            {
                                /* If binding of ITouchAreas is enabled and this is an ACTION_DOWN event,
                                 *  bind this ITouchArea to the PointerID. */
                                if (this.mTouchAreaBindingEnabled && isDownAction)
                                {
                                    this.mTouchAreaBindings[pSceneTouchEvent.GetPointerID()] = touchArea;
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
                        ITouchArea touchArea = touchAreas[i];
                        if (touchArea.Contains(sceneTouchEventX, sceneTouchEventY))
                        {
                            /* final */
                            bool? handled = this.OnAreaTouchEvent(pSceneTouchEvent, sceneTouchEventX, sceneTouchEventY, touchArea);
                            if (handled != null && handled.Value)
                            {
                                /* If binding of ITouchAreas is enabled and this is an ACTION_DOWN event,
                                 *  bind this ITouchArea to the PointerID. */
                                if (this.mTouchAreaBindingEnabled && isDownAction)
                                {
                                    this.mTouchAreaBindings[pSceneTouchEvent.GetPointerID()] = touchArea;
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
                return this.mOnSceneTouchListener.OnSceneTouchEvent(this, pSceneTouchEvent);
            }
            else
            {
                return false;
            }
        }

        private bool? OnAreaTouchEvent(/* final */ TouchEvent pSceneTouchEvent, /* final */ float sceneTouchEventX, /* final */ float sceneTouchEventY, /* final */ ITouchArea touchArea)
        {
            /* final */
            float[] touchAreaLocalCoordinates = touchArea.ConvertSceneToLocalCoordinates(sceneTouchEventX, sceneTouchEventY);
            /* final */
            float touchAreaLocalX = touchAreaLocalCoordinates[Constants.VERTEX_INDEX_X];
            /* final */
            float touchAreaLocalY = touchAreaLocalCoordinates[Constants.VERTEX_INDEX_Y];

            /* final */
            bool handledSelf = touchArea.OnAreaTouched(pSceneTouchEvent, touchAreaLocalX, touchAreaLocalY);
            if (handledSelf)
            {
                //return Boolean.TRUE;
                return true;
            }
            else if (this.mOnAreaTouchListener != null)
            {
                return this.mOnAreaTouchListener.OnAreaTouched(pSceneTouchEvent, touchArea, touchAreaLocalX, touchAreaLocalY);
            }
            else
            {
                return null;
            }
        }

        protected virtual bool OnChildSceneTouchEvent(/* final */ TouchEvent pSceneTouchEvent)
        {
            return this.mChildScene.OnSceneTouchEvent(pSceneTouchEvent);
        }

        public override void Reset()
        {
            //super.reset();
            base.Reset();

            this.ClearChildScene();

            /* final */
            ILayer[] layers = this.mLayers;
            for (int i = this.mLayerCount - 1; i >= 0; i--)
            {
                layers[i].Reset();
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public void PostRunnable(/* final */ IRunnable pRunnable)
        {
            this.mRunnableHandler.PostRunnable(pRunnable);
        }

        public void Back()
        {
            this.ClearChildScene();

            if (this.mParentScene != null)
            {
                this.mParentScene.ClearChildScene();
                this.mParentScene = null;
            }
        }

        private void CreateLayers()
        {
            /* final */
            ILayer[] layers = this.mLayers;
            for (int i = this.mLayerCount - 1; i >= 0; i--)
            {
                layers[i] = new DynamicCapacityLayer();
            }
        }

        private void CreateLayers(/* final */ bool pFixedCapacityLayers, /* final */ int[] pLayerCapacities)
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

        private void UpdateLayers(/* final */ float pSecondsElapsed)
        {
            /* final */
            ILayer[] layers = this.mLayers;
            /* final */
            int layerCount = this.mLayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                layers[i].OnUpdate(pSecondsElapsed);
            }
        }

        private void DrawLayers(/* final */ GL10 pGL, /* final */ Camera pCamera)
        {
            /* final */
            ILayer[] layers = this.mLayers;
            /* final */
            int layerCount = this.mLayerCount;
            for (int i = 0; i < layerCount; i++)
            {
                layers[i].OnDraw(pGL, pCamera);
            }
        }

        private void UpdateUpdateHandlers(/* final */ float pSecondsElapsed)
        {
            if (this.mChildScene == null || !this.mChildSceneModalUpdate)
            {
                this.mUpdateHandlers.OnUpdate(pSecondsElapsed);
            }

            if (this.mChildScene != null)
            {
                this.mChildScene.UpdateUpdateHandlers(pSecondsElapsed);
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

            /* public */ bool Contains(/* final */ float pX, /* final */ float pY);

            /* public */ float[] ConvertSceneToLocalCoordinates(/* final */ float pX, /* final */ float pY);
            /* public */ float[] ConvertLocalToSceneCoordinates(/* final */ float pX, /* final */ float pY);

            /**
             * This method only fires if this {@link ITouchArea} is registered to the {@link Scene} via {@link Scene#registerTouchArea(ITouchArea)} or to a {@link ILayer} via {@link ILayer#registerTouchArea(ITouchArea)}.
             * @param pSceneTouchEvent
             * @return <code>true</code> if the event was handled (that means {@link IOnAreaTouchListener} of the {@link Scene} will not be fired!), otherwise <code>false</code>.
             */
            /* public */ bool OnAreaTouched(/* final */ TouchEvent pSceneTouchEvent, /* final */ float pTouchAreaLocalX, /* final */ float pTouchAreaLocalY);
        }

        public /* static */ interface IOnAreaTouchListener
        {
            // ===========================================================
            // Constants
            // ===========================================================

            // ===========================================================
            // Methods
            // ===========================================================

            /* public */ bool OnAreaTouched(/* final */ TouchEvent pSceneTouchEvent, /* final */ ITouchArea pTouchArea, /* final */ float pTouchAreaLocalX, /* final */ float pTouchAreaLocalY);
        }

        public /* static */ interface IOnSceneTouchListener
        {
            // ===========================================================
            // Final Fields
            // ===========================================================

            // ===========================================================
            // Methods
            // ===========================================================

            /* public */ bool OnSceneTouchEvent(/* final */ Scene pScene, /* final */ TouchEvent pSceneTouchEvent);
        }

    }
}

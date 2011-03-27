namespace andengine.util.modifier
{

    //import java.util.ArrayList;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using andengine.engine.handler/*.IUpdateHandler*/;

    /**
     * @author Nicolas Gramlich
     * @since 14:34:57 - 03.09.2010
     */
    public class ModifierList<T> : /*ArrayList<IModifier<T>>*/ List<IModifier<T>>, IUpdateHandler
    {
        // ===========================================================
        // Constants
        // ===========================================================

        //private static final long serialVersionUID = 1610345592534873475L;
        private const long serialVersionUID = 1436103455925462373L;

        // ===========================================================
        // Fields
        // ===========================================================

        private /* final */ readonly T mTarget;

        // ===========================================================
        // Constructors
        // ===========================================================

        public ModifierList(/* final */ T pTarget)
        {
            this.mTarget = pTarget;
        }

        public ModifierList(T pTarget, int pCapacity) : base(pCapacity)
        {
		    this.mTarget = pTarget;
	    }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public T GetTarget()
        {
            return this.mTarget;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public /* override */ void OnUpdate(/* final */ float pSecondsElapsed)
        {
            // final ArrayList<IModifier<T>> modifiers = this;
            List<IModifier<T>> modifiers = this;
            /* final */ int modifierCount = this.Count;
            if (modifierCount > 0)
            {
                for (int i = modifierCount - 1; i >= 0; i--)
                {
                    /* final */
                    IModifier<T> modifier = modifiers[i];
                    modifier.OnUpdate(pSecondsElapsed, this.mTarget);
                    //if (modifier.isFinished() && modifier.isRemoveWhenFinished())
                    if (modifier.Finished && modifier.RemoveWhenFinished)
                    {
                        modifiers.RemoveAt(i);
                    }
                }
            }
        }

        public /* override */ void Reset()
        {
            // final ArrayList<IModifier<T>> modifiers = this;
            List<IModifier<T>> modifiers = this;
            for (int i = modifiers.Count - 1; i >= 0; i--)
            {
                modifiers[i].Reset();
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
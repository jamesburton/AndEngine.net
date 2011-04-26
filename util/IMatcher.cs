﻿namespace andengine.util
{
    public interface IMatcher<in T> {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        bool Matches(T pObject);
    }
}
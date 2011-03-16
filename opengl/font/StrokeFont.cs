namespace andengine.opengl.font
{

    using Texture = andengine.opengl.texture.Texture;

    using Paint = Android.Graphics.Paint;
    using Typeface = Android.Graphics.Typeface;
    using Style = Android.Graphics.Paint.Style;
    using System;

    /**
     * @author Nicolas Gramlich
     * @since 10:39:33 - 03.04.2010
     */
    public class StrokeFont : Font
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private Paint mStrokePaint;
        private bool mStrokeOnly;

        // ===========================================================
        // Constructors
        // ===========================================================

        public StrokeFont(Texture pTexture, Typeface pTypeface, float pSize, bool pAntiAlias, int pColor, float pStrokeWidth, int pStrokeColor)
            : base(pTexture, pTypeface, pSize, pAntiAlias, pColor)
        {
            Init(pTexture, pTypeface, pSize, pAntiAlias, pColor, pStrokeWidth, pStrokeColor, false);
        }

        public StrokeFont(Texture pTexture, Typeface pTypeface, float pSize, bool pAntiAlias, int pColor, float pStrokeWidth, int pStrokeColor, bool pStrokeOnly)
            : base(pTexture, pTypeface, pSize, pAntiAlias, pColor)
        {
            Init(pTexture, pTypeface, pSize, pAntiAlias, pColor, pStrokeWidth, pStrokeColor, pStrokeOnly);
        }

        protected void Init(Texture pTexture, Typeface pTypeface, float pSize, bool pAntiAlias, int pColor, float pStrokeWidth, int pStrokeColor, bool pStrokeOnly)
        {
            this.mStrokePaint = new Paint();
            this.mStrokePaint.SetTypeface(pTypeface);
            this.mStrokePaint.SetStyle(Style.Stroke);
            this.mStrokePaint.StrokeWidth = pStrokeWidth;
            this.mStrokePaint.Color = pStrokeColor;
            this.mStrokePaint.TextSize = pSize;
            this.mStrokePaint.AntiAlias = pAntiAlias;

            this.mStrokeOnly = pStrokeOnly;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected override void DrawCharacterString(String pCharacterAsString)
        {
            if (this.mStrokeOnly == false)
            {
                base.DrawCharacterString(pCharacterAsString);
            }
            this.mCanvas.DrawText(pCharacterAsString, LETTER_LEFT_OFFSET, -this.mFontMetrics.Ascent, this.mStrokePaint);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
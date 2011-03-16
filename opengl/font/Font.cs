using System;

namespace andengine.opengl.font
{

    //import java.util.ArrayList;
    using System.Collections.Generic;

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;

    using Texture = andengine.opengl.texture.Texture;
    using GLHelper = andengine.opengl.util.GLHelper;

    using Bitmap = Android.Graphics.Bitmap;
    using Canvas = Android.Graphics.Canvas;
    using Color = Android.Graphics.Color;
    using Paint = Android.Graphics.Paint;
    using Rect = Android.Graphics.Rect;
    using Typeface = Android.Graphics.Typeface;
    using FontMetrics = Android.Graphics.Paint.FontMetrics;
    using Style = Android.Graphics.Paint.Style;
    using GLUtils = Android.Opengl.GLUtils;
    using FloatMath = Android.Util.FloatMath;
    using System.Runtime.CompilerServices;
    //using android.util.SparseArray;

    /**
     * @author Nicolas Gramlich
     * @since 10:39:33 - 03.04.2010
     */
    public class Font
    {
        // ===========================================================
        // Constants
        // ===========================================================

        protected static readonly float LETTER_LEFT_OFFSET = 0;
        private static readonly int LETTER_EXTRA_WIDTH = 10;

        // ===========================================================
        // Fields
        // ===========================================================

        private readonly Texture mTexture;
        private readonly float mTextureWidth;
        private readonly float mTextureHeight;
        private int mCurrentTextureX = 0;
        private int mCurrentTextureY = 0;

        private readonly SparseArray<Letter> mManagedCharacterToLetterMap = new SparseArray<Letter>();
        //private final ArrayList<Letter> mLettersPendingToBeDrawnToTexture = new ArrayList<Letter>();
        private readonly List<Letter> mLettersPendingToBeDrawnToTexture = new List<Letter>();

        protected readonly Paint mPaint;
        private readonly Paint mBackgroundPaint;

        protected readonly FontMetrics mFontMetrics;
        private readonly int mLineHeight;
        private readonly int mLineGap;

        private readonly Size mCreateLetterTemporarySize = new Size();
        private readonly Rect mGetLetterBitmapTemporaryRect = new Rect();
        private readonly Rect mGetStringWidthTemporaryRect = new Rect();
        private readonly Rect mGetLetterBoundsTemporaryRect = new Rect();
        private readonly float[] mTemporaryTextWidthFetchers = new float[1];

        protected readonly Canvas mCanvas = new Canvas();

        // ===========================================================
        // Constructors
        // ===========================================================

        public Font(Texture pTexture, Typeface pTypeface, float pSize, bool pAntiAlias, int pColor)
        {
            this.mTexture = pTexture;
            this.mTextureWidth = pTexture.GetWidth();
            this.mTextureHeight = pTexture.GetHeight();

            this.mPaint = new Paint();
            this.mPaint.SetTypeface(pTypeface);
            this.mPaint.Color = pColor;
            this.mPaint.TextSize = pSize;
            this.mPaint.AntiAlias = pAntiAlias;

            this.mBackgroundPaint = new Paint();
            this.mBackgroundPaint.Color = Color.Transparent;
            this.mBackgroundPaint.SetStyle(Style.Fill);

            this.mFontMetrics = this.mPaint.GetFontMetrics();
            this.mLineHeight = (int)FloatMath.Ceil(Math.Abs(this.mFontMetrics.Ascent) + Math.Abs(this.mFontMetrics.Descent));
            this.mLineGap = (int)(FloatMath.Ceil(this.mFontMetrics.Leading));
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public int GetLineGap()
        {
            return this.mLineGap;
        }
        public int LineGap { get { return GetLineGap(); } }

        public int GetLineHeight()
        {
            return this.mLineHeight;
        }
        public int LineHeight { get { return GetLineHeight(); } }

        public Texture GetTexture()
        {
            return this.mTexture;
        }
        public Texture Texture { get { return GetTexture(); } }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        //public synchronized void reload() {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Reload()
        {
            //final ArrayList<Letter> lettersPendingToBeDrawnToTexture = this.mLettersPendingToBeDrawnToTexture;
            List<Letter> lettersPendingToBeDrawnToTexture = this.mLettersPendingToBeDrawnToTexture;
            SparseArray<Letter> managedCharacterToLetterMap = this.mManagedCharacterToLetterMap;

            /* Make all letters redraw to the texture. */
            for (int i = managedCharacterToLetterMap.Count - 1; i >= 0; i--)
            {
                lettersPendingToBeDrawnToTexture.Add(managedCharacterToLetterMap[i]);
            }
        }

        private int GetLetterAdvance(char pCharacter)
        {
            this.mPaint.GetTextWidths(pCharacter.ToString(), this.mTemporaryTextWidthFetchers);
            return (int)(FloatMath.Ceil(this.mTemporaryTextWidthFetchers[0]));
        }

        private Bitmap GetLetterBitmap(char pCharacter)
        {
            Rect getLetterBitmapTemporaryRect = this.mGetLetterBitmapTemporaryRect;
            String characterAsString = pCharacter.ToString();
            this.mPaint.GetTextBounds(characterAsString, 0, 1, getLetterBitmapTemporaryRect);

            int lineHeight = this.GetLineHeight();
            Bitmap bitmap = Bitmap.CreateBitmap(getLetterBitmapTemporaryRect.Width() == 0 ? 1 : getLetterBitmapTemporaryRect.Width() + LETTER_EXTRA_WIDTH, lineHeight, Bitmap.Config.Argb8888);
            this.mCanvas.SetBitmap(bitmap);

            /* Make background transparent. */
            this.mCanvas.DrawRect(0, 0, getLetterBitmapTemporaryRect.Width() + LETTER_EXTRA_WIDTH, lineHeight, this.mBackgroundPaint);

            /* Actually draw the character. */
            this.DrawCharacterString(characterAsString);

            return bitmap;
        }

        protected virtual void DrawCharacterString(String pCharacterAsString)
        {
            this.mCanvas.DrawText(pCharacterAsString, LETTER_LEFT_OFFSET, -this.mFontMetrics.Ascent, this.mPaint);
        }

        public int GetStringWidth(String pText)
        {
            this.mPaint.GetTextBounds(pText, 0, pText.Length, this.mGetStringWidthTemporaryRect);
            return this.mGetStringWidthTemporaryRect.Width();
        }

        private void GetLetterBounds(char pCharacter, Size pSize)
        {
            this.mPaint.GetTextBounds(pCharacter.ToString(), 0, 1, this.mGetLetterBoundsTemporaryRect);
            pSize.Set(this.mGetLetterBoundsTemporaryRect.Width() + LETTER_EXTRA_WIDTH, this.GetLineHeight());
        }

        //public synchronized Letter getLetter(final char pCharacter) {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Letter GetLetter(char pCharacter)
        {
            SparseArray<Letter> managedCharacterToLetterMap = this.mManagedCharacterToLetterMap;
            Letter letter = managedCharacterToLetterMap[pCharacter];
            if (letter == null)
            {
                letter = this.CreateLetter(pCharacter);

                this.mLettersPendingToBeDrawnToTexture.Add(letter);
                managedCharacterToLetterMap[pCharacter] = letter;
            }
            return letter;
        }

        private Letter CreateLetter(char pCharacter)
        {
            float textureWidth = this.mTextureWidth;
            float textureHeight = this.mTextureHeight;

            Size createLetterTemporarySize = this.mCreateLetterTemporarySize;
            this.GetLetterBounds(pCharacter, createLetterTemporarySize);

            float letterWidth = createLetterTemporarySize.GetWidth();
            float letterHeight = createLetterTemporarySize.GetHeight();

            if (this.mCurrentTextureX + letterWidth >= textureWidth)
            {
                this.mCurrentTextureX = 0;
                this.mCurrentTextureY += this.GetLineGap() + this.GetLineHeight();
            }

            float letterTextureX = this.mCurrentTextureX / textureWidth;
            float letterTextureY = this.mCurrentTextureY / textureHeight;
            float letterTextureWidth = letterWidth / textureWidth;
            float letterTextureHeight = letterHeight / textureHeight;

            Letter letter = new Letter(pCharacter, this.GetLetterAdvance(pCharacter), (int)letterWidth, (int)letterHeight, letterTextureX, letterTextureY, letterTextureWidth, letterTextureHeight);
            this.mCurrentTextureX += (int)letterWidth;

            return letter;
        }

        //public synchronized void update(final GL10 pGL) {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(GL10 pGL)
        {
            //final ArrayList<Letter> lettersPendingToBeDrawnToTexture = this.mLettersPendingToBeDrawnToTexture;
            List<Letter> lettersPendingToBeDrawnToTexture = this.mLettersPendingToBeDrawnToTexture;
            if (lettersPendingToBeDrawnToTexture.Count > 0)
            {
                int hardwareTextureID = this.mTexture.GetHardwareTextureID();

                float textureWidth = this.mTextureWidth;
                float textureHeight = this.mTextureHeight;

                for (int i = lettersPendingToBeDrawnToTexture.Count - 1; i >= 0; i--)
                {
                    Letter letter = lettersPendingToBeDrawnToTexture[i];
                    Bitmap bitmap = this.GetLetterBitmap(letter.mCharacter);

                    GLHelper.BindTexture(pGL, hardwareTextureID);
                    GLUtils.TexSubImage2D(Javax.Microedition.Khronos.Opengles.GL10Consts.GlTexture2d, 0, (int)(letter.mTextureX * textureWidth), (int)(letter.mTextureY * textureHeight), bitmap);

                    bitmap.Recycle();
                }
                lettersPendingToBeDrawnToTexture.Clear();
                //System.gc();
                // TODO: Verify if this is a good match: System.gc() -> Dispose() ... leaving it to standard GC at present
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
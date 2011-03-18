namespace andengine.opengl.texture.buffer
{

    using BufferObject = andengine.opengl.buffer.BufferObject;
    using Font = andengine.opengl.font.Font;
    using Letter = andengine.opengl.font.Letter;
    using FastFloatBuffer = andengine.opengl.util.FastFloatBuffer;
    using System.Runtime.CompilerServices;
    using Java.Lang;

    /**
     * @author Nicolas Gramlich
     * @since 11:05:56 - 03.04.2010
     */
    public class TextTextureBuffer : BufferObject
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        // ===========================================================
        // Constructors
        // ===========================================================

        public TextTextureBuffer(int pCapacity, int pDrawType)
            : base(pCapacity, pDrawType)
        {
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public void update(Font pFont, String[] pLines)
        {
            lock (_methodLock)
            {
                FastFloatBuffer textureFloatBuffer = this.GetFloatBuffer();
                textureFloatBuffer.Position(0);

                Font font = pFont;
                String[] lines = pLines;

                int lineCount = lines.Length;
                for (int i = 0; i < lineCount; i++)
                {
                    String line = lines[i];

                    int lineLength = line.Length();
                    for (int j = 0; j < lineLength; j++)
                    {
                        Letter letter = font.GetLetter(line.CharAt(j));

                        float letterTextureX = letter.mTextureX;
                        float letterTextureY = letter.mTextureY;
                        float letterTextureX2 = letterTextureX + letter.mTextureWidth;
                        float letterTextureY2 = letterTextureY + letter.mTextureHeight;

                        textureFloatBuffer.Put(letterTextureX);
                        textureFloatBuffer.Put(letterTextureY);

                        textureFloatBuffer.Put(letterTextureX);
                        textureFloatBuffer.Put(letterTextureY2);

                        textureFloatBuffer.Put(letterTextureX2);
                        textureFloatBuffer.Put(letterTextureY2);

                        textureFloatBuffer.Put(letterTextureX2);
                        textureFloatBuffer.Put(letterTextureY2);

                        textureFloatBuffer.Put(letterTextureX2);
                        textureFloatBuffer.Put(letterTextureY);

                        textureFloatBuffer.Put(letterTextureX);
                        textureFloatBuffer.Put(letterTextureY);
                    }
                }
                textureFloatBuffer.Position(0);

                this.SetHardwareBufferNeedsUpdate();
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
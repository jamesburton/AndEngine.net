namespace andengine.opengl.util
{
    using Java.Nio;
    using Buffer = Java.Nio.Buffer;
    using ByteBuffer = Java.Nio.ByteBuffer;
    using ByteOrder = Java.Nio.ByteOrder;

    using IntBuffer = Java.Nio.IntBuffer;

    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL11 = Javax.Microedition.Khronos.Opengles.IGL11;

    using RenderOptions = andengine.engine.options.RenderOptions;
    using Debug = andengine.util.Debug;

    using Bitmap = Android.Graphics.Bitmap;
    using GLUtils = Android.Opengl.GLUtils;
    using Build = Android.OS.Build;
    using System;

    /**
     * @author Nicolas Gramlich
     * @since 18:00:43 - 08.03.2010
     */
    public class GLHelper
    {
        // ===========================================================
        // Constants
        // ===========================================================

        public static /* final */ readonly int BYTES_PER_FLOAT = 4;
        public static /* final */ readonly int BYTES_PER_PIXEL_RGBA = 4;

        private static /* final */ readonly bool IS_LITTLE_ENDIAN = (ByteOrder.NativeOrder() == ByteOrder.LittleEndian);

        private static /* final */ readonly int[] HARDWARETEXTUREID_CONTAINER = new int[1];
        private static /* final */ readonly int[] HARDWAREBUFFERID_CONTAINER = new int[1];

        // ===========================================================
        // Fields
        // ===========================================================

        private static int sCurrentHardwareBufferID = -1;
        private static int sCurrentHardwareTextureID = -1;
        private static int sCurrentMatrix = -1;

        private static int sCurrentSourceBlendMode = -1;
        private static int sCurrentDestinationBlendMode = -1;

        private static FastFloatBuffer sCurrentTextureFloatBuffer = null;
        private static FastFloatBuffer sCurrentVertexFloatBuffer = null;

        private static bool sEnableDither = true;
        private static bool sEnableLightning = true;
        private static bool sEnableDepthTest = true;
        private static bool sEnableMultisample = true;

        private static bool sEnableBlend = false;
        private static bool sEnableCulling = false;
        private static bool sEnableTextures = false;
        private static bool sEnableTexCoordArray = false;
        private static bool sEnableVertexArray = false;

        private static float sLineWidth = 1;

        private static float sRed = -1;
        private static float sGreen = -1;
        private static float sBlue = -1;
        private static float sAlpha = -1;

        public static bool EXTENSIONS_VERTEXBUFFEROBJECTS = false;
        public static bool EXTENSIONS_DRAWTEXTURE = false;

        // ===========================================================
        // Methods
        // ===========================================================

        public static void reset(/* final */ GL10 pGL)
        {
            GLHelper.sCurrentHardwareBufferID = -1;
            GLHelper.sCurrentHardwareTextureID = -1;
            GLHelper.sCurrentMatrix = -1;

            GLHelper.sCurrentSourceBlendMode = -1;
            GLHelper.sCurrentDestinationBlendMode = -1;

            GLHelper.sCurrentTextureFloatBuffer = null;
            GLHelper.sCurrentVertexFloatBuffer = null;

            GLHelper.enableDither(pGL);
            GLHelper.enableLightning(pGL);
            GLHelper.enableDepthTest(pGL);
            GLHelper.enableMultisample(pGL);

            GLHelper.disableBlend(pGL);
            GLHelper.disableCulling(pGL);
            GLHelper.disableTextures(pGL);
            GLHelper.disableTexCoordArray(pGL);
            GLHelper.disableVertexArray(pGL);

            GLHelper.sLineWidth = 1;

            GLHelper.sRed = -1;
            GLHelper.sGreen = -1;
            GLHelper.sBlue = -1;
            GLHelper.sAlpha = -1;

            GLHelper.EXTENSIONS_VERTEXBUFFEROBJECTS = false;
            GLHelper.EXTENSIONS_DRAWTEXTURE = false;
        }

        public static void enableExtensions(/* final */ GL10 pGL, /* final */ RenderOptions pRenderOptions)
        {
            /* final */
            String version = pGL.glGetString(GL10.GL_VERSION);
            /* final */
            String renderer = pGL.glGetString(GL10.GL_RENDERER);
            /* final */
            String extensions = pGL.glGetString(GL10.GL_EXTENSIONS);

            Debug.d("RENDERER: " + renderer);
            Debug.d("VERSION: " + version);
            Debug.d("EXTENSIONS: " + extensions);

            /* final */
            bool isOpenGL10 = version.Contains("1.0");
            /* final */
            bool isSoftwareRenderer = renderer.Contains("PixelFlinger");
            /* final */
            bool isVBOCapable = extensions.Contains("_vertex_buffer_object");
            /* final */
            bool isDrawTextureCapable = extensions.Contains("draw_texture");

            GLHelper.EXTENSIONS_VERTEXBUFFEROBJECTS = !pRenderOptions.isDisableExtensionVertexBufferObjects() && !isSoftwareRenderer && (isVBOCapable || !isOpenGL10);
            GLHelper.EXTENSIONS_DRAWTEXTURE = isDrawTextureCapable;

            GLHelper.hackBrokenDevices();
            Debug.d("EXTENSIONS_VERXTEXBUFFEROBJECTS = " + GLHelper.EXTENSIONS_VERTEXBUFFEROBJECTS);
            Debug.d("EXTENSIONS_DRAWTEXTURE = " + GLHelper.EXTENSIONS_DRAWTEXTURE);
        }

        private static void hackBrokenDevices()
        {
            if (Build.Product.Contains("morrison"))
            {
                // This is the Motorola Cliq. This device LIES and says it supports
                // VBOs, which it actually does not (or, more likely, the extensions string
                // is correct and the GL JNI glue is broken).
                GLHelper.EXTENSIONS_VERTEXBUFFEROBJECTS = false;
                // TODO: if Motorola fixes this, I should switch to using the fingerprint
                // (blur/morrison/morrison/morrison:1.5/CUPCAKE/091007:user/ota-rel-keys,release-keys)
                // instead of the product name so that newer versions use VBOs
            }
        }

        public static void setColor(/* final */ GL10 pGL, /* final */ float pRed, /* final */ float pGreen, /* final */ float pBlue, /* final */ float pAlpha)
        {
            if (pAlpha != GLHelper.sAlpha || pRed != GLHelper.sRed || pGreen != GLHelper.sGreen || pBlue != GLHelper.sBlue)
            {
                GLHelper.sAlpha = pAlpha;
                GLHelper.sRed = pRed;
                GLHelper.sGreen = pGreen;
                GLHelper.sBlue = pBlue;
                pGL.glColor4f(pRed, pGreen, pBlue, pAlpha);
            }
        }

        public static void enableVertexArray(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableVertexArray)
            {
                GLHelper.sEnableVertexArray = true;
                pGL.glEnableClientState(GL10.GL_VERTEX_ARRAY);
            }
        }
        public static void disableVertexArray(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableVertexArray)
            {
                GLHelper.sEnableVertexArray = false;
                pGL.glDisableClientState(GL10.GL_VERTEX_ARRAY);
            }
        }

        public static void enableTexCoordArray(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableTexCoordArray)
            {
                GLHelper.sEnableTexCoordArray = true;
                pGL.glEnableClientState(GL10.GL_TEXTURE_COORD_ARRAY);
            }
        }
        public static void disableTexCoordArray(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableTexCoordArray)
            {
                GLHelper.sEnableTexCoordArray = false;
                pGL.glDisableClientState(GL10.GL_TEXTURE_COORD_ARRAY);
            }
        }

        public static void enableBlend(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableBlend)
            {
                GLHelper.sEnableBlend = true;
                pGL.glEnable(GL10.GL_BLEND);
            }
        }
        public static void disableBlend(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableBlend)
            {
                GLHelper.sEnableBlend = false;
                pGL.glDisable(GL10.GL_BLEND);
            }
        }

        public static void enableCulling(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableCulling)
            {
                GLHelper.sEnableCulling = true;
                pGL.glEnable(GL10.GL_CULL_FACE);
            }
        }
        public static void disableCulling(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableCulling)
            {
                GLHelper.sEnableCulling = false;
                pGL.glDisable(GL10.GL_CULL_FACE);
            }
        }

        public static void enableTextures(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableTextures)
            {
                GLHelper.sEnableTextures = true;
                pGL.glEnable(GL10.GL_TEXTURE_2D);
            }
        }
        public static void disableTextures(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableTextures)
            {
                GLHelper.sEnableTextures = false;
                pGL.glDisable(GL10.GL_TEXTURE_2D);
            }
        }

        public static void enableLightning(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableLightning)
            {
                GLHelper.sEnableLightning = true;
                pGL.glEnable(GL10.GL_LIGHTING);
            }
        }
        public static void disableLightning(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableLightning)
            {
                GLHelper.sEnableLightning = false;
                pGL.glDisable(GL10.GL_LIGHTING);
            }
        }

        public static void enableDither(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableDither)
            {
                GLHelper.sEnableDither = true;
                pGL.glEnable(GL10.GL_DITHER);
            }
        }
        public static void disableDither(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableDither)
            {
                GLHelper.sEnableDither = false;
                pGL.glDisable(GL10.GL_DITHER);
            }
        }

        public static void enableDepthTest(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableDepthTest)
            {
                GLHelper.sEnableDepthTest = true;
                pGL.glEnable(GL10.GL_DEPTH_TEST);
            }
        }
        public static void disableDepthTest(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableDepthTest)
            {
                GLHelper.sEnableDepthTest = false;
                pGL.glDisable(GL10.GL_DEPTH_TEST);
            }
        }

        public static void enableMultisample(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableMultisample)
            {
                GLHelper.sEnableMultisample = true;
                pGL.glEnable(GL10.GL_MULTISAMPLE);
            }
        }
        public static void disableMultisample(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableMultisample)
            {
                GLHelper.sEnableMultisample = false;
                pGL.glDisable(GL10.GL_MULTISAMPLE);
            }
        }

        public static void bindBuffer(/* final */ GL11 pGL11, /* final */ int pHardwareBufferID)
        {
            /* Reduce unnecessary buffer switching calls. */
            if (GLHelper.sCurrentHardwareBufferID != pHardwareBufferID)
            {
                GLHelper.sCurrentHardwareBufferID = pHardwareBufferID;
                pGL11.glBindBuffer(GL11.GL_ARRAY_BUFFER, pHardwareBufferID);
            }
        }

        public static void deleteBuffer(/* final */ GL11 pGL11, /* final */ int pHardwareBufferID)
        {
            GLHelper.HARDWAREBUFFERID_CONTAINER[0] = pHardwareBufferID;
            pGL11.glDeleteBuffers(1, GLHelper.HARDWAREBUFFERID_CONTAINER, 0);
        }

        public static void bindTexture(/* final */ GL10 pGL, /* final */ int pHardwareTextureID)
        {
            /* Reduce unnecessary texture switching calls. */
            if (GLHelper.sCurrentHardwareTextureID != pHardwareTextureID)
            {
                GLHelper.sCurrentHardwareTextureID = pHardwareTextureID;
                pGL.glBindTexture(GL10.GL_TEXTURE_2D, pHardwareTextureID);
            }
        }

        public static void deleteTexture(/* final */ GL10 pGL, /* final */ int pHardwareTextureID)
        {
            GLHelper.HARDWARETEXTUREID_CONTAINER[0] = pHardwareTextureID;
            pGL.glDeleteTextures(1, GLHelper.HARDWARETEXTUREID_CONTAINER, 0);
        }

        public static void texCoordPointer(/* final */ GL10 pGL, /* final */ FastFloatBuffer pTextureFloatBuffer)
        {
            if (GLHelper.sCurrentTextureFloatBuffer != pTextureFloatBuffer)
            {
                GLHelper.sCurrentTextureFloatBuffer = pTextureFloatBuffer;
                pGL.glTexCoordPointer(2, GL10.GL_FLOAT, 0, pTextureFloatBuffer.mByteBuffer);
            }
        }

        public static void texCoordZeroPointer(/* final */ GL11 pGL11)
        {
            pGL11.glTexCoordPointer(2, GL10.GL_FLOAT, 0, 0);
        }

        public static void vertexPointer(/* final */ GL10 pGL, /* final */ FastFloatBuffer pVertexFloatBuffer)
        {
            if (GLHelper.sCurrentVertexFloatBuffer != pVertexFloatBuffer)
            {
                GLHelper.sCurrentVertexFloatBuffer = pVertexFloatBuffer;
                pGL.glVertexPointer(2, GL10.GL_FLOAT, 0, pVertexFloatBuffer.mByteBuffer);
            }
        }

        public static void vertexZeroPointer(/* final */ GL11 pGL11)
        {
            pGL11.glVertexPointer(2, GL10.GL_FLOAT, 0, 0);
        }

        public static void blendFunction(/* final */ GL10 pGL, /* final */ int pSourceBlendMode, /* final */ int pDestinationBlendMode)
        {
            if (GLHelper.sCurrentSourceBlendMode != pSourceBlendMode || GLHelper.sCurrentDestinationBlendMode != pDestinationBlendMode)
            {
                GLHelper.sCurrentSourceBlendMode = pSourceBlendMode;
                GLHelper.sCurrentDestinationBlendMode = pDestinationBlendMode;
                pGL.glBlendFunc(pSourceBlendMode, pDestinationBlendMode);
            }
        }

        public static void lineWidth(/* final */ GL10 pGL, /* final */ float pLineWidth)
        {
            if (GLHelper.sLineWidth != pLineWidth)
            {
                GLHelper.sLineWidth = pLineWidth;
                pGL.glLineWidth(pLineWidth);
            }
        }

        public static void switchToModelViewMatrix(/* final */ GL10 pGL)
        {
            /* Reduce unnecessary matrix switching calls. */
            if (GLHelper.sCurrentMatrix != GL10.GL_MODELVIEW)
            {
                GLHelper.sCurrentMatrix = GL10.GL_MODELVIEW;
                pGL.glMatrixMode(GL10.GL_MODELVIEW);
            }
        }

        public static void switchToProjectionMatrix(/* final */ GL10 pGL)
        {
            /* Reduce unnecessary matrix switching calls. */
            if (GLHelper.sCurrentMatrix != GL10.GL_PROJECTION)
            {
                GLHelper.sCurrentMatrix = GL10.GL_PROJECTION;
                pGL.glMatrixMode(GL10.GL_PROJECTION);
            }
        }

        public static void setProjectionIdentityMatrix(/* final */ GL10 pGL)
        {
            GLHelper.switchToProjectionMatrix(pGL);
            pGL.glLoadIdentity();
        }

        public static void setModelViewIdentityMatrix(/* final */ GL10 pGL)
        {
            GLHelper.switchToModelViewMatrix(pGL);
            pGL.glLoadIdentity();
        }

        public static void setShadeModelFlat(/* final */ GL10 pGL)
        {
            pGL.glShadeModel(GL10.GL_FLAT);
        }

        public static void setPerspectiveCorrectionHintFastest(/* final */ GL10 pGL)
        {
            pGL.glHint(GL10.GL_PERSPECTIVE_CORRECTION_HINT, GL10.GL_FASTEST);
        }

        public static void bufferData(/* final */ GL11 pGL11, /* final */ ByteBuffer pByteBuffer, /* final */ int pUsage)
        {
            pGL11.glBufferData(GL11.GL_ARRAY_BUFFER, pByteBuffer.capacity(), pByteBuffer, pUsage);
        }

        /**
         * <b>Note:</b> does not pre-multiply the alpha channel!</br>
         * Except that difference, same as: {@link GLUtils#texSubImage2D(int, int, int, int, Bitmap, int, int)}</br>
         * </br>
         * See topic: '<a href="http://groups.google.com/group/android-developers/browse_thread/thread/baa6c33e63f82fca">PNG loading that doesn't premultiply alpha?</a>'
         */
        public static void glTexSubImage2D(/* final */ GL10 pGL, /* final */ int target, /* final */ int level, /* final */ int xoffset, /* final */ int yoffset, /* final */ Bitmap bitmap, /* final */ int format, /* final */ int type)
        {
            /* final */
            int[] pixels = GLHelper.getPixels(bitmap);

            /* final */
            Buffer pixelBuffer = GLHelper.convertARGBtoRGBABuffer(pixels);

            pGL.glTexSubImage2D(GL10.GlTexture2d, 0, xoffset, yoffset, bitmap.Width, bitmap.Height, GL10.GlRgba, GL10.GlUnsignedByte, pixelBuffer);
        }

        private static Buffer convertARGBtoRGBABuffer(/* final */ int[] pPixels)
        {
            if (GLHelper.IS_LITTLE_ENDIAN)
            {
                for (int i = pPixels.Length - 1; i >= 0; i--)
                {
                    /* final */
                    int pixel = pPixels[i];

                    /* final */
                    int red = ((pixel >> 16) & 0xFF);
                    /* final */
                    int green = ((pixel >> 8) & 0xFF);
                    /* final */
                    int blue = ((pixel) & 0xFF);
                    /* final */
                    int alpha = (pixel >> 24);

                    pPixels[i] = alpha << 24 | blue << 16 | green << 8 | red;
                }
            }
            else
            {
                for (int i = pPixels.length - 1; i >= 0; i--)
                {
                    /* final */
                    int pixel = pPixels[i];

                    /* final */
                    int red = ((pixel >> 16) & 0xFF);
                    /* final */
                    int green = ((pixel >> 8) & 0xFF);
                    /* final */
                    int blue = ((pixel) & 0xFF);
                    /* final */
                    int alpha = (pixel >> 24);

                    pPixels[i] = red << 24 | green << 16 | blue << 8 | alpha;
                }
            }
            return IntBuffer.wrap(pPixels);
        }

        public static int[] getPixels(/* final */ Bitmap pBitmap)
        {
            /* final */
            int w = pBitmap.Width;
            /* final */
            int h = pBitmap.Height;

            /* final */
            int[] pixels = new int[w * h];
            pBitmap.GetPixels(pixels, 0, w, 0, 0, w, h);

            return pixels;
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
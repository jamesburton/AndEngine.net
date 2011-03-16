namespace andengine.opengl.util
{
    using Java.Nio;
    using Buffer = Java.Nio.Buffer;
    using ByteBuffer = Java.Nio.ByteBuffer;
    using ByteOrder = Java.Nio.ByteOrder;

    using IntBuffer = Java.Nio.IntBuffer;

    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;
    using GL11 = Javax.Microedition.Khronos.Opengles.IGL11;
    using GL11Consts = Javax.Microedition.Khronos.Opengles.GL11Consts;

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

        public static void Reset(/* final */ GL10 pGL)
        {
            GLHelper.sCurrentHardwareBufferID = -1;
            GLHelper.sCurrentHardwareTextureID = -1;
            GLHelper.sCurrentMatrix = -1;

            GLHelper.sCurrentSourceBlendMode = -1;
            GLHelper.sCurrentDestinationBlendMode = -1;

            GLHelper.sCurrentTextureFloatBuffer = null;
            GLHelper.sCurrentVertexFloatBuffer = null;

            GLHelper.EnableDither(pGL);
            GLHelper.EnableLightning(pGL);
            GLHelper.EnableDepthTest(pGL);
            GLHelper.EnableMultisample(pGL);

            GLHelper.DisableBlend(pGL);
            GLHelper.DisableCulling(pGL);
            GLHelper.DisableTextures(pGL);
            GLHelper.DisableTexCoordArray(pGL);
            GLHelper.DisableVertexArray(pGL);

            GLHelper.sLineWidth = 1;

            GLHelper.sRed = -1;
            GLHelper.sGreen = -1;
            GLHelper.sBlue = -1;
            GLHelper.sAlpha = -1;

            GLHelper.EXTENSIONS_VERTEXBUFFEROBJECTS = false;
            GLHelper.EXTENSIONS_DRAWTEXTURE = false;
        }

        public static void EnableExtensions(/* final */ GL10 pGL, /* final */ RenderOptions pRenderOptions)
        {
            /* final */
            String version = pGL.GlGetString(GL10Consts.GlVersion);
            /* final */
            String renderer = pGL.GlGetString(GL10Consts.GlRenderer);
            /* final */
            String extensions = pGL.GlGetString(GL10Consts.GlExtensions);

            Debug.D("RENDERER: " + renderer);
            Debug.D("VERSION: " + version);
            Debug.D("EXTENSIONS: " + extensions);

            /* final */
            bool isOpenGL10 = version.Contains("1.0");
            /* final */
            bool isSoftwareRenderer = renderer.Contains("PixelFlinger");
            /* final */
            bool isVBOCapable = extensions.Contains("_vertex_buffer_object");
            /* final */
            bool isDrawTextureCapable = extensions.Contains("draw_texture");

            //GLHelper.EXTENSIONS_VERTEXBUFFEROBJECTS = !pRenderOptions.isDisableExtensionVertexBufferObjects() && !isSoftwareRenderer && (isVBOCapable || !isOpenGL10);
            GLHelper.EXTENSIONS_VERTEXBUFFEROBJECTS = !pRenderOptions.IsDisableExtensionVertexBufferObjects() && !isSoftwareRenderer && (isVBOCapable || !isOpenGL10);
            GLHelper.EXTENSIONS_DRAWTEXTURE = isDrawTextureCapable;

            GLHelper.HackBrokenDevices();
            Debug.D("EXTENSIONS_VERXTEXBUFFEROBJECTS = " + GLHelper.EXTENSIONS_VERTEXBUFFEROBJECTS);
            Debug.D("EXTENSIONS_DRAWTEXTURE = " + GLHelper.EXTENSIONS_DRAWTEXTURE);
        }

        private static void HackBrokenDevices()
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

        public static void SetColor(/* final */ GL10 pGL, /* final */ float pRed, /* final */ float pGreen, /* final */ float pBlue, /* final */ float pAlpha)
        {
            if (pAlpha != GLHelper.sAlpha || pRed != GLHelper.sRed || pGreen != GLHelper.sGreen || pBlue != GLHelper.sBlue)
            {
                GLHelper.sAlpha = pAlpha;
                GLHelper.sRed = pRed;
                GLHelper.sGreen = pGreen;
                GLHelper.sBlue = pBlue;
                pGL.GlColor4f(pRed, pGreen, pBlue, pAlpha);
            }
        }

        public static void EnableVertexArray(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableVertexArray)
            {
                GLHelper.sEnableVertexArray = true;
                pGL.GlEnableClientState(GL10Consts.GlVertexArray);
            }
        }
        public static void DisableVertexArray(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableVertexArray)
            {
                GLHelper.sEnableVertexArray = false;
                pGL.GlDisableClientState(GL10Consts.GlVertexArray);
            }
        }

        public static void EnableTexCoordArray(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableTexCoordArray)
            {
                GLHelper.sEnableTexCoordArray = true;
                pGL.GlEnableClientState(GL10Consts.GlTextureCoordArray);
            }
        }
        public static void DisableTexCoordArray(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableTexCoordArray)
            {
                GLHelper.sEnableTexCoordArray = false;
                pGL.GlDisableClientState(GL10Consts.GlTextureCoordArray);
            }
        }

        public static void EnableBlend(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableBlend)
            {
                GLHelper.sEnableBlend = true;
                pGL.GlEnable(GL10Consts.GlBlend);
            }
        }
        public static void DisableBlend(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableBlend)
            {
                GLHelper.sEnableBlend = false;
                pGL.GlDisable(GL10Consts.GlBlend);
            }
        }

        public static void EnableCulling(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableCulling)
            {
                GLHelper.sEnableCulling = true;
                //pGL.GlEnable(GL10.GL_CULL_FACE);
                pGL.GlEnable(GL11Consts.GlCullFaceMode);
            }
        }
        public static void DisableCulling(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableCulling)
            {
                GLHelper.sEnableCulling = false;
                //pGL.glDisable(GL10.GL_CULL_FACE);
                pGL.GlDisable(GL11Consts.GlCullFaceMode);
            }
        }

        public static void EnableTextures(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableTextures)
            {
                GLHelper.sEnableTextures = true;
                pGL.GlEnable(GL10Consts.GlTexture2d);
            }
        }
        public static void DisableTextures(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableTextures)
            {
                GLHelper.sEnableTextures = false;
                pGL.GlDisable(GL10Consts.GlTexture2d);
            }
        }

        public static void EnableLightning(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableLightning)
            {
                GLHelper.sEnableLightning = true;
                pGL.GlEnable(GL10Consts.GlLighting);
            }
        }
        public static void DisableLightning(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableLightning)
            {
                GLHelper.sEnableLightning = false;
                pGL.GlDisable(GL10Consts.GlLighting);
            }
        }

        public static void EnableDither(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableDither)
            {
                GLHelper.sEnableDither = true;
                pGL.GlEnable(GL10Consts.GlDither);
            }
        }
        public static void DisableDither(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableDither)
            {
                GLHelper.sEnableDither = false;
                pGL.GlDisable(GL10Consts.GlDither);
            }
        }

        public static void EnableDepthTest(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableDepthTest)
            {
                GLHelper.sEnableDepthTest = true;
                pGL.GlEnable(GL10Consts.GlDepthTest);
            }
        }
        public static void DisableDepthTest(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableDepthTest)
            {
                GLHelper.sEnableDepthTest = false;
                pGL.GlDisable(GL10Consts.GlDepthTest);
            }
        }

        public static void EnableMultisample(/* final */ GL10 pGL)
        {
            if (!GLHelper.sEnableMultisample)
            {
                GLHelper.sEnableMultisample = true;
                pGL.GlEnable(GL10Consts.GlMultisample);
            }
        }
        public static void DisableMultisample(/* final */ GL10 pGL)
        {
            if (GLHelper.sEnableMultisample)
            {
                GLHelper.sEnableMultisample = false;
                pGL.GlDisable(GL10Consts.GlMultisample);
            }
        }

        public static void BindBuffer(/* final */ GL11 pGL11, /* final */ int pHardwareBufferID)
        {
            /* Reduce unnecessary buffer switching calls. */
            if (GLHelper.sCurrentHardwareBufferID != pHardwareBufferID)
            {
                GLHelper.sCurrentHardwareBufferID = pHardwareBufferID;
                pGL11.GlBindBuffer(GL11Consts.GlArrayBuffer, pHardwareBufferID);
            }
        }

        public static void DeleteBuffer(/* final */ GL11 pGL11, /* final */ int pHardwareBufferID)
        {
            GLHelper.HARDWAREBUFFERID_CONTAINER[0] = pHardwareBufferID;
            pGL11.GlDeleteBuffers(1, GLHelper.HARDWAREBUFFERID_CONTAINER, 0);
        }

        public static void BindTexture(/* final */ GL10 pGL, /* final */ int pHardwareTextureID)
        {
            /* Reduce unnecessary texture switching calls. */
            if (GLHelper.sCurrentHardwareTextureID != pHardwareTextureID)
            {
                GLHelper.sCurrentHardwareTextureID = pHardwareTextureID;
                pGL.GlBindTexture(GL10Consts.GlTexture2d, pHardwareTextureID);
            }
        }

        public static void DeleteTexture(/* final */ GL10 pGL, /* final */ int pHardwareTextureID)
        {
            GLHelper.HARDWARETEXTUREID_CONTAINER[0] = pHardwareTextureID;
            pGL.GlDeleteTextures(1, GLHelper.HARDWARETEXTUREID_CONTAINER, 0);
        }

        public static void TexCoordPointer(/* final */ GL10 pGL, /* final */ FastFloatBuffer pTextureFloatBuffer)
        {
            if (GLHelper.sCurrentTextureFloatBuffer != pTextureFloatBuffer)
            {
                GLHelper.sCurrentTextureFloatBuffer = pTextureFloatBuffer;
                pGL.GlTexCoordPointer(2, GL10Consts.GlFloat, 0, pTextureFloatBuffer.mByteBuffer);
            }
        }

        public static void TexCoordZeroPointer(/* final */ GL11 pGL11)
        {
            pGL11.GlTexCoordPointer(2, GL10Consts.GlFloat, 0, 0);
        }

        public static void VertexPointer(/* final */ GL10 pGL, /* final */ FastFloatBuffer pVertexFloatBuffer)
        {
            if (GLHelper.sCurrentVertexFloatBuffer != pVertexFloatBuffer)
            {
                GLHelper.sCurrentVertexFloatBuffer = pVertexFloatBuffer;
                pGL.GlVertexPointer(2, GL10Consts.GlFloat, 0, pVertexFloatBuffer.mByteBuffer);
            }
        }

        public static void VertexZeroPointer(/* final */ GL11 pGL11)
        {
            pGL11.GlVertexPointer(2, GL10Consts.GlFloat, 0, 0);
        }

        public static void BlendFunction(/* final */ GL10 pGL, /* final */ int pSourceBlendMode, /* final */ int pDestinationBlendMode)
        {
            if (GLHelper.sCurrentSourceBlendMode != pSourceBlendMode || GLHelper.sCurrentDestinationBlendMode != pDestinationBlendMode)
            {
                GLHelper.sCurrentSourceBlendMode = pSourceBlendMode;
                GLHelper.sCurrentDestinationBlendMode = pDestinationBlendMode;
                pGL.GlBlendFunc(pSourceBlendMode, pDestinationBlendMode);
            }
        }

        public static void LineWidth(/* final */ GL10 pGL, /* final */ float pLineWidth)
        {
            if (GLHelper.sLineWidth != pLineWidth)
            {
                GLHelper.sLineWidth = pLineWidth;
                pGL.GlLineWidth(pLineWidth);
            }
        }

        public static void SwitchToModelViewMatrix(/* final */ GL10 pGL)
        {
            /* Reduce unnecessary matrix switching calls. */
            if (GLHelper.sCurrentMatrix != GL10Consts.GlModelview)
            {
                GLHelper.sCurrentMatrix = GL10Consts.GlModelview;
                pGL.GlMatrixMode(GL10Consts.GlModelview);
            }
        }

        public static void SwitchToProjectionMatrix(/* final */ GL10 pGL)
        {
            /* Reduce unnecessary matrix switching calls. */
            if (GLHelper.sCurrentMatrix != GL10Consts.GlProjection)
            {
                GLHelper.sCurrentMatrix = GL10Consts.GlProjection;
                pGL.GlMatrixMode(GL10Consts.GlProjection);
            }
        }

        public static void SetProjectionIdentityMatrix(/* final */ GL10 pGL)
        {
            GLHelper.SwitchToProjectionMatrix(pGL);
            pGL.GlLoadIdentity();
        }

        public static void SetModelViewIdentityMatrix(/* final */ GL10 pGL)
        {
            GLHelper.SwitchToModelViewMatrix(pGL);
            pGL.GlLoadIdentity();
        }

        public static void SetShadeModelFlat(/* final */ GL10 pGL)
        {
            pGL.GlShadeModel(GL10Consts.GlFlat);
        }

        public static void SetPerspectiveCorrectionHintFastest(/* final */ GL10 pGL)
        {
            pGL.GlHint(GL10Consts.GlPerspectiveCorrectionHint, GL10Consts.GlFastest);
        }

        public static void BufferData(/* final */ GL11 pGL11, /* final */ ByteBuffer pByteBuffer, /* final */ int pUsage)
        {
            pGL11.GlBufferData(GL11Consts.GlArrayBuffer, pByteBuffer.Capacity(), pByteBuffer, pUsage);
        }

        /**
         * <b>Note:</b> does not pre-multiply the alpha channel!</br>
         * Except that difference, same as: {@link GLUtils#texSubImage2D(int, int, int, int, Bitmap, int, int)}</br>
         * </br>
         * See topic: '<a href="http://groups.google.com/group/android-developers/browse_thread/thread/baa6c33e63f82fca">PNG loading that doesn't premultiply alpha?</a>'
         */
        public static void GlTexSubImage2D(/* final */ GL10 pGL, /* final */ int target, /* final */ int level, /* final */ int xoffset, /* final */ int yoffset, /* final */ Bitmap bitmap, /* final */ int format, /* final */ int type)
        {
            /* final */
            int[] pixels = GLHelper.GetPixels(bitmap);

            /* final */
            Buffer pixelBuffer = GLHelper.ConvertARGBtoRGBABuffer(pixels);

            pGL.GlTexSubImage2D(GL10Consts.GlTexture2d, 0, xoffset, yoffset, bitmap.Width, bitmap.Height, GL10Consts.GlRgba, GL10Consts.GlUnsignedByte, pixelBuffer);
        }

        private static Buffer ConvertARGBtoRGBABuffer(/* final */ int[] pPixels)
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

                    pPixels[i] = red << 24 | green << 16 | blue << 8 | alpha;
                }
            }
            return IntBuffer.Wrap(pPixels);
        }

        public static int[] GetPixels(/* final */ Bitmap pBitmap)
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
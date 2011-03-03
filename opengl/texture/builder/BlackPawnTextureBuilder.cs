namespace andengine.opengl.texture.builder
{

    //import java.util.ArrayList;
    using System.Collections.Generic;
    //import java.util.Collections;
    //import java.util.Comparator;
    //using Comparator = Java.Util.IComparator;

    using BuildableTexture = andengine.opengl.texture.BuildableTexture;
    using TextureSourceWithLocationCallback = andengine.opengl.texture.BuildableTexture.TextureSourceWithWithLocationCallback;
    using TextureSourceWithLocation = andengine.opengl.texture.Texture.TextureSourceWithLocation;
    using ITextureSource = andengine.opengl.texture.source.ITextureSource;
    using Java.Lang;

    /**
     * @author Nicolas Gramlich
     * @since 16:03:01 - 12.08.2010
     * @see http://www.blackpawn.com/texts/lightmaps/default.html
     */
    public class BlackPawnTextureBuilder : ITextureBuilder
    {
        // ===========================================================
        // Constants
        // ===========================================================

        /*
        private static final Comparator<ITextureSource> TEXTURESOURCE_COMPARATOR = new Comparator<ITextureSource>() {
            @Override
            public int compare(final ITextureSource pTextureSourceA, final ITextureSource pTextureSourceB) {
                final int deltaWidth = pTextureSourceB.getWidth() - pTextureSourceA.getWidth();
                if(deltaWidth != 0) {
                    return deltaWidth;
                } else {
                    return pTextureSourceB.getHeight() - pTextureSourceA.getHeight();
                }
            }
        };
        */

        private readonly Comparer<ITextureSource> TEXTURESOURCE_COMPARER;
        public class TextureSourceComparer : Comparer<ITextureSource>
        {
            protected readonly BlackPawnTextureBuilder Parent;
            public TextureSourceComparer(BlackPawnTextureBuilder parent)
            {
                Parent = parent;
            }

            public override int Compare(ITextureSource x, ITextureSource y)
            {
                int deltaWidth = y.getWidth() - x.getWidth();
                if (deltaWidth != 0)
                {
                    return deltaWidth;
                }
                else
                {
                    return y.getHeight() - x.getHeight();
                }
            }
        }

        // ===========================================================
        // Fields
        // ===========================================================

        private readonly int mTextureSourceSpacing;

        // ===========================================================
        // Constructors
        // ===========================================================

        public BlackPawnTextureBuilder(int pTextureSourceSpacing)
        {
            TEXTURESOURCE_COMPARER = new TextureSourceComparer(this);
            this.mTextureSourceSpacing = pTextureSourceSpacing;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        //@SuppressWarnings("deprecation")
        //@Override
        //public override void pack(final BuildableTexture pBuildableTexture, final ArrayList<TextureSourceWithWithLocationCallback> pTextureSourcesWithLocationCallback) throws IllegalArgumentException {
        public override void pack(BuildableTexture pBuildableTexture, List<TextureSourceWithLocationCallback> pTextureSourcesWithLocationCallback)
        {
            //Collections.sort(pTextureSourcesWithLocationCallback, TEXTURESOURCE_COMPARATOR);
            pTextureSourcesWithLocationCallback.Sort((IComparer<TextureSourceWithLocationCallback>)TEXTURESOURCE_COMPARER);

            NodeClass root = new NodeClass(new RectClass(0, 0, pBuildableTexture.getWidth(), pBuildableTexture.getHeight()));

            int textureSourceCount = pTextureSourcesWithLocationCallback.Count;

            for (int i = 0; i < textureSourceCount; i++)
            {
                TextureSourceWithLocationCallback textureSourceWithLocationCallback = pTextureSourcesWithLocationCallback[i];
                ITextureSource textureSource = textureSourceWithLocationCallback.getTextureSource();

                NodeClass inserted = root.insert(textureSource, pBuildableTexture.getWidth(), pBuildableTexture.getHeight(), this.mTextureSourceSpacing);

                if (inserted == null)
                {
                    throw new IllegalArgumentException("Could not pack: " + textureSource.ToString());
                }
                TextureSourceWithLocation textureSourceWithLocation = pBuildableTexture.addTextureSource(textureSource, inserted.mRect.mLeft, inserted.mRect.mTop);
                textureSourceWithLocationCallback.getCallback().onCallback(textureSourceWithLocation);
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        protected static RectClass Rect;
        //protected static class Rect {
        public class RectClass
        {
            // ===========================================================
            // Constants
            // ===========================================================

            // ===========================================================
            // Fields
            // ===========================================================

            private readonly int mLeft;
            private readonly int mTop;
            private readonly int mWidth;
            private readonly int mHeight;

            // ===========================================================
            // Constructors
            // ===========================================================

            //public Rect(int pLeft, int pTop, int pWidth, int pHeight) {
            public RectClass(int pLeft, int pTop, int pWidth, int pHeight)
            {
                this.mLeft = pLeft;
                this.mTop = pTop;
                this.mWidth = pWidth;
                this.mHeight = pHeight;
            }

            // ===========================================================
            // Getter & Setter
            // ===========================================================

            public int Width { get { return getWidth(); } }
            public int Height { get { return getHeight(); } }
            public int Top { get { return getTop(); } }
            public int Bottom { get { return getBottom(); } }
            public int Left { get { return getLeft(); } }
            public int Right { get { return getRight(); } }

            public int getWidth()
            {
                return this.mWidth;
            }

            public int getHeight()
            {
                return this.mHeight;
            }

            public int getLeft()
            {
                return this.mLeft;
            }

            public int getTop()
            {
                return this.mTop;
            }

            public int getRight()
            {
                return this.mLeft + this.mWidth;
            }

            public int getBottom()
            {
                return this.mTop + this.mHeight;
            }

            // ===========================================================
            // Methods for/from SuperClass/Interfaces
            // ===========================================================

            /*
            public override String toString() {
                return "@: " + this.mLeft + "/" + this.mTop + " * " + this.mWidth + "x" + this.mHeight;
            }
            */
            public override System.String ToString()
            {
                return System.String.Format("@: {0}/{1} * {2}x{3}", this.mLeft, this.mTop, this.mWidth, this.mHeight);
            }
            public override Java.Lang.String toString()
            {
                return new Java.Lang.String(ToString());
            }

            // ===========================================================
            // Methods
            // ===========================================================

            // ===========================================================
            // Inner and Anonymous Classes
            // ===========================================================
        }

        //protected static class Node {
        protected static NodeClass Node;
        public class NodeClass
        {
            // ===========================================================
            // Constants
            // ===========================================================

            // ===========================================================
            // Fields
            // ===========================================================

            //private Node mChildA;
            private NodeClass mChildA;
            //private Node mChildB;
            private NodeClass mChildB;
            //private final Rect mRect;
            private readonly RectClass mRect;
            private ITextureSource mTextureSource;

            // ===========================================================
            // Constructors
            // ===========================================================

            //public Node(final int pLeft, final int pTop, final int pWidth, final int pHeight)
            public NodeClass(int pLeft, int pTop, int pWidth, int pHeight)
                : this(new RectClass(pLeft, pTop, pWidth, pHeight))
            {
            }

            //public Node(final Rect pRect) {
            public NodeClass(RectClass pRect)
            {
                this.mRect = pRect;
            }

            // ===========================================================
            // Getter & Setter
            // ===========================================================

            //public Rect getRect() {
            public RectClass getRect()
            {
                return this.mRect;
            }
            public RectClass Rect { get { return getRect(); } }

            //public Node getChildA() {
            public NodeClass getChildA()
            {
                return this.mChildA;
            }
            public NodeClass ChildA { get { return getChildA(); } }

            //public Node getChildB() {
            public NodeClass getChildB()
            {
                return this.mChildB;
            }
            public NodeClass ChildB { get { return getChildB(); } }

            // ===========================================================
            // Methods for/from SuperClass/Interfaces
            // ===========================================================

            // ===========================================================
            // Methods
            // ===========================================================

            //	public Node insert(final ITextureSource pTextureSource, final int pTextureWidth, final int pTextureHeight, final int pTextureSpacing) throws IllegalArgumentException {
            public NodeClass insert(ITextureSource pTextureSource, int pTextureWidth, int pTextureHeight, int pTextureSpacing) /* throws IllegalArgumentException */ {
                if (this.mChildA != null && this.mChildB != null)
                {
                    NodeClass newNode = this.mChildA.insert(pTextureSource, pTextureWidth, pTextureHeight, pTextureSpacing);
                    if (newNode != null)
                    {
                        return newNode;
                    }
                    else
                    {
                        return this.mChildB.insert(pTextureSource, pTextureWidth, pTextureHeight, pTextureSpacing);
                    }
                }
                else
                {
                    if (this.mTextureSource != null)
                    {
                        return null;
                    }

                    int textureSourceWidth = pTextureSource.getWidth();
                    int textureSourceHeight = pTextureSource.getHeight();

                    int rectWidth = this.mRect.getWidth();
                    int rectHeight = this.mRect.getHeight();

                    if (textureSourceWidth > rectWidth || textureSourceHeight > rectHeight)
                    {
                        return null;
                    }

                    int textureSourceWidthWithSpacing = textureSourceWidth + pTextureSpacing;
                    int textureSourceHeightWithSpacing = textureSourceHeight + pTextureSpacing;

                    int rectLeft = this.mRect.getLeft();
                    int rectTop = this.mRect.getTop();

                    bool fitToBottomWithoutSpacing = textureSourceHeight == rectHeight && rectTop + textureSourceHeight == pTextureHeight;
                    bool fitToRightWithoutSpacing = textureSourceWidth == rectWidth && rectLeft + textureSourceWidth == pTextureWidth;

                    if (textureSourceWidthWithSpacing == rectWidth)
                    {
                        if (textureSourceHeightWithSpacing == rectHeight)
                        { /* Normal case with padding. */
                            this.mTextureSource = pTextureSource;
                            return this;
                        }
                        else if (fitToBottomWithoutSpacing)
                        { /* Bottom edge of the Texture. */
                            this.mTextureSource = pTextureSource;
                            return this;
                        }
                    }

                    if (fitToRightWithoutSpacing)
                    { /* Right edge of the Texture. */
                        if (textureSourceHeightWithSpacing == rectHeight)
                        {
                            this.mTextureSource = pTextureSource;
                            return this;
                        }
                        else if (fitToBottomWithoutSpacing)
                        { /* Bottom edge of the Texture. */
                            this.mTextureSource = pTextureSource;
                            return this;
                        }
                        else if (textureSourceHeightWithSpacing > rectHeight)
                        {
                            return null;
                        }
                        else
                        {

                            return createChildren(pTextureSource, pTextureWidth, pTextureHeight, pTextureSpacing, rectWidth - textureSourceWidth, rectHeight - textureSourceHeightWithSpacing);
                        }
                    }

                    if (fitToBottomWithoutSpacing)
                    {
                        if (textureSourceWidthWithSpacing == rectWidth)
                        {
                            this.mTextureSource = pTextureSource;
                            return this;
                        }
                        else if (textureSourceWidthWithSpacing > rectWidth)
                        {
                            return null;
                        }
                        else
                        {
                            return createChildren(pTextureSource, pTextureWidth, pTextureHeight, pTextureSpacing, rectWidth - textureSourceWidthWithSpacing, rectHeight - textureSourceHeight);
                        }
                    }
                    else if (textureSourceWidthWithSpacing > rectWidth || textureSourceHeightWithSpacing > rectHeight)
                    {
                        return null;
                    }
                    else
                    {
                        return createChildren(pTextureSource, pTextureWidth, pTextureHeight, pTextureSpacing, rectWidth - textureSourceWidthWithSpacing, rectHeight - textureSourceHeightWithSpacing);
                    }
                }
            }

            //private Node createChildren(final ITextureSource pTextureSource, final int pTextureWidth, final int pTextureHeight, final int pTextureSpacing, final int pDeltaWidth, final int pDeltaHeight) {
            private NodeClass createChildren(ITextureSource pTextureSource, int pTextureWidth, int pTextureHeight, int pTextureSpacing, int pDeltaWidth, int pDeltaHeight)
            {
                RectClass rect = this.mRect;

                if (pDeltaWidth >= pDeltaHeight)
                {
                    /* Split using a vertical axis. */
                    this.mChildA = new NodeClass(
                            rect.getLeft(),
                            rect.getTop(),
                            pTextureSource.getWidth() + pTextureSpacing,
                            rect.getHeight()
                    );

                    this.mChildB = new NodeClass(
                            rect.getLeft() + (pTextureSource.getWidth() + pTextureSpacing),
                            rect.getTop(),
                            rect.getWidth() - (pTextureSource.getWidth() + pTextureSpacing),
                            rect.getHeight()
                    );
                }
                else
                {
                    /* Split using a horizontal axis. */
                    this.mChildA = new NodeClass(
                            rect.getLeft(),
                            rect.getTop(),
                            rect.getWidth(),
                            pTextureSource.getHeight() + pTextureSpacing
                    );

                    this.mChildB = new NodeClass(
                            rect.getLeft(),
                            rect.getTop() + (pTextureSource.getHeight() + pTextureSpacing),
                            rect.getWidth(),
                            rect.getHeight() - (pTextureSource.getHeight() + pTextureSpacing)
                    );
                }

                return this.mChildA.insert(pTextureSource, pTextureWidth, pTextureHeight, pTextureSpacing);
            }

            // ===========================================================
            // Inner and Anonymous Classes
            // ===========================================================
        }
    }
}
namespace andengine.examples.launcher {

using R = andengine.net.examples.Resource;


/**
 * @author Nicolas Gramlich
 * @since 11:13:34 - 27.06.2010
 */
public class ExampleGroup : Java.Lang.Object
{
	// ===========================================================
	// Elements
	// ===========================================================

    public static readonly ExampleGroup SIMPLE = new ExampleGroup(R.String.examplegroup_simple,
                                                                  Example.LINE
        /*, Example.RECTANGLE, Example.SPRITE, Example.SPRITEREMOVE*/);
/*
	public static readonly ExampleGroup MODIFIER_AND_ANIMATION = new ExampleGroup(R.string.examplegroup_modifier_and_animation, 
			Example.MOVINGBALL, Example.ENTITYMODIFIER, Example.ENTITYMODIFIERIRREGULAR, Example.PATHMODIFIER, Example.ANIMATEDSPRITES, Example.EASEFUNCTION, Example.ROTATION3D ),
	public static readonly ExampleGroup TOUCH = new ExampleGroup(R.string.examplegroup_touch, 
			Example.TOUCHDRAG, Example.MULTITOUCH, Example.ANALOGONSCREENCONTROL, Example.DIGITALONSCREENCONTROL, Example.ANALOGONSCREENCONTROLS, Example.COORDINATECONVERSION, Example.PINCHZOOM),
	public static readonly ExampleGroup PARTICLESYSTEM = new ExampleGroup(R.string.examplegroup_particlesystems,
			Example.PARTICLESYSTEMSIMPLE, Example.PARTICLESYSTEMCOOL, Example.PARTICLESYSTEMNEXUS),
	public static readonly ExampleGroup MULTIPLAYER = new ExampleGroup(R.string.examplegroup_multiplayer,
			Example.MULTIPLAYER),
	public static readonly ExampleGroup PHYSICS = new ExampleGroup(R.string.examplegroup_physics,
			Example.COLLISIONDETECTION, Example.PHYSICS, Example.PHYSICSFIXEDSTEP, Example.PHYSICSCOLLISIONFILTERING, Example.PHYSICSJUMP, Example.PHYSICSREVOLUTEJOINT, Example.PHYSICSMOUSEJOINT, Example.PHYSICSREMOVE ),
	public static readonly ExampleGroup TEXT = new ExampleGroup(R.string.examplegroup_text,
			Example.TEXT, Example.TICKERTEXT, Example.CHANGEABLETEXT, Example.CUSTOMFONT, Example.STROKEFONT),
	public static readonly ExampleGroup AUDIO = new ExampleGroup(R.string.examplegroup_audio, 
			Example.SOUND, Example.MUSIC, Example.MODPLAYER),
	public static readonly ExampleGroup ADVANCED = new ExampleGroup(R.string.examplegroup_advanced, 
			Example.SPLITSCREEN, Example.BOUNDCAMERA ), // Example.AUGMENTEDREALITY, Example.AUGMENTEDREALITYHORIZON),
	public static readonly ExampleGroup BACKGROUND = new ExampleGroup(R.string.examplegroup_background, 
			Example.REPEATINGSPRITEBACKGROUND, Example.AUTOPARALLAXBACKGROUND, Example.TMXTILEDMAP),
	public static readonly ExampleGroup OTHER = new ExampleGroup(R.string.examplegroup_other, 
			Example.SCREENCAPTURE, Example.PAUSE, Example.MENU, Example.SUBMENU, Example.TEXTMENU, Example.ZOOM , Example.IMAGEFORMATS, Example.TEXTUREOPTIONS, Example.COLORKEYTEXTURESOURCEDECORATOR, Example.LOADTEXTURE, Example.UPDATETEXTURE, Example.XMLLAYOUT, Example.LEVELLOADER),
	public static readonly ExampleGroup APP = new ExampleGroup(R.string.examplegroup_app, 
			Example.APP_CITYRADAR),
	public static readonly ExampleGroup GAME = new ExampleGroup(R.string.examplegroup_game, 
			Example.GAME_PONG, Example.GAME_SNAKE, Example.GAME_RACER),
	public static readonly ExampleGroup BENCHMARK = new ExampleGroup(R.string.examplegroup_benchmark, 
			Example.BENCHMARK_SPRITE, Example.BENCHMARK_ENTITYMODIFIER, Example.BENCHMARK_ANIMATION, Example.BENCHMARK_TICKERTEXT, Example.BENCHMARK_PARTICLESYSTEM, Example.BENCHMARK_PHYSICS);
*/

	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================
	public readonly Example[] EXAMPLES;
    public readonly int NAMERESID;

	// ===========================================================
	// Constructors
	// ===========================================================

	private ExampleGroup(int pNameResID, params Example[] pExamples) {
		this.NAMERESID = pNameResID;
		this.EXAMPLES = pExamples;
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

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
}
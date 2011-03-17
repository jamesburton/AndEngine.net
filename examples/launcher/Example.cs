namespace andengine.examples.launcher
{

	// TODO: Restore references as examples are converted
	/* Remaining example references:-
using AnalogOnScreenControlExample = andengine.examples.AnalogOnScreenControlExample;
using AnalogOnScreenControlsExample  = andengine.examples.AnalogOnScreenControlsExample;
using AnimatedSpritesExample  = andengine.examples.AnimatedSpritesExample;
using AugmentedRealityExample  = andengine.examples.AugmentedRealityExample;
using AugmentedRealityHorizonExample = andengine.examples.AugmentedRealityHorizonExample;
using AutoParallaxBackgroundExample = andengine.examples.AutoParallaxBackgroundExample;
using BoundCameraExample = andengine.examples.BoundCameraExample;
using ChangeableTextExample = andengine.examples.ChangeableTextExample;
using CollisionDetectionExample = andengine.examples.CollisionDetectionExample;
using ColorKeyTextureSourceDecoratorExample = andengine.examples.ColorKeyTextureSourceDecoratorExample;
using CoordinateConversionExample = andengine.examples.CoordinateConversionExample;
using CustomFontExample = andengine.examples.CustomFontExample;
using DigitalOnScreenControlExample = andengine.examples.DigitalOnScreenControlExample;
using EaseFunctionExample = andengine.examples.EaseFunctionExample;
using EntityModifierExample = andengine.examples.EntityModifierExample;
using EntityModifierIrregularExample = andengine.examples.EntityModifierIrregularExample;
using ImageFormatsExample = andengine.examples.ImageFormatsExample;
using LevelLoaderExample = andengine.examples.LevelLoaderExample;
	 */
using LineExample = andengine.examples.LineExample;
	/*
using LoadTextureExample = andengine.examples.LoadTextureExample;
using MenuExample = andengine.examples.MenuExample;
using ModPlayerExample = andengine.examples.ModPlayerExample;
using MovingBallExample = andengine.examples.MovingBallExample;
using MultiTouchExample = andengine.examples.MultiTouchExample;
using MultiplayerExample = andengine.examples.MultiplayerExample;
using MusicExample = andengine.examples.MusicExample;
using ParticleSystemCoolExample = andengine.examples.ParticleSystemCoolExample;
using ParticleSystemNexusExample = andengine.examples.ParticleSystemNexusExample;
using ParticleSystemSimpleExample = andengine.examples.ParticleSystemSimpleExample;
using PathModifierExample = andengine.examples.PathModifierExample;
using PauseExample = andengine.examples.PauseExample;
using PhysicsCollisionFilteringExample = andengine.examples.PhysicsCollisionFilteringExample;
using PhysicsExample = andengine.examples.PhysicsExample;
using PhysicsFixedStepExample = andengine.examples.PhysicsFixedStepExample;
using PhysicsJumpExample = andengine.examples.PhysicsJumpExample;
using PhysicsMouseJointExample = andengine.examples.PhysicsMouseJointExample;
using PhysicsRemoveExample = andengine.examples.PhysicsRemoveExample;
using PhysicsRevoluteJointExample = andengine.examples.PhysicsRevoluteJointExample;
using PinchZoomExample = andengine.examples.PinchZoomExample;
	 */
using R = andengine.net.examples.Resource;
	/*
using RectangleExample = andengine.examples.RectangleExample;
using RepeatingSpriteBackgroundExample = andengine.examples.RepeatingSpriteBackgroundExample;
using Rotation3DExample = andengine.examples.Rotation3DExample;
using ScreenCaptureExample = andengine.examples.ScreenCaptureExample;
using SoundExample = andengine.examples.SoundExample;
using SplitScreenExample = andengine.examples.SplitScreenExample;
using SpriteExample = andengine.examples.SpriteExample;
using SpriteRemoveExample = andengine.examples.SpriteRemoveExample;
using StrokeFontExample = andengine.examples.StrokeFontExample;
using SubMenuExample = andengine.examples.SubMenuExample;
using TMXTiledMapExample = andengine.examples.TMXTiledMapExample;
using TextExample = andengine.examples.TextExample;
using TextMenuExample = andengine.examples.TextMenuExample;
using TextureOptionsExample = andengine.examples.TextureOptionsExample;
using TickerTextExample = andengine.examples.TickerTextExample;
using TouchDragExample = andengine.examples.TouchDragExample;
using UnloadResourcesExample = andengine.examples.UnloadResourcesExample;
using UpdateTextureExample = andengine.examples.UpdateTextureExample;
using XMLLayoutExample = andengine.examples.XMLLayoutExample;
using ZoomExample = andengine.examples.ZoomExample;
using CityRadarActivity = andengine.examples.app.cityradar.CityRadarActivity;
using AnimationBenchmark = andengine.examples.benchmark.AnimationBenchmark;
using EntityModifierBenchmark = andengine.examples.benchmark.EntityModifierBenchmark;
using ParticleSystemBenchmark = andengine.examples.benchmark.ParticleSystemBenchmark;
using PhysicsBenchmark = andengine.examples.benchmark.PhysicsBenchmark;
using SpriteBenchmark = andengine.examples.benchmark.SpriteBenchmark;
using TickerTextBenchmark = andengine.examples.benchmark.TickerTextBenchmark;
using PongGameActivity = andengine.examples.game.pong.PongGameActivity;
using RacerGameActivity = andengine.examples.game.racer.RacerGameActivity;
using SnakeGameActivity = andengine.examples.game.snake.SnakeGameActivity;
	 */
	using BaseGameActivity = andengine.ui.activity.BaseGameActivity;

/**
 * @author Nicolas Gramlich
 * @since 20:42:27 - 16.06.2010
 * /
enum Example {
	// ===========================================================
	// Elements
	// ===========================================================

	ANALOGONSCREENCONTROL(AnalogOnScreenControlExample.class, R.string.example_analogonscreencontrol),
	ANALOGONSCREENCONTROLS(AnalogOnScreenControlsExample.class, R.string.example_analogonscreencontrols),
	ANIMATEDSPRITES(AnimatedSpritesExample.class, R.string.example_animatedsprites),
	AUGMENTEDREALITY(AugmentedRealityExample.class, R.string.example_augmentedreality),
	AUGMENTEDREALITYHORIZON(AugmentedRealityHorizonExample.class, R.string.example_augmentedrealityhorizon),
	AUTOPARALLAXBACKGROUND(AutoParallaxBackgroundExample.class, R.string.example_autoparallaxbackground),
	BOUNDCAMERA(BoundCameraExample.class, R.string.example_boundcamera),
	CHANGEABLETEXT(ChangeableTextExample.class, R.string.example_changeabletext),
	COLLISIONDETECTION(CollisionDetectionExample.class, R.string.example_collisiondetection),
	COLORKEYTEXTURESOURCEDECORATOR(ColorKeyTextureSourceDecoratorExample.class, R.string.example_colorkeytexturesourcedecorator),
	COORDINATECONVERSION(CoordinateConversionExample.class, R.string.example_coordinateconversion),
	CUSTOMFONT(CustomFontExample.class, R.string.example_customfont),
	DIGITALONSCREENCONTROL(DigitalOnScreenControlExample.class, R.string.example_digitalonscreencontrol),
	EASEFUNCTION(EaseFunctionExample.class, R.string.example_easefunction),
	IMAGEFORMATS(ImageFormatsExample.class, R.string.example_imageformats),
	LEVELLOADER(LevelLoaderExample.class, R.string.example_levelloader),
	LINE(LineExample.class, R.string.example_line),
	LOADTEXTURE(LoadTextureExample.class, R.string.example_loadtexture),
	MENU(MenuExample.class, R.string.example_menu),
	MODPLAYER(ModPlayerExample.class, R.string.example_modplayer),
	MOVINGBALL(MovingBallExample.class, R.string.example_movingball),
	MULTIPLAYER(MultiplayerExample.class, R.string.example_multiplayer),
	MULTITOUCH(MultiTouchExample.class, R.string.example_multitouch),
	MUSIC(MusicExample.class, R.string.example_music),
	PAUSE(PauseExample.class, R.string.example_pause),
	PATHMODIFIER(PathModifierExample.class, R.string.example_pathmodifier),
	PARTICLESYSTEMNEXUS(ParticleSystemNexusExample.class, R.string.example_particlesystemnexus),
	PARTICLESYSTEMCOOL(ParticleSystemCoolExample.class, R.string.example_particlesystemcool),
	PARTICLESYSTEMSIMPLE(ParticleSystemSimpleExample.class, R.string.example_particlesystemsimple),
	PHYSICSCOLLISIONFILTERING(PhysicsCollisionFilteringExample.class, R.string.example_physicscollisionfiltering),
	PHYSICS(PhysicsExample.class, R.string.example_physics),
	PHYSICSFIXEDSTEP(PhysicsFixedStepExample.class, R.string.example_physicsfixedstep),
	PHYSICSMOUSEJOINT(PhysicsMouseJointExample.class, R.string.example_physicsmousejoint),
	PHYSICSJUMP(PhysicsJumpExample.class, R.string.example_physicsjump),
	PHYSICSREVOLUTEJOINT(PhysicsRevoluteJointExample.class, R.string.example_physicsrevolutejoint),
	PHYSICSREMOVE(PhysicsRemoveExample.class, R.string.example_physicsremove),
	PINCHZOOM(PinchZoomExample.class, R.string.example_pinchzoom),
	RECTANGLE(RectangleExample.class, R.string.example_rectangle),
	REPEATINGSPRITEBACKGROUND(RepeatingSpriteBackgroundExample.class, R.string.example_repeatingspritebackground),
	ROTATION3D(Rotation3DExample.class, R.string.example_rotation3d),
	ENTITYMODIFIER(EntityModifierExample.class, R.string.example_entitymodifier),
	ENTITYMODIFIERIRREGULAR(EntityModifierIrregularExample.class, R.string.example_entitymodifierirregular),
	SCREENCAPTURE(ScreenCaptureExample.class, R.string.example_screencapture),
	SOUND(SoundExample.class, R.string.example_sound),
	SPLITSCREEN(SplitScreenExample.class, R.string.example_splitscreen),
	SPRITE(SpriteExample.class, R.string.example_sprite),
	SPRITEREMOVE(SpriteRemoveExample.class, R.string.example_spriteremove),
	STROKEFONT(StrokeFontExample.class, R.string.example_strokefont),
	SUBMENU(SubMenuExample.class, R.string.example_submenu),
	TEXT(TextExample.class, R.string.example_text),
	TEXTMENU(TextMenuExample.class, R.string.example_textmenu),
	TEXTUREOPTIONS(TextureOptionsExample.class, R.string.example_textureoptions),
	TMXTILEDMAP(TMXTiledMapExample.class, R.string.example_tmxtiledmap),
	TICKERTEXT(TickerTextExample.class, R.string.example_tickertext),
	TOUCHDRAG(TouchDragExample.class, R.string.example_touchdrag),
	UNLOADRESOURCES(UnloadResourcesExample.class, R.string.example_unloadresources),
	UPDATETEXTURE(UpdateTextureExample.class, R.string.example_updatetexture),
	XMLLAYOUT(XMLLayoutExample.class, R.string.example_xmllayout),
	ZOOM(ZoomExample.class, R.string.example_zoom),

	BENCHMARK_ANIMATION(AnimationBenchmark.class, R.string.example_benchmark_animation),
	BENCHMARK_PARTICLESYSTEM(ParticleSystemBenchmark.class, R.string.example_benchmark_particlesystem),
	BENCHMARK_PHYSICS(PhysicsBenchmark.class, R.string.example_benchmark_physics),
	BENCHMARK_ENTITYMODIFIER(EntityModifierBenchmark.class, R.string.example_benchmark_entitymodifier),
	BENCHMARK_SPRITE(SpriteBenchmark.class, R.string.example_benchmark_sprite),
	BENCHMARK_TICKERTEXT(TickerTextBenchmark.class, R.string.example_benchmark_tickertext),

	APP_CITYRADAR(CityRadarActivity.class, R.string.example_app_cityradar),

	GAME_PONG(PongGameActivity.class, R.string.example_game_pong),
	GAME_SNAKE(SnakeGameActivity.class, R.string.example_game_snake),
	GAME_RACER(RacerGameActivity.class, R.string.example_game_racer);
	
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	public final Class<? : BaseGameActivity> CLASS;
	public final int NAMERESID;

	// ===========================================================
	// Constructors
	// ===========================================================

	private Example(final Class<? : BaseGameActivity> pExampleClass, final int pNameResID) {
		this.CLASS = pExampleClass;
		this.NAMERESID = pNameResID;
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
*/

	// TODO: Verify if I need to be closer to the CLASS<?... syntax, or if BaseGameActivity is fine ... will see a little further into conversion.
	public class Example<T> : BaseGameActivity
    {
		public readonly int NAMERESID;

		internal Example(int pNameResID) {
			this.NAMERESID = pNameResID;
		}
    }

    public class Example
    {
		public readonly BaseGameActivity LINE = new Example<LineExample>(R.String.example_line);

		// TODO: Convert remaining example references to C# equivalent:
		/* Remaining java enum source example references
	ANALOGONSCREENCONTROL(AnalogOnScreenControlExample.class, R.string.example_analogonscreencontrol),
	ANALOGONSCREENCONTROLS(AnalogOnScreenControlsExample.class, R.string.example_analogonscreencontrols),
	ANIMATEDSPRITES(AnimatedSpritesExample.class, R.string.example_animatedsprites),
	AUGMENTEDREALITY(AugmentedRealityExample.class, R.string.example_augmentedreality),
	AUGMENTEDREALITYHORIZON(AugmentedRealityHorizonExample.class, R.string.example_augmentedrealityhorizon),
	AUTOPARALLAXBACKGROUND(AutoParallaxBackgroundExample.class, R.string.example_autoparallaxbackground),
	BOUNDCAMERA(BoundCameraExample.class, R.string.example_boundcamera),
	CHANGEABLETEXT(ChangeableTextExample.class, R.string.example_changeabletext),
	COLLISIONDETECTION(CollisionDetectionExample.class, R.string.example_collisiondetection),
	COLORKEYTEXTURESOURCEDECORATOR(ColorKeyTextureSourceDecoratorExample.class, R.string.example_colorkeytexturesourcedecorator),
	COORDINATECONVERSION(CoordinateConversionExample.class, R.string.example_coordinateconversion),
	CUSTOMFONT(CustomFontExample.class, R.string.example_customfont),
	DIGITALONSCREENCONTROL(DigitalOnScreenControlExample.class, R.string.example_digitalonscreencontrol),
	EASEFUNCTION(EaseFunctionExample.class, R.string.example_easefunction),
	IMAGEFORMATS(ImageFormatsExample.class, R.string.example_imageformats),
	LEVELLOADER(LevelLoaderExample.class, R.string.example_levelloader),
	LOADTEXTURE(LoadTextureExample.class, R.string.example_loadtexture),
	MENU(MenuExample.class, R.string.example_menu),
	MODPLAYER(ModPlayerExample.class, R.string.example_modplayer),
	MOVINGBALL(MovingBallExample.class, R.string.example_movingball),
	MULTIPLAYER(MultiplayerExample.class, R.string.example_multiplayer),
	MULTITOUCH(MultiTouchExample.class, R.string.example_multitouch),
	MUSIC(MusicExample.class, R.string.example_music),
	PAUSE(PauseExample.class, R.string.example_pause),
	PATHMODIFIER(PathModifierExample.class, R.string.example_pathmodifier),
	PARTICLESYSTEMNEXUS(ParticleSystemNexusExample.class, R.string.example_particlesystemnexus),
	PARTICLESYSTEMCOOL(ParticleSystemCoolExample.class, R.string.example_particlesystemcool),
	PARTICLESYSTEMSIMPLE(ParticleSystemSimpleExample.class, R.string.example_particlesystemsimple),
	PHYSICSCOLLISIONFILTERING(PhysicsCollisionFilteringExample.class, R.string.example_physicscollisionfiltering),
	PHYSICS(PhysicsExample.class, R.string.example_physics),
	PHYSICSFIXEDSTEP(PhysicsFixedStepExample.class, R.string.example_physicsfixedstep),
	PHYSICSMOUSEJOINT(PhysicsMouseJointExample.class, R.string.example_physicsmousejoint),
	PHYSICSJUMP(PhysicsJumpExample.class, R.string.example_physicsjump),
	PHYSICSREVOLUTEJOINT(PhysicsRevoluteJointExample.class, R.string.example_physicsrevolutejoint),
	PHYSICSREMOVE(PhysicsRemoveExample.class, R.string.example_physicsremove),
	PINCHZOOM(PinchZoomExample.class, R.string.example_pinchzoom),
	RECTANGLE(RectangleExample.class, R.string.example_rectangle),
	REPEATINGSPRITEBACKGROUND(RepeatingSpriteBackgroundExample.class, R.string.example_repeatingspritebackground),
	ROTATION3D(Rotation3DExample.class, R.string.example_rotation3d),
	ENTITYMODIFIER(EntityModifierExample.class, R.string.example_entitymodifier),
	ENTITYMODIFIERIRREGULAR(EntityModifierIrregularExample.class, R.string.example_entitymodifierirregular),
	SCREENCAPTURE(ScreenCaptureExample.class, R.string.example_screencapture),
	SOUND(SoundExample.class, R.string.example_sound),
	SPLITSCREEN(SplitScreenExample.class, R.string.example_splitscreen),
	SPRITE(SpriteExample.class, R.string.example_sprite),
	SPRITEREMOVE(SpriteRemoveExample.class, R.string.example_spriteremove),
	STROKEFONT(StrokeFontExample.class, R.string.example_strokefont),
	SUBMENU(SubMenuExample.class, R.string.example_submenu),
	TEXT(TextExample.class, R.string.example_text),
	TEXTMENU(TextMenuExample.class, R.string.example_textmenu),
	TEXTUREOPTIONS(TextureOptionsExample.class, R.string.example_textureoptions),
	TMXTILEDMAP(TMXTiledMapExample.class, R.string.example_tmxtiledmap),
	TICKERTEXT(TickerTextExample.class, R.string.example_tickertext),
	TOUCHDRAG(TouchDragExample.class, R.string.example_touchdrag),
	UNLOADRESOURCES(UnloadResourcesExample.class, R.string.example_unloadresources),
	UPDATETEXTURE(UpdateTextureExample.class, R.string.example_updatetexture),
	XMLLAYOUT(XMLLayoutExample.class, R.string.example_xmllayout),
	ZOOM(ZoomExample.class, R.string.example_zoom),

	BENCHMARK_ANIMATION(AnimationBenchmark.class, R.string.example_benchmark_animation),
	BENCHMARK_PARTICLESYSTEM(ParticleSystemBenchmark.class, R.string.example_benchmark_particlesystem),
	BENCHMARK_PHYSICS(PhysicsBenchmark.class, R.string.example_benchmark_physics),
	BENCHMARK_ENTITYMODIFIER(EntityModifierBenchmark.class, R.string.example_benchmark_entitymodifier),
	BENCHMARK_SPRITE(SpriteBenchmark.class, R.string.example_benchmark_sprite),
	BENCHMARK_TICKERTEXT(TickerTextBenchmark.class, R.string.example_benchmark_tickertext),

	APP_CITYRADAR(CityRadarActivity.class, R.string.example_app_cityradar),

	GAME_PONG(PongGameActivity.class, R.string.example_game_pong),
	GAME_SNAKE(SnakeGameActivity.class, R.string.example_game_snake),
	GAME_RACER(RacerGameActivity.class, R.string.example_game_racer);
		*/
	}
}
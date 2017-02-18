using UnityEngine;
using System.Collections;

namespace Types {
	
	/// Holds all enumerated types

	public enum GameScene
	{
		SPLASH,
		MENU,
		LEVELS,
		PLAY
	}

	public enum GalaxyType
	{
		NONE,
		GALAXY_VIEW,
		HOME_GALAXY,
		DAHKRI_GALAXY,
		KYDOR_GALAXY,
		ZAX_GALAXY,
		MALIX_GALAXY,
		XILYANTIPHOR_GALAXY,
		VIDON_GALAXY,
		RYKTAR_GALAXY
	}

	public enum GalaxyColor
	{
		BLUE,
		RED,
		PINK,
		PURPLE,
		ORANGE,
		BLACK,
		GREEN
	}

	public enum TutorialType
	{
		GALAXIES,
		HOW_TO_PLAY,
		CONTROLS_THRUST_SHIP,
		GRAVITY_WELLS,
		ANTIGRAVITY_WELLS
	}

	public enum ShipType {
		SHIP_01,
		SHIP_02,
		SHIP_03,
		SHIP_04,
		SHIP_05,
		SHIP_06,
		SHIP_07,
		SHIP_08
	}

	public enum TimestampType {
		SHIP_01_ARMOR,
		SHIP_02_ARMOR,
		SHIP_03_ARMOR,
		SHIP_04_ARMOR,
		SHIP_05_ARMOR,
		SHIP_06_ARMOR,
		SHIP_07_ARMOR,
		SHIP_08_ARMOR
	}

	public enum MiscType {
		PLAYER_SHIP_TYPE
	}

	public enum AudioState 
	{
		ON,
		OFF
	}

	public enum AudioType
	{
		MUSIC,
		SFX
	}

	public enum Hotkey 
	{
		L_CLICK=KeyCode.Mouse0,
		R_CLICK=KeyCode.Mouse1,
		M_CLICK=KeyCode.Mouse2,
		CTRL=KeyCode.LeftControl,
		ALT=KeyCode.LeftAlt,
		L_ARROW=KeyCode.LeftArrow,
		R_ARROW=KeyCode.RightArrow,
		U_ARROW=KeyCode.UpArrow,
		D_ARROW=KeyCode.DownArrow,

		A=KeyCode.A,
		B=KeyCode.B,
		C=KeyCode.C,
		D=KeyCode.D,
		E=KeyCode.E,
		F=KeyCode.F,
		G=KeyCode.G,
		H=KeyCode.H,
		I=KeyCode.I,
		J=KeyCode.J,
		K=KeyCode.K,
		L=KeyCode.L,
		M=KeyCode.M,
		N=KeyCode.N,
		O=KeyCode.O,
		P=KeyCode.P,
		Q=KeyCode.Q,
		R=KeyCode.R,
		S=KeyCode.S,
		T=KeyCode.T,
		U=KeyCode.U,
		V=KeyCode.V,
		W=KeyCode.W,
		X=KeyCode.X,
		Y=KeyCode.Y,
		Z=KeyCode.Z,
	}

}

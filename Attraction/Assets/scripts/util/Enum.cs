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
		CONTROLS_ROTATE_SHIP,
		CONTROLS_LAUNCH_SHIP,
		GRAVITY_WELLS,
		ANTIGRAVITY_WELLS
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

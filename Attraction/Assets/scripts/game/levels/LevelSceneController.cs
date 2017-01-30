using UnityEngine;
using System.Collections;
using Util;
using Menu;
using Types;

public class LevelSceneController : MonoBehaviour {

	[System.Serializable]
	public class GalaxyZone
	{
		public Transform camTarget;
		public PageType pageToLoad;
	}

	public Transform cam;
	public GalaxyZone galaxyViewPoint;
	public GalaxyZone homeGalaxy;
	public GalaxyZone dahkriGalaxy;
	public GalaxyZone kydorGalaxy;
	public GalaxyZone zaxGalaxy;
	public GalaxyZone malixGalaxy;
	public GalaxyZone xilyantiphorGalaxy;
	public GalaxyZone vidonGalaxy;
	public GalaxyZone ryktarGalaxy;
	public float zoomSpeed;

	ApplicationLoader appLoader;
	PageManager pageManager;
	GalaxyZone activeZone;
	GalaxyType activeGalaxy;
	EnvironmentColorModule environment;

	void Start()
	{
		appLoader = ApplicationLoader.Instance;
		pageManager = PageManager.Instance;
		environment = EnvironmentColorModule.Instance;
		StartCoroutine("WaitToZoomOut");
	}

	IEnumerator WaitToZoomOut()
	{
		while (appLoader.sceneIsFadedOut) //while the loading screen is still fading out
			yield return null;

		ViewGalaxy(GalaxyType.GALAXY_VIEW);
	}

	IEnumerator ZoomToGalaxy(GalaxyZone galaxy)
	{
		if (activeZone != null) {
			//wait until the current galaxy page is gone
			pageManager.TurnOffPage(activeZone.pageToLoad, PageType.NONE);
			while (pageManager.PageIsExiting(activeZone.pageToLoad))
				yield return new WaitForSeconds(0.25f);
		}

		Transform t = galaxy.camTarget;
		float distance = Vector3.Distance(cam.position, t.position);
		float totalDistance = distance;
		while (distance > 0.1f)
		{
			cam.position = Vector3.Lerp(cam.position, t.position, ((totalDistance - distance)*0.1f + zoomSpeed) * Time.deltaTime);
			distance = Vector3.Distance(cam.position, t.position);
			yield return null;
		}

		activeZone = galaxy;
		
		if (activeZone == galaxyViewPoint)
		{
			pageManager.LoadPage(activeZone.pageToLoad);
			TutorialManager.Instance.StartTutorial(TutorialType.GALAXIES);
		}
		else {
			int status = ProgressManager.Instance.GetStatus(activeGalaxy, 0);
			if (status == 0) {
				pageManager.LoadPage(PageType.GALAXY_INFO);
				GalaxyInfoPage infoPage = GameObject.FindObjectOfType<GalaxyInfoPage>();
				infoPage.ConfigureInfo(activeGalaxy, activeZone.pageToLoad);
			} else {
				pageManager.LoadPage(activeZone.pageToLoad);
			}
		}
	}

	public void ViewGalaxy(GalaxyType galaxyType)
	{
		if (activeGalaxy == galaxyType)
			return;

		StopCoroutine("ZoomToGalaxy");

		switch (galaxyType)
		{
			case GalaxyType.GALAXY_VIEW: 
				StartCoroutine("ZoomToGalaxy", galaxyViewPoint); 
				//environment.SetEnvironmentColor(GalaxyColor.BLUE);
				break;
			case GalaxyType.HOME_GALAXY: 
				StartCoroutine("ZoomToGalaxy", homeGalaxy); 
				//environment.SetEnvironmentColor(GalaxyColor.BLUE);
				break;
			case GalaxyType.DAHKRI_GALAXY: 
				StartCoroutine("ZoomToGalaxy", dahkriGalaxy); 
				//environment.SetEnvironmentColor(GalaxyColor.PINK);
				break;
			case GalaxyType.KYDOR_GALAXY: 
				StartCoroutine("ZoomToGalaxy", kydorGalaxy); 
				//environment.SetEnvironmentColor(GalaxyColor.PURPLE);
				break;
			case GalaxyType.ZAX_GALAXY: 
				StartCoroutine("ZoomToGalaxy", zaxGalaxy); 
				//environment.SetEnvironmentColor(GalaxyColor.RED);
				break;
			case GalaxyType.MALIX_GALAXY: 
				StartCoroutine("ZoomToGalaxy", malixGalaxy); 
				//environment.SetEnvironmentColor(GalaxyColor.BLACK);
				break;
			case GalaxyType.XILYANTIPHOR_GALAXY: 
				StartCoroutine("ZoomToGalaxy", xilyantiphorGalaxy); 
				//environment.SetEnvironmentColor(GalaxyColor.RED);
				break;
			case GalaxyType.VIDON_GALAXY: 
				StartCoroutine("ZoomToGalaxy", vidonGalaxy); 
				//environment.SetEnvironmentColor(GalaxyColor.PINK);
				break;
			case GalaxyType.RYKTAR_GALAXY: 
				StartCoroutine("ZoomToGalaxy", ryktarGalaxy); 
				//environment.SetEnvironmentColor(GalaxyColor.GREEN);
				break;
		}

		activeGalaxy = galaxyType;
	}
}

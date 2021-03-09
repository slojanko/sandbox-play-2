using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sandbox { 
	public class SettingsManager : ManagerBase<SettingsManager>
	{
		public GraphicsDeviceType[] availableGraphicsAPI;
		public GraphicsDeviceType selectedAPI;
		public Resolution[] availableResolutions;

		protected override void Awake()
		{
			base.Awake();

	#if UNITY_EDITOR
			availableGraphicsAPI = PlayerSettings.GetGraphicsAPIs(EditorUserBuildSettings.activeBuildTarget);
	#endif
			selectedAPI = SystemInfo.graphicsDeviceType;
			availableResolutions = Screen.resolutions;

			Application.targetFrameRate = 9999;
			SceneManager.LoadScene("GameScene");
		}
	}
}
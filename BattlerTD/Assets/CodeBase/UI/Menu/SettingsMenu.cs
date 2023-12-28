using UnityEngine;
using UnityEngine.Audio;

namespace CodeBase.UI.Menu
{
	public class SettingsMenu : MonoBehaviour
	{
		private const string Volume = "Volume";
		
		[SerializeField] private AudioMixer _audioMixer;
		[SerializeField] private GameObject _mainMenu;

		public void SetVolume(float volume) =>
			_audioMixer.SetFloat(Volume, volume);

		public void SetQuality(int qualityIndex) =>
			QualitySettings.SetQualityLevel(qualityIndex);

		public void GoMainMenu()
		{
			gameObject.SetActive(false);
			_mainMenu.SetActive(true);
		}

	}
}
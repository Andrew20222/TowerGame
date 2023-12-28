using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Audio
{
	public class AudioService : IAudioService
	{
		private readonly AudioSource _audioSource;
		private readonly SoundStaticData _soundData;

		public AudioService(IStaticDataService staticData, AudioSource audioSource)
		{
			_soundData = staticData.ForSounds();
			_audioSource = audioSource;
		}

		public void PlaySound(AudioClip clip)
		{
			_audioSource.clip = clip;
			_audioSource.Play();
		}

		public void PlayMainMenuMusic()
		{
			_audioSource.clip = _soundData.MainMenuTheme;
			_audioSource.Play();
		}	
		
		public void PlayFightStageMusic()
		{
			_audioSource.clip = _soundData.FightStageTheme;
			_audioSource.Play();
		}

		public void PauseSound()
		{
			_audioSource.Pause();
		}

		public void ResumeSound()
		{
			_audioSource.UnPause();
		}

		public void StopSound()
		{
			_audioSource.Stop();
		}

		public void SetVolume(float volume)
		{
			_audioSource.volume = volume;
		}
	}

}
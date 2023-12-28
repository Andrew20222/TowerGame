using UnityEngine;

namespace CodeBase.Infrastructure.Services.Audio
{
	public interface IAudioService : IService
	{
		void PlaySound(AudioClip clip);
		void PlayMainMenuMusic();
		void PlayFightStageMusic();
		void PauseSound();
		void ResumeSound();
		void StopSound();
		void SetVolume(float volume);
	}
}
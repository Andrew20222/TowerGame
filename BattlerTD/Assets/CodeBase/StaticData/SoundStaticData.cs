using UnityEngine;

namespace CodeBase.StaticData
{
	[CreateAssetMenu(fileName = "SoundData", menuName = "StaticData/Sound")]
	public class SoundStaticData : ScriptableObject
	{
		[Header("BackgroundMusic")]
		
		public AudioClip MainMenuTheme;
		public AudioClip FightStageTheme;
		public AudioClip BuildingStageTheme;

		[Header("GameSFX")]
		[Header("Enemy")]
		public AudioClip Explosion;
	}
}
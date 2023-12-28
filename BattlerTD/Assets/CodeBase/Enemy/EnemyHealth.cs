using System;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{

	public class EnemyHealth : MonoBehaviour, IHealth
	{
		public EnemyAnimator Animator;

		[SerializeField] private float _current;

		[SerializeField] private float _max;

		[SerializeField] private PickUpPopUp PickupPopup;
		[SerializeField] private Canvas PickupPopupPrefab;
		
		private IRandomService _randomService;
		private IPersistentProgressService _progressService;

		[SerializeField] private int _criticalCounter = 0;
		public event Action HealthChanged;

		public float Current
		{
			get => _current;
			set => _current = value;
		}

		public float Max
		{
			get => _max;
			set => _max = value;
		}

		public void Construct(IRandomService randomService, IPersistentProgressService progressService)
		{
			_randomService = randomService;
			_progressService = progressService;
		}

		public void TakeDamage(float damage,  IHealth invoker = null)
		{
			float next = _randomService.Next(1, 100);
			if (next < _progressService.Progress.HeroStats.CriticalChance)
			{
				_criticalCounter++;
				Debug.Log("Crit");
				Debug.Log(_criticalCounter);
				Current -= damage * _progressService.Progress.HeroStats.CriticalMultiplier;
			}
			else
			{
				Current -= damage;
			}

			//PickUpPopUp pickUpPopUp = Instantiate(PickupPopup, PickupPopupPrefab.transform);

			//pickUpPopUp.DamageText.text = $"{damage}";

			//pickUpPopUp.PlayPopUp();
			Animator.PlayHit();

			HealthChanged?.Invoke();
		}

		public void LevelUp() { }

	}

}
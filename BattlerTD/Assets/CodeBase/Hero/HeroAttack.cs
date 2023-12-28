using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Inputs;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Hero
{
	public abstract class HeroAttack : MonoBehaviour, ISavedProgressReader
	{
		private const string Hittable = "Hittable";

		[SerializeField] protected GameObject _sword;
		[SerializeField] protected GameObject _bow;
		[SerializeField] protected HeroAnimator _animator;
		[SerializeField] protected float _meleeAttackTimer;

		protected IInputService _inputService;
		protected IPersistentProgressService _progressService;

		protected float _attackButtonPressedTimer;
		protected Stats _stats;
		protected int _layerMask;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
			_progressService = AllServices.Container.Single<IPersistentProgressService>();
			_inputService.AttackButtonUnpressed += AttackButtonUnpressed;

			_layerMask = 1 << LayerMask.NameToLayer(Hittable);
		}

		private void OnDestroy() =>
			_inputService.AttackButtonUnpressed -= AttackButtonUnpressed;

		public void LoadProgress(PlayerProgress progress) =>
			_stats = progress.HeroStats;

		private void Update()
		{
			if (!_inputService.IsAttackButton())
				return;

			if (_inputService.Tap)
				Debug.Log("da");

			OnUpdate();
			UpdateTimer();
		}

		protected virtual void OnUpdate() { }

		private void UpdateTimer() =>
			_attackButtonPressedTimer += Time.deltaTime;

		protected virtual void AttackButtonUnpressed() =>
			_attackButtonPressedTimer = 0;

	}
}
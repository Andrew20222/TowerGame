using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;
using PhysicsDebug = CodeBase.Logic.PhysicsDebug;

namespace CodeBase.Hero
{

	[RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
	public class HeroMeleeAttack : HeroAttack
	{
		private GameObject _impactVfx;
		private Collider[] _hits = new Collider[3];

		[SerializeField] private float _damage;
		[SerializeField] private HeroHealth _health;
		[SerializeField] private ParticleSystem _impactFxPrefab;
		[SerializeField] private AudioSource _impactSx;

		protected override void AttackButtonUnpressed()
		{
			if (_attackButtonPressedTimer <= _meleeAttackTimer)
				_animator.PlayAttack();

			base.AttackButtonUnpressed();
		}

		private void OnAttack()
		{
			PhysicsDebug.DrawDebug(transform.position + transform.forward, _stats.SwordRadius, 1.0f);
			for (int i = 0; i < Hit(); ++i)
			{
				float swordDamageMultiplier = _stats.SwordDamage * (_stats.DamageMultiplier + _stats.SwordDamageMultiplier);
				_hits[i].transform.GetComponentInParent<IHealth>().TakeDamage(swordDamageMultiplier);
				_health.Heal(swordDamageMultiplier * _progressService.Progress.HeroStats.Vampiric);
				PlayTakeDamageFx(_hits[i].transform.position);
			}

			if (Hit() > 0)
				_impactSx.Play();
		}

		private int Hit() =>
			Physics.OverlapSphereNonAlloc(transform.position + transform.forward, _stats.SwordRadius, _hits, _layerMask);

		private void PlayTakeDamageFx(Vector3 position)
		{
			_impactFxPrefab.transform.position = position;
			_impactFxPrefab.Play();
		}
	}
}
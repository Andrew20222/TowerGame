using System.Linq;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
	public class EnemyMeleeAttack : Attack
	{
		public float Cleavage = 0.5f;
		public float EffectiveDistance = 0.5f;
		public float Damage = 10;
		
		private Collider[] _hits = new Collider[1];

		[SerializeField] private ParticleSystem _impactFxPrefab;

		public override void OnAttack()
		{
			MoveToPlayer.StopMove();
			if (Hit(out Collider hit))
			{
				//PhysicsDebug.DrawDebugCross(StartPoint(), Cleavage, 1.0f);
				hit.transform.GetComponent<IHealth>().TakeDamage(Damage, _enemyHealth);
				PlayTakeDamageFx(hit.transform.position);
			}
		}

		public override void OnAttackEnded()
		{
			base.OnAttackEnded();
			MoveToPlayer.StartMove();
		}


		private bool Hit(out Collider hit)
		{
			int hitAmount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);

			hit = _hits.FirstOrDefault();

			return hitAmount > 0;
		}

		private void OnDrawGizmos() =>
			Gizmos.DrawWireSphere(StartPoint(), Cleavage);

		private Vector3 StartPoint() =>
			new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
			transform.forward * EffectiveDistance;



		private void PlayTakeDamageFx(Vector3 position)
		{
			_impactFxPrefab.transform.position = position;
			_impactFxPrefab.Play();
		}


	}
}
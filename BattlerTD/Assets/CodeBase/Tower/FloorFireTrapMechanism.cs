using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Tower
{
	public class FloorFireTrapMechanism : MonoBehaviour
	{
		private const string Hittable = "Hittable";

		[SerializeField] private Collider _size;
		[SerializeField] private float _damage;
		[SerializeField] private float _damageDelay;

		private float _damageTimer;
		private readonly Collider[] _enemies = new Collider[100];
		private int _layerMask;

		private void Awake() =>
			_layerMask = 1 << LayerMask.NameToLayer(Hittable);

		private void Update()
		{
			for (int i = 0; i < Hit(); ++i)
			{
				_damageTimer += Time.deltaTime;

				if (_damageTimer >= _damageDelay)
				{
					_enemies[i].transform.parent.TryGetComponent(out IHealth health);
					health.TakeDamage(_damage);
					_damageTimer = 0;
				}
			}
		}

		private int Hit() =>
			Physics.OverlapBoxNonAlloc(_size.bounds.center, _size.bounds.extents * 2, _enemies, Quaternion.identity, _layerMask);

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireCube(_size.bounds.center, _size.bounds.extents * 2);
		}
	}
}
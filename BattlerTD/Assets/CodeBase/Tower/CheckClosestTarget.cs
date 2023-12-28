using System.Linq;
using UnityEngine;

namespace CodeBase.Tower
{
	public class CheckClosestTarget : MonoBehaviour
	{
		private const string Enemy = "Hittable";
		private Collider[] _hits = new Collider[1];
		private int _layerMask;
		private Transform _target;
		public float Cleavage;
		public Transform Target => _target;
		
		private void Awake() =>
			_layerMask = 1 << LayerMask.NameToLayer(Enemy);

		private void Update()
		{
			if (Hit(out Collider hit))
			{
				_target = hit.transform;
			}
			else
			{
				_target = null;
			}
		}
		
		private bool Hit(out Collider hit)
		{
			int hitAmount = Physics.OverlapSphereNonAlloc(transform.position, Cleavage, _hits, _layerMask);

			hit = _hits.FirstOrDefault();

			return hitAmount > 0;
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireSphere(transform.position, Cleavage);
		}
	}
}
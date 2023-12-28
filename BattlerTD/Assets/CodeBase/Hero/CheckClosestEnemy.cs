using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Hero
{
	public class CheckClosestEnemy : MonoBehaviour
	{
		private Collider[] _hits = new Collider[2];
		private int _layerMask;
		private Transform _closestEnemy;
		public Vector3 Cleavage;
		public Transform ClosestEnemy => _closestEnemy;


		private void Awake() =>
			_layerMask = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("MainBuilding");

		private void Update()
		{
			if (Hit(out Collider hit))
			{
				_closestEnemy = hit.transform;
			}
			else
			{
				_closestEnemy = null;
			}
		}


		private bool Hit(out Collider hit)
		{
			int hitAmount = Physics.OverlapBoxNonAlloc(transform.position, Cleavage, _hits, Quaternion.identity, _layerMask);

			List<Collider> hits = new List<Collider>();
			for (int i = 0; i < _hits.Length; i++)
			{
				if (_hits[i] != null)
					hits.Add(_hits[i]);
			}

			if (hits.Count > 1)
			{
				hits.Sort((a, b) => Vector3.Distance(transform.position, a.transform.position).CompareTo(Vector3.Distance(transform.position, b.transform.position)));
			}

			hit = hits.FirstOrDefault();

			return hitAmount > 0;
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireCube(transform.position, Cleavage * 2);
		}
	}
}
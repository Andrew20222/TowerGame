using System;
using UnityEngine;
using UnityEngine.VFX;

namespace CodeBase.Tower
{
	public class OrbLightningTower : PlaceableObject
	{
		[SerializeField] private MeshRenderer _tower;
		[SerializeField] private ParticleSystem[] _particleSystems;

		private Color _originalColor;
		private void Awake()
		{
			foreach (ParticleSystem particleSystem in _particleSystems)
			{
				particleSystem.gameObject.SetActive(false);
			}
		}

		private void Update()
		{
			if (Placed)
				return;

			if (_buildingService.CanBePlaced(this, _inAnotherTower))
			{
				foreach (Material material in _tower.materials)
				{
					material.color = Color.green;
				}

				foreach (ParticleSystem particleSystem in _particleSystems)
				{
					particleSystem.gameObject.SetActive(true);
				}
			}
			else
			{
				foreach (Material material in _tower.materials)
				{
					material.color = Color.red;
				}
			}
		}

		public override void Place(ObjectDrag objectDrag)
		{
			base.Place(objectDrag);
		}
	}
}
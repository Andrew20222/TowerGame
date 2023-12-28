using UnityEngine;

namespace CodeBase.Tower
{
	public class FloorTrap : PlaceableObject
	{
		[SerializeField] private MeshRenderer _tower;

		private void Update()
		{
			if (Placed)
				return;
			
			if (_buildingService.CanBePlaced(this, _inAnotherTower))
			{
				_tower.material.color = Color.green;
			}
			else
			{
				_tower.material.color = Color.red;
			}
		}
	}
}
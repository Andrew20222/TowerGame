using UnityEngine;

namespace CodeBase.Tower
{
	public class ArrowWallTrap : PlaceableObject
	{
		[SerializeField] private MeshRenderer _trapMesh;
		[SerializeField] private MeshRenderer[] _arrows;

		private void Update()
		{
			Debug.Log(_inAnotherTower);
			if (Placed)
				return;

			if (_buildingService.CanBePlaced(this, _inAnotherTower))
			{
				_trapMesh.material.color = Color.green;

				for (int i = 0; i < _arrows.Length; i++)
				{
					_arrows[i].material.color = Color.green;
				}
			}
			else
			{
				_trapMesh.material.color = Color.red;

				for (int i = 0; i < _arrows.Length; i++)
				{
					_arrows[i].material.color = Color.red;
				}
			}
		}
	}
}
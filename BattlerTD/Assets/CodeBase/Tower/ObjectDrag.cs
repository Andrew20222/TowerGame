using System;
using System.Collections;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Inputs;
using UnityEngine;

namespace CodeBase.Tower
{
	public class ObjectDrag : MonoBehaviour
	{
		[SerializeField] private LayerMask _layerMask;
		private Vector3 _offset;
		private IBuildingService _buildingService;

		public void Construct(IBuildingService buildingService, IInputService inputService)
		{
			_buildingService = buildingService;
		}

		public void StopDraggingCoroutine()
		{
			StopCoroutine(OnDraggingObject());
		}

		public void OnObjectTapped()
		{
			_offset = transform.position - _buildingService.GetMouseWorldPosition();
			StartCoroutine(OnDraggingObject());
		}

		private IEnumerator OnDraggingObject()
		{
			while (_buildingService.IsDraggingObject)
			{
				if (CheckTouchPosition())
				{
					Vector3 position = _buildingService.GetMouseWorldPosition() + _offset;
					transform.position = _buildingService.SnapCoordinateToGrid(position);
				}
				else
				{
					Vector3 position = _buildingService.GetMouseWorldPosition() + _offset;
					Vector3 snappedPosition = new Vector3(transform.position.x, transform.position.y, position.z);
					transform.position = _buildingService.SnapCoordinateToGrid(snappedPosition);
				}
				yield return null;
			}
		}

		private bool CheckTouchPosition()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (!Physics.Raycast(ray, 100,_layerMask))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
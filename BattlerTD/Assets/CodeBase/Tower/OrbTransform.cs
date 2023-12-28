using System;
using UnityEngine;

namespace CodeBase.Tower
{
	public class OrbTransform : MonoBehaviour
	{
		[SerializeField] private float _rayLengthUp = 5.0f;
		[SerializeField] private float _rayLengthDown = 5.0f;
		[SerializeField] private LayerMask _layerMask;
		[SerializeField] private Transform _visual;
		[SerializeField] private float _offset;
		[SerializeField] private float _sideMultiplier;
		
		private Ray _upRay;
		private Ray _downwRay;
		private float _cachedHeightPosition;

		private void Start()
		{
			_cachedHeightPosition = _visual.position.y;
		}

		private void Update()
		{
			Vector3 rayOrigin = _visual.position;

			_upRay = new Ray(rayOrigin, transform.up);
			_downwRay = new Ray(rayOrigin, -transform.up);

			bool raycastUp = Physics.Raycast(_upRay, out RaycastHit _, _rayLengthUp * _sideMultiplier, _layerMask);
			bool raycastDown = Physics.Raycast(_downwRay, out RaycastHit _, _rayLengthDown * _sideMultiplier, _layerMask);
			
			if (raycastUp)
			{
				_visual.localPosition = new Vector3(_visual.localPosition.x, _visual.localPosition.y + _offset, _visual.localPosition.z);
				Debug.Log("Up");
			}

			if (!raycastUp && !raycastDown)
			{
				_visual.localPosition = new Vector3(_visual.localPosition.x, _cachedHeightPosition, _visual.localPosition.z);
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawRay(transform.position,transform.up * _sideMultiplier);
			Gizmos.DrawRay(transform.position,-transform.up * _sideMultiplier);
		}
	}
}
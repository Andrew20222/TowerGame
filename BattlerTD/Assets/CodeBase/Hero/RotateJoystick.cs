using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Inputs;
using UnityEngine;

namespace CodeBase.Hero
{
	public class RotateJoystick : MonoBehaviour
	{
		public float rotateSpeed = 15;
		
		private IInputService _inputService;

		private void Awake() =>
			_inputService = AllServices.Container.Single<IInputService>();

		private void Update()
		{
			Vector3 positionToLook = new Vector3(_inputService.RotatingAxis.x, 0f, _inputService.RotatingAxis.y); 

		
			if (positionToLook == Vector3.zero)
				return;
			
			Quaternion targetRotation = Quaternion.LookRotation(positionToLook); 
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime); 
		}
	}
}
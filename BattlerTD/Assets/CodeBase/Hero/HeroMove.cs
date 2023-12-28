using CodeBase.Infrastructure.Services.Inputs;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Timers;
using UnityEngine;

namespace CodeBase.Hero
{
	public class HeroMove : MonoBehaviour
	{
		[SerializeField] private CharacterController _characterController;
		[SerializeField] private HeroRangeAttack _heroRangeAttack;
		[SerializeField] private Transform _lineView;
		[SerializeField] private Transform _heroView;
		[SerializeField] private float _movementSpeed;

		private Camera _camera;
		private IInputService _inputService;
		private ITimerService _timerService;
		private IPersistentProgressService _progressService;
		private Vector3 _cachedRotation;


		public void Construct(ITimerService timerService, IInputService inputService, IPersistentProgressService progressService)
		{
			_timerService = timerService;
			_inputService = inputService;
			_progressService = progressService;
		}
		
		private void Start()
		{
			_movementSpeed = _progressService.Progress.HeroStats.MoveSpeed * _progressService.Progress.HeroStats.MoveSpeedMultiplier;
			_camera = Camera.main;
		}

		private void Update()
		{
			Vector3 movementVector = Vector3.zero;
			
			if (_inputService?.MovingAxis.sqrMagnitude > 0.001 )
			{
				movementVector = _camera.transform.TransformDirection(_inputService.MovingAxis);
				movementVector.y = 0;
				movementVector.Normalize();

				if (!_heroRangeAttack.ShootMode)
				{
					_heroView.transform.forward = movementVector;
					
					_lineView.forward = movementVector;
				}
				else
				{
					//_lineView.forward = _cachedRotation;
				}
			}
    
			movementVector += Physics.gravity;

			if ( _timerService != null)
				_characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
		}
	}
}

using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
	public class ExplosionEnemy : MonoBehaviour
	{
		[SerializeField] private TriggerObserver _triggerObserver;
		[SerializeField] private ParticleSystem _explosionVFX;
		[SerializeField] private AudioSource _explosionSFX;
		[SerializeField] private float _damage;

		private IPersistentProgressService _progressService;

		private void Awake()
		{
			_progressService = AllServices.Container.Single<IPersistentProgressService>();
			_triggerObserver.TriggerEnter += TriggerEnter;
		}

		private void OnDestroy() =>
			_triggerObserver.TriggerEnter -= TriggerEnter;

		private void TriggerEnter(Collider obj)
		{
			
			obj.GetComponent<IHealth>().TakeDamage(_damage);
			_explosionSFX.Play();
			Instantiate(_explosionVFX, transform.position, Quaternion.identity);	
			Destroy(gameObject, 0.5f);
			_progressService.Progress.KillData.Add(1);           
		}
	}
}
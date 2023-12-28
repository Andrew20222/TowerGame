using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Weapon
{
	public class BulletEnemy : MonoBehaviour
	{ 
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private float _damage;
		[SerializeField] private GameObject _explosionVFX;

		public float _moveSpeed;

		private void Start()
		{
			Destroy(gameObject, 7);
		}

		private void Update()
		{
			_rigidbody.velocity = transform.forward * (_moveSpeed * Time.deltaTime);
		}

		private void OnTriggerEnter(Collider other)
		{
			other.transform.GetComponent<IHealth>().TakeDamage(_damage);
			//Instantiate(_explosionVFX, other.transform.position, Quaternion.identity);
			Destroy(gameObject);
		}

	}
}
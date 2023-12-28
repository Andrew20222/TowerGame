using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Hero
{
	public class MagicShield : MonoBehaviour
	{
		[SerializeField] private float _timer;

		private bool _isActive;
		public bool IsActive
		{
			get
			{
				return _isActive;
			}
		}

		public void AppearShield()
		{
			_isActive = true;
			gameObject.SetActive(true);
			transform.DOScale(2.5f, 0.2f).OnComplete(() =>
			{
				StartCoroutine(ShieldTimer());
			});
		}

		private IEnumerator ShieldTimer()
		{
			
			yield return new WaitForSeconds(_timer);
			DisappearShield();
		}

		private void DisappearShield()
		{
			transform.DOScale(0.1f, 0.2f).OnComplete(() =>
			{
				_isActive = false;
				gameObject.SetActive(true);
			});
		}

	}
}
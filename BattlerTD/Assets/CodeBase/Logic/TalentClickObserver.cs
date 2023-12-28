using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Logic
{
	public class TalentClickObserver : MonoBehaviour
	{
		public event Action MouseDown;

		public void OnPointerDown() =>
			MouseDown?.Invoke();
	}
}
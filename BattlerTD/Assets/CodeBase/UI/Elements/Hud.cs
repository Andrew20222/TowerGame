using System;
using CodeBase.Tower;
using DG.Tweening;
using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
	public class Hud : MonoBehaviour
	{
		[SerializeField] private RectTransform _towerPanelStartPosition;
		[SerializeField] private RectTransform _towerPanelInGamePosition;
		[SerializeField] private TowerPanel _towerPanel;
		[SerializeField] private RectTransform _hudTransform;
		[SerializeField] private float _appearDuration;

		private Vector3 _startPosition;
		private IBuildingService _buildingService;
		private ButtonInputUI[] _buttons;
		public RectTransform HUDTransform
		{
			get
			{
				return _hudTransform;
			}
			set
			{
				_hudTransform = value;
			}
		}

		public void Construct(IBuildingService buildingService) =>
			_buildingService = buildingService;

		private void OnDestroy() =>
			DOTween.KillAll();

		public void AppearAttackButton()
		{
			_buttons = _towerPanel.GetComponentsInChildren<ButtonInputUI>();
			foreach (ButtonInputUI button in _buttons)
			{
				button.enabled = false;
			}
			
			_buildingService.IsActive = false;
		}
		
		public void DisappearTowerPanel() =>
			_towerPanel.transform.DOLocalMove(_towerPanelStartPosition.localPosition, _appearDuration).OnComplete(() =>
			{
				_towerPanel.IsClicked = false;
			});

		public void AppearTowerPanel()
		{
			foreach (ButtonInputUI button in _buttons)
			{
				button.enabled = true;
			}
			_buildingService.IsActive = true;
			_towerPanel.transform.DOLocalMove(_towerPanelInGamePosition.localPosition, _appearDuration);
		}
	}
}
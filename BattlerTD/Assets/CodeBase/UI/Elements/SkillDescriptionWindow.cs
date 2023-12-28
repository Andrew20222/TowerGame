using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.UI.Menu;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
	public class SkillDescriptionWindow : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _skillDescription;
		[SerializeField] private TextMeshProUGUI _buyButtonText;
		[SerializeField] private TextMeshProUGUI _closeButtonText;
		[SerializeField] private Image _skillDescriptionBackground;
		[SerializeField] private Button _closeButton;
		[SerializeField] private Button _buyButton;
		[SerializeField] private float _duration;

		private ProgressIconButton _talentButton;
		private IPersistentProgressService _progressService;
		public Button BuyButton
		{
			get
			{
				return _buyButton;
			}
			set
			{
				_buyButton = value;
			}
		}

		private void Awake()
		{
			_progressService = AllServices.Container.Single<IPersistentProgressService>();
			_closeButton.onClick.AddListener(Disappear);
			BuyButton.onClick.AddListener(BuyTalent);
			Disappear();
		}

		private void BuyTalent()
		{
			if (_progressService.Progress.WorldData.LootData.CollectedGold < _talentButton.Price || _talentButton.Level >= _talentButton.MaxLevel)
				return;

			if (_talentButton.Level < 5)
				_talentButton.Level++;
			
			_talentButton.UpdateTalent();
		}

		public void Appear(ProgressIconButton talentButton)
		{
			gameObject.SetActive(true);
			_talentButton = talentButton;
			_skillDescription.DOFade(1, _duration);
			_skillDescriptionBackground.DOFade(1, _duration);
			_closeButton.targetGraphic.DOFade(1, _duration);
			BuyButton.targetGraphic.DOFade(1, _duration);
			BuyButton.enabled = true;
			_buyButtonText.DOFade(1, _duration);
			_closeButtonText.DOFade(1, _duration);
		}

		private void Disappear()
		{
			BuyButton.enabled = false;
			_skillDescription.DOFade(0, _duration);
			_skillDescriptionBackground.DOFade(0, _duration);
			_closeButton.targetGraphic.DOFade(0, _duration);
			BuyButton.targetGraphic.DOFade(0, _duration);
			_buyButtonText.DOFade(0, _duration);
			_closeButtonText.DOFade(0, _duration).OnComplete(() =>
			{
				gameObject.SetActive(false);
			});
		}

		public void SetDescription(string description) =>
			_skillDescription.text = description;
	}
}
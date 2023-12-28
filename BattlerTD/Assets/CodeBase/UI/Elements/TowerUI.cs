using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Inputs;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Tower;
using SimpleInputNamespace;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
	public class TowerUI : MonoBehaviour
	{
		[SerializeField] private TowerType _towerType;
		[SerializeField] private int _towerCost;
		[SerializeField] private ButtonInputUI _button;
		[SerializeField] private TextMeshProUGUI _costText;

		private IBuildingService _buildingService;
		private IInputService _inputService;
		private IPersistentProgressService _progressService;
		public TowerType TowerType
		{
			get
			{
				return _towerType;
			}
		}
		public int TowerCost
		{
			get
			{
				return _towerCost;
			}
			set
			{
				_costText.text = value.ToString();
				_towerCost = value;
			}
		}

		private void Awake()
		{
			_buildingService = AllServices.Container.Single<IBuildingService>();
			_inputService = AllServices.Container.Single<IInputService>();
			_progressService = AllServices.Container.Single<IPersistentProgressService>();
			_inputService.TowerButtonPressed += InitTower;
			
		}

		private void OnDestroy()
		{
			_inputService.TowerButtonPressed -= InitTower;
		}

		private void InitTower()
		{
			if (_button.button.value && _progressService.Progress.WorldData.LootData.CollectedSilver >= TowerCost)
				_buildingService.InitTowerType(TowerType, _towerCost);
		}
	}
}
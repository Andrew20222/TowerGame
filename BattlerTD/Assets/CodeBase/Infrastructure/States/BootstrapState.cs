using CodeBase.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Audio;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Inputs;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.Tower;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
	public class BootstrapState : IState
	{
		private const string Initial = "Initial";
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly AllServices _services;
		private readonly AudioSource _audioSource;

		public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services, AudioSource audioSource)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_services = services;
			_audioSource = audioSource;
			RegisterServices();
		}

		public void Enter() =>
			_sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);

		public void Exit() { }
		public void Update() { }


		private void RegisterServices()
		{
			RegisterStaticDataService();

			_services.RegisterSingle<IAudioService>(new AudioService(_services.Single<IStaticDataService>(), _audioSource));
			_services.RegisterSingle<IGameStateMachine>(_stateMachine);
			_services.RegisterSingle<IAssetProvider>(new AssetProvider());
			_services.RegisterSingle(InputService());
			_services.RegisterSingle<IRandomService>(new RandomService());
			_services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());

			_services.RegisterSingle<ITimerService>(new TimerService());
			_services.RegisterSingle<IUIFactory>(new UIFactory(
				_services.Single<IAssetProvider>(),
				_services.Single<IStaticDataService>(),
				_services.Single<IPersistentProgressService>(),
				_services.Single<ITimerService>(),
				_stateMachine));

			_services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>(), _services.Single<ITimerService>()));

			_services.RegisterSingle<IGameFactory>(new GameFactory(
				_services.Single<IAssetProvider>(),
				_services.Single<IStaticDataService>(),
				_services.Single<IRandomService>(),
				_services.Single<IPersistentProgressService>(),
				_services.Single<IWindowService>(),
				_services.Single<ITimerService>(),
				_services.Single<IInputService>()));

			_services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
				_services.Single<IPersistentProgressService>(),
				_services.Single<IGameFactory>()));

			_services.RegisterSingle<IBuildingService>(new BuildingService(_services.Single<IInputService>(), _services.Single<IGameFactory>(),
				_services.Single<IStaticDataService>(), _services.Single<IPersistentProgressService>()));
		}


		private void RegisterStaticDataService()
		{
			IStaticDataService staticData = new StaticDataService();
			staticData.Load();
			_services.RegisterSingle(staticData);
		}

		private void EnterLoadLevel() =>
			_stateMachine.Enter<LoadProgressState>();

		private static IInputService InputService() =>
			Application.isEditor
				? new StandaloneInputService()
				: new MobileInputService();

	}
}
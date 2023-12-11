using CodeBase.Infrastructure.Services.Audio;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Infrastructure.States
{
	public class GameLoopBuildingState : IState
	{
		private readonly IGameFactory _factory;
		private readonly IPersistentProgressService _progressService;
		private readonly IUIFactory _uiFactory;
		private readonly IAudioService _audioService;

		public GameLoopBuildingState(IGameFactory factory, IPersistentProgressService progressService, IUIFactory uiFactory, IAudioService audioService)
		{
			_factory = factory;
			_progressService = progressService;
			_uiFactory = uiFactory;
			_audioService = audioService;
		}

		public void Exit() =>
			_factory.HUD.DisappearTowerPanel();

		public void Update()
		{
			
		}

		public void Enter()
		{
			_audioService.PlayMainMenuMusic();
			if (IsAllWaveDone())
				_uiFactory.CreateWinPanel();
			else
				_factory.HUD.AppearTowerPanel();
		}
		
		private bool IsAllWaveDone() =>
			_progressService.Progress.KillData.CurrentMonsterWaves >= _progressService.Progress.KillData.MonsterWavesForFinish;
	}
}
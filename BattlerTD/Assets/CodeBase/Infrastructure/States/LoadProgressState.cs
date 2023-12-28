using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.StaticData;

namespace CodeBase.Infrastructure.States
{
  
  
  public class LoadProgressState : IState
  {
    private const string InitialLevel = "Main";
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadProgress;
    private readonly IStaticDataService _staticDataService;

    public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadProgress, IStaticDataService staticDataService)
    {
      _gameStateMachine = gameStateMachine;
      _progressService = progressService;
      _saveLoadProgress = saveLoadProgress;
      _staticDataService = staticDataService;
    }

    public void Enter()
    {
      LoadProgressOrInitNew();

      _gameStateMachine.Enter<LoadLevelState, string>("LevelOne");
    }

    public void Exit()
    {
    }

    public void Update()
    {
      
    }

    private void LoadProgressOrInitNew()
    {
      _progressService.Progress = 
        _saveLoadProgress.LoadProgress() 
        ?? NewProgress();
    }

    private PlayerProgress NewProgress()
    {
      PlayerProgress progress =  new PlayerProgress(InitialLevel);

      HeroStaticData heroData = _staticDataService.ForHero();

      progress.HeroState.MaxHP = heroData.Hp;
      progress.HeroState.MaxHPMultiplier = 1;
      progress.HeroState.Regeneration = 0;
      progress.HeroStats.Vampiric = 0;
      progress.HeroStats.MaxPowerShot = 0;
      progress.HeroStats.MoveSpeed = heroData.MoveSpeed;
      progress.HeroStats.MoveSpeedMultiplier = 1;
      progress.HeroStats.SwordDamage = heroData.SwordDamage;
      progress.HeroStats.SwordDamageMultiplier = 1;
      progress.HeroStats.ArrowDamage = heroData.ArrowDamage;
      progress.HeroStats.ArrowDamageMultiplier = 1;
      progress.HeroStats.ArrowSpeed = heroData.ArrowSpeed;
      progress.HeroStats.SwordRadius = heroData.Cleavage;
      progress.HeroStats.DamageMultiplier = 1;
      progress.HeroStats.CriticalChance = 1;
      progress.HeroStats.CriticalMultiplier = 1;
      progress.HeroStats.MagicShieldChance = 1;
      progress.HeroState.ResetHp();
      progress.KingState.MaxHP = heroData.KingHp;
      progress.KingState.ResetHp();
      return progress;
    }
  }
}
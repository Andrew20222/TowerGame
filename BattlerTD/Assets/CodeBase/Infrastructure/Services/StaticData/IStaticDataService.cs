using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.Tower;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Infrastructure.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    MonsterStaticData ForMonster(MonsterTypeId typeId);
    LevelStaticData ForLevel(string sceneKey);
    SoundStaticData ForSounds();
    TowerStaticData ForTower(TowerType towerType);
    
    HeroStaticData ForHero();
    WindowConfig ForWindow(WindowId window);
    WindowConfig ForWinPanel(WindowId windowId);
    WindowConfig ForDeathPanel(WindowId windowId);
    WindowConfig ForMenu(WindowId windowId);
    TalentStaticData ForTalent();
  }
}
using System;
using CodeBase.UI.Elements;
using CodeBase.UI.Menu;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;

namespace CodeBase.StaticData.Windows
{
  [Serializable]
  public class WindowConfig
  {
    public WindowId WindowId;
    public MainMenu MenuPrefab;
    public WindowBase Template;
    public ScorePanel ScorePrefab;
    public DeathPanel DeathPrefab;
  }
}
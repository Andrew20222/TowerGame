using CodeBase.Infrastructure.Services;
using CodeBase.UI.Elements;
using CodeBase.UI.Menu;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public interface IUIFactory: IService
  {
    void CreateUIRoot();
    void CreateShop();
    Transform UiRoot { get; }
    void CreateWinPanel();
    void CreateDeathPanel();
    MainMenu CreateMainMenu();
  }
}
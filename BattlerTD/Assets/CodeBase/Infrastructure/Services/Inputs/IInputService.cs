using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Inputs
{
  public interface IInputService : IService, IDisposable
  {
    event Action TowerButtonPressed;
    event Action TowerButtonUnpressed;
    Vector2 MovingAxis { get; }
    Touch Touch { get; set; }
    bool Tap { get; set; }
    bool IsDragging { get; }
    bool SwipeLeft{ get; set; } 
    bool SwipeRight { get; set; }
    bool SwipeUp { get; set; } 
    bool SwipeDown { get; set; }
    Vector2 RotatingAxis { get; }
    void Update();
    bool IsAttackButtonUp();
    bool IsAttackButton();
    event Action AttackButtonUnpressed;
    bool IsAttackButtonDown();
    bool IsTalentButton();
    bool IsTalentButtonUp();
  }
}
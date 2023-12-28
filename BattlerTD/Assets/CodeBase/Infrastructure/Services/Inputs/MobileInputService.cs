using UnityEngine;

namespace CodeBase.Infrastructure.Services.Inputs
{
  public class MobileInputService : InputService
  {
    public override Vector2 MovingAxis => SimpleInputMovingAxis();
    public override Vector2 RotatingAxis => SimpleInputRotatingAxis();
  }
}
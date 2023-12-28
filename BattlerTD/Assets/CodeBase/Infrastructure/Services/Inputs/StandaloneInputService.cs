using UnityEngine;

namespace CodeBase.Infrastructure.Services.Inputs
{
  public class StandaloneInputService : InputService
  {
    public override Vector2 MovingAxis
    {
      get
      {
        Vector2 axis = SimpleInputMovingAxis();

        if (axis == Vector2.zero)
        {
          axis = UnityMovingAxis();
        }

        return axis;
      }
    }
    public override Vector2 RotatingAxis
    {
      get
      {
        Vector2 axis = SimpleInputRotatingAxis();

        if (axis == Vector2.zero)
        {
          axis = UnityRotatingAxis();
        }

        return axis;
      }
    }

    private static Vector2 UnityMovingAxis() =>
      new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical)); 
    
    private static Vector2 UnityRotatingAxis() =>
      new Vector2(Input.GetAxis(Horizontal2), Input.GetAxis(Vertical2));
  }
}
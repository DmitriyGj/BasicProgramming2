  using System;
 
namespace func_rocket
{
    public class ControlTask
    {
        public static double TotalAngle;

        public static Turn ControlRocket(Rocket rocket, Vector target)
        {
            TotalAngle = FindScalarMultiply(rocket.Velocity,target);
            while(TotalAngle!=0)
            {
                if (TotalAngle < 0)
                    return Turn.Right;
                else if (TotalAngle > 0)
                    return Turn.Left;
                else
                    return Turn.None;
            }
            return Turn.None;
        }
        public static double FindScalarMultiply(Vector a, Vector b)
        {
            return a.X * b.X + a.Y * b.Y / (Math.Sqrt(a.X * a.X + a.Y * a.Y) * Math.Sqrt(b.X * b.X + b.Y * b.Y));
        }
    }
}

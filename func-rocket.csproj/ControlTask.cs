using System;

namespace func_rocket
{
    public class ControlTask
    { 
        public static Turn ControlRocket(Rocket rocket, Vector target)
        {
            var distanceVector = target - rocket.Location;
            var flagAngle = 0.0;
            if (Math.Abs(distanceVector.Angle - rocket.Direction) < 0.5 ||
                 Math.Abs(distanceVector.Angle - rocket.Velocity.Angle) < 0.5)
                flagAngle = distanceVector.Angle - (rocket.Direction + rocket.Velocity.Angle) / 2;
            else
                flagAngle = distanceVector.Angle - rocket.Direction;
            if (flagAngle < 0)
                return Turn.Left;
            return flagAngle > 0 ? Turn.Right : Turn.None;
        }
    }
}

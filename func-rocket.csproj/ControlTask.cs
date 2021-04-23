using System;

namespace func_rocket
{
<<<<<<< HEAD
    { 
        public static Turn ControlRocket(Rocket rocket, Vector target)
        {
=======
	public class ControlTask
	{
		{
>>>>>>> 5173306aa9f366f02a683126b606f654d1fa88d6
            var distanceVector = target - rocket.Location;
            var flagAngle = 0.0;
            if (Math.Abs(distanceVector.Angle - rocket.Direction) < 0.5 ||
                 Math.Abs(distanceVector.Angle - rocket.Velocity.Angle) < 0.5)
<<<<<<< HEAD
                flagAngle = distanceVector.Angle - (rocket.Direction + rocket.Velocity.Angle) / 2;
=======
                flagAngle = distanceVector.Angle - (rocket.Direction + rocket.Velocity.Angle)/2;
>>>>>>> 5173306aa9f366f02a683126b606f654d1fa88d6
            else
                flagAngle = distanceVector.Angle - rocket.Direction;
            if (flagAngle < 0)
                return Turn.Left;
<<<<<<< HEAD
            return flagAngle > 0 ? Turn.Right : Turn.None;
        }
    }
}
=======
            return flagAngle> 0 ? Turn.Right : Turn.None;
        }
	}
}
>>>>>>> 5173306aa9f366f02a683126b606f654d1fa88d6

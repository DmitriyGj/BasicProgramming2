using System;
using System.Collections.Generic;

namespace func_rocket
{
	public class LevelsTask
	{
		static readonly Physics standardPhysics = new Physics();
		static readonly Vector standardTarget = new Vector(600, 200);
		static readonly Rocket standardRocket = new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);
	    public static Func<string,Vector, Gravity, Level> CreateLevel =  (name,targetDisp,gravity) => 
			     new Level(name,standardRocket,standardTarget + targetDisp, gravity,standardPhysics);

		static Func<Vector, Vector> calculateWhiteHole = (v) =>
		{
			var wayToTarget = v - standardTarget;
			return wayToTarget.Normalize() * 140 * wayToTarget.Length / (wayToTarget.Length * wayToTarget.Length + 1);
		};

		static Func<Vector, Vector> calculateBlackHole = (v) =>
		{
			   var wayToTarget = (standardTarget + standardRocket.Location) / 2;
			   var distanceToAnomaly = (wayToTarget - v).Length;
			   return (wayToTarget - v).Normalize() * 300 * distanceToAnomaly / (distanceToAnomaly * distanceToAnomaly + 1);
		};

		public static IEnumerable<Level> CreateLevels()
		{
			yield return CreateLevel("Zero",new Vector(0,0), (size, v) => Vector.Zero);
			yield return CreateLevel("Heavy",new Vector(0,0), (size, v) => new Vector(0, 0.9));
			yield return CreateLevel("Up",new Vector(100,300),(size,v)=> new Vector(0,-300/(size.Height -v.Y +300)));
			yield return CreateLevel("WhiteHole",new Vector(0, 0), (size, v) => calculateWhiteHole(v));
			yield return CreateLevel("BlackHole", new Vector(0, 0),(size, v) => calculateBlackHole(v));
			yield return CreateLevel("BlackAndWhite",new Vector(0,0),(size,v)=> (calculateWhiteHole(v)+calculateBlackHole(v))/2
				);
		}
	}
}
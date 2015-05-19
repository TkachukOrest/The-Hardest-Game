using System;

namespace TheHardestGame.GameEngine.AdditionalClasses
{
    public class EnginePoint
	{
		#region Fields

		public double X { get; set; }
	    public double Y { get; set; }

		#endregion

		#region Constructors

		public EnginePoint(double x, double y)
	    {
		    this.X = x;
		    this.Y = y;
	    }

	    public EnginePoint(EnginePoint ep)
	    {
		    this.X = ep.X;
		    this.Y = ep.Y;
	    }

		#endregion

		#region Methods

		public double GetDistance(Double X2, double Y2)
		{
			return Math.Sqrt(Math.Pow(this.X - X2, 2) + Math.Pow(this.Y - Y2, 2));
		}

		public double GetDistance(EnginePoint ep2)
		{
			return Math.Sqrt(Math.Pow(this.X - ep2.X, 2) + Math.Pow(this.Y - ep2.Y, 2));
		}

		#endregion
	}
}

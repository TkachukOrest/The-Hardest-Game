using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TheHardestGame.GameEngine.AdditionalClasses;

namespace TheHardestGame.GameEngine.Test.AdditionalClasses
{
	[TestClass]
	public class EnginePointTest
	{
		#region Constructor Tests

		[TestMethod]
		public void ConstructorTest()
		{
			EnginePoint ep = new EnginePoint(5.1, 3.7);

			Assert.AreEqual(5.1, ep.X);
			Assert.AreEqual(3.7, ep.Y);
		}

		[TestMethod]
		public void CopyConstructorTest()
		{
			EnginePoint ep = new EnginePoint(1.5, 2.3);
			EnginePoint ep2 = new EnginePoint(ep);

			Assert.AreEqual(ep.X, ep2.X);
			Assert.AreEqual(ep.Y, ep2.Y);
		}

		#endregion

		#region Method Tests

		[TestMethod]
		public void GetDistanceTest()
		{
			EnginePoint ep = new EnginePoint(1.0, 2.0);
			double distance = ep.GetDistance(3.0, 5.0);

			Assert.AreEqual(Math.Sqrt(13),distance);
		}

		[TestMethod]
		public void GetDistance2Test()
		{
			EnginePoint ep = new EnginePoint(1.0, 2.0);
			EnginePoint ep2 = new EnginePoint(3.0, 5.0);
			double distance = ep.GetDistance(ep2);

			Assert.AreEqual(Math.Sqrt(13), distance);
		}

		#endregion
	}
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TheHardestGame.GameEngine.AdditionalClasses;
using TheHardestGame.GameEngine.Enums;

namespace TheHardestGame.GameEngine.Test.AdditionalClasses
{
	[TestClass]
	public class EngineRectangleTest
	{
		#region Constructor Tests

		[TestMethod]
		public void ConstructorTest()
		{
			EngineRectangle er = new EngineRectangle(1.5, 4.2, 5.0, 3.3);

			Assert.AreEqual(1.5, er.X);
			Assert.AreEqual(4.2,er.Y);
			Assert.AreEqual(5.0,er.Height);
			Assert.AreEqual(3.3,er.Width);
			Assert.AreEqual(Directions.Wrong, er.Direction);
			Assert.AreEqual(false, er.IsBarrier);
			
		}

		[TestMethod]
		public void Constructor2Test()
		{
			EnginePoint ep = new EnginePoint(1.5, 4.2);
			EngineSize es = new EngineSize(5.0,3.3);

			EngineRectangle er = new EngineRectangle(ep,es);

			Assert.AreEqual(ep.X, er.X);
			Assert.AreEqual(ep.Y, er.Y);
			Assert.AreEqual(es.Height, er.Height);
			Assert.AreEqual(es.Width, er.Width);
			Assert.AreEqual(Directions.Wrong, er.Direction);
			Assert.AreEqual(false, er.IsBarrier);
			
		}

		[TestMethod]
		public void CopyConstructorTest()
		{
			EngineRectangle er = new EngineRectangle(1.5, 4.2, 5.0, 3.3);
			EngineRectangle er2 = new EngineRectangle(er);

			Assert.AreEqual(er.X, er2.X);
			Assert.AreEqual(er.Y, er2.Y);
			Assert.AreEqual(er.Height, er2.Height);
			Assert.AreEqual(er.Width, er2.Width);
			Assert.AreEqual(er.IsBarrier, er2.IsBarrier);
			Assert.AreEqual(er.Direction, er2.Direction);
		}

		#endregion

		#region Methods Tests

		[TestMethod]
		public void MoveUpTest()
		{
			EngineRectangle er = new EngineRectangle(1.5, 4.2, 5.0, 3.3, Directions.Up);

			er.Move(1.5);
		
			Assert.AreEqual(2.7, er.Y);
		}

		[TestMethod]
		public void MoveDownTest()
		{
			EngineRectangle er = new EngineRectangle(1.5, 4.2, 5.0, 3.3, Directions.Down);

			er.Move(1.5);

			Assert.AreEqual(5.7, er.Y);
		}

		[TestMethod]
		public void MoveLeftTest()
		{
			EngineRectangle er = new EngineRectangle(1.5, 4.2, 5.0, 3.3, Directions.Left);

			er.Move(1.5);

			Assert.AreEqual(0.0, er.X);
		}

		[TestMethod]
		public void MoveRightTest()
		{
			EngineRectangle er = new EngineRectangle(1.5, 4.2, 5.0, 3.3, Directions.Right);

			er.Move(1.5);

			Assert.AreEqual(3.0, er.X);
		}

		#endregion

		#region Property Tests

		[TestMethod]
		public void HeightTest()
		{
			EngineRectangle er = new EngineRectangle(1.5, 4.2, 5.0, 3.3);
			er.Height = 3.5;

			Assert.AreEqual(3.5, er.Height);
		}


		[TestMethod]
		public void WidthTest()
		{
			EngineRectangle er = new EngineRectangle(1.5, 4.2, 5.0, 3.3);
			er.Width = 2.3;

			Assert.AreEqual(2.3, er.Width);
		}

		#endregion
	}	
}

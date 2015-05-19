using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheHardestGame.GameEngine.AdditionalClasses;
using TheHardestGame.GameEngine.Enums;
using TheHardestGame.GameEngine.Game;

namespace TheHardestGame.GameEngine.Test.Game
{
	[TestClass]
	public class EngineLogicTest
	{
		#region Constructor Tests

		[TestMethod]
		public void ConstructorTest()
		{
			EngineGame game = new EngineGame(15000.0);

			Assert.AreEqual(15000.0, game.Width);
			Assert.AreEqual(game.Width/20*6, game.Height);
			Assert.AreEqual(game.Width/20, game.RectWidth);
			Assert.AreEqual(game.Height/6, game.RectHeight);
		}

		#endregion

		#region Method Tests

		[TestMethod]
		public void MoveRectangleUpTest()
		{
			EngineGame game = new EngineGame();
			game.Square.Direction = Directions.Up;

			bool moved = false;
			game.EventMove += ((sender, e) => { moved = true; });

			const double offset = 15.0;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue(moved);
		}

		[TestMethod]
		public void MoveRectangleUpOffsetTest()
		{
			EngineGame game = new EngineGame();
			game.Square.Direction = Directions.Up;

			bool moved = false;
			game.EventMove += ((sender, e) => { moved = true; });

			const double offset = 15000.0;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue(moved);
		}

		[TestMethod]
		public void MoveRectangleUpChangeOffsetTest()
		{
			EngineGame game = new EngineGame();
			game.Square.X = game.RectWidth*4;
			game.Square.Y = game.RectHeight*5 + 50;
			game.Square.Direction = Directions.Up;

			bool moved = false;
			game.EventMove += ((sender, e) => { moved = true; });

			const double offset = 1000.0;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue(moved);
		}

		[TestMethod]
		public void MoveRectangleUpChangeOffsetFalseTest()
		{
			EngineGame game = new EngineGame();
			game.Square.X = game.RectWidth*8;
			game.Square.Y = game.RectHeight;
			game.Square.Direction = Directions.Up;

			const double offset = 10.0;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue(game.Square.LastOffset != offset);
		}

		[TestMethod]
		public void MoveRectangleDownTest()
		{
			EngineGame game = new EngineGame();
			game.Square.Direction = Directions.Down;

			bool moved = false;
			game.EventMove += ((sender, e) => { moved = true; });

			const double offset = 15.0;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue(moved);
		}

		[TestMethod]
		public void MoveRectangleDownOffsetTest()
		{
			EngineGame game = new EngineGame();
			game.Square.Direction = Directions.Down;

			bool moved = false;
			game.EventMove += ((sender, e) => { moved = true; });

			const double offset = 15000.0;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue(moved);
		}

		[TestMethod]
		public void MoveRectangleDownChangeOffsetTest()
		{
			EngineGame game = new EngineGame();
			game.Square.X = game.RectWidth*15;
			game.Square.Y = 0;
			game.Square.Direction = Directions.Down;

			bool moved = false;
			game.EventMove += ((sender, e) => { moved = true; });

			const double offset = 10000.0;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue(moved);
		}

		[TestMethod]
		public void MoveRectangleDownChangeOffsetFalseTest()
		{
			EngineGame game = new EngineGame();
			game.Square.X = game.RectWidth*8;
			game.Square.Y = game.RectHeight*5 - game.Square.Height;
			game.Square.Direction = Directions.Down;

			const double offset = 10.0;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue(game.Square.LastOffset != offset);
		}

		[TestMethod]
		public void MoveRectangleLeftTest()
		{
			EngineGame game = new EngineGame();
			game.Square.Direction = Directions.Left;

			bool moved = false;
			game.EventMove += ((sender, e) => { moved = true; });

			const double offset = 15.0;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue(moved);
		}

		[TestMethod]
		public void MoveRectangleLeftOffsetTest()
		{
			EngineGame game = new EngineGame();
			game.Square.Direction = Directions.Left;

			bool moved = false;
			game.EventMove += ((sender, e) => { moved = true; });

			const double offset = 15000.0;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue(moved);
		}

		[TestMethod]
		public void MoveRectangleLeftChangeOffsetTest()
		{
			EngineGame game = new EngineGame();
			game.Square.X = game.RectWidth*5 + 100;
			game.Square.Y = game.RectHeight*2;
			game.Square.Direction = Directions.Left;

			bool moved = false;
			game.EventMove += ((sender, e) => { moved = true; });

			const double offset = 500.0;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue(moved);
		}

		[TestMethod]
		public void MoveRectangleRightTest()
		{
			EngineGame game = new EngineGame();
			game.Square.Direction = Directions.Right;

			bool finished = false;

			game.EventFinish += ((sender, e) => { finished = e; });

			bool moved = false;
			game.EventMove += ((sender, e) => { moved = true; });

			const double offset = 15.0;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue(moved);
			Assert.IsFalse(finished);
		}

		[TestMethod]
		public void MoveRectangleRightOffsetTest()
		{
			EngineGame game = new EngineGame();
			game.Square.Direction = Directions.Right;

			bool moved = false;
			game.EventMove += ((sender, e) => { moved = true; });

			const double offset = 15000.0;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue(moved);
		}

		[TestMethod]
		public void MoveRectangleRightChangeOffsetTest()
		{
			EngineGame game = new EngineGame();
			game.Square.X = game.RectWidth*14 + 100;
			game.Square.Y = game.RectHeight;
			game.Square.Direction = Directions.Right;

			bool moved = false;
			game.EventMove += ((sender, e) => { moved = true; });

			double offset = game.RectWidth;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue(moved);
		}

		[TestMethod]
		public void MoveRectangleOverlapTest()
		{
			EngineGame game = new EngineGame();
			game.Square.Location = new EnginePoint(game.RectWidth*4, 100);
			game.Square.Size = new EngineSize(100, 100);
			game.Square.Direction = Directions.Right;

			const double offset = 100.0;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue(game.Square.LastOffset < offset);
		}

		[TestMethod]
		public void MoveRectangleRightFinishTest()
		{
			EngineGame game = new EngineGame();
			game.Square.Direction = Directions.Right;

			bool finished = false;

			game.EventFinish += ((sender, e) => { finished = e; });

			bool moved = false;
			game.EventMove += ((sender, e) => { moved = true; });

			const double offset = 13000.0;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue(moved);
			Assert.IsTrue(finished);
		}

		[TestMethod]
		public void MoveBarrierCantLeft()
		{
			EngineGame game = new EngineGame();
			game.Barriers[0].Location = new EnginePoint(game.RectWidth*5, game.RectHeight);
			game.Barriers[0].Size = new EngineSize(game.RectHeight*0.5, game.RectWidth*5);
			game.Barriers[0].Direction = Directions.Left;

			bool moved = false;
			game.EventMove += ((sender, e) => { moved = true; });

			const double offset = 100.0;
			game.MoveRectangle(game.Barriers[0], offset);

			Assert.IsTrue(moved);
		}

		[TestMethod]
		public void MoveBarrierCantRight()
		{
			EngineGame game = new EngineGame();
			game.Barriers[0].Location = new EnginePoint(game.RectWidth*14 + game.RectWidth*0.5, game.RectHeight);
			game.Barriers[0].Size = new EngineSize(game.RectHeight*0.5, game.RectWidth*0.5);
			game.Barriers[0].Direction = Directions.Right;

			bool moved = false;
			game.EventMove += ((sender, e) => { moved = true; });

			const double offset = 100.0;
			game.MoveRectangle(game.Barriers[0], offset);

			Assert.IsTrue(moved);
		}

		[TestMethod]
		public void GetStartSquareTest()
		{
			EngineGame game = new EngineGame();

			Assert.IsFalse(game.Square == null);
			Assert.IsTrue(game.Square.X >= 0);
			Assert.IsTrue(game.Square.Y >= 0);
			Assert.IsTrue(game.Square.Height > 0);
			Assert.IsTrue(game.Square.Width > 0);
			Assert.IsTrue(game.Square.Direction == Directions.Wrong);
			Assert.IsTrue(game.Square.IsBarrier == false);
			Assert.IsTrue(game.Square.Height == game.RectHeight*0.8);
			Assert.IsTrue(game.Square.Width == game.RectHeight*0.8);
		}

		[TestMethod]
		public void GetStartBarriersTest()
		{
			EngineGame game = new EngineGame();

			Assert.IsTrue(game.Barriers.Length == 4);
		}

		[TestMethod]
		public void RestartTest()
		{
			EngineGame game = new EngineGame();
			game.Deaths = 5;
			game.Restart();
			Assert.IsTrue(game.Square.X == game.RectWidth*1.5);
			Assert.IsTrue(game.Square.Y == game.RectHeight*2.5);
			Assert.IsTrue(game.Barriers[0].X == game.RectWidth*5 + game.RectWidth*0.25);
			Assert.IsTrue(game.Barriers[0].Y == game.RectHeight + game.RectHeight*0.25);
			Assert.IsTrue(game.Deaths == 0);
		}

		#endregion

		#region Event Tests

		[TestMethod]
		public void IsDiedXTest()
		{
			EngineGame game = new EngineGame();
			game.Square.Location = new EnginePoint(game.Barriers[0].Location);
			game.Square.Direction = Directions.Right;

			EngineRectangle er = null;
			game.EventDie += ((sender, e) => { er = e; });

			const double offset = 0.0;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue((er.X - game.Square.X) > 100);
		}

		[TestMethod]
		public void IsDiedYTest()
		{
			EngineGame game = new EngineGame();
			game.Square.Location = new EnginePoint(game.Barriers[0].X + 1, game.Barriers[0].Y - 10);
			game.Square.Direction = Directions.Left;

			EngineRectangle er = null;
			game.EventDie += ((sender, e) => { er = e; });

			const double offset = 1.0;
			game.MoveRectangle(game.Square, offset);

			Assert.IsTrue((er.X - game.Square.X) > 100);
		}

		#endregion*/
	}
}
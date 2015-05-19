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

			Assert.AreEqual(15000.0,game.Width);
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
			EngineRectangle square = game.GetStartSquare();
			square.Direction = 0;

			bool finished = false;

			game.EventFinish += ((sender, e) =>
			{
				finished = e.Finish;
			});


			bool moved = game.MoveRectangle(ref square, 15.0);

			Assert.IsTrue(moved);
			Assert.IsFalse(finished);

		}

		[TestMethod]
		public void MoveRectangleCantUpTest()
		{
			EngineGame game = new EngineGame();
			EngineRectangle square = game.GetStartSquare();
			square.Direction = 0;

			bool moved = game.MoveRectangle(ref square, 15000);

			Assert.IsFalse(moved);
		}

		[TestMethod]
		public void MoveRectangleDownTest()
		{
			EngineGame game = new EngineGame();
			EngineRectangle square = game.GetStartSquare();
			square.Direction = Directions.Down;

			bool finished = false;

			game.EventFinish += ((sender, e) =>
			{
				finished = e.Finish;
			});


			bool moved = game.MoveRectangle(ref square, 15.0);

			Assert.IsTrue(moved);
			Assert.IsFalse(finished);

		}

		[TestMethod]
		public void MoveRectangleCantDownTest()
		{
			EngineGame game = new EngineGame();
			EngineRectangle square = game.GetStartSquare();
			square.Direction = Directions.Down;
			
			bool moved = game.MoveRectangle(ref square, 15000);

			Assert.IsFalse(moved);
		}

		[TestMethod]
		public void MoveRectangleLeftTest()
		{
			EngineGame game = new EngineGame();
			EngineRectangle square = game.GetStartSquare();
			square.Direction = Directions.Left;

			bool finished = false;

			game.EventFinish += ((sender, e) =>
			{
				finished = e.Finish;
			});


			bool moved = game.MoveRectangle(ref square, 15.0);

			Assert.IsTrue(moved);
			Assert.IsFalse(finished);

		}

		[TestMethod]
		public void MoveRectangleCantLeftTest()
		{
			EngineGame game = new EngineGame();
			EngineRectangle square = game.GetStartSquare();
			square.Direction = Directions.Left;

			bool moved = game.MoveRectangle(ref square, game.RectWidth * 1.5 + 100);

			Assert.IsFalse(moved);
		}

		[TestMethod]
		public void MoveRectangleRightTest()
		{
			EngineGame game = new EngineGame();
			EngineRectangle square = game.GetStartSquare();
			square.Direction = Directions.Right;

			bool finished = false;

			game.EventFinish += ((sender, e) =>
			{
				finished = e.Finish;
			});


			bool moved = game.MoveRectangle(ref square, 15.0);

			Assert.IsTrue(moved);
			Assert.IsFalse(finished);

		}

		[TestMethod]
		public void MoveRectangleCantRightTest()
		{
			EngineGame game = new EngineGame();
			EngineRectangle square = game.GetStartSquare();
			square.Direction = Directions.Right;
		
			bool moved = game.MoveRectangle(ref square, game.Width);

			Assert.IsFalse(moved);
		}

		[TestMethod]
		public void MoveRectangleOverlapTest()
		{
			EngineGame game = new EngineGame();
			EngineRectangle square = new EngineRectangle(game.RectWidth * 4, 100, 100, 100);
			square.Direction = Directions.Right;


			bool moved = game.MoveRectangle(ref square, 100);

			Assert.IsFalse(moved);
		}

		[TestMethod]
		public void MoveRectangleRightFinishTest()
		{
			EngineGame game = new EngineGame();
			EngineRectangle square = game.GetStartSquare();
			square.Direction = Directions.Right;

			bool finished = false;

			game.EventFinish += ((sender, e) =>
			{
				finished = e.Finish;
			});


			bool moved = game.MoveRectangle(ref square, 8000.0);

			Assert.IsTrue(moved);
			Assert.IsTrue(finished);
		}

		[TestMethod]
		public void MoveBarrierCantLeft()
		{
			EngineGame game = new EngineGame();
			EngineRectangle square = new EngineRectangle(game.RectWidth * 5, game.RectHeight, game.RectHeight * 0.5, game.RectWidth * 5, Directions.Left, true);
			
			bool moved = game.MoveRectangle(ref square, 100.0);

			Assert.IsTrue(moved);
		}

		[TestMethod]
		public void MoveBarrierCantRight()
		{
			EngineGame game = new EngineGame();
			EngineRectangle square = new EngineRectangle(game.RectWidth * 14 + game.RectWidth * 0.5, game.RectHeight, game.RectHeight * 0.5, game.RectWidth * 0.5, Directions.Right, true);

			bool moved = game.MoveRectangle(ref square, 100.0);

			Assert.IsTrue(moved);
		}
		
		[TestMethod]
		public void GetStartSquareTest()
		{
			EngineGame game = new EngineGame();
			EngineRectangle square = game.GetStartSquare();
		
			Assert.IsFalse(square == null);
			Assert.IsTrue(square.X >=0);
			Assert.IsTrue(square.Y >= 0);
			Assert.IsTrue(square.Height > 0);
			Assert.IsTrue(square.Width > 0);
			Assert.IsTrue(square.Direction == Directions.Wrong);
			Assert.IsTrue(square.IsBarrier == false);
			Assert.IsTrue(square.Height == game.RectHeight * 0.8);
			Assert.IsTrue(square.Width == game.RectHeight * 0.8);
		}

		[TestMethod]
		public void GetStartBarriersTest()
		{
			EngineGame game = new EngineGame();
			EngineRectangle[] barriers = game.GetStartBarriers();

			Assert.IsTrue(barriers.Length == 4);
		}

		#endregion

		#region Event Tests

		[TestMethod]
		public void CheckDeathDieXTest()
		{
			EngineGame game = new EngineGame();
			EngineRectangle[] barriers = game.GetStartBarriers();
			EngineRectangle square = new EngineRectangle(barriers[0].Location, game.GetStartSquare().Size);
			
			bool die = false;
			game.EventDie += ((sender, e) =>
			{
				die = e.Die;
			});

			game.CheckDeath(square, barriers);

			Assert.IsTrue(die);
		}

		[TestMethod]
		public void CheckDeathDieYTest()
		{
			EngineGame game = new EngineGame();
			EngineRectangle[] barriers = game.GetStartBarriers();
			EngineRectangle square = new EngineRectangle(new EnginePoint(barriers[1].X - 1, barriers[1].Y - 10), new EngineSize(game.GetStartSquare().Height, barriers[1].Width * 0.5));

			bool die = false;
			game.EventDie += ((sender, e) =>
			{
				die = e.Die;
			});

			game.CheckDeath(square, barriers);

			Assert.IsTrue(die);
		}

		[TestMethod]
		public void CheckDeathDieY2Test()
		{
			EngineGame game = new EngineGame();
			EngineRectangle[] barriers = game.GetStartBarriers();
			EngineRectangle square = new EngineRectangle(new EnginePoint(barriers[1].X + 1, barriers[1].Y - 10), new EngineSize(game.GetStartSquare().Height, game.GetStartSquare().Width));

			bool die = false;
			game.EventDie += ((sender, e) =>
			{
				die = e.Die;
			});

			game.CheckDeath(square, barriers);

			Assert.IsTrue(die);
		}

		[TestMethod]
		public void CheckDeathNotDieTest()
		{
			EngineGame game = new EngineGame();
			EngineRectangle[] barriers = game.GetStartBarriers();
			EngineRectangle square = game.GetStartSquare();

			bool die = true;
			game.EventDie += ((sender, e) =>
			{
				die = e.Die;
			});

			game.CheckDeath(square, barriers);

			Assert.IsFalse(die);
		}
		#endregion
	}
}

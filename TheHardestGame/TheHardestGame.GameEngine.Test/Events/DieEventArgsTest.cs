using Microsoft.VisualStudio.TestTools.UnitTesting;

using TheHardestGame.GameEngine.Events;

namespace TheHardestGame.GameEngine.Test.Events
{
	[TestClass]
	public class DieEventArgsTest
	{
		#region Constructor Tests

		[TestMethod]
		public void ConstructorTest()
		{
			DieEventArgs e = new DieEventArgs(true);

			Assert.IsTrue(e.Die);
		}

		#endregion
	}
}

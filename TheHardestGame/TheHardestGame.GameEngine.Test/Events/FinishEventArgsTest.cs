using Microsoft.VisualStudio.TestTools.UnitTesting;

using TheHardestGame.GameEngine.Events;

namespace TheHardestGame.GameEngine.Test.Events
{
	[TestClass]
	public class FinishEventArgsTest
	{
		#region Constructor Tests

		[TestMethod]
		public void ConstructorTest()
		{
			FinishEventArgs e = new FinishEventArgs(false);

			Assert.IsFalse(e.Finish);
		}

		#endregion
	}
}

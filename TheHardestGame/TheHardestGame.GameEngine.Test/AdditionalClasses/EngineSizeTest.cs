using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheHardestGame.GameEngine.AdditionalClasses;

namespace TheHardestGame.GameEngine.Test.AdditionalClasses
{
	[TestClass]
	public class EngineSizeTest
	{
		#region Constructor Tests

		[TestMethod]
		public void ConstructorTest()
		{
			EngineSize es = new EngineSize(1.2,4.6);

			Assert.AreEqual(1.2, es.Height);
			Assert.AreEqual(4.6, es.Width);
		}

		[TestMethod]
		public void CopyConstructorTest()
		{
			EngineSize es = new EngineSize(1.2, 4.6);
			EngineSize es2 = new EngineSize(es);

			Assert.AreEqual(es.Height, es2.Height);
			Assert.AreEqual(es.Width, es2.Width);
		}

		#endregion
	}
}

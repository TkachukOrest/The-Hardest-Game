namespace TheHardestGame.GameEngine.AdditionalClasses
{
	public class EngineSize
	{
		#region Fields

		public double Height { get; set; }
		public double Width { get; set; }

		#endregion

		#region Constructors

		public EngineSize(double height, double width)
		{
			this.Height = height;
			this.Width = width;
		}

		public EngineSize(EngineSize es)
		{
			this.Height = es.Height;
			this.Width = es.Width;
		}

		#endregion
	}
}

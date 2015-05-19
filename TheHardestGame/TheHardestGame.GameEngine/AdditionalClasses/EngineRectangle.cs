using TheHardestGame.GameEngine.Enums;

namespace TheHardestGame.GameEngine.AdditionalClasses
{
	public class EngineRectangle
	{
		#region Fields

		public EnginePoint Location { get; set; }
		public EngineSize Size { get; set; }

		public double LastOffset { get; set; }
		public bool IsBarrier { get; private set; }
		public Directions Direction { get; set; }

		#endregion

		#region Constructors

		public EngineRectangle(EnginePoint location, EngineSize size, double lastOffset = 0,
			Directions direction = Directions.Wrong, bool isBarrier = false)
		{
			this.Location = location;
			this.Size = size;
			this.IsBarrier = isBarrier;
			this.Direction = direction;
			this.LastOffset = lastOffset;
		}

		public EngineRectangle(double x, double y, double height, double width, double lastOffset = 0,
			Directions direction = Directions.Wrong, bool isBarrier = false)
		{
			this.Location = new EnginePoint(x, y);
			this.Size = new EngineSize(height, width);
			this.IsBarrier = isBarrier;
			this.Direction = direction;
			this.LastOffset = lastOffset;
		}

		public EngineRectangle(EngineRectangle er)
		{
			this.Location = er.Location;
			this.Size = er.Size;
			this.IsBarrier = er.IsBarrier;
			this.Direction = er.Direction;
			this.LastOffset = er.LastOffset;
		}

		#endregion

		#region Methods

		public void Move(double offset)
		{
			switch (this.Direction)
			{
				case Directions.Up:
					this.Y -= offset;
					break;

				case Directions.Down:
					this.Y += offset;
					break;

				case Directions.Left:
					this.X -= offset;
					break;

				case Directions.Right:
					this.X += offset;
					break;
			}
		}

		#endregion

		#region Properties

		public double X
		{
			get { return this.Location.X; }
			set { this.Location.X = value; }
		}

		public double Y
		{
			get { return this.Location.Y; }
			set { this.Location.Y = value; }
		}

		public double Height
		{
			get { return this.Size.Height; }
			set { this.Size.Height = value; }
		}

		public double Width
		{
			get { return this.Size.Width; }
			set { this.Size.Width = value; }
		}

		#endregion
	}
}
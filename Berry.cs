using Godot;
using System;

namespace Snake;

public partial class Berry : Sprite2D
{
	private Random random = new Random();
	private float min = 0.2f;
	private float max = 0.8f;
	
	// Draw berry with random color
	public override void _Draw()
	{
		float red = (float)(random.NextDouble() * (max - min) + min);
		float green = (float)(random.NextDouble() * (max - min) + min);
		float blue = (float)(random.NextDouble() * (max - min) + min);
		DrawCircle(new Vector2(20,20), 15, new Color(red, green, blue));
	}
}

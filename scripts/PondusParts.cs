using Godot;
using System;
using System.Numerics;
using Vector2 = Godot.Vector2;

public partial class PondusParts : Node2D
{
	public Godot.Vector2 vectorToCursor;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		vectorToCursor = (GetGlobalMousePosition() - GlobalPosition).Normalized();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		vectorToCursor = (GetGlobalMousePosition() - GlobalPosition).Normalized();
		// We flip it if mouse position is to the left
		Scale = vectorToCursor.X < 0 ? new Vector2(-1 * Mathf.Abs(Scale.X), Scale.Y) : new Vector2(Mathf.Abs(Scale.X), Scale.Y);
	}
}

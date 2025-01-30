using Godot;
using System;
using System.Collections.ObjectModel;

public partial class PondusBody : Sprite2D
{
	[Export] private Godot.Collections.Array<Sprite2D> pondusPartsRotating;
	[Export] private Godot.Collections.Array<Sprite2D> pondusPartsStatic;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		foreach (var bp in pondusPartsRotating)
		{
			bp.FlipV = FlipH;
		}

		foreach (var bp in pondusPartsStatic)
		{
			bp.FlipH = FlipV;
		}
	}
}

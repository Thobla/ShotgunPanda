using Godot;
using System;

public partial class HostileHomer : CharacterBody2D
{
	// Called when the node enters the scene tree for the first time.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{// Add the gravity.
		Vector2 velocityAdd = Vector2.Zero;
		if (!IsOnFloor())
			velocityAdd.Y += gravity * (float)delta;

		Velocity += velocityAdd;
		MoveAndSlide();

	}
}

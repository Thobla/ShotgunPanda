using Godot;
using System;

public partial class HostileHomer : CharacterBody2D
{
	// Called when the node enters the scene tree for the first time.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private CharacterBody2D player;
	private float speed = 200;
	private float maxSpeed = 400;
	public override void _Ready()
	{
		player = GetTree().GetNodesInGroup("Player")[0] as CharacterBody2D;
	}

	public void chasePlayer(double delta, ref Vector2 addVelocity)
	{
		Vector2 target = player.GlobalPosition;
		Vector2 direction = (target - GlobalPosition).Normalized();

		if (Mathf.Abs(direction.X * speed * delta + Velocity.X) > maxSpeed)
		{
			addVelocity += new Vector2((float)Mathf.Max(0, (maxSpeed - Mathf.Abs(direction.X * speed + Velocity.X)) * delta), 0);
		}
		else
		{
			addVelocity += new Vector2((float)(direction.X * speed * delta), 0);
		}
	}

	private void applyGravity(double delta)
	{
		Vector2 velocityAdd = Vector2.Zero;
		if (!IsOnFloor())
			velocityAdd.Y += gravity * (float)delta;
		Velocity += velocityAdd;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{// Add the gravity.
		Vector2 addVelocity = Vector2.Zero;
		applyGravity(delta);
		chasePlayer(delta, ref addVelocity);

		Velocity += addVelocity;
		MoveAndSlide();
	}
}

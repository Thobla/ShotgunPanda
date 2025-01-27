using Godot;
using System;

public partial class PandaPondus : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public int JumpCharges = 0;
	private int JumpChargeLimit = 3;
	private const float MaxJumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();


	private void OnPedroRecieveShotSignal ()
	{
		GD.Print("Hello!!!!");	
	}
	public override void _Ready()
	{
	}

	private void ResetJumpCharges() {
		JumpCharges = JumpChargeLimit;
	}

	private void Jump(double delta, ref Vector2 velocity)
	{
		if (IsOnFloor())
		{
			ResetJumpCharges();
		}

		if (!Input.IsActionJustPressed("up") || JumpCharges <= 0) return;
		velocity.Y = Mathf.Max(JumpVelocity, MaxJumpVelocity);
		JumpCharges -= 1;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		Jump(delta, ref velocity);

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}

using Godot;
using System;

public partial class PandaPondus : CharacterBody2D
{
	private const float maxSpeed = 400.0f;
	private const float acceleration = 1500.0f;
	private const float friction = 600.0f;
	public const float JumpVelocity = -400.0f;
	public int JumpCharges = 0;
	private int JumpChargeLimit = 1;
	private const float MaxJumpVelocity = -550.0f;

	private Sprite2D Pedro;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private Vector2 input = Vector2.Zero;


	private void OnPedroRecieveShotSignal ()
	{
		GD.Print("Bang!!!!");	
		AddJumpCharges(1);
		ShootKnockback();
	}

	private void GetInput()
	{
		input = Input.GetVector("left", "right", "up", "down").Normalized();
	}

	private void Knockback(Vector2 force)
	{
		Velocity -= force;
	}

	private void ShootKnockback()
	{
		var pedroScript = Pedro as PedroMain;
		Knockback(pedroScript.TargetVector * pedroScript.Knockback);

	}
	public override void _Ready()
	{
		Pedro = GetNode<Sprite2D>("PondusParts/PangPangPedro");
	}

	private void ResetJumpCharges() {
		JumpCharges = JumpChargeLimit;
	}

	private void AddJumpCharges(int amount)
	{
		JumpCharges = Mathf.Min(JumpCharges + amount, JumpChargeLimit);
	}

	private void Jump(double delta, ref Vector2 velocity)
	{
		if (IsOnFloor())
		{
			ResetJumpCharges();
		}

		if (!Input.IsActionJustPressed("up") || JumpCharges <= 0) return;
		velocity.Y += (Velocity.Y + velocity.Y + JumpVelocity < MaxJumpVelocity) ? Mathf.Min(MaxJumpVelocity - Velocity.Y, 0) : JumpVelocity;
		if (Velocity.Y + velocity.Y < MaxJumpVelocity)
		{
			GD.Print("Velocity will be ", Velocity.Y + velocity.Y, " Which is too mucj");
		}
		JumpCharges -= 1;
	}

	private void MovementAdd(double delta, ref Vector2 velocity)
	{
		if (input.X == 0)
		{
			if (Mathf.Abs(Velocity.X) > (friction * delta))
			{
				velocity.X += (Velocity.X < 0) ? (friction * (float)delta) : -(friction * (float)delta);
			}
			else
			{
				velocity.X = 0;
			}
		}
		else
		{
			var acc = input.X * acceleration * (float) delta;
			if (input.X < 0)
			{
				velocity.X += ((Velocity.X + acc) > -maxSpeed) ? (acc) : 0;
			}
			else
			{
				velocity.X += ((Velocity.X + acc) < maxSpeed) ? (acc) : 0;
			}
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		var velocityAdd = Vector2.Zero;
		MovementAdd(delta, ref velocityAdd);

		// Add the gravity.
		if (!IsOnFloor())
			velocityAdd.Y += gravity * (float)delta;

		// Handle Jump.
		Jump(delta, ref velocityAdd);

		Velocity += velocityAdd;
		MoveAndSlide();
	}
	
}

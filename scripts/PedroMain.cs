using Godot;
using System;

public partial class PedroMain : Sprite2D
{
	private Node2D PedroNose;
	private Timer CooldownTimer;

	// Might not need Nose and Target Vectors 😢
	public Vector2 NoseVectorRelative { get; private set;  } = Vector2.Zero;
	public Vector2 TargetVector { get; private set; } = Vector2.Zero;

	public float Knockback { get; private set; } = 300.0f;


	[Signal]
	public delegate void ShootSignalEventHandler();
	private void Shoot()
	{
		if (Input.IsActionJustPressed("cursor_left") && CooldownTimer.IsStopped())
		{
			EmitSignal(SignalName.ShootSignal);
		}
	}


	private void UpdateVectors()
	{
		NoseVectorRelative = PedroNose.Position;
		NoseVectorRelative = NoseVectorRelative.Normalized();
		TargetVector = GetGlobalMousePosition() - GlobalPosition;
		TargetVector = TargetVector.Normalized();
	}

	private void PedroSpin()
	{
		//float AngleSum = NoseVectorRelative.AngleTo(Vector2.Right) + Vector2.Right.AngleTo(TargetVector);
		float AngleSum = Vector2.Right.AngleTo(TargetVector);
		Rotation = AngleSum;
	}
	public override void _Ready()
	{
		PedroNose = (Node2D) GetNode("PedroNose");
		CooldownTimer = (Timer) GetNode("CooldownTimer");

		ShootSignal += () => GD.Print("Shooting");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdateVectors();
		PedroSpin();
		Shoot();
	}
}

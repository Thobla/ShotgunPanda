using Godot;
using System;

public partial class PedroMain : Sprite2D
{
	private Node2D PedroNose;

	private CharacterBody2D Pondus;
	// Might not need Nose and Target Vectors 😢
	private Vector2 NoseVectorRelative = Vector2.Zero;
	private Vector2 TargetVector = Vector2.Zero;


	[Signal]
	public delegate void ShootSignalEventHandler();
	private void Shoot()
	{
		EmitSignal(SignalName.ShootSignal);
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
		//float dot = NoseVector.Dot(TargetVector);
		//GD.Print(dot);
		//Rotation = dot;
		//Rotation = GetGlobalMousePosition().AngleToPoint(GlobalPosition);
		float AngleSum = NoseVectorRelative.AngleTo(Vector2.Right) + Vector2.Right.AngleTo(TargetVector);
		Rotation = AngleSum;
		//LookAt(TargetVector + GlobalPosition);
	}
	public override void _Ready()
	{
		PedroNose = (Node2D) GetNode("PedroNose");
		Pondus = (CharacterBody2D)GetNode("..");

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

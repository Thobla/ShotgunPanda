using Godot;
using System;

public partial class PedroMain : Sprite2D
{
	private Node2D PedroNose;
	// Might not need Nose and Target Vectors 😢
	private Vector2 NoseVector = Vector2.Zero;
	private Vector2 TargetVector = Vector2.Zero;
	// Called when the node enters the scene tree for the first time.


	private void UpdateVectors()
	{
		NoseVector = PedroNose.GlobalPosition - GlobalPosition;
		NoseVector = NoseVector.Normalized();
		TargetVector = GetGlobalMousePosition() - GlobalPosition;
		TargetVector = TargetVector.Normalized();
	}

	private void PedroSpin()
	{
		//float dot = NoseVector.Dot(TargetVector);
		//GD.Print(dot);
		//Rotation = dot;
		//Rotation = GetGlobalMousePosition().AngleToPoint(GlobalPosition);
		LookAt(TargetVector + GlobalPosition);
	}
	public override void _Ready()
	{
		PedroNose = (Node2D) GetNode("PedroNose");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdateVectors();
		PedroSpin();
	}
}

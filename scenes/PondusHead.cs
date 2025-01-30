using Godot;
using System;

public partial class PondusHead : Sprite2D
{
	private Sprite2D pondusBody;
	
	private void PedroSpin()
    	{
    		//float AngleSum = NoseVectorRelative.AngleTo(Vector2.Right) + Vector2.Right.AngleTo(TargetVector);
		    Vector2 targetVector = (GetGlobalMousePosition() - GlobalPosition).Normalized();
    		float AngleSum = Vector2.Right.AngleTo(targetVector);

		    GD.Print("Angle Sum: ", AngleSum);
		    pondusBody.FlipH = Mathf.Abs(AngleSum) > Mathf.Pi / 2;
    		Rotation = AngleSum;
    	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		pondusBody = (Sprite2D) GetParent();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		PedroSpin();
	}
}

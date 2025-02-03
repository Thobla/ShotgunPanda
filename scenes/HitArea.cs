using Godot;
using System;

public partial class HitArea : Area2D
{
	public void OnPedroRecieveShotSignal()
	{
		foreach (IHitable hitableArea in GetTree().GetNodesInGroup("HitableArea"))
		{
			foreach (Area2D area in GetOverlappingAreas())
			{
				if (area.Equals(hitableArea))
				{
					GD.Print("Area found");
					hitableArea.Hit(1);		
				}
			}
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

using Godot;
using System;

public partial class CooldownTimer : Timer
{
	[Export] private float BaseCooldown = 1.0f;


	public void OnPedroRecieveShotSignal()
	{
		
		Start(Mathf.Max(BaseCooldown, TimeLeft));
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

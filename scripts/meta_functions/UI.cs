using Godot;
using System;

public partial class UI : CanvasLayer
{
	private CustomAlerts CustomAlerts;
	public ProgressBar lives;
	public ProgressBar health;
	public override void _Ready()
	{
		CustomAlerts = GetNode<CustomAlerts>("/root/CustomAlerts");
		lives = GetNode<ProgressBar>("Control/Lives");
		health = GetNode<ProgressBar>("Control/Health");
		lives.Value = 3;
		health.Value = 3;
		CustomAlerts.LivesChange += LivesBarChange;
		CustomAlerts.HPChange += HealthBarChange;
	}

	private void HealthBarChange(int new_hp) {
		health.Value = new_hp;
	}

	private void LivesBarChange(int new_lives) {
		lives.Value = new_lives;
	}
}

using Godot;
using System;

public partial class UI : CanvasLayer
{
	private CustomAlerts CustomAlerts;
	public ProgressBar lives;
	public ProgressBar health;
	public Control deathMenu;
	public Control pauseMenu;

	public override void _Ready()
	{
		CustomAlerts = GetNode<CustomAlerts>("/root/CustomAlerts");
		lives = GetNode<ProgressBar>("UIControl/Lives");
		health = GetNode<ProgressBar>("UIControl/Health");
		deathMenu = GetNode<Control>("UIControl/DeathMenu");
		pauseMenu = GetNode<Control>("UIControl/PauseMenu");
		lives.Value = 3;
		health.Value = 3;
		CustomAlerts.LivesChange += LivesBarChange;
		CustomAlerts.HPChange += HealthBarChange;
	}

    public override void _UnhandledKeyInput(InputEvent @event)
    {
		if (@event.IsAction("pause")) {
			GetTree().Paused = true;
			pauseMenu.Show();
		}
	}

    public void _on_give_up() {
		GetTree().Paused = false;
		string menu_scene = "res://scenes/menus/bountyBoard.tscn";
		GetTree().ChangeSceneToFile(menu_scene);
	}

	private void HealthBarChange(int new_hp) {
		health.Value = new_hp;
	}

	private void LivesBarChange(int new_lives) {
		lives.Value = new_lives;
		deathMenu.Show();
	}
}

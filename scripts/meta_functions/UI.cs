using Godot;
using System;

public partial class UI : CanvasLayer
{
	private CustomAlerts CustomAlerts;
	private PlayerStats PlayerStats;
	public ProgressBar lives;
	public ProgressBar health;
	public Control deathMenu;
	public Control pauseMenu;
	public Control deadEndMenu;
	public bool UIOpen = false;

	public override void _Ready()
	{
		PlayerStats = GetNode<PlayerStats>("/root/contract1/PlayerStats");
		CustomAlerts = GetNode<CustomAlerts>("/root/contract1/CustomAlerts");
		lives = GetNode<ProgressBar>("/root/contract1/UI/UIControl/Lives");
		health = GetNode<ProgressBar>("/root/contract1/UI/UIControl/Health");
		deathMenu = GetNode<Control>("/root/contract1/UI/UIControl/DeathMenu");
		pauseMenu = GetNode<Control>("/root/contract1/UI/UIControl/PauseMenu");
		deadEndMenu = GetNode<Control>("/root/contract1/UI/UIControl/DeadEndMenu");
		lives.Value = 3;
		health.Value = 3;
		CustomAlerts.LivesChange += LivesBarChange;
		CustomAlerts.HPChange += HealthBarChange;
	}

    public override void _UnhandledKeyInput(InputEvent @event)
    {
		if (@event.IsAction("pause") && !UIOpen) {
			UIOpen = true;
			GetTree().Paused = true;
			pauseMenu.Show();
		}
	}

	public void _on_continue() {
		GetTree().Paused = false;
		UIOpen = false;
		pauseMenu.Hide();
	}

    public void _on_give_up() {
		GetTree().Paused = false;
		UIOpen = false;
		string menu_scene = "res://scenes/menus/bountyBoard.tscn";
		GetTree().ChangeSceneToFile(menu_scene);
	}

	public void _on_retry() {
		UIOpen = false;
		GetTree().ChangeSceneToFile("res://scenes/levels/contract1.tscn");
	}

	private void HealthBarChange(int new_hp) {
		health.Value = new_hp;
	}

	private void LivesBarChange(int new_lives, bool end) {
		lives.Value = new_lives;
		UIOpen = true;
		if (end) {
			deadEndMenu.Show();
		} else {
			deathMenu.Show();
		}
	}
}

using Godot;
using System;

public partial class UI : CanvasLayer
{
	private CustomAlerts CustomAlerts;
	private PlayerStats PlayerStats;
	public Control lifeBar;
	public Control healthBar;
	public int lives;
	public int health;
	public Control deathMenu;
	public Control pauseMenu;
	public Control deadEndMenu;
	public bool UIOpen = false;

	public override void _Ready()
	{
		PlayerStats = GetNode<PlayerStats>("/root/contract1/PlayerStats");
		CustomAlerts = GetNode<CustomAlerts>("/root/contract1/CustomAlerts");
		healthBar = GetNode<Control>("/root/contract1/UI/UIControl/LivingStatus");
		lifeBar = GetNode<Control>("/root/contract1/UI/UIControl/DeadStatus");
		deathMenu = GetNode<Control>("/root/contract1/UI/UIControl/DeathMenu");
		pauseMenu = GetNode<Control>("/root/contract1/UI/UIControl/PauseMenu");
		deadEndMenu = GetNode<Control>("/root/contract1/UI/UIControl/DeadEndMenu");
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

	public void _on_spirit_pressed() {
		UIOpen = false;
		deathMenu.Hide();
		healthBar.Hide();
		lifeBar.Show();
		PlayerStats._summon_spirit();
	}

	private void HealthBarChange(int new_hp) {
		health = new_hp;
	}

	private void LivesBarChange(int new_lives, bool end) {
		lives = new_lives;
		UIOpen = true;
		if (end) {
			deadEndMenu.Show();
		} else {
			deathMenu.Show();
		}
	}
}

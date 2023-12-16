using Godot;
using System;
using System.Numerics;

public partial class PlayerStats : Node
{
	private CharacterBody2D Player;
	private CharacterBody2D Spirit;
	private UI uiScripts;
	public int lives = 3;
	public int hp = 3;
	public override void _Ready()
	{
		Player = GetNode<CharacterBody2D>("/root/contract1/player");
		Spirit = GetNode<CharacterBody2D>("/root/contract1/Spirit");
		uiScripts = GetNode<UI>("/root/contract1/UI");
	}

	public void _lives_update(int new_lives)
	{
		lives = new_lives;
	}

	public void _summon_spirit()
	{
		Godot.Vector2 placement = Spirit.Position;
		Godot.Vector2 summon_place = Player.Position;
		Player.Position = placement;
		Spirit.Position = summon_place;
		Spirit.Call("_enable");
		uiScripts.healthBar.Hide();
		uiScripts.lifeBar.Show();
	}

	public void _revive()
	{
		Godot.Vector2 revive_place = Spirit.Position;
		Godot.Vector2 placement = Player.Position;
		Spirit.Position = placement;
		Player.Position = revive_place;
		Player.Call("_enable");
		uiScripts.lifeBar.Hide();
		uiScripts.healthBar.Show();
	}
}

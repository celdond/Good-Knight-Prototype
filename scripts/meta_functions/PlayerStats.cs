using Godot;
using System;
using System.Numerics;

public partial class PlayerStats : Node
{
	private CharacterBody2D Player;
	private CharacterBody2D Spirit;
	public int lives = 3;
	public int hp = 3;
	public override void _Ready()
	{
		Player = GetNode<CharacterBody2D>("/root/contract1/player");
		Spirit = GetNode<CharacterBody2D>("/root/contract1/Spirit");
	}

	public void _lives_update(int new_lives)
	{
		lives = new_lives;
	}

	public void _summon_spirit()
	{
		_lives_update(lives -= 1);
		Godot.Vector2 placement = Spirit.Position;
		Godot.Vector2 summon_place = Player.Position;
		Player.Position = placement;
		Spirit.Position = summon_place;
		Spirit.Call("_enable");
	}
}

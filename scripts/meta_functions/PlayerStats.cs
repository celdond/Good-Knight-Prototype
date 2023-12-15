using Godot;
using System;

public partial class PlayerStats : Node
{
	public int lives = 3;
	public int hp = 3;
	public override void _Ready()
	{
	}

	public void _lives_update(int new_lives) {
		lives = new_lives;
	}
}

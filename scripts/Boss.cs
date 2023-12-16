using Godot;
using System;

public partial class Boss : CharacterBody2D
{
	public int health = 3;
	private AnimatedSprite2D _animatedSprite;
	private Timer HitboxCooldown;

	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		HitboxCooldown = GetNode<Timer>("hitbox_cooldown");
	}

	public void OnHit() {
		health -= 1;
		_animatedSprite.Play("hit");
		HitboxCooldown.Start();
		if (health <= 0) {
			string credit_scene = "res://scenes/menus/credits.tscn";
			GetTree().ChangeSceneToFile(credit_scene);
		}
	}

	public void _on_hitbox_cooldown_timeout()
	{
		_animatedSprite.Play("idle");
	}
}

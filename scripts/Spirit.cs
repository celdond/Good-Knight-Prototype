using Godot;
using System;
using System.Net.Http;

public partial class Spirit : CharacterBody2D
{
	public const float Speed = 300.0f;
	private CustomAlerts CustomAlerts;
	private PlayerStats PlayerStats;
	private Timer HitboxCooldown;
	private AnimatedSprite2D _animatedSprite;
	private CollisionShape2D hitbox;
	private CollisionShape2D collider;
	public bool i_frames = false;
	public bool alive = true;
	public int lives;

	public override void _Ready()
	{
		hitbox = GetNode<CollisionShape2D>("Area2D/Attack");
		collider = GetNode<CollisionShape2D>("Collision");
		SetPhysicsProcess(false);
		CustomAlerts = GetNode<CustomAlerts>("/root/contract1/CustomAlerts");
		PlayerStats = GetNode<PlayerStats>("/root/contract1/PlayerStats");
		hitbox = GetNode<CollisionShape2D>("Area2D/Attack");
		collider = GetNode<CollisionShape2D>("Collision");
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		HitboxCooldown = GetNode<Timer>("hitbox_cooldown");
		lives = PlayerStats.lives;
	}

	public void _enable() {
		SetPhysicsProcess(true);
		hitbox.Disabled = false;
		collider.Disabled = false;
	}

	public void _disable() {
		hitbox.Disabled = true;
		collider.Disabled = true;
		SetPhysicsProcess(false);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (!alive)
		{
			velocity.X = 0;
			velocity.Y = 0;
			Velocity = velocity;
			MoveAndSlide();
			_animatedSprite.Play("down");
			_disable();
			return;
		}

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction.X != 0)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		if (direction.Y != 0)
		{
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void OnHit()
	{
		if (!i_frames)
		{
			lives -= 1;
			PlayerStats._lives_update(lives);
			bool end = false;
			if (lives == 0)
			{
				end = true;
			}
			CustomAlerts.EmitSignal(nameof(CustomAlerts.LivesChange), PlayerStats.lives, end);
			if (end)
			{
				alive = false;
			}
			i_frames = true;
			HitboxCooldown.Start();
		}
	}

	public void Death(bool end) {
		PlayerStats._lives_update(0);
		CustomAlerts.EmitSignal(nameof(CustomAlerts.LivesChange), PlayerStats.lives, end);
	}

	public void _on_hitbox_cooldown_timeout()
	{
		i_frames = false;
	}
	public void _on_body_entered(Node2D body)
	{
		if (body is CharacterBody2D && body is not Spirit && body is not Player)
		{
			body.Call("OnHit");
			PlayerStats._revive();
		}
	}
}

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
	public bool i_frames = false;
	public bool alive = false;
	public int lives;

	public override void _Ready()
	{
		CustomAlerts = GetNode<CustomAlerts>("/root/contract1/CustomAlerts");
		PlayerStats = GetNode<PlayerStats>("/root/contract1/PlayerStats");
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		HitboxCooldown = GetNode<Timer>("hitbox_cooldown");
		lives = PlayerStats.lives;
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

	public void _on_hitbox_cooldown_timeout()
	{
		i_frames = false;
	}
	public void _on_body_entered(Node2D body)
	{
		if (body is CharacterBody2D && body is not Spirit)
		{
			body.Call("OnHit");
		}
	}
}

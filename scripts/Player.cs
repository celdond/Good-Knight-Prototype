using Godot;
using System;
using System.Linq.Expressions;

public partial class Player : CharacterBody2D
{
	private CustomAlerts CustomAlerts;
	private PlayerStats PlayerStats;
	private Timer HitboxCooldown;
	private AnimatedSprite2D _animatedSprite;
	private CollisionShape2D attack;
	public const float Speed = 300.0f;
	public const float JumpVelocity = -450.0f;
	public int hp;
	private int attack_frames = 0;
	public bool i_frames = false;
	public bool alive = true;

	public override void _Ready()
	{
		PlayerStats = GetNode<PlayerStats>("/root/contract1/PlayerStats");
		CustomAlerts = GetNode<CustomAlerts>("/root/contract1/CustomAlerts");
		HitboxCooldown = GetNode<Timer>("hitbox_cooldown");
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		attack = GetNode<CollisionShape2D>("./AnimatedSprite2D/Area2D/Attack");
		hp = PlayerStats.hp;
	}

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
		}

		if (!alive)
		{
			velocity.X = 0;
			Velocity = velocity;
			MoveAndSlide();
			_animatedSprite.Play("down");
			return;
		}

		if (i_frames)
		{
			_animatedSprite.Play("hit");
		}
		else if (Input.IsActionJustPressed("attack"))
		{
			_animatedSprite.Play("attack");
			attack.Disabled = false;
		}
		else if (Input.IsActionJustReleased("attack"))
		{
			attack.Disabled = true;
		}
		else if (Input.IsActionJustPressed("ui_down"))
		{
			SetCollisionMaskValue(2, false);
		}
		else if (Input.IsActionJustReleased("ui_down"))
		{
			SetCollisionMaskValue(2, true);
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction.X != 0)
		{
			velocity.X = direction.X * Speed;
			if (!i_frames)
			{
				if (velocity.Y != 0)
				{
					if (attack.Disabled) {
						_animatedSprite.Play("jump");
					}
				}
				else
				{
					_animatedSprite.Play("run");
					attack.Disabled = true;
				}
			}

			if (velocity.X > 0)
			{
				_animatedSprite.Scale = new Vector2(0.25f, 0.25f);
			}
			else if (velocity.X < 0)
			{
				_animatedSprite.Scale = new Vector2(-0.25f, 0.25f);
			}
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			if (velocity.Y != 0 && attack.Disabled && !i_frames)
			{
				_animatedSprite.Play("jump");
			}
			else if (attack.Disabled && !i_frames)
			{
				_animatedSprite.Play("idle");
			}
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void Death(bool end)
	{
		alive = false;
		if (end) {
			CustomAlerts.EmitSignal(nameof(CustomAlerts.LivesChange), PlayerStats.lives, end);
		}
	}

	private void Set_hp(int new_hp)
	{
		hp = new_hp;
		CustomAlerts.EmitSignal(nameof(CustomAlerts.HPChange), hp);
		if (hp <= 0)
		{
			Death(false);
		}
	}

	public void OnHit()
	{
		if (!i_frames)
		{
			Set_hp(hp - 1);
			i_frames = true;
			_animatedSprite.Play("hit");
			HitboxCooldown.Start();
		}
	}

	public void _on_hitbox_cooldown_timeout()
	{
		i_frames = false;
	}

	public void _on_attack_entered(Node2D body)
	{
		if (body is CharacterBody2D && body is not Player)
		{
			body.Call("OnHit");
		}
	}
}

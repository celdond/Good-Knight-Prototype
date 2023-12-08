using Godot;
using System;
using System.Linq.Expressions;

public partial class Player : CharacterBody2D
{
	private CustomAlerts CustomAlerts;
	private Timer HitboxCooldown;
	public const float Speed = 300.0f;
	public const float JumpVelocity = -450.0f;
	public int hp = 3;
	public int lives = 3;
	public bool i_frames = false;

	public override void _Ready()
	{
		CustomAlerts = GetNode<CustomAlerts>("/root/CustomAlerts");
		HitboxCooldown = GetNode<Timer>("hitbox_cooldown");
	}

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (Input.IsActionJustPressed("ui_down"))
		{
			SetCollisionMaskValue(2, false);
		}
		else if (Input.IsActionJustReleased("ui_down"))
		{
			SetCollisionMaskValue(2, true);
		}

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void Death()
	{
		lives -= 1;
		SetProcessInput(false);
		CustomAlerts.EmitSignal(nameof(CustomAlerts.LivesChange), lives);
	}

	private void Set_hp(int new_hp)
	{
		hp = new_hp;
		CustomAlerts.EmitSignal(nameof(CustomAlerts.HPChange), hp);
		if (hp <= 0)
		{
			Death();
		}
	}

	public void OnHit()
	{
		if (!i_frames)
		{
			Set_hp(hp - 1);
			i_frames = true;
			HitboxCooldown.Start();
		}
	}

	public void _on_hitbox_cooldown_timeout()
	{
		i_frames = false;
	}
}

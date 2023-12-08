using Godot;
using System;
using System.Linq.Expressions;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -450.0f;
	public int hp = 3;
	public int lives = 3;

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

	private void Death() {
		lives -= 1;
		SetProcessInput(false);
		EmitSignal("LivesChange", lives);
	}

	private void Set_hp(int new_hp)
	{
		hp = new_hp;
		EmitSignal("HpChange", hp);
		if (hp <= 0) {
			Death();
		}
	}

	public void Take_Damage() {
		Set_hp(1);
	}
}

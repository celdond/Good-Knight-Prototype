using Godot;
using System;

public partial class Spirit : CharacterBody2D
{
	public const float Speed = 300.0f;
	private CustomAlerts CustomAlerts;
	private AnimatedSprite2D _animatedSprite;

	public override void _Ready()
	{
		CustomAlerts = GetNode<CustomAlerts>("/root/contract1/CustomAlerts");
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction.X != 0)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		if (direction.Y != 0) {
			velocity.Y = direction.Y * Speed;
		} else {
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}

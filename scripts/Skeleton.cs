using Godot;
using System;

public partial class Skeleton : CharacterBody2D
{
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private AnimatedSprite2D _animatedSprite;
	public Vector2 direction = Vector2.Right;
	public RayCast2D rightCheck;
	public RayCast2D leftCheck;
	public bool enemy = true;
	public override void _Ready()
	{
		rightCheck = GetNode<RayCast2D>("LedgeCheckRight");
		leftCheck = GetNode<RayCast2D>("LedgeCheckLeft");
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		bool ledgeFound = (!rightCheck.IsColliding()) || (!leftCheck.IsColliding());

		if (IsOnWall() || ledgeFound)
		{
			direction *= -1;
		}

		velocity.X = direction.X * 15;
		if (velocity.X > 0)
		{
			_animatedSprite.FlipH = true;
		}
		else if (velocity.X < 0)
		{
			_animatedSprite.FlipH = false;
		}

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		Velocity = velocity;
		MoveAndSlide();
	}

	public void OnHit()
	{
		
	}
}

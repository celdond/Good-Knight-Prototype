using Godot;
using System;

public partial class Skeleton : CharacterBody2D
{
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public Vector2 direction = Vector2.Right;
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (IsOnWall()) {
			direction *= -1;
		}

		velocity.X = direction.X * 15;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		Velocity = velocity;
		MoveAndSlide();
	}
}

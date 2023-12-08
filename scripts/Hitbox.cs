using Godot;
using System;

public partial class Hitbox : Area2D
{
	public void _hitbox_body_entered(Node2D body)
	{
		if (body is Player)
		{
			GetNode("CollisionShape2D").SetDeferred("disabled", true);
			body.Call("OnHit");
		}
	}
}

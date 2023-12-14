using Godot;
using System;

public partial class Hitbox : Area2D
{
	public void _hitbox_body_entered(Node2D body)
	{
		if (body is Player)
		{
			body.Call("OnHit");
		} else {
			GD.Print(body);
		}
	}
}

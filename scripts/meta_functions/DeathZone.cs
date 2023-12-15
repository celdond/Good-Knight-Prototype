using Godot;
using System;

public partial class DeathZone : Area2D
{
	public void _on_body_entered(Node2D body)
	{
		if (body is Player || body is Spirit)
		{
			body.Call("Death", true);
		}
	}
}

using Godot;
using System;

public partial class CameraSwap : Area2D
{
	public Camera2D mainCamera;
	public Camera2D subCamera;
	public RemoteTransform2D playerSight;
	public RemoteTransform2D spiritSight;

	public override void _Ready()
	{
		mainCamera = GetNode<Camera2D>("../CameraForPlatforming");
		subCamera = GetNode<Camera2D>("../CameraForBoss");
		playerSight = GetNode<RemoteTransform2D>("../player/RemoteTransform2D");
		spiritSight = GetNode<RemoteTransform2D>("../Spirit/RemoteTransform2D");
	}

	public void _on_body_entered(Node2D body) {
		if (body is Player)
		{
			subCamera.Enabled = true;
			playerSight.RemotePath = "../../CameraForBoss";
			mainCamera.Enabled = false;

		} else if (body is Spirit)
		{
			subCamera.Enabled = true;
			spiritSight.RemotePath = "../../CameraForBoss";
			mainCamera.Enabled = false;
		}
		QueueFree();
	}
}

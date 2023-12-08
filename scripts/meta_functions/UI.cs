using Godot;
using System;

public partial class UI : CanvasLayer
{

	public ProgressBar lives;
	public ProgressBar health;
	public override void _Ready()
	{
		lives = GetNode<ProgressBar>("Lives");
		health = GetNode<ProgressBar>("Health");
		lives.Value = 3;
		health.Value = 3;
	}
	[Signal]public delegate void LivesChangeEventHandler();
	[Signal]public delegate void HPChangeEventHandler();
}

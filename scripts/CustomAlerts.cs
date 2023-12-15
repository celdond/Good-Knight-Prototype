using Godot;
using System;

public partial class CustomAlerts : Node
{
	[Signal]public delegate void LivesChangeEventHandler(int new_lives, bool end);
	[Signal]public delegate void HPChangeEventHandler(int new_hp);
}

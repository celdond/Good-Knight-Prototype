using Godot;
using System;

public partial class Contracts : VBoxContainer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_contract_button_down(int contract)
	{
		string target_scene = "res://scenes/levels/contract" + contract.ToString() + ".tscn";
		GetTree().ChangeSceneToFile(target_scene);
	}
}

using Godot;
using System;

public partial class Contracts : VBoxContainer
{
	public void _on_contract_button_down(int contract)
	{
		string target_scene = "res://scenes/levels/contract" + contract.ToString() + ".tscn";
		GetTree().ChangeSceneToFile(target_scene);
	}
}

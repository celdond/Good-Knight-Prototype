using Godot;
using System;

public partial class Contracts : Control
{
	public void _on_contract_button_down(int contract)
	{
		string target_scene = "res://scenes/levels/contract" + contract.ToString() + ".tscn";
		GetTree().ChangeSceneToFile(target_scene);
	}

	public void _on_exit_button_down()
	{
		GetTree().Quit();
	}
}

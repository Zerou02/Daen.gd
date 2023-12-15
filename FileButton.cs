using Godot;
using System;
using System.Reflection;

#pragma warning disable IDE1006 // Naming Styles
public partial class FileButton : Button
{
	string fileName = "";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Pressed += onButtonPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void setFileName(string name)
	{
		this.fileName = name;
		this.Text = name.Replace(".json", "");
	}

	public void onButtonPressed()
	{
		var output = new Godot.Collections.Array();
		var jsonName = this.fileName.Replace(".json", "");
		int exitCode = OS.Execute("./rusty-bush", new string[] { "presets/" + jsonName }, output);
	}

}
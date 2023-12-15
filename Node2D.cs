using Godot;
using System.Collections.Generic;

public partial class Node2D : Godot.Node2D
{
	PackedScene fileBtnScn = GD.Load("res://Test.tscn") as PackedScene;
	List<VBoxContainer> containers = new List<VBoxContainer>();
	int boxWidth = 300;
	// Called when the node enters the scene tree for the first time
	public override void _Ready()
	{
		containers.Add(GetNode("VBoxContainer") as VBoxContainer);

		createFileBtns(DirAccess.Open("res://presets"), 0, 0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void createFileBtns(DirAccess acc, int indentLvl, int count)
	{
		GD.Print(indentLvl);
		if (indentLvl == containers.Count)
		{
			containers.Add(new VBoxContainer());
			var idx = containers.Count - 1;
			var element = containers[idx];
			this.AddChild(element);
			element.Position = Position with { X = element.Position.X + 50 + idx * boxWidth, Y = element.Position.Y };
		}
		var files = acc.GetFiles();
		int fCount = count;
		for (int i = 0; i < files.Length; i++)
		{
			GD.Print(files[i]);
			if (files[i].Contains(".json"))
			{
				var btn = fileBtnScn.Instantiate() as FileButton;
				btn.setFileName(files[i]);
				containers[indentLvl].AddChild(btn);
				fCount++;
			}
		}
		var dirs = acc.GetDirectories();
		for (int i = 0; i < dirs.Length; i++)
		{
			var errFlag = acc.ChangeDir(dirs[i]);
			if (errFlag == Error.Ok)
			{
				createFileBtns(acc, indentLvl + 1, fCount);
			}
		}
	}
}

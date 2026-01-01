using Godot;
using System;

public partial class AmmoItem : Area2D
{
	public AmmoManagement ammoManagement;

	public override void _Ready()
	{
		Node sceneRoot = GetTree().CurrentScene;
		Node player = sceneRoot.GetNode<Node>("player");
		ammoManagement = player.GetNode<AmmoManagement>("AmmoManagement");

        BodyEntered += OnBodyEntered;
    }

	private void OnBodyEntered(Node body)
	{
		if (body.IsInGroup("player"))
		{
            ammoManagement.addMagazineAndBullet();
            QueueFree();
        }
	}
}

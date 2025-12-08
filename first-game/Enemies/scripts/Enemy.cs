using Godot;
using System;

public partial class Enemy : RigidBody2D
{
    [Export] public float Speed = 50.0f;

    public override void _PhysicsProcess(double delta)
	{
        Node2D player = GetTree().GetNodesInGroup("player")[0] as Node2D;
        Vector2 playerPosition = player.Position;

        Position = Position.MoveToward(playerPosition, Speed * (float)delta);
	}
}

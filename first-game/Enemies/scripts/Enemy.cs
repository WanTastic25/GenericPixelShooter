using Godot;
using System;

public partial class Enemy : RigidBody2D
{
    [Export] public float Speed = 50.0f;
    [Export] public int enemyhealth = 10;
    [Export] public HealthBar healthBarCode;

    public override void _Ready()
    {
        healthBarCode = GetNode<HealthBar>("healthBar");
        healthBarCode.InitHealth(enemyhealth);
    }

    public override void _PhysicsProcess(double delta)
	{
        Node2D player = GetTree().GetNodesInGroup("player")[0] as Node2D;
        Vector2 playerPosition = player.Position;

        Position = Position.MoveToward(playerPosition, Speed * (float)delta);
	}
}

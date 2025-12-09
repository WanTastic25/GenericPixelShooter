using Godot;
using System;

public partial class Enemy : RigidBody2D
{
    [Export] public float Speed = 50.0f;
    [Export] public int enemyhealth = 10;
    [Export] public HealthBar healthBarCode;
    private PlayerMovement playerMovementScript;
    private bool playerDetected;
    private Vector2 enemyDirection;

    public override void _Ready()
    {
        playerMovementScript = GetTree().GetFirstNodeInGroup("player") as PlayerMovement;
        healthBarCode = GetNode<HealthBar>("healthBar");
        healthBarCode.InitHealth(enemyhealth);

        this.BodyEntered += OnBodyEntered;
        this.BodyExited += OnBodyExited;
    }

    public override void _PhysicsProcess(double delta)
	{
        Node2D player = GetTree().GetNodesInGroup("player")[0] as Node2D;
        Vector2 playerPosition = player.Position;

        enemyDirection = (playerPosition - GlobalPosition).Normalized();
        Position += enemyDirection * Speed * (float)delta;
    }

    private void OnBodyEntered(Node body)
    {
        if (body.IsInGroup("player"))
        {
            playerDetected = true;

            if (playerMovementScript.invulnerable == false && playerDetected)
            {
                var healthCode = body.GetNode<HealthBar>("healthBar");
                healthCode.Health = -10;

                playerMovementScript.invulnearble();
                _ = playerMovementScript.knockback(enemyDirection);
            }
        }
    }

    private void OnBodyExited(Node body)
    {
        if (body.IsInGroup("player"))
        {
            playerDetected = false;
        }
    }
}

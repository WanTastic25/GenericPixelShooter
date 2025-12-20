using Godot;
using System;

public partial class Enemy : RigidBody2D
{
    [Export] public EnemyData enemyData;

    [Export] public HealthBar healthBarCode;
    private PlayerMovement playerMovementScript;
    private bool playerDetected;
    private Vector2 enemyDirection;

    public override void _Ready()
    {
        playerMovementScript = PlayerMovement.Instance;
        healthBarCode = GetNode<HealthBar>("healthBar");
        healthBarCode.InitHealth(enemyData.EnemyHealth);

        this.BodyEntered += OnBodyEntered;
        this.BodyExited += OnBodyExited;
    }

    public override void _PhysicsProcess(double delta)
	{
        Node2D player = PlayerMovement.Instance;
        Vector2 playerPosition = player.Position;

        enemyDirection = (playerPosition - GlobalPosition).Normalized();
        Position += enemyDirection * enemyData.EnemySpeed * (float)delta;
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

                playerMovementScript.applyInvulnerability();
                playerMovementScript.applyKnockback(enemyDirection, 300f, 0.1);
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

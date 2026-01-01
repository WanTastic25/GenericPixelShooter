using Godot;
using System;

public partial class Enemy : RigidBody2D
{
    [Export] public EnemyData enemyData;

    [Export] public HealthBar healthBarCode;
    private bool playerDetected;
    private Vector2 enemyDirection;
    private PlayerMovement _player;

    public override void _Ready()
    {
        _player = PlayerMovement.Instance;
        _player.TreeExited += OnPlayerExitTree;

        healthBarCode = GetNode<HealthBar>("healthBar");
        healthBarCode.InitHealth(enemyData.EnemyHealth);

        this.BodyEntered += OnBodyEntered;
        this.BodyExited += OnBodyExited;
    }

    public override void _PhysicsProcess(double delta)
	{
        Vector2 playerPosition = _player.Position;

        enemyDirection = (playerPosition - GlobalPosition).Normalized();
        Position += enemyDirection * enemyData.EnemySpeed * (float)delta;
    }

    public override void _ExitTree()
    {
        // Unsubscribe to prevents memory leaks with events
        _player.TreeExited -= OnPlayerExitTree;
    }

    private void OnBodyEntered(Node body)
    {
        if (body.IsInGroup("player"))
        {
            playerDetected = true;

            if (_player.invulnerable == false && playerDetected)
            {
                var healthCode = body.GetNode<HealthBar>("healthBar");
                healthCode.Health = -10;

                _player.applyInvulnerability();
                _player.applyKnockback(enemyDirection, 300f, 0.1);
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

    private void OnPlayerExitTree()
    {
        SetPhysicsProcess(false);
    }
}

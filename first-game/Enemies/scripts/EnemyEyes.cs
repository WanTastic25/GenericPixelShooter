using Godot;
using System;

public partial class EnemyEyes : AnimatedSprite2D
{
    private Vector2 rootEyePosition;
    private PlayerMovement _player;
    public float MaxOffset = 5f;

    public override void _Ready()
    {
        _player = PlayerMovement.Instance;
        _player.TreeExited += OnPlayerExitTree;
        rootEyePosition = Position;
    }

    public override void _Process(double delta)
    {
        Vector2 playerPos = _player.Position;
        Vector2 toPlayer = playerPos - GlobalPosition;

        float distance = toPlayer.Length();
        Vector2 direction = toPlayer.Normalized();

        float offsetStrength = Mathf.Min(distance / 100f, 1f);
        Position = rootEyePosition + direction * MaxOffset * offsetStrength;
    }

    public override void _ExitTree()
    {
        // Unsubscribe to prevents memory leaks with events
        _player.TreeExited -= OnPlayerExitTree;
    }

    private void OnPlayerExitTree()
    {
        SetProcess(false);
    }
}

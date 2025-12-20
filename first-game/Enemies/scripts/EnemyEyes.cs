using Godot;
using System;

public partial class EnemyEyes : AnimatedSprite2D
{
    private Vector2 rootEyePosition;
    public float MaxOffset = 5f;

    public override void _Ready()
    {
        rootEyePosition = Position;
    }

    public override void _Process(double delta)
    {
        Node2D player = PlayerMovement.Instance;

        Vector2 playerPos = player.Position;
        Vector2 toPlayer = playerPos - GlobalPosition;

        float distance = toPlayer.Length();
        Vector2 direction = toPlayer.Normalized();

        float offsetStrength = Mathf.Min(distance / 100f, 1f);
        Position = rootEyePosition + direction * MaxOffset * offsetStrength;
    }
}

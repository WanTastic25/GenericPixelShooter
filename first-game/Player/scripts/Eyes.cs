using Godot;
using System;

public partial class Eyes : AnimatedSprite2D
{
    private Vector2 rootEyePosition;
    public float MaxOffset = 5f;

    public override void _Ready()
	{
        rootEyePosition = Position;
    }

	public override void _Process(double delta)
	{
        Vector2 mousePos = GetGlobalMousePosition();
        Vector2 toMouse = mousePos - GlobalPosition;

        float distance = toMouse.Length();
        Vector2 direction = toMouse.Normalized();

        float offsetStrength = Mathf.Min(distance / 100f, 1f);
        Position = rootEyePosition + direction * MaxOffset * offsetStrength;
    }
}

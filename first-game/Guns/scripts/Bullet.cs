using Godot;
using System;

public partial class Bullet : Area2D
{
    [Export] public float bulletSpeed = 200f;

    public override void _Ready()
	{
        DeleteAfterSeconds(2f);

        this.BodyEntered += OnBodyEntered;
    }

    private async void DeleteAfterSeconds(float seconds)
    {
        await ToSignal(GetTree().CreateTimer(seconds), "timeout");
        QueueFree();
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += Transform.X * bulletSpeed * (float)delta;
    }

    private void OnBodyEntered(Node body)
    {
        if (body.IsInGroup("enemy"))
        {
            GD.Print("Delete enemy");
            body.QueueFree();
            QueueFree();
        }
    }
}

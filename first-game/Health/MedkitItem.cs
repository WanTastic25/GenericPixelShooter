using Godot;
using System;

public partial class MedkitItem : Area2D
{
    public CharacterBody2D player;
    public HealthBar playerHealthBar;

    public override void _Ready()
    {
        player = PlayerMovement.Instance;
        playerHealthBar = player.GetNode<HealthBar>("healthBar");

        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node body)
    {
        if (body.IsInGroup("player"))
        {
            playerHealthBar.Health += 50;
        }

        QueueFree();
    }
}

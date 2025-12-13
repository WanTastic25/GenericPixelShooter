using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Gun : Node2D
{
    [Export] public PackedScene bulletGameObject;

    [Export] public Node2D muzzle;

    [Export] public Sprite2D gunSprite;

    [Export] public Boolean canShoot = true;

    [Export] public AmmoManagement ammoManagement;

    public override void _Ready()
    {
        ammoManagement.ammoInit(3, 30);
    }

    public override void _Process(double delta)
    {
        LookAt(GetGlobalMousePosition());

        RotationDegrees = Mathf.Wrap(RotationDegrees, 0f, 360f);              

        if (RotationDegrees > 90 && RotationDegrees < 270)
        {
            gunSprite.Scale = new Vector2(0.3f, -0.3f);
        }
        else
        {
            gunSprite.Scale = new Vector2(0.3f, 0.3f);
        }

        if (Input.IsActionPressed("mouseLeft"))
        {
            Shooting();
        }
    }

    public async void Shooting()
    {
        if (!canShoot)
            return;

        canShoot = false;

        Shoot();
        await ToSignal(GetTree().CreateTimer(0.1f), "timeout");

        canShoot = true;
    }

    public void Shoot()
    {
        ammoManagement.checkBullet();
        // Later figure out how to make the reload cycle work without needing to lift my finger off the left mouse button

        if (ammoManagement.ammoAvailable)
        {
            var bullet = bulletGameObject.Instantiate<Node2D>();
            bullet.GlobalPosition = muzzle.GlobalPosition;
            GetTree().CurrentScene.AddChild(bullet);
            bullet.GlobalRotation = gunSprite.GlobalRotation;

            return;
        }
        else
        {
            GD.Print("No Ammo");
            return;
        }
    }
}

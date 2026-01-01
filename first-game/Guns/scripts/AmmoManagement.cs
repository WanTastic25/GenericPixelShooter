using Godot;
using System;

public partial class AmmoManagement : Node
{
	[Export] public Timer reloadTimer;
	public int currentMagazine;
	public int maxMagazine;
	public int currentBullet;
	public int maxBullet;
	public bool reloading;
	public bool ammoAvailable = true;
	Label bulletCount;
	Label magazineCount;

    public override void _Ready()
    {
		reloadTimer = GetNode<Timer>("Timer");
		Node root = GetTree().CurrentScene;
		Node canvas = root.GetNode<CanvasLayer>("CanvasLayer");
		Control ammoUI = canvas.GetNode<Control>("AmmoUI");
		magazineCount = ammoUI.GetNode<Label>("MarginContainer2/VBoxContainer/HBoxContainer/magazineCount");
		bulletCount = ammoUI.GetNode<Label>("MarginContainer2/VBoxContainer/HBoxContainer/bulletCount");

        reloadTimer = GetNode<Timer>("Timer");
    }
    }

    public void ammoInit(int getMagazine, int getBullet)
	{
		maxMagazine = getMagazine;
		maxBullet = getBullet;

		currentMagazine = getMagazine;
		currentBullet = getBullet;
	}

	public void addMagazineAndBullet()
	{
		currentBullet = maxBullet;
		currentMagazine += 3;
        ammoAvailable = true;
    }

	public void checkBullet()
	{
		if (currentBullet <= 0)
			checkMagazine();
		else
		{
            currentBullet--;
            GD.Print("Bullet Count" + currentBullet + "/" + currentMagazine);
        }
    }

	public void checkMagazine()
	{
		if (currentMagazine <= 0)
		{
            ammoAvailable = false;
        }
		else 
		{
            ammoAvailable = false;
            reloadTimer.Start();
            updateAmmoUI();
        }
	}

    public void _on_timer_timeout()
	{
		GD.Print("Reload Done");
        reloading = false;
        currentMagazine--;
        currentBullet = maxBullet;
        ammoAvailable = true;
    }

	public void updateAmmoUI()
	{
		magazineCount.Text = "x" + currentMagazine;
		bulletCount.Text = "" + currentBullet;
	}
}

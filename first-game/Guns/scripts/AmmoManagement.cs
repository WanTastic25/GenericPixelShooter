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

    // See if there is more or equal to 1 bullet
    // if there is bullet, shoot
    // if there is no bullet, check magazine count
    // if there is magazine, remove one magazine, add 30 bullet
    // if there is no magazine, do not shoot

    public override void _Ready()
    {
		reloadTimer = GetNode<Timer>("Timer");
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
}

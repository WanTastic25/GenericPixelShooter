using Godot;
using System;

public partial class HealthBar : ProgressBar
{
	[Export] public ProgressBar damageBar;
	[Export] public Timer timer;
    private int health = 0;
    private int prevHealth = 0; 

    public override void _Ready()
	{
        timer = GetNode<Timer>("Timer");
        damageBar = GetNode<ProgressBar>("damageBar");
    }

	public int Health
	{
		get => health;
		set => SetHealth(value);
    }

	private void SetHealth(int value)
	{
        prevHealth = health;
        health = prevHealth + value;
        Value = health;

        if (health <= 0)
        {
            GetParent().QueueFree();
        }

        if (health < prevHealth)
        {
            timer.Start();
        }
	}

    public void InitHealth(int value)
    {
        health = value;

        MaxValue = health;
        Value = health;
        damageBar.MaxValue = health;
        damageBar.Value = health;
    }

    public void _on_timer_timeout()
	{
        damageBar.Value = health;
    }
}

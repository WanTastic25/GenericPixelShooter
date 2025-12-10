using Godot;
using System;
using System.Threading.Tasks;
using static Godot.TextServer;

public partial class PlayerMovement : CharacterBody2D
{
    [Export] public float Speed = 100.0f;
    [Export] public Boolean canDash = true;
    [Export] public Boolean invulnerable = false;
    [Export] public Boolean isDashing = false;
	//[Export] public PlayerStats stats;
	[Export] public int playerHealth = 100;
	private Timer dashAgainTimer;
	private Timer invulnerabilityTimer;
	private HealthBar healthBarCode;
	private double knockbackTimer;
	private Vector2 knockback;

    public override void _Ready()
	{
        dashAgainTimer = GetNode<Timer>("dashAgainTimer");
        invulnerabilityTimer = GetNode<Timer>("invulnerabilityTimer");
		healthBarCode = GetNode<HealthBar>("healthBar");
		healthBarCode.InitHealth(playerHealth);
    }

    public override void _PhysicsProcess(double delta)
	{
        if (knockbackTimer > 0.00)
        {
            Velocity = knockback;
            knockbackTimer -= delta;

            if (knockbackTimer <= 0.00)
                knockback = Vector2.Zero;
        }
        else
        {
            movement();
        }

            MoveAndSlide();
	}

    public async Task Dash(Vector2 dashDirection, float distance, float duration)
    {
		isDashing = true;
		
        var tween = CreateTween();
        Vector2 start = GlobalPosition;
        Vector2 end = start + dashDirection.Normalized() * distance;

        tween.TweenProperty(this, "global_position", end, duration)
             .SetTrans(Tween.TransitionType.Cubic)
             .SetEase(Tween.EaseType.Out);

        await ToSignal(tween, "finished");

        isDashing = false;
	}

	public void movement()
	{
        Vector2 velocity = Velocity;

        Vector2 direction = Input.GetVector("left", "right", "up", "down");

        if (direction != Vector2.Zero)
            velocity = direction * Speed;

        if (direction == Vector2.Zero)
            velocity = Velocity.MoveToward(Vector2.Zero, Speed);

        if (Input.IsActionPressed("dash") && canDash && !isDashing)
        {
            _ = Dash(velocity, 150f, 0.08f);
            canDash = false;
            dashAgainTimer.Start();
        }

        Velocity = velocity;
    }

	public void applyKnockback(Vector2 enemyDirection, float knockBackStrength, double knockbackDuration)
	{
        knockback = enemyDirection * knockBackStrength;
		knockbackTimer = knockbackDuration;
	}

	public void applyInvulnerability()
	{
        invulnerable = true;
		invulnerabilityTimer.Start();
    }

	private void _on_dash_again_timer_timeout()
	{
		canDash = true;
	}

	private void _on_invulnerability_timer_timeout()
	{
        invulnerable = false;
	}
}

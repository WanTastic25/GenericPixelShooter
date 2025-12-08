using Godot;
using System;
using System.Threading.Tasks;
using static Godot.TextServer;

public partial class PlayerMovement : CharacterBody2D
{
    [Export] public float Speed = 100.0f;
    [Export] public Boolean canDodge = true;
    [Export] public Boolean isDodging = false;
    [Export] public float dodgeSpeed = 300.0f;

    public override void _PhysicsProcess(double delta)
	{
		Timer dodgeAgainTimer = GetNode<Timer>("dodgeAgainTimer");

        Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("left", "right", "up", "down");

		if (direction != Vector2.Zero)
			velocity = direction * Speed;

		if (direction == Vector2.Zero)
			velocity = Velocity.MoveToward(Vector2.Zero, Speed);

		if (Input.IsActionPressed("dodge") && canDodge && !isDodging)
		{	
			_ = Dash(velocity, 150f, 0.08f);
			canDodge = false;
			dodgeAgainTimer.Start();
        }

		Velocity = velocity;
		MoveAndSlide();
	}

    public async Task Dash(Vector2 dashDirection, float distance, float duration)
    {
		isDodging = true;
		
        var tween = CreateTween();
        Vector2 start = GlobalPosition;
        Vector2 end = start + dashDirection.Normalized() * distance;

        tween.TweenProperty(this, "global_position", end, duration)
             .SetTrans(Tween.TransitionType.Cubic)
             .SetEase(Tween.EaseType.Out);

        await ToSignal(tween, "finished");

        isDodging = false;        
	}

	private void _on_dodge_again_timer_timeout()
	{
		canDodge = true;
	}
}

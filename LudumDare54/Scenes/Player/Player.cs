using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export] private float moveSpeed = 160;
    [Export] private float dashMultiplier = 4;
    [Export] private float accelerationSmoothing = 16;
    [Export] private Timer dashTimer;

    private GameInput input;
    private Vector2 currentDirection;
    private bool isDashing;

    public override void _Ready()
    {
        Game.inst.refs.player = this;
        input = Game.inst.input;
        dashTimer.Timeout += OnDashTimer;
    }

    public override void _Process(double delta)
    {
        CheckDash();
        Move(delta);
    }

    private void Move(double delta)
    {
        if(!isDashing)
        {
            currentDirection = input.GetMove().Normalized();
            Vector2 targetVelocity = currentDirection * moveSpeed;
            Velocity = Velocity.Lerp(targetVelocity, (float)(1 - Mathf.Exp(-delta * accelerationSmoothing)));
        }
        else
        {
            Vector2 targetVelocity = currentDirection * moveSpeed * dashMultiplier;
            Velocity = targetVelocity;
        }

        MoveAndSlide();
    }

    private void CheckDash()
    {
        if(!isDashing && input.GetDashDown())
        {
            isDashing = true;
            dashTimer.Start();
        }
        else if(isDashing && input.GetDashUp())
        {
            isDashing = false;
            dashTimer.Stop();
        }
    }

    private void OnDashTimer()
    {
        isDashing = false;
    }
}

using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export] private float moveSpeed = 160;
    [Export] private float dashMultiplier = 4;
    [Export] private float accelerationSmoothing = 16;
    [Export] private int invinciblityFrames = 8;
    [Export] private Timer dashTimer;
    [Export] private Area2D area;
    [Export] private HealthComponent healthComponent;
    [Export] private Sprite2D sprite;

    private GameInput input;
    private Vector2 currentDirection;
    private bool isDashing;
    private bool isInvincible;

    public override void _Ready()
    {
        Game.inst.refs.player = this;
        input = Game.inst.input;

        dashTimer.Timeout += OnDashTimer;
        area.AreaEntered += OnAreaEntered;
        area.AreaExited += OnAreaExited;
        healthComponent.Died += Die;
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
            CancelDash();
        }
    }

    private void CancelDash()
    {
        isDashing = false;
        dashTimer.Stop();
    }

    private void OnDashTimer()
    {
        isDashing = false;
    }

    private void OnAreaEntered(Area2D other)
    {
        Node otherParent = other.GetParent();
        GD.Print(otherParent.Name);
        if (other is LimitAreaOuter && (otherParent as Limit).CanHurt())
        {
            if(!isDashing)
            {
                OnEnterDamage(other.GlobalPosition, 64f);
            }            
        }

        if(otherParent is Other)
        {
            OnEnterDamage(other.GlobalPosition, 16f);
            // (otherParent as Other).Die();
        }

        if (otherParent is Border)
        {
            healthComponent.TakeDamage(100);
            // (otherParent as Other).Die();
        }

        if (otherParent is Coin)
        {
            Game.inst.state.AddCoin();
            Coin otherCoin = otherParent as Coin;
            if(otherCoin.CanCollect())
            {
                otherCoin.OnPlayerCollect();
            }            
        }
    }

    private void OnAreaExited(Area2D other)
    {
        Node otherParent = other.GetParent();
        if (other is LimitAreaInner && (otherParent as Limit).CanHurt())
        {
            if (!isDashing)
            {
                OnEnterDamage(other.GlobalPosition, 64f);
            }
        }
    }

    private void OnEnterDamage(Vector2 otherPosition, float knockback)
    {
        //Knockback(otherPosition, knockback);
        if (isInvincible)
            return;

        healthComponent.TakeDamage(1);
        InvincibleSequence();
    }

    private void Knockback(Vector2 otherPosition, float knockback)
    {
        CancelDash();
        Velocity = Vector2.Zero;
        Vector2 knockbackDir = (GlobalPosition - otherPosition).Normalized();
        GlobalPosition += knockbackDir * knockback;
    }

    private void Die()
    {
        //area.Monitoring = false;
        Game.inst.OnPlayerDeath();
    }

    public float GetHealth()
    {
        return healthComponent.currentHealth;
    }

    private async void InvincibleSequence()
    {
        isInvincible = true;

        for (int i = 0; i < invinciblityFrames; i++) 
        {
            sprite.Visible = false;
            await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
            sprite.Visible = true;
            await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        }

        isInvincible = false;
    }
}

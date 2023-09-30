using Godot;
using Godot.Collections;
using System;

public partial class Other : CharacterBody2D
{
    [Export] private Area2D area;
    [Export] private Area2D scanArea;
    [Export] private Timer moveTimer;
    [Export] private Timer limitTimer;
    [Export] public double minSpeed = 80, maxSpeed = 160;

    private Coin coin;

    public override void _Ready()
    {
        moveTimer.Timeout += OnMoveTimer;
        limitTimer.Timeout += OnLimitTimer;
        area.AreaEntered += OnAreaEntered;

        ChangeDir();
    }

    public override void _Process(double delta)
    {
        MoveAndSlide();
    }

    private void OnMoveTimer()
    {
        ChangeDir();
    }

    private void OnLimitTimer()
    {
        if(IsLimitAhead())
        {
            if(GD.Randf() < 0.1)
            {
                ChangeDir();
            }
            else
            {
                Velocity *= (float)GD.RandRange(-0.67, -1.33);
            }
        }
    }

    private void ChangeDir()
    {
        float speed = (float)GD.RandRange(minSpeed, maxSpeed);
        Velocity = GetRandomDir() * speed;        
    }

    private bool IsLimitAhead()
    {
        PhysicsDirectSpaceState2D spaceState = GetWorld2D().DirectSpaceState;
        PhysicsRayQueryParameters2D query = PhysicsRayQueryParameters2D.Create(GlobalPosition, GlobalPosition + Velocity * 1);
        query.CollideWithAreas = true;
        Dictionary result = spaceState.IntersectRay(query);
        //GD.Print($"from {query.From} to {query.To}");  
        if(result.Count > 0)
        {
            return true;
        }
        return false;
    }

    public void Die()
    {
        if(coin != null)
        {
            coin.OnOtherDie(GlobalPosition);
        }

        QueueFree();
    }

    private Vector2 GetRandomDir()
    {
        return Vector2.Right.Rotated((float)GD.RandRange(0, Mathf.Tau)).Normalized();
    }

    private void OnAreaEntered(Area2D other)
    {
        Node otherParent = other.GetParent();
        if (otherParent is Limit)
        {
            Die();
        }

        if (otherParent is Coin)
        {
            if(coin != null)
            {
                coin.OnOtherCollectNewCoin();
            }

            coin = otherParent as Coin;
            coin.OnOtherCollect();
        }
    }

    private void OnScanAreaEntered(Area2D other)
    {
        Node otherParent = other.GetParent();
        if (otherParent is Limit)
        {
            Die();
        }
    }
}

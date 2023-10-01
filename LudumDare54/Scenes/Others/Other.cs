using Godot;
using Godot.Collections;
using System;

public partial class Other : CharacterBody2D
{
    [Export] private Area2D area;
    [Export] private Area2D scanArea;
    [Export] private Timer moveTimer;
    [Export] public double minSpeed = 80, maxSpeed = 160;
    [Export] public double minMoveTimer = 2, maxMoveTimer = 8;

    private Coin coin;

    public override void _Ready()
    {
        moveTimer.Timeout += OnMoveTimer;
        area.AreaEntered += OnAreaEntered;
        scanArea.AreaEntered += OnScanAreaEntered;

        ChangeDir();
    }

    public override void _Process(double delta)
    {
        MoveAndSlide();
    }

    private void OnMoveTimer()
    {
        RandomizeMoveTimer();
        ChangeDir();
    }

    private void RandomizeMoveTimer()
    {
        moveTimer.WaitTime = GD.RandRange(minMoveTimer, maxMoveTimer);
    }

    private void ChangeDir()
    {
        float speed = (float)GD.RandRange(minSpeed, maxSpeed);
        Velocity = GetRandomDir() * speed;        
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
        if (otherParent is Border)
        {
            Die();
        }

        if (otherParent is Coin)
        {
            Coin newCoin = otherParent as Coin;
            if(newCoin.CanCollect())
            {
                if (coin != null)
                {
                    coin.OnOtherCollectNewCoin();
                }

                coin = newCoin;
                coin.OnOtherCollect();
            }            
        }
    }

    private void OnScanAreaEntered(Area2D other)
    {
        Node otherParent = other.GetParent();
        if (otherParent is Border)
        {
            Velocity *= -1;
        }
    }
}

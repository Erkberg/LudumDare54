using Godot;
using System;

public partial class Coin : Node2D
{
    [Export] private Area2D area;

    public bool CanCollect()
    {
        return Visible;
    }

    public void OnPlayerCollect()
    {
        QueueFree();
    }

    public void OnOtherCollect()
    {
        Visible = false;
    }

    public void OnOtherDie(Vector2 position)
    {
        GlobalPosition = position;
        Visible = true;
    }

    public void OnOtherCollectNewCoin()
    {
        QueueFree();
    }
}

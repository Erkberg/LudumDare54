using Godot;
using System;

public partial class Coin : Node2D
{
    [Export] private Area2D area;

    public void OnPlayerCollect()
    {
        QueueFree();
    }

    public void OnOtherCollect()
    {
        Visible = false;
        area.Monitorable = false;
    }

    public void OnOtherDie(Vector2 position)
    {
        GlobalPosition = position;
        Visible = true;
        area.Monitorable = true;
    }

    public void OnOtherCollectNewCoin()
    {
        QueueFree();
    }
}

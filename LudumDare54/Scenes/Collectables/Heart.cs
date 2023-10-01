using Godot;
using System;

public partial class Heart : Node2D
{
    public void OnPlayerCollect()
    {
        QueueFree();
    }
}

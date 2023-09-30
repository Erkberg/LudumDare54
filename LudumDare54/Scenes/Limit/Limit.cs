using Godot;
using System;

public partial class Limit : Node2D
{
    [Export] private Area2D area;

    public override void _Ready()
    {
        area.AreaEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Area2D other)
    {
        Node otherParent = other.GetParent();
        if(otherParent is Player)
        {
            
        }
    }
}

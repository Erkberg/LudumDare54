using Godot;
using System;

public partial class Limit : Node2D
{
    [Export] public double minScale = 0.33, maxScale = 1.33;
    [Export] public double minDuration = 1.33, maxDuration = 3.33;
    [Export] private Area2D area;

    private int scaleDir = 0;

    private const string ScaleTweenProperty = "scale";

    public override void _Ready()
    {
        area.AreaEntered += OnAreaEntered;
        scaleDir = GD.Randf() < 0.5 ? 1 : -1;
        StartTween();
    }

    private void OnAreaEntered(Area2D other)
    {
        Node otherParent = other.GetParent();
        if(otherParent is Player)
        {
            
        }
    }

    private void StartTween()
    {
        scaleDir *= -1;
        float scaleValue = scaleDir == -1 ? (float)GD.RandRange(minScale, Scale.X) : (float)GD.RandRange(Scale.X, maxScale);
        double duration = GD.RandRange(minDuration, maxDuration);
        Vector2 scaleVector = new Vector2(scaleValue, scaleValue);

        Tween tween = GetTree().CreateTween().BindNode(this);
        tween.TweenProperty(this, ScaleTweenProperty, scaleVector, 1.0f);
        tween.TweenCallback(Callable.From(OnTweenEnded));
    }

    private void OnTweenEnded()
    {
        StartTween();
    }
}

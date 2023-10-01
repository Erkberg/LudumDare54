using Godot;
using System;

public partial class Limit : Node2D
{
    [Export] public Vector2 minScale = Vector2.One * 0.5f, maxScale = Vector2.One * 1.5f;
    [Export] public double minDuration = 2, maxDuration = 8;
    [Export] private Sprite2D sprite;
    [Export] private Timer activeTimer;
    [Export] public bool randomizeScaleEachTween = false;
    [Export] public bool randomizeDurationEachTween = true;

    private int scaleDir = 0;
    private bool isActive;

    private const string ScaleTweenProperty = "scale";

    public override void _Ready()
    {
        activeTimer.Timeout += () => isActive = true;

        scaleDir = GD.Randf() < 0.5 ? 1 : -1;
        StartTween();
    }

    private void StartTween()
    {
        float scaleX = (float)GD.RandRange(minScale.X, maxScale.X);
        float scaleY = (float)GD.RandRange(minScale.Y, maxScale.Y);
        double duration = GD.RandRange(minDuration, maxDuration);
        Vector2 scaleVector = new Vector2(scaleX, scaleY);

        Tween tween = GetTree().CreateTween().BindNode(this);
        tween.TweenProperty(this, ScaleTweenProperty, scaleVector, duration);
        tween.TweenCallback(Callable.From(OnTweenEnded));
    }

    private void OnTweenEnded()
    {
        StartTween();
    }

    public bool CanHurt()
    {
        return isActive;
    }
}

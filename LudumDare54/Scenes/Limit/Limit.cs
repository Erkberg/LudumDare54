using Godot;
using System;

public partial class Limit : Node2D
{
    [Export] public Vector2 minScale = Vector2.One * 0.5f, maxScale = Vector2.One * 1.5f;
    [Export] public double minDuration = 1.33, maxDuration = 3.33;
    [Export] private Area2D area;
    [Export] private Sprite2D sprite;
    [Export] public bool isDashThrough;

    private int scaleDir = 0;

    private const string ScaleTweenProperty = "scale";

    public override void _Ready()
    {
        area.AreaEntered += OnAreaEntered;
        scaleDir = GD.Randf() < 0.5 ? 1 : -1;
        StartTween();

        if(isDashThrough)
        {
            Color selfMod = sprite.SelfModulate;
            selfMod.A = 0.5f;
            sprite.SelfModulate = selfMod;
        }
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
        float scaleX = (float)GD.RandRange(minScale.X, maxScale.X);
        float scaleY = (float)GD.RandRange(minScale.Y, maxScale.Y);
        double duration = GD.RandRange(minDuration, maxDuration);
        Vector2 scaleVector = new Vector2(scaleX, scaleY);

        Tween tween = GetTree().CreateTween().BindNode(this);
        tween.TweenProperty(this, ScaleTweenProperty, scaleVector, 1.0f);
        tween.TweenCallback(Callable.From(OnTweenEnded));
    }

    private void OnTweenEnded()
    {
        StartTween();
    }
}

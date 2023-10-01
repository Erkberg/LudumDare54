using Godot;
using System;

public partial class SpawnAnticipation : Node2D
{
    [Export] private Timer timer;
    [Export] private Sprite2D sprite;

    public Action<Vector2> onSpawn;
    public Type spawnType;

    public enum Type
    {
        Limit,
        Other
    }

    public override void _Ready()
    {
        timer.Timeout += OnTimer;
    }

    public void SetScale(float scale)
    {
        sprite.Scale = Vector2.One * scale;
    }

    private void OnTimer()
    {
        onSpawn?.Invoke(GlobalPosition);
        QueueFree();
    }
}

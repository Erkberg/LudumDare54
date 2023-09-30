using Godot;
using System;

public partial class WorldSpawner : Node2D
{
    [Export] private PackedScene limitScene;
    [Export] private PackedScene otherScene;
    [Export] private Timer limitSpawnTimer;
    [Export] private Timer otherSpawnTimer;
    [Export] private Node2D limitsHolder;
    [Export] private Node2D othersHolder;
    [Export] private Vector2 spawnBounds;
    [Export] private Vector2 limitSpawnOffset;
    [Export] private Vector2 otherSpawnOffset;

    public override void _Ready()
    {
        limitSpawnTimer.Timeout += OnLimitSpawnTimer;
        otherSpawnTimer.Timeout += OnOtherSpawnTimer;
    }

    private void OnLimitSpawnTimer()
    {
        SpawnLimit();
    }

    private void OnOtherSpawnTimer()
    {
        SpawnOther();
    }

    private void SpawnLimit()
    {
        Limit limit = limitScene.Instantiate() as Limit;
        Vector2 spawnPosition = GetRandomSpawnPosition(limitSpawnOffset);
        limitsHolder.AddChild(limit);
        limit.GlobalPosition = spawnPosition;
        limit.isDashThrough = true;
    }

    private void SpawnOther()
    {
        Other other = otherScene.Instantiate() as Other;
        Vector2 spawnPosition = GetRandomSpawnPosition(otherSpawnOffset);
        othersHolder.AddChild(other);
        other.GlobalPosition = spawnPosition;
    }

    private Vector2 GetRandomSpawnPosition(Vector2 innerOffset)
    {
        float x = (float)GD.RandRange(-spawnBounds.X + innerOffset.X, spawnBounds.X - innerOffset.X);
        float y = (float)GD.RandRange(-spawnBounds.Y + innerOffset.Y, spawnBounds.Y - innerOffset.Y);
        Vector2 spawnPosition = new Vector2(x, y);

        return spawnPosition;
    }
}

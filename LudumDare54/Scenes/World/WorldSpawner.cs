using Godot;
using System;

public partial class WorldSpawner : Node2D
{
    [Export] private PackedScene spawnAnticipationScene;
    [Export] private PackedScene limitScene;
    [Export] private PackedScene otherScene;
    [Export] private PackedScene coinScene;
    [Export] private PackedScene heartScene;

    [Export] private Timer limitSpawnTimer;
    [Export] private Timer otherSpawnTimer;
    [Export] private Timer coinSpawnTimer;
    [Export] private Timer heartSpawnTimer;

    [Export] private Node2D anticipationsHolder;
    [Export] private Node2D limitsHolder;
    [Export] private Node2D othersHolder;
    [Export] private Node2D collectablesHolder;

    [Export] private Vector2 spawnBounds;
    [Export] private Vector2 limitSpawnOffset;
    [Export] private Vector2 otherSpawnOffset;
    [Export] private Vector2 collectablesSpawnOffset;

    public override void _Ready()
    {
        limitSpawnTimer.Timeout += OnLimitSpawnTimer;
        otherSpawnTimer.Timeout += OnOtherSpawnTimer;
        coinSpawnTimer.Timeout += OnCoinSpawnTimer;
        //heartSpawnTimer.Timeout += OnHeartSpawnTimer;
    }

    private void OnLimitSpawnTimer()
    {
        SpawnAnticipation(global::SpawnAnticipation.Type.Limit);
    }

    private void OnOtherSpawnTimer()
    {
        SpawnAnticipation(global::SpawnAnticipation.Type.Other);
    }

    private void OnCoinSpawnTimer()
    {
        SpawnCoin();
    }

    private void OnHeartSpawnTimer()
    {
        SpawnHeart();
    }

    private void SpawnAnticipation(SpawnAnticipation.Type type)
    {
        SpawnAnticipation anticipation = spawnAnticipationScene.Instantiate() as SpawnAnticipation;
        Vector2 spawnPosition = GetRandomSpawnPosition(limitSpawnOffset);
        anticipationsHolder.AddChild(anticipation);
        anticipation.GlobalPosition = spawnPosition;
        anticipation.spawnType = type;

        switch (type)
        {
            case global::SpawnAnticipation.Type.Limit:
                anticipation.SetScale(1);
                anticipation.onSpawn += SpawnLimit;
                break;

            case global::SpawnAnticipation.Type.Other:
                anticipation.SetScale(0.133f);
                anticipation.onSpawn += SpawnOther;
                break;
        }
    }

    private void SpawnLimit(Vector2 spawnPosition)
    {
        Game.inst.refs.screenShake.ApplyShake(16f, 16f);
        Limit limit = limitScene.Instantiate() as Limit;
        limitsHolder.AddChild(limit);
        limit.GlobalPosition = spawnPosition;
    }

    private void SpawnOther(Vector2 spawnPosition)
    {
        Game.inst.refs.screenShake.ApplyShake(8f, 16f);
        Other other = otherScene.Instantiate() as Other;
        othersHolder.AddChild(other);
        other.GlobalPosition = spawnPosition;
    }

    private void SpawnCoin()
    {
        Coin coin = coinScene.Instantiate() as Coin;
        Vector2 spawnPosition = GetRandomSpawnPosition(collectablesSpawnOffset);
        collectablesHolder.AddChild(coin);
        coin.GlobalPosition = spawnPosition;
    }

    private void SpawnHeart()
    {
        Heart heart = heartScene.Instantiate() as Heart;
        Vector2 spawnPosition = GetRandomSpawnPosition(collectablesSpawnOffset);
        collectablesHolder.AddChild(heart);
        heart.GlobalPosition = spawnPosition;
    }

    private Vector2 GetRandomSpawnPosition(Vector2 innerOffset)
    {
        float x = (float)GD.RandRange(-spawnBounds.X + innerOffset.X, spawnBounds.X - innerOffset.X);
        float y = (float)GD.RandRange(-spawnBounds.Y + innerOffset.Y, spawnBounds.Y - innerOffset.Y);
        Vector2 spawnPosition = new Vector2(x, y);

        return spawnPosition;
    }
}

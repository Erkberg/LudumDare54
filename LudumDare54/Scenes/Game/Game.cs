using Godot;
using System;

public partial class Game : Node
{
    public static Game inst;

    [Export] public GameInput input;    
    [Export] public GameRefs refs;
    [Export] public GameState state;
    [Export] public GameStats stats;

    public override void _Ready()
    {
        inst = this;
    }

    public void OnPlayerDeath()
    {
        GD.Print("restart game");
        SaveStats();
        Reset();
        GetTree().ReloadCurrentScene();
    }

    private void SaveStats()
    {
        stats.CheckIncreaseHighscoreDuration((float)state.GetTimeElapsed());
        stats.CheckIncreaseHighscoreCoins(state.coins);
    }

    private void Reset()
    {
        state.Reset();
    }
}

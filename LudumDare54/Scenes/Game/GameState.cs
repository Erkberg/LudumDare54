using Godot;
using System;

public partial class GameState : Node
{
    [Export] private Timer timer;

    public float coins;    

    public double GetTimeElapsed()
    {
        return timer.WaitTime - timer.TimeLeft;
    }

    public float GetPlayerHealth()
    {
        return Game.inst.refs.player.GetHealth();
    }

    public void AddCoin()
    {
        coins++;
    }

    public void Reset()
    {
        coins = 0;
        timer.Start();
    }
}

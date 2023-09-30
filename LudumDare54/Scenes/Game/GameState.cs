using Godot;
using System;

public partial class GameState : Node
{
    public float coins;

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
    }
}

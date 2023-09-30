using Godot;
using System;

public partial class GameState : Node
{
    public float GetPlayerHealth()
    {
        return Game.inst.refs.player.GetHealth();
    }
}

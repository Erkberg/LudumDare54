using Godot;
using System;

public partial class Game : Node
{
    public static Game inst;

    [Export] public GameProgress progress;
    [Export] public GameInput input;    
    [Export] public GameRefs refs;

    public override void _Ready()
    {
        GD.Print("game ready");
        inst = this;
    }

    public void OnPlayerDeath()
    {
        GD.Print("restart game!");
        progress.Reset();
        GetTree().ReloadCurrentScene();
    }
}

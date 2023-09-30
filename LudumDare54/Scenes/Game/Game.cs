using Godot;
using System;

public partial class Game : Node
{
    public static Game inst;

    public GameInput input;
    public GameRefs refs;

    public override void _Ready()
    {
        GD.Print("game ready");
        inst = this;

        input = new GameInput();
        refs = new GameRefs();
    }
}

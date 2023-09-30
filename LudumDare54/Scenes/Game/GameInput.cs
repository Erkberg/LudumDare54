using Godot;
using System;

public partial class GameInput : Node
{
    private const string MoveLeft = "MoveLeft";
    private const string MoveRight = "MoveRight";
    private const string MoveUp = "MoveUp";
    private const string MoveDown = "MoveDown";

    public Vector2 GetMove()
    {
        Vector2 moveDir = new Vector2();

        moveDir.X = Input.GetActionStrength(MoveRight) - Input.GetActionStrength(MoveLeft);
        moveDir.Y = Input.GetActionStrength(MoveDown) - Input.GetActionStrength(MoveUp);

        return moveDir;
    }
}

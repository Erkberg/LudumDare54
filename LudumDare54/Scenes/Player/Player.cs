using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export] private float moveSpeed = 160;
    [Export] private float accelerationSmoothing = 16;

    private GameInput input;

    public override void _Ready()
    {
        Game.inst.refs.player = this;
        input = Game.inst.input;
    }

    public override void _Process(double delta)
    {
        Move(delta);
    }

    private void Move(double delta)
    {
        Vector2 direction = input.GetMove().Normalized();
        Vector2 targetVelocity = direction * moveSpeed;
        Velocity = Velocity.Lerp(targetVelocity, (float)(1 - Mathf.Exp(-delta * accelerationSmoothing)));
        MoveAndSlide();
    }
}

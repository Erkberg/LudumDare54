using Godot;
using System;

public partial class HealthBarUI : MarginContainer
{
    private GameState state;

    public override void _Ready()
    {
        state = Game.inst.state;
    }

    public override void _Process(double delta)
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        float healthPercent = state.GetPlayerHealthPercent();
        Vector2 scale = Scale;
        scale.X = healthPercent;
        Scale = scale;
    }
}

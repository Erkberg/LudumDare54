using Godot;
using System;

public partial class IngameHealthUI : MarginContainer
{
    [Export] private Label healthLabel;

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
        float health = state.GetPlayerHealth();
        healthLabel.Text = health.ToString();
    }
}

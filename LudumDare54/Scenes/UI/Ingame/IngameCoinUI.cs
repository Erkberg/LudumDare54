using Godot;
using System;

public partial class IngameCoinUI : MarginContainer
{
    [Export] private Label coinLabel;

    private GameState state;

    public override void _Ready()
    {
        state = Game.inst.state;
    }

    public override void _Process(double delta)
    {
        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        float coins = state.coins;
        coinLabel.Text = coins.ToString();
    }
}

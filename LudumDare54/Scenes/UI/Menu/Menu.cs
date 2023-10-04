using Godot;
using System;

public partial class Menu : MarginContainer
{
    [Export] private Button startButton;
    [Export] private Control container;
    [Export] private Label highscoreTimeLabel;
    [Export] private Label highscoreCoinsLabel;

    public override void _Ready()
    {
        startButton.Pressed += OnStartButtonPressed;
        startButton.GrabFocus();

        highscoreTimeLabel.Text = IngameTimeUI.GetFormattedTimeString(Game.inst.stats.highscoreDuration);
        highscoreCoinsLabel.Text = Game.inst.stats.highscoreCoins.ToString();

        Engine.TimeScale = 0;
    }

    private void OnStartButtonPressed()
    {
        container.Visible = false;

        Engine.TimeScale = 1;
    }
}

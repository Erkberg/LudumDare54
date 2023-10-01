using Godot;
using System;

public partial class IngameTimeUI : MarginContainer
{
    [Export] private Label timeLabel;

    private GameState state;

    private const string SecondsFormat = "00";

    public override void _Ready()
    {
        state = Game.inst.state;
    }

    public override void _Process(double delta)
    {
        UpdateTimeUI();
    }

    private void UpdateTimeUI()
    {
        double timeElapsed = state.GetTimeElapsed();
        timeLabel.Text = GetFormattedTimeString(timeElapsed);
    }

    public static string GetFormattedTimeString(double seconds)
    {
        double minutes = Mathf.Floor(seconds / 60);
        double remainingSeconds = seconds - (minutes * 60);
        string secondsString = Mathf.Floor(remainingSeconds).ToString(SecondsFormat);
        return $"{minutes}:{secondsString}";
    }
}

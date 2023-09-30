using Godot;
using System;

public partial class GameProgress : Node
{
    [Export] private Timer timer;

    public double GetTimeElapsed()
    {
        return timer.WaitTime - timer.TimeLeft;
    }

    public void Reset()
    {
        timer.Start();
    }
}

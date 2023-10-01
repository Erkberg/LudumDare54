using Godot;
using System;

public partial class GameStats : Node
{
    public float highscoreDuration = 0;
    public float highscoreCoins = 0;

    public void CheckIncreaseHighscoreDuration(float newHighscore)
    {
        if(newHighscore > highscoreDuration) {  highscoreDuration = newHighscore; }
    }

    public void CheckIncreaseHighscoreCoins(float newHighscore)
    {
        if (newHighscore > highscoreCoins) { highscoreCoins = newHighscore; }
    }
}

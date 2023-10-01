using Godot;
using System;

public partial class GameAudio : Node
{
    [Export] private AudioStreamPlayer dashPlayer;
    [Export] private AudioStreamPlayer coinPlayer;
    [Export] private AudioStreamPlayer spawnPlayer;
    [Export] private AudioStreamPlayer hurtPlayer;

    public override void _Ready()
    {
        
    }

    public void PlayDashSound()
    {
        dashPlayer.Play();
    }

    public void PlayCoinSound()
    {
        coinPlayer.Play();
    }

    public void PlaySpawnSound()
    {
        spawnPlayer.Play();
    }

    public void PlayHurtSound()
    {
        hurtPlayer.Play();
    }
}

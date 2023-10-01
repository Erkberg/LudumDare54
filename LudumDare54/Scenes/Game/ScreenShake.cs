using Godot;
using System;

public partial class ScreenShake : Node
{
    [Export] private Camera2D camera;

    private RandomNumberGenerator rand = new RandomNumberGenerator();

    private float shakeStrength = 0;
    private float shakeDecayRate = 5;

    public override void _Ready()
    {
        Game.inst.refs.screenShake = this;

        rand.Randomize();
    }

    public void ApplyShake(float strength, float decayRate = 5)
    {
        shakeStrength = strength;
        shakeDecayRate = decayRate;
    }

    public override void _Process(double delta)
    {
        shakeStrength = Mathf.Lerp(shakeStrength, 0, shakeDecayRate * (float)delta);
        if (shakeStrength < 0.1f) 
            shakeStrength = 0;
        camera.Offset = GetRandomOffset((float)delta);
    }
    

    public Vector2 GetRandomOffset(float delta)
    {
	    return new Vector2(
            rand.RandfRange(-shakeStrength, shakeStrength),
            rand.RandfRange(-shakeStrength, shakeStrength)
        );
    }    
}

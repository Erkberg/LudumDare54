using Godot;
using System;

public partial class HealthComponent : Node
{
    [Export] public float maxHealth = 3;

    [Signal] public delegate void DiedEventHandler();

    public float currentHealth;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        CallDeferred(MethodName.CheckDeath);
    }

    private void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            EmitSignal(SignalName.Died);
        }
    }
}

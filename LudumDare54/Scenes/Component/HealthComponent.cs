using Godot;
using System;

public partial class HealthComponent : Node
{
    [Export] public float maxHealth = 3;
    [Export] public float damagePerSecond;

    [Signal] public delegate void DiedEventHandler();

    public float currentHealth;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        currentHealth = maxHealth;
    }

    public override void _Process(double delta)
    {
        TakeDamage(damagePerSecond * (float)delta);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        CallDeferred(MethodName.CheckDeath);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            EmitSignal(SignalName.Died);
        }
    }
}

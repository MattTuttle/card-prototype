using Godot;
using System;

public partial class HealthComponent : Node
{
    private int health;
    private int maxHealth;

    [Export]
    public int Health {
        get { return health; }
        private set { health = value; EmitSignal(SignalName.HealthChanged, value); }
    }

    public int MaxHealth {
        get { return maxHealth; }
        set {
            maxHealth = value;
            if (Health > maxHealth) {
                Health = maxHealth;
                EmitSignal(SignalName.HealthChanged, Health);
            }
        }
    }

    [Signal]
    public delegate void HealthChangedEventHandler(int health);

    [Signal]
    public delegate void DiedEventHandler();

    public override void _Ready()
    {
        maxHealth = health;
    }

    public void Change(int amount)
    {
        Health = Math.Clamp(health + amount, 0, maxHealth);
        if (IsAlive)
        {
            EmitSignal(SignalName.HealthChanged, Health);
        }
        else
        {
            EmitSignal(SignalName.Died);
        }
    }

    public bool IsAlive => Health > 0;
}

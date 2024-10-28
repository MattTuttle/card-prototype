using Godot;
using System;

public partial class HealthComponent : Node
{
    [Signal]
    public delegate void HealthChangedEventHandler(int health);

    [Signal]
    public delegate void DiedEventHandler();

    public bool IsAlive => health > 0;

    // get percentage of remaining health [0-1]
    public float PercentRemaining => maxHealth == 0 ? 0 : (health / (float)maxHealth);

    public int Health {
        get { return health; }
        private set {
            var newHealth = Math.Clamp(value, 0, maxHealth);

            if (health != newHealth)
            {
                health = newHealth;
                if (IsAlive)
                {
                    EmitSignal(SignalName.HealthChanged, Health);
                }
                else
                {
                    EmitSignal(SignalName.Died);
                }
            }
        }
    }

    [Export]
    public int MaxHealth {
        get { return maxHealth; }
        set {
            maxHealth = value;
            if (Health > maxHealth) {
                Health = maxHealth; // change property not private
            }
        }
    }

    private int health;
    private int maxHealth = 1;

    public override void _Ready()
    {
        health = maxHealth;
    }

    public void Damage(int amount)
    {
        // assume negative amounts are positive damage
        Health -= Math.Abs(amount);
    }

    public void FullHeal()
    {
        Health = maxHealth;
    }

}

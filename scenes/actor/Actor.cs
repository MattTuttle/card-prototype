using Godot;
using System;

public partial class Actor : Node2D
{
    private HealthComponent healthComponent;
    private Label healthLabel;

    public override void _Ready()
    {
        healthComponent = GetNode<HealthComponent>("HealthComponent");
        healthLabel = GetNode<Label>("Label");
        healthComponent.HealthChanged += OnHealthChanged;
        OnHealthChanged(healthComponent.Health);
    }

    private void OnHealthChanged(int health)
    {
        healthLabel.Text = $"Health: {health}";
    }
}

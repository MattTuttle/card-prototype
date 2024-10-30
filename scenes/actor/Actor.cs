using Godot;
using Game.Component;

public partial class Actor : Node
{
    public HealthComponent HealthComponent;
    private Label healthLabel;

    public override void _Ready()
    {
        HealthComponent = GetNode<HealthComponent>("HealthComponent");
        healthLabel = GetNode<Label>("Label");
        HealthComponent.HealthChanged += OnHealthChanged;
        OnHealthChanged(HealthComponent.Health);
    }

    public void Damage(int amount)
    {
        HealthComponent.Damage(amount);
    }

    private void OnHealthChanged(int health)
    {
        healthLabel.Text = $"Health: {health}";
    }
}

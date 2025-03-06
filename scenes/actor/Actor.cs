using Godot;
using Game.Component;

public partial class Actor : Control
{
    [Signal]
    public delegate void SelectEventHandler(Actor self);

    private readonly StringName ACTION_ACTION = "action";

    public HealthComponent HealthComponent;
    private Label healthLabel;
    private TextureRect activeMarker;

    public override void _Ready()
    {
        HealthComponent = GetNode<HealthComponent>("HealthComponent");
        healthLabel = GetNode<Label>("%HealthLabel");
        activeMarker = GetNode<TextureRect>("%ActiveMarker");

        HealthComponent.HealthChanged += OnHealthChanged;
        OnHealthChanged(HealthComponent.Health);
    }

    public override void _Input(InputEvent evt)
    {
        if (evt is InputEventMouseButton mouseEvent &&
                mouseEvent.ButtonIndex == MouseButton.Left &&
                mouseEvent.Pressed)
        {
            if (GetGlobalRect().HasPoint(mouseEvent.Position))
            {
                EmitSignal(SignalName.Select, this);
            }
        }
    }

    public void SetActive()
    {
        activeMarker.Visible = true;
    }

    public void SetInactive()
    {
        activeMarker.Visible = false;
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

using Godot;

namespace Game.Resources.Card;

public enum CardActionType
{
    Attack,
    Shield,
    Heal,
}

[GlobalClass]
public partial class CardResource : Resource
{
    [Export]
    public string DisplayName { get; private set; }
    [Export]
    public int Power { get; private set; }
    [Export]
    public CardActionType FinalAction { get; private set; }
}

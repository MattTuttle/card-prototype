using Godot;

namespace Game.Resources.Card;

[GlobalClass]
public partial class CardResource : Resource
{
    [Export]
    public string DisplayName { get; private set; }
    [Export]
    public int Power { get; private set; }
}

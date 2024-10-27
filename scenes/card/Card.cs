using Godot;
using Game.Resources.Card;

namespace Game.Scenes.Card;

public partial class Card : PanelContainer
{
    private CardResource cardResource;

    private Label nameLabel;
    private Label powerLabel;

    public override void _Ready()
    {
        nameLabel = GetNode<Label>("%NameLabel");
        powerLabel = GetNode<Label>("%PowerLabel");
    }

    public override void _Notification(int what)
    {
        if (what == NotificationDragEnd)
        {
            Visible = true;
        }
    }

    public void AssignResource(CardResource cardResource)
    {
        GD.Print(cardResource.DisplayName);
        nameLabel.Text = cardResource.DisplayName;
        powerLabel.Text = $"{cardResource.Power}";
        this.cardResource = cardResource;
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        var preview = this.Duplicate() as Control;
        SetDragPreview(preview);
        this.Visible = false;
        return this;
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        return data.Obj is Card;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        var card = data.As<Card>();
        card.GetParent().RemoveChild(card);
        GetParent().AddChild(card);
    }

}

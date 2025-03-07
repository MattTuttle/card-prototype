using Godot;
using Game.Resources.Card;

namespace Game.Scenes.Card;

public partial class Card : PanelContainer
{
    public CardResource CardResource { get; private set; }

    private Label nameLabel;
    private Label powerLabel;
    private Label actionLabel;

    public override void _Ready()
    {
        nameLabel = GetNode<Label>("%NameLabel");
        powerLabel = GetNode<Label>("%PowerLabel");
        actionLabel = GetNode<Label>("%ActionLabel");
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
        nameLabel.Text = cardResource.DisplayName;
        powerLabel.Text = $"{cardResource.Power}";
        actionLabel.Text = $"Action:\n{cardResource.FinalAction}";
        CardResource = cardResource;
    }

    public void MoveTo(Node newParent)
    {
        GetParent().RemoveChild(this);
        newParent.AddChild(this);
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
        // move to parent of the card we dropped on
        data.As<Card>().MoveTo(GetParent());
    }

}

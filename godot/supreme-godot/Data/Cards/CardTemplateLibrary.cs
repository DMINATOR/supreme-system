using Godot;

public partial class CardTemplateLibrary : Resource
{
    [Export] public CardTemplateResource[] Templates { get; set; } = [];
}

public interface IDragContainer
{
    string DragKey { get; }
    DropContent DraggedContent { get; }
    void SetHighlight(bool highlighted);
}

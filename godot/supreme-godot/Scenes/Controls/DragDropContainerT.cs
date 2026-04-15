#pragma warning disable GD0001
public abstract class DragDropContainer<TContent> : DragDropContainerBase
    where TContent : DropContent
{
    public new TContent DraggedContent => (TContent)base.DraggedContent;

    protected override DropContent CreateDragContent() => OnDragStarted();
    protected override void HandleDragCancelled(DropContent content) => OnDragCancelled((TContent)content);
    protected override void HandleDropReceived(IDragContainer source, DropContent content) => OnDropReceived(source, (TContent)content);
    protected override void HandleDropCompleted(IDragContainer source, DropContent content) => OnDropCompleted(source, (TContent)content);

    protected virtual TContent OnDragStarted() => null;
    protected virtual void OnDragCancelled(TContent content) { }
    protected virtual void OnDropReceived(IDragContainer source, TContent content) { }
    protected virtual void OnDropCompleted(IDragContainer source, TContent content) { }
}
#pragma warning restore GD0001

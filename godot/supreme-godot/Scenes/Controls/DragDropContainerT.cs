using Godot;

public abstract partial class DragDropContainer<TContent> : PanelContainer, IDragContainer
    where TContent : DropContent
{
    private DragBus _dragBus;

    public bool IsEnabled { get; set; }
    public string DragKey { get; set; } = "drag";

    DropContent IDragContainer.DraggedContent => DraggedContent;
    protected virtual bool CanDrag() => false;
    protected virtual bool CanAcceptDrop() => false;
    public TContent DraggedContent { get; private set; }
    public void ClearDraggedContent() => DraggedContent = null;

    public override void _Ready()
    {
        LoadNodes();
        PrepareNodes();
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        if (!IsEnabled || !CanDrag())
            return default;

        _dragBus.ActiveSource = this;
        DraggedContent = OnDragStarted();

        var preview = GetDragPreviewNode();
        if (preview is not null)
            SetDragPreview(preview);

        _dragBus.RaiseDragStarted(this);
        return Variant.From(DragKey);
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        var active = _dragBus.ActiveSource;
        return IsEnabled
            && CanAcceptDrop()
            && active is not null
            && active != (IDragContainer)this
            && data.As<string>() == DragKey
            && CanReceiveDrop(active);
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        var source = _dragBus.ActiveSource;
        _dragBus.ActiveSource = null;
        OnDropReceived(source, (TContent)source.DraggedContent);
        _dragBus.RaiseDragEnded(source, this);
        source.ClearDraggedContent();
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == NotificationDragEnd && _dragBus.ActiveSource == (IDragContainer)this)
        {
            _dragBus.ActiveSource = null;
            OnDragCancelled(DraggedContent);
            ClearDraggedContent();
            _dragBus.RaiseDragCancelled(this);
        }

        if (what == NotificationPredelete)
        {
            _dragBus.DragStarted -= OnDragBusStarted;
            _dragBus.DragCancelled -= OnDragBusCancelled;
            _dragBus.DragEnded -= OnDragBusEnded;
        }
    }

    public void SetHighlight(bool highlighted)
    {
        if (highlighted)
        {
            var style = new StyleBoxFlat();
            style.BgColor = new Color(0, 1, 0, 0.3f);
            AddThemeStyleboxOverride("panel", style);
        }
        else
        {
            RemoveThemeStyleboxOverride("panel");
        }
    }

    protected void UpdateCursor()
    {
        MouseDefaultCursorShape = IsEnabled && CanDrag()
            ? CursorShape.PointingHand
            : CursorShape.Arrow;
    }

    protected virtual Control GetDragPreviewNode() => null;
    protected virtual TContent OnDragStarted() => null;
    protected virtual void OnDragCancelled(TContent content) { }
    protected virtual void OnDropReceived(IDragContainer source, TContent content) { }
    protected virtual void OnDropCompleted(IDragContainer source, TContent content) { }
    protected virtual bool CanReceiveDrop(IDragContainer source) => true;

    private void LoadNodes()
    {
        _dragBus = GetNode<DragBus>(AutoloadPath.DragBus);
    }

    private void PrepareNodes()
    {
        _dragBus.DragStarted += OnDragBusStarted;
        _dragBus.DragCancelled += OnDragBusCancelled;
        _dragBus.DragEnded += OnDragBusEnded;
    }

    private void OnDragBusStarted(IDragContainer source)
    {
        if (source.DragKey == DragKey)
            SetHighlight(true);
    }

    private void OnDragBusCancelled(IDragContainer source)
    {
        if (source.DragKey == DragKey)
            SetHighlight(false);
    }

    private void OnDragBusEnded(IDragContainer source, IDragContainer target)
    {
        SetHighlight(false);

        if (this == target)
            OnDropCompleted(source, (TContent)source.DraggedContent);
    }
}

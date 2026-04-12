using Godot;

public partial class DragDropContainer : PanelContainer
{
    private static DragDropContainer _activeDragSource;

    protected DragBus _dragBus;

    public bool IsEnabled { get; set; }
    public string DragKey { get; set; } = "drag";

    public DropContent Content { get; set; }
    public DropContent DraggedContent { get; private set; }

    public override void _Ready()
    {
        LoadNodes();
        PrepareNodes();
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        if (!IsEnabled || Content is null)
            return default;

        _activeDragSource = this;
        DraggedContent = OnDragStarted();

        var preview = GetDragPreviewNode();
        if (preview is not null)
            SetDragPreview(preview);

        _dragBus.RaiseDragStarted(this);
        return Variant.From(DragKey);
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        return IsEnabled
            && Content is null
            && _activeDragSource is not null
            && _activeDragSource != this
            && data.As<string>() == DragKey
            && CanReceiveDrop(_activeDragSource);
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        var source = _activeDragSource;
        _activeDragSource = null;
        OnDropReceived(source, source.DraggedContent);
        _dragBus.RaiseDragEnded(source, this);
        source.DraggedContent = null;
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == NotificationDragEnd && _activeDragSource == this)
        {
            _activeDragSource = null;
            OnDragCancelled(DraggedContent);
            DraggedContent = null;
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
        MouseDefaultCursorShape = IsEnabled && Content is not null
            ? CursorShape.PointingHand
            : CursorShape.Arrow;
    }

    protected virtual Control GetDragPreviewNode() => null;
    protected virtual DropContent OnDragStarted() => Content;
    protected virtual void OnDragCancelled(DropContent content) { }
    protected virtual void OnDropReceived(DragDropContainer source, DropContent content) { }
    protected virtual void OnDropCompleted(DragDropContainer source, DropContent content) { }
    protected virtual bool CanReceiveDrop(DragDropContainer source) => true;

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

    private void OnDragBusStarted(DragDropContainer source)
    {
        if (source.DragKey == DragKey)
            SetHighlight(true);
    }

    private void OnDragBusCancelled(DragDropContainer source)
    {
        if (source.DragKey == DragKey)
            SetHighlight(false);
    }

    private void OnDragBusEnded(DragDropContainer source, DragDropContainer target)
    {
        SetHighlight(false);

        if (this == target)
            OnDropCompleted(source, source.DraggedContent);
    }
}

using Godot;
using System;

public partial class DragBus : Node
{
    public IDragContainer ActiveSource { get; set; }

    public event Action<IDragContainer> DragStarted;
    public event Action<IDragContainer> DragCancelled;
    public event Action<IDragContainer, IDragContainer> DragEnded;

    public void RaiseDragStarted(IDragContainer source) => DragStarted?.Invoke(source);
    public void RaiseDragCancelled(IDragContainer source) => DragCancelled?.Invoke(source);
    public void RaiseDragEnded(IDragContainer source, IDragContainer target) => DragEnded?.Invoke(source, target);
}

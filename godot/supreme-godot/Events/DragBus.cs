using Godot;
using System;

public partial class DragBus : Node
{
    public event Action<DragDropContainer> DragStarted;
    public event Action<DragDropContainer> DragCancelled;
    public event Action<DragDropContainer, DragDropContainer> DragEnded;

    public void RaiseDragStarted(DragDropContainer source) => DragStarted?.Invoke(source);
    public void RaiseDragCancelled(DragDropContainer source) => DragCancelled?.Invoke(source);
    public void RaiseDragEnded(DragDropContainer source, DragDropContainer target) => DragEnded?.Invoke(source, target);
}

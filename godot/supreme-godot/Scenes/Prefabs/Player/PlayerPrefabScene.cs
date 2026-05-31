using Godot;

public partial class PlayerPrefabScene : Node2D
{
	[Export]
	public float MoveSpeed { get; set; } = 200f;

	private Sprite2D _sprite;
	private Camera2D _camera;

	public override void _Ready()
	{
		LoadNodes();
	}

	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero;

		if (Input.IsActionPressed("ui_right")) velocity.X += 1;
		if (Input.IsActionPressed("ui_left")) velocity.X -= 1;
		if (Input.IsActionPressed("ui_down")) velocity.Y += 1;
		if (Input.IsActionPressed("ui_up")) velocity.Y -= 1;

		if (velocity != Vector2.Zero)
			velocity = velocity.Normalized();

		Position += velocity * MoveSpeed * (float)delta;
	}

	private void LoadNodes()
	{
		_sprite = GetNode<Sprite2D>("Sprite2D");
		_camera = GetNode<Camera2D>("Camera2D");
	}
}

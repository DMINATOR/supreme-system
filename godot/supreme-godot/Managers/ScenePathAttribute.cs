using System;

[AttributeUsage(AttributeTargets.Field)]
public sealed class ScenePathAttribute : Attribute
{
	public string Path { get; }

	public ScenePathAttribute(string path)
	{
		Path = path;
	}
}

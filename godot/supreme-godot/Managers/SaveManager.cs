using Godot;
using System.Text.Json;
using SupremeEngine;

public partial class SaveManager : Node
{
	public const int SlotCount = 3;

	private const string SaveDir = "user://saves";

	public int ActiveSlotIndex { get; private set; } = -1;

	public override void _Ready()
	{
		var dir = DirAccess.Open("user://");
		if (!dir.DirExists("saves"))
			dir.MakeDir("saves");
	}

	public SlotSummary[] GetAllSummaries()
	{
		var summaries = new SlotSummary[SlotCount];
		for (int i = 0; i < SlotCount; i++)
			summaries[i] = GetSlotSummary(i);
		return summaries;
	}

	public SlotSummary GetSlotSummary(int index)
	{
		var path = SlotPath(index);
		if (!FileAccess.FileExists(path))
			return new SlotSummary { Index = index, State = SlotState.Empty };

		var data = LoadSaveData(index);
		if (data == null)
			return new SlotSummary { Index = index, State = SlotState.Empty };

		return new SlotSummary
		{
			Index = index,
			State = SlotState.InProgress,
			Data = data
		};
	}

	public WorldSaveData LoadSaveData(int index)
	{
		var json = GetRawJson(index);
		if (json == null)
			return null;

		return JsonSerializer.Deserialize<WorldSaveData>(json);
	}

	public string GetRawJson(int index)
	{
		var path = SlotPath(index);
		if (!FileAccess.FileExists(path))
			return null;

		using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
		return file.GetAsText();
	}

	public void SaveWorld(int index, WorldSaveData data)
	{
		var path = SlotPath(index);
		var options = new JsonSerializerOptions { WriteIndented = true };
		var json = JsonSerializer.Serialize(data, options);
		using var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
		file.StoreString(json);
	}

	public void DeleteSlot(int index)
	{
		var path = SlotPath(index);
		if (!FileAccess.FileExists(path))
			return;

		var dir = DirAccess.Open(SaveDir);
		dir.Remove($"slot_{index}.json");
	}

	public void SetActiveSlot(int index)
	{
		ActiveSlotIndex = index;
	}

	private static string SlotPath(int index) => $"{SaveDir}/slot_{index}.json";
}

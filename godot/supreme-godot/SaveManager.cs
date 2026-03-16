using Godot;
using System.Text.Json;
using SupremeEngine;

public partial class SaveManager : Node
{
	public const int SlotCount = 3;

	private const string SaveDir = "user://saves";
	private double _sessionStartTime;

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

		var state = LoadWorld(index);
		if (state == null)
			return new SlotSummary { Index = index, State = SlotState.Empty };

		return new SlotSummary
		{
			Index = index,
			State = SlotState.InProgress,
			TotalSecondsPlayed = state.TotalSecondsPlayed
		};
	}

	public WorldState LoadWorld(int index)
	{
		var path = SlotPath(index);
		if (!FileAccess.FileExists(path))
			return null;

		using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
		var json = file.GetAsText();
		return JsonSerializer.Deserialize<WorldState>(json);
	}

	public void SaveWorld(int index, WorldState state)
	{
		var path = SlotPath(index);
		var options = new JsonSerializerOptions { WriteIndented = true };
		var json = JsonSerializer.Serialize(state, options);
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
		_sessionStartTime = Time.GetUnixTimeFromSystem();
	}

	public void AccumulateSessionTime(WorldState state)
	{
		var now = Time.GetUnixTimeFromSystem();
		state.TotalSecondsPlayed += now - _sessionStartTime;
		_sessionStartTime = now;
	}

	public WorldState SnapshotWorld(Bag bag, WorldState current)
	{
		AccumulateSessionTime(current);
		return new WorldState
		{
			TotalSecondsPlayed = current.TotalSecondsPlayed,
			Bag = bag.ToDto()
		};
	}

	public Bag RestoreBag(WorldState state) => Bag.FromDto(state.Bag);

	private static string SlotPath(int index) => $"{SaveDir}/slot_{index}.json";
}

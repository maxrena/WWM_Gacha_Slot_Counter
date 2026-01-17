using System.Text.Json;

namespace WWM_Gacha_SlotCounter_Web.Services;

public class PullRecord
{
    public int PullNumber { get; set; }
    public DateTime Timestamp { get; set; }
    public Dictionary<string, string> SlotColors { get; set; } = new();
    public Dictionary<string, int> SlotCounters { get; set; } = new();
    public string InputSource { get; set; } = "Click"; // "Click" or "Data"
}

public class GachaService
{
    private int totalPulls = 0;
    private int sessionPulls = 0;
    private int sessionCount = 0;
    private Dictionary<string, string?> slotSelections = new();
    private Dictionary<string, int> slotCounters = new();
    private List<PullRecord> pullHistory = new();
    private const string HISTORY_FILE = "pull_history.json";

    public GachaService()
    {
        InitializeSlots();
        LoadPullHistory();
    }

    private void InitializeSlots()
    {
        for (int i = 1; i <= 5; i++)
        {
            string slotName = $"Slot{i}";
            slotSelections[slotName] = null;
            slotCounters[slotName] = 0;
        }
    }

    public void SelectColor(string slotName, string colorName)
    {
        slotSelections[slotName] = colorName;
    }

    public string? GetSlotSelection(string slotName)
    {
        return slotSelections.ContainsKey(slotName) ? slotSelections[slotName] : null;
    }

    public int GetSlotCounter(string slotName)
    {
        return slotCounters.ContainsKey(slotName) ? slotCounters[slotName] : 0;
    }

    public bool AreAllSlotSelected()
    {
        return slotSelections.All(x => x.Value != null);
    }

    public void ConfirmPull(string inputSource = "Click")
    {
        // Update counters based on selections (only for selected slots)
        foreach (var slot in slotSelections.Where(x => x.Value != null))
        {
            string slotName = slot.Key;
            string colorName = slot.Value!;

            if (colorName == "Gold")
            {
                slotCounters[slotName] = 0;
            }
            else if (colorName == "White" || colorName == "Purple")
            {
                slotCounters[slotName]++;
            }
        }

        // Create and save pull record
        totalPulls++;
        sessionPulls++;

        PullRecord record = new PullRecord
        {
            PullNumber = totalPulls,
            Timestamp = DateTime.Now,
            SlotColors = new Dictionary<string, string>(slotSelections.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value!)),
            SlotCounters = new Dictionary<string, int>(slotCounters),
            InputSource = inputSource
        };

        pullHistory.Add(record);
        SavePullHistory();

        // Clear selections for next pull
        ClearSelections();
    }

    public void ConfirmDataPull(Dictionary<string, string?> dataSelections, Dictionary<string, int> dataCounts)
    {
        // Create and save pull record with user-provided counts
        totalPulls++;
        sessionPulls++;

        PullRecord record = new PullRecord
        {
            PullNumber = totalPulls,
            Timestamp = DateTime.Now,
            SlotColors = new Dictionary<string, string>(dataSelections.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value!)),
            SlotCounters = new Dictionary<string, int>(dataCounts.Where(x => dataSelections.ContainsKey(x.Key) && dataSelections[x.Key] != null)),
            InputSource = "Data"
        };

        pullHistory.Add(record);
        SavePullHistory();
    }

    public void ClearSelections()
    {
        for (int i = 1; i <= 5; i++)
        {
            slotSelections[$"Slot{i}"] = null;
        }
    }

    public void BackClick()
    {
        ClearSelections();
    }

    public void ResetAll()
    {
        totalPulls = 0;
        sessionPulls = 0;
        sessionCount = 0;
        pullHistory.Clear();
        InitializeSlots();
        SavePullHistory();
    }

    public int GetTotalPulls()
    {
        return totalPulls;
    }

    public int GetSessionPulls()
    {
        return sessionPulls;
    }

    public double GetAveragePulls()
    {
        return sessionCount > 0 ? (double)totalPulls / sessionCount : 0;
    }

    public List<PullRecord> GetPullHistory()
    {
        return pullHistory.OrderByDescending(x => x.PullNumber).ToList();
    }

    private void SavePullHistory()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(pullHistory, options);
            File.WriteAllText(HISTORY_FILE, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving pull history: {ex.Message}");
        }
    }

    private void LoadPullHistory()
    {
        try
        {
            if (File.Exists(HISTORY_FILE))
            {
                string json = File.ReadAllText(HISTORY_FILE);
                var loaded = JsonSerializer.Deserialize<List<PullRecord>>(json);
                if (loaded != null)
                {
                    pullHistory = loaded;
                    totalPulls = pullHistory.Count;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading pull history: {ex.Message}");
        }
    }
}

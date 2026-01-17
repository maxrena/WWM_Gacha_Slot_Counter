using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WWM_Gacha_SlotCounter;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private int totalPulls = 0;
    private int sessionPulls = 0;
    private int sessionCount = 0;
    private Dictionary<string, string?> slotSelections = new();
    private Dictionary<string, int> slotCounters = new();

    public MainWindow()
    {
        InitializeComponent();
        InitializeSlots();
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

    private void SlotColorClicked(object sender, RoutedEventArgs e)
    {
        Button? button = sender as Button;
        if (button == null) return;

        string? slotName = button.Tag?.ToString();
        if (string.IsNullOrEmpty(slotName)) return;

        string? colorName = null;
        
        if (button.Name.Contains("White"))
            colorName = "White";
        else if (button.Name.Contains("Purple"))
            colorName = "Purple";
        else if (button.Name.Contains("Gold"))
            colorName = "Gold";

        if (colorName == null) return;

        // Store selection
        slotSelections[slotName] = colorName;

        // Update UI - reset all buttons for this slot
        ResetSlotButtons(slotName);

        // Highlight selected button
        button.BorderThickness = new Thickness(3);
        button.BorderBrush = new SolidColorBrush(Colors.Black);

        // Update result text
        TextBlock? resultBlock = FindName($"{slotName}Result") as TextBlock;
        if (resultBlock != null)
        {
            resultBlock.Text = colorName;
            resultBlock.Foreground = new SolidColorBrush(Colors.Green);
        }
    }

    private void SlotCounter_Click(object sender, RoutedEventArgs e)
    {
        Button? button = sender as Button;
        if (button == null) return;

        string? slotName = button.Tag?.ToString();
        if (string.IsNullOrEmpty(slotName)) return;

        // Increment counter for this slot
        slotCounters[slotName]++;
        button.Content = $"Count: {slotCounters[slotName]}";
    }

    private void ResetSlotButtons(string slotName)
    {
        Button?[] buttons = new Button?[3];
        buttons[0] = FindName($"{slotName}White") as Button;
        buttons[1] = FindName($"{slotName}Purple") as Button;
        buttons[2] = FindName($"{slotName}Gold") as Button;

        foreach (var btn in buttons)
        {
            if (btn != null)
            {
                btn.BorderThickness = new Thickness(1);
                btn.BorderBrush = new SolidColorBrush(Color.FromRgb(0x99, 0x99, 0x99));
            }
        }
    }

    private void ConfirmButton_Click(object sender, RoutedEventArgs e)
    {
        bool allSelected = slotSelections.All(x => x.Value != null);
        
        if (!allSelected)
        {
            MessageBox.Show("Please select a color for all slots before confirming!", "Incomplete Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // All slots selected - increment counter
        totalPulls++;
        sessionPulls++;
        UpdateDisplay();
        
        // Clear selections for next pull
        ClearSlotSelections();
        
        MessageBox.Show("✅ Pull confirmed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void ClearSlotSelections()
    {
        slotSelections.Clear();
        slotCounters.Clear();
        InitializeSlots();
        
        for (int i = 1; i <= 5; i++)
        {
            string slotName = $"Slot{i}";
            ResetSlotButtons(slotName);
            
            TextBlock? resultBlock = FindName($"{slotName}Result") as TextBlock;
            if (resultBlock != null)
            {
                resultBlock.Text = "Select";
                resultBlock.Foreground = new SolidColorBrush(Color.FromRgb(0x99, 0x99, 0x99));
            }

            Button? counterButton = FindName($"{slotName}Counter") as Button;
            if (counterButton != null)
            {
                counterButton.Content = "Count: 0";
            }
        }
    }

    private void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        if (sessionPulls > 0)
        {
            sessionCount++;
        }

        sessionPulls = 0;
        ClearSlotSelections();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        CounterDisplay.Text = totalPulls.ToString();
        SessionPulls.Text = sessionPulls.ToString();

        if (sessionCount > 0)
        {
            double average = (double)totalPulls / sessionCount;
            AveragePulls.Text = average.ToString("F1");
        }
        else
        {
            AveragePulls.Text = "0";
        }
    }
}

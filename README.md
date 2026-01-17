# Where Gacha Meets - Slot Counter 🎰

A web application for tracking gacha game pulls with an interactive slot machine interface.

**Made by your beloved Will - MaiMaiYeuEm** ❤️

## Version
2.0.0 - Now Available as Web App! 🚀

## Features

✨ **Interactive Slot Selection**
- 5 customizable slots with color options (White, Purple, Gold)
- Visual feedback for selected colors
- Single-color selection per slot

📊 **Pull Tracking**
- Count total pulls across all sessions
- Track individual slot counters with smart logic:
  - White/Purple: Counter increments on confirm
  - Gold: Counter resets to 0 on confirm
- Session-based statistics
- Average pulls per session calculation

📝 **Pull History & Statistics**
- Detailed pull log showing which color each slot received
- Timestamp for each pull
- Persistent JSON storage of all pull data
- Real-time statistics display

🌐 **Web-Based (v2.0)**
- ASP.NET Core Blazor Server application
- Full-featured web interface matching desktop version
- Interactive real-time updates
- Responsive design for any screen size
- Access from any browser

🔧 **User-Friendly Interface**
- Clean, modern design with intuitive controls
- Color-coded buttons for easy interaction
- Real-time statistics updates
- Confirmation system before recording pulls
- Back button to clear selections without losing counters

## Getting Started

### Prerequisites
- .NET 10.0 SDK or later
- A modern web browser (Chrome, Firefox, Safari, Edge)
- Git (for cloning the repository)

### Installation & Running the Web App (v2.0)

1. **Clone the repository**
   ```bash
   git clone https://github.com/maxrena/WWM_Gacha_Slot_Counter.git
   cd WWM_Gacha_Slot_Counter
   ```

2. **Navigate to the web project**
   ```bash
   cd WWM_Gacha_SlotCounter_Web
   ```

3. **Build and run**
   ```bash
   dotnet run
   ```

4. **Access the application**
   - Open your web browser
   - Go to: `http://localhost:5199`
   - Start tracking your gacha pulls!

### Running the Legacy Desktop App (v1.0)

If you prefer the standalone WPF version:

```bash
cd WWM_Gacha_SlotCounter
dotnet run
```

## How to Use

### Recording a Pull

1. **Select Colors for All Slots**
   - Click on ⚪ White, 🟣 Purple, or 🟡 Gold button for each slot (1-5)
   - Selected color will be highlighted with a bold border
   - Color name appears next to the slot
   - Click the "Count: X" button for any slot to increment its counter
   - Useful for tracking rare results or patterns

3. **Confirm the Pull**
   - Click the ✅ **Confirm** button once all 5 slots have colors selected
   - A success message will appear
   - Counters will automatically reset for the next pull
   - Total pull count increases by 1

4. **Reset Session**
   - Click 🔄 **Reset** to start a new session
   - Previous session data is saved for statistics
   - All selections and counters reset

### Statistics

The app tracks:
- **Total Pulls**: Cumulative count of all confirmed pulls
- **Session Pulls**: Count of pulls in the current session
- **Average per Session**: Total pulls divided by number of sessions

## Project Structure

```
WWM_Gacha_Slot_Counter/
├── WWM_Gacha_SlotCounter/
│   ├── MainWindow.xaml          # UI layout
│   ├── MainWindow.xaml.cs       # Logic and event handlers
│   ├── App.xaml                 # Application configuration
│   ├── App.xaml.cs              # Application code-behind
│   ├── AssemblyInfo.cs          # Assembly information
│   └── WWM_Gacha_SlotCounter.csproj  # Project configuration
├── WWM_Gacha_Slot_Counter.sln   # Solution file
├── .gitignore                   # Git ignore rules
└── README.md                    # This file
```

## Technology Stack

- **Framework**: .NET 10.0
- **UI Framework**: WPF (Windows Presentation Foundation)
- **Language**: C# 13
- **IDE**: Visual Studio 2022 / VS Code

## Building from Source

### Prerequisites for Development
- Visual Studio 2022 or VS Code with C# extension
- .NET 10.0 SDK

### Build Steps
```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run tests (if applicable)
dotnet test

# Publish as standalone executable
dotnet publish -c Release
```

## Configuration

The application uses default WPF settings. To customize:
- Modify colors in `MainWindow.xaml` (HEX color codes)
- Change window size: Update `Height` and `Width` in the Window tag
- Adjust button styling: Edit the ControlTemplate definitions

## Troubleshooting

### App won't start
- Ensure .NET 10.0 is installed: `dotnet --version`
- Check Windows version (Windows 10 or later required)
- Verify you're running from the correct directory

### Colors not displaying correctly
- Update your graphics drivers
- Check DPI settings on your monitor
- Try restarting the application

### Pull count not updating
- Ensure all 5 slots have a color selected before confirming
- Check that the Confirm button is clicked (not just selected)

## Future Enhancements

Potential features for future versions:
- 📈 Pull history with export to CSV/Excel
- 🎨 Customizable color themes
- 💾 Save/load pull sessions
- 📱 Multi-slot configuration options
- 🔔 Notifications for milestone pulls
- 📊 Advanced statistics and analytics

## License

This project is provided as-is for personal use.

## Support & Contact

For issues, suggestions, or contributions, please visit:
- GitHub Repository: https://github.com/maxrena/WWM_Gacha_Slot_Counter
- Create an issue on GitHub for bug reports

## Credits

**Created by**: Will (MaiMaiYeuEm) ❤️

## Changelog

### Version 1.0.0 (Initial Release)
- ✨ Interactive slot color selection (White, Purple, Gold)
- 📊 Pull tracking and session statistics
- 🎨 Modern WPF user interface
- 🔧 Individual slot counters
- ✅ Confirmation system for pull recording

---

Made with ❤️ for gacha enthusiasts everywhere!

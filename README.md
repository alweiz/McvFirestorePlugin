# McvFirestorePlugin

A Firebase Firestore plugin for [MultiCommentViewer](https://github.com/ryu-s/MultiCommentViewer) that enables real-time storage of YouTube Live chat data.

## Features

- **Real-time Data Storage**: Automatically stores YouTube Live comments, user information, and connection events to Firebase Firestore
- **Configurable Collections**: Customize Firestore collection names for different data types
- **User Management**: Tracks and stores YouTube user profiles with automatic updates
- **Connection Logging**: Records connection and disconnection events for analytics
- **Easy Setup**: Simple configuration through MultiCommentViewer's plugin interface

## Installation

### Prerequisites

- MultiCommentViewer (compatible version)
- Firebase project with Firestore enabled
- Firebase service account JSON file
- Visual Studio 2019+ or .NET SDK (for building from source)

### Method 1: Integration with MultiCommentViewer (Recommended)

Since this plugin requires MultiCommentViewer's interface dependencies, the recommended approach is to integrate it directly:

1. **Clone MultiCommentViewer**
   ```bash
   git clone https://github.com/CommentViewerCollection/MultiCommentViewer.git
   cd MultiCommentViewer
   ```

2. **Add the Plugin as Submodule**
   ```bash
   git submodule add https://github.com/alweiz/McvFirestorePlugin.git McvFirestorePlugin
   ```

3. **Update Solution File**
   - Open `MultiCommentViewer.sln` in Visual Studio
   - Add the plugin project: Right-click solution → Add → Existing Project
   - Select `McvFirestorePlugin/McvFirestorePlugin.csproj`

4. **Configure Build Integration**
   - Open `MultiCommentViewer/MultiCommentViewer.csproj`
   - Add the following to the PostBuild target (around line 109):
   ```xml
   :: McvFirestorePluginのファイルを全部持ってくる
   if not exist "$(OutDir)plugins\McvFirestorePlugin" mkdir "$(OutDir)plugins\McvFirestorePlugin"
   copy "$(SolutionDir)\McvFirestorePlugin\bin\$(ConfigurationName)\*" "$(OutDir)plugins\McvFirestorePlugin"
   ```

5. **Build the Solution**
   ```bash
   dotnet build
   ```

### Method 2: Standalone Installation (Advanced)

1. **Download Release**
   - Download from [Releases](../../releases) page
   - Extract `McvFirestorePlugin.dll` and dependencies

2. **Manual Installation**
   - Place all files in `MultiCommentViewer/Output/Debug/plugins/McvFirestorePlugin/`
   - Note: This method may have dependency issues

2. **Firebase Configuration**
   - Create a Firebase project at [Firebase Console](https://console.firebase.google.com/)
   - Enable Firestore Database
   - Generate a service account key (JSON file)
   - Note your Firebase Project ID

3. **Plugin Configuration**
   - Launch MultiCommentViewer
   - Go to Plugin settings and find "Firestore連携"
   - Configure the following settings:
     - **Firebase Project ID**: Your Firebase project ID
     - **Firebase Config JSON Path**: Path to your service account JSON file
     - **Collection Paths**: Customize Firestore collection names (optional)

## Data Structure

The plugin stores data in the following Firestore collections:

### YouTube Live Comments (`youTubeLiveChatMessages`)
```javascript
{
  userIconUrl: string,
  userDisplayName: string,
  messageType: string,
  messageId: string,
  text: string,
  postedAt: timestamp
}
```

### YouTube Users (`youTubeUsers`)
```javascript
{
  id: string,
  iconUrl: string,
  displayName: string,
  postedAt: timestamp
}
```

### Connection Logs (`youTubeLiveConnectionLogs`)
```javascript
{
  text: string,
  messageType: string  // "Connected" or "Disconnected"
}
```

## Configuration Options

| Setting | Description | Default Value |
|---------|-------------|---------------|
| Firebase Project ID | Your Firebase project identifier | "Your project ID" |
| Firebase Config JSON Path | Path to service account JSON | `%APPDATA%/MultiCommentViewer/McvFirestorePlugin/your-project-id-XXXXXXXXXXXX.json` |
| YouTube Live Comment Collection | Firestore collection for chat messages | "youTubeLiveChatMessages" |
| YouTube User Collection | Firestore collection for user data | "youTubeUsers" |
| Connection Log Collection | Firestore collection for connection events | "youTubeLiveConnectionLogs" |

## Security and Privacy

- **Authentication**: Uses Firebase service account for secure access
- **Data Privacy**: Only processes public YouTube Live chat data
- **Local Storage**: Configuration stored locally in MultiCommentViewer settings
- **No Data Collection**: This plugin does not collect personal data beyond public chat information

## Development

### Building from Source

```bash
# Clone the repository
git clone https://github.com/alweiz/McvFirestorePlugin.git
cd McvFirestorePlugin

# Build the project
dotnet build

# Run tests
dotnet test McvFirestorePluginTests/

# The compiled plugin will be in bin/Debug/
```

### Testing

The project includes comprehensive unit tests covering:

- **Options Management**: Configuration serialization/deserialization
- **Plugin Lifecycle**: Loading, initialization, and cleanup
- **Data Validation**: Input validation and error handling

To run tests locally within MultiCommentViewer integration:
```bash
cd MultiCommentViewer
dotnet test McvFirestorePluginTests/
```

### Requirements

- .NET Framework 4.6.2 or later
- Visual Studio 2019+ or .NET SDK

### Dependencies

- Google.Cloud.Firestore (3.8.0)
- Newtonsoft.Json (13.0.3)
- Extended.Wpf.Toolkit (4.6.1)
- Microsoft.Toolkit.Mvvm (7.1.2)
- System.ComponentModel.Composition (8.0.0)

## Troubleshooting

### Common Issues

1. **Plugin not loading**
   - Ensure `McvFirestorePlugin.dll` is in the correct plugins directory
   - Check that all dependencies are available

2. **Firebase connection errors**
   - Verify Firebase project ID is correct
   - Ensure service account JSON file path is valid
   - Check that Firestore is enabled in your Firebase project

3. **Data not appearing in Firestore**
   - Confirm the plugin is enabled in settings
   - Check collection names match your Firestore setup
   - Verify service account has write permissions

### Getting Help

- Check the [Issues](../../issues) page for known problems
- Create a new issue if you encounter a bug
- Include relevant error messages and configuration details

## License

This project is licensed under the Apache License 2.0 - see the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## Acknowledgments

- [MultiCommentViewer](https://github.com/ryu-s/MultiCommentViewer) - The excellent multi-platform comment viewer
- [Firebase](https://firebase.google.com/) - Real-time database platform
- Google Cloud Firestore - NoSQL document database
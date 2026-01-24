# ClarionMarkdownEditor

A modern Markdown file viewer and editor addin for the Clarion IDE. Features a split-pane interface with live preview, syntax highlighting, dark mode, scroll synchronization, and seamless IDE integration.

![Clarion IDE Addin](https://img.shields.io/badge/Clarion-IDE%20Addin-blue)
![.NET Framework 4.8](https://img.shields.io/badge/.NET%20Framework-4.8-purple)
![WebView2](https://img.shields.io/badge/WebView2-Chromium-green)
![License](https://img.shields.io/badge/License-MIT-green)

## Features

- **Split-Pane Editor**: Side-by-side markdown source and live HTML preview
- **Live Preview**: Real-time rendering as you type
- **Syntax Highlighting**: Full code syntax highlighting powered by Highlight.js
  - Custom Clarion language definition included
  - Supports 190+ languages (JavaScript, Python, C#, SQL, etc.)
  - Atom One Dark theme (works in both light and dark modes)
  
  Example usage:
  ```
  Type this in your markdown:
  
  ```clarion
  MyProc PROCEDURE
  CODE
    MESSAGE('Hello from Clarion!')
    RETURN
  ```
  
  And it renders with beautiful syntax highlighting!
  ```
- **Dark Mode**: Toggle between light and dark themes with 🌓/☀️ button
- **Scroll Synchronization**: Bidirectional scroll sync between editor and preview (toggleable)
- **Horizontal Scrolling**: Long lines scroll instead of wrapping
- **Expand/Collapse Preview**: Toggle between split view and full-width preview mode
- **Formatting Toolbar**: Quick buttons for common markdown syntax:
  - Bold, Italic, Inline Code, Code Blocks
  - Headers (H1, H2, H3)
  - Bullet Lists, Numbered Lists
  - Blockquotes, Horizontal Rules
  - Links, Images, Tables
- **File Operations**: New, Open, Save, Save As
- **IDE Integration**: Insert markdown content directly into the active Clarion editor
- **Keyboard Shortcuts**:
  - `Ctrl+Alt+M` - Open Markdown Editor pad
  - `Ctrl+B` - Bold
  - `Ctrl+I` - Italic
- **Dockable Pad**: Can be docked anywhere in the Clarion IDE workspace
- **Remembers Settings**: Last opened folder is saved between sessions
- **Clean UI**: No context menus or distractions

## Requirements

### Runtime (End Users)
- Clarion 11.1 or Clarion 12
- .NET Framework 4.8 or higher
- **Microsoft Edge WebView2 Runtime** (usually pre-installed on Windows 10/11)
  - Download: https://developer.microsoft.com/microsoft-edge/webview2/

### Development (Building from Source)
- Visual Studio 2017 or later (or MSBuild 15+)
- .NET Framework 4.8 SDK
- Clarion IDE installed (for reference DLLs)
- NuGet Package Manager

## Installation

### From Release

1. Download the latest release
2. Copy all files to:
   ```
   {CLARION_PATH}\accessory\addins\MarkdownEditor\
   ```
   Required files:
   - `ClarionMarkdownEditor.dll`
   - `ClarionMarkdownEditor.addin`
   - `Microsoft.Web.WebView2.Core.dll`
   - `Microsoft.Web.WebView2.WinForms.dll`
   - `WebView2Loader.dll`
   - `Resources\highlight.min.js`
   - `Resources\atom-one-dark.min.css`
3. Ensure WebView2 Runtime is installed
4. Restart Clarion IDE

### Building from Source

1. **Clone this repository**
   ```bash
   git clone https://github.com/msarson/ClarionMarkdownEditor.git
   cd ClarionMarkdownEditor
   ```

2. **Restore NuGet packages**
   The project uses `packages.config` to reference:
   - Microsoft.Web.WebView2 (v1.0.2792.45)
   
   Packages will restore automatically on build, or manually:
   ```bash
   nuget restore ClarionMarkdownEditor.sln
   # OR
   dotnet restore ClarionMarkdownEditor.sln
   ```

3. **Update Clarion reference paths**
   
   Edit `ClarionMarkdownEditor\ClarionMarkdownEditor.csproj` and update `HintPath` to match your Clarion installation:
   ```xml
   <HintPath>C:\Clarion\Clarion11.1\bin\ICSharpCode.Core.dll</HintPath>
   <HintPath>C:\Clarion\Clarion11.1\bin\ICSharpCode.SharpDevelop.dll</HintPath>
   ```
   
   Change `C:\Clarion\Clarion11.1` to your Clarion path (e.g., `C:\Clarion12`).

4. **Build in Release configuration**
   ```bash
   dotnet build ClarionMarkdownEditor.sln -c Release
   # OR
   msbuild ClarionMarkdownEditor.sln /p:Configuration=Release
   ```

5. **Deploy to Clarion**
   
   Copy from `ClarionMarkdownEditor\bin\Release\` to `{CLARION_PATH}\accessory\addins\MarkdownEditor\`:
   - `ClarionMarkdownEditor.dll`
   - `ClarionMarkdownEditor.addin`
   - `Microsoft.Web.WebView2.Core.dll`
   - `Microsoft.Web.WebView2.WinForms.dll`
   - `Microsoft.Web.WebView2.Wpf.dll`
   - `WebView2Loader.dll`
   - `Resources\highlight.min.js`
   - `Resources\atom-one-dark.min.css`

6. **Restart Clarion IDE**

## Screenshots

### Split View (Editor + Preview)
```
┌─────────────────────────────────────────────────────────────┐
│ New  Open  Save  Save As │ Insert to IDE │ filename.md     │
├─────────────────────────────────────────────────────────────┤
│ B │ I │ </> │ {} │ Link │ Img │ H1 │ H2 │ H3 │ List │ ...  │
├────────────────────────────┬────────────────────────────────┤
│ MARKDOWN                   │ PREVIEW                [Expand]│
├────────────────────────────┼────────────────────────────────┤
│ # My Document              │ My Document                    │
│                            │ ───────────                    │
│ This is **bold** text.     │ This is bold text.             │
│                            │                                │
│ - Item 1                   │ • Item 1                       │
│ - Item 2                   │ • Item 2                       │
└────────────────────────────┴────────────────────────────────┘
```

### Expanded Preview Mode
```
┌─────────────────────────────────────────────────────────────┐
│ PREVIEW                                              [Split]│
├─────────────────────────────────────────────────────────────┤
│                                                             │
│  My Document                                                │
│  ───────────────────────────────────────────                │
│                                                             │
│  This is bold text.                                         │
│                                                             │
│  • Item 1                                                   │
│  • Item 2                                                   │
│                                                             │
└─────────────────────────────────────────────────────────────┘
```

## Requirements

### Runtime (End Users)
- Clarion 11.1 or Clarion 12
- .NET Framework 4.8 or higher
- **Microsoft Edge WebView2 Runtime** (usually pre-installed on Windows 10/11)
  - Download: https://developer.microsoft.com/microsoft-edge/webview2/

### Development (Building from Source)
- Visual Studio 2017 or later (or MSBuild 15+)
- .NET Framework 4.8 SDK
- Clarion IDE installed (for reference DLLs)
- NuGet Package Manager

## Installation

### From Release

1. Download the latest release
2. Copy all files to:
   ```
   {CLARION_PATH}\accessory\addins\MarkdownEditor\
   ```
   Required files:
   - `ClarionMarkdownEditor.dll`
   - `ClarionMarkdownEditor.addin`
   - `Microsoft.Web.WebView2.Core.dll`
   - `Microsoft.Web.WebView2.WinForms.dll`
   - `WebView2Loader.dll`
   - `Resources\highlight.min.js`
   - `Resources\atom-one-dark.min.css`
3. Ensure WebView2 Runtime is installed
4. Restart Clarion IDE

### Building from Source

1. **Clone this repository**
   ```bash
   git clone https://github.com/msarson/ClarionMarkdownEditor.git
   cd ClarionMarkdownEditor
   ```

2. **Restore NuGet packages**
   The project uses `packages.config` to reference:
   - Microsoft.Web.WebView2 (v1.0.2792.45)
   
   Packages will restore automatically on build, or manually:
   ```bash
   nuget restore ClarionMarkdownEditor.sln
   # OR
   dotnet restore ClarionMarkdownEditor.sln
   ```

3. **Update Clarion reference paths**
   
   Edit `ClarionMarkdownEditor\ClarionMarkdownEditor.csproj` and update `HintPath` to match your Clarion installation:
   ```xml
   <HintPath>C:\Clarion\Clarion11.1\bin\ICSharpCode.Core.dll</HintPath>
   <HintPath>C:\Clarion\Clarion11.1\bin\ICSharpCode.SharpDevelop.dll</HintPath>
   ```
   
   Change `C:\Clarion\Clarion11.1` to your Clarion path (e.g., `C:\Clarion12`).

4. **Build in Release configuration**
   ```bash
   dotnet build ClarionMarkdownEditor.sln -c Release
   # OR
   msbuild ClarionMarkdownEditor.sln /p:Configuration=Release
   ```

5. **Deploy to Clarion**
   
   Copy from `ClarionMarkdownEditor\bin\Release\` to `{CLARION_PATH}\accessory\addins\MarkdownEditor\`:
   - `ClarionMarkdownEditor.dll`
   - `ClarionMarkdownEditor.addin`
   - `Microsoft.Web.WebView2.Core.dll`
   - `Microsoft.Web.WebView2.WinForms.dll`
   - `Microsoft.Web.WebView2.Wpf.dll`
   - `WebView2Loader.dll`
   - `Resources\highlight.min.js`
   - `Resources\atom-one-dark.min.css`

6. **Restart Clarion IDE**

## Usage

### Opening the Editor

- **Keyboard**: Press `Ctrl+Alt+M`
- **Menu**: Go to `Tools → Markdown Editor`

### Editing Markdown

1. Click **Open** to load an existing `.md` file, or start typing in a new document
2. Use the formatting toolbar for quick markdown syntax insertion
3. Preview updates in real-time as you type

### Preview Modes

- **Split View**: Editor and preview side-by-side (default)
- **Expanded Preview**: Click **Expand** button to hide the editor and view preview full-width
- **Return to Split**: Click **Split** button to restore side-by-side view

### Insert to IDE

Click **Insert to IDE** to insert the current markdown content at the cursor position in the active Clarion source editor.

## Supported Markdown Syntax

| Element | Syntax |
|---------|--------|
| Heading 1 | `# Heading` |
| Heading 2 | `## Heading` |
| Heading 3 | `### Heading` |
| Bold | `**bold**` or `__bold__` |
| Italic | `*italic*` or `_italic_` |
| Inline Code | `` `code` `` |
| Code Block | ` ```language ... ``` ` |
| Link | `[text](url)` |
| Image | `![alt](url)` |
| Unordered List | `- item` or `* item` |
| Ordered List | `1. item` |
| Blockquote | `> quote` |
| Horizontal Rule | `---` or `***` |
| Table | `| Col1 | Col2 |` |

## Project Structure

```
MarkDownAddin/
├── ClarionMarkdownEditor.sln
├── addin-config.json
├── README.md
└── ClarionMarkdownEditor/
    ├── ClarionMarkdownEditor.csproj
    ├── ClarionMarkdownEditor.addin      # SharpDevelop addin manifest
    ├── Properties/
    │   └── AssemblyInfo.cs
    ├── MarkdownEditorPad.cs             # Dockable pad container
    ├── MarkdownEditorControl.cs         # Main control with WebBrowser
    ├── MarkdownEditorControl.Designer.cs
    ├── ShowMarkdownEditorCommand.cs     # Tools menu command
    ├── Services/
    │   ├── EditorService.cs             # IDE editor interaction
    │   ├── SettingsService.cs           # User settings persistence
    │   └── ScriptBridge.cs              # JS-to-C# communication
    └── Resources/
        └── markdown-editor.html         # Embedded HTML/JS UI
```

## Technical Details

### Architecture

- **UI Layer**: HTML/CSS/JavaScript in WebView2 (Chromium-based)
- **Modern Browser Engine**: WebView2 provides full modern web standards support
- **Native Toolbar**: WinForms ToolStrip for file operations
- **Markdown Parser**: Custom lightweight parser implemented in JavaScript
- **Syntax Highlighting**: Highlight.js 11.9.0 with custom Clarion language definition
- **IDE Integration**: Uses reflection for compatibility across Clarion IDE versions

### Why WebView2?

Migrated from old IE-based WebBrowser to WebView2 (Chromium) to enable:
- Modern JavaScript support (ES6+)
- Proper execution of minified libraries
- Full CSS3 support including flexbox and grid
- Better performance and security
- Syntax highlighting with Highlight.js

### Syntax Highlighting Implementation

- **Library**: Highlight.js 11.9.0
- **Theme**: Atom One Dark
- **Injection**: C#-based file injection (no CDN dependencies, works offline)
- **Custom Language**: Full Clarion language definition from [discourse-highlightjs-clarion](https://github.com/msarson/discourse-highlightjs-clarion)
- **Files**: `highlight.min.js` (121KB) and `atom-one-dark.min.css` (856 bytes)

### Settings Storage

User settings are stored in:
```
%APPDATA%\ClarionMarkdownEditor\settings.txt
```

## Development Notes

### Key Commits

1. **Initial commit**: Basic markdown editor with WebBrowser control
2. **Dark mode & UI enhancements**: Added dark mode, scroll sync, horizontal scrolling
3. **WebView2 migration**: Replaced IE WebBrowser with Chromium WebView2
4. **Syntax highlighting**: Integrated Highlight.js with custom Clarion support

### Known Limitations

- **WebView2 Runtime Required**: Users must have WebView2 Runtime installed (typically pre-installed on Windows 10/11)
- **32-bit Only**: Built for x86 to match Clarion IDE architecture
- **Print Feature**: Print styles included but pagination needs work

### Future Enhancements

- Multi-page print support
- Export to PDF
- Markdown templates
- Spell checking
- Find/Replace in editor

## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.

## License

MIT License - see LICENSE file for details.

## Author

msarson (fork with WebView2 enhancements)

Original author: John Hickey

## Acknowledgments

- Built for the Clarion IDE (SharpDevelop-based)
- Inspired by popular markdown editors like Typora and Mark Text
- Custom Clarion syntax highlighting from [discourse-highlightjs-clarion](https://github.com/msarson/discourse-highlightjs-clarion)


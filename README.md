# ClarionMarkdownEditor

A Markdown file viewer and editor addin for the Clarion IDE. Features a split-pane interface with live preview, formatting toolbar, and seamless IDE integration.

![Clarion IDE Addin](https://img.shields.io/badge/Clarion-IDE%20Addin-blue)
![.NET Framework 4.0](https://img.shields.io/badge/.NET%20Framework-4.0-purple)
![License](https://img.shields.io/badge/License-MIT-green)

## Features

- **Split-Pane Editor**: Side-by-side markdown source and live HTML preview
- **Live Preview**: Real-time rendering as you type
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

- Clarion 11 or Clarion 12
- .NET Framework 4.0

## Installation

### From Release

1. Download the latest release
2. Copy `ClarionMarkdownEditor.dll` and `ClarionMarkdownEditor.addin` to:
   ```
   {CLARION_PATH}\accessory\addins\MarkdownAddin\
   ```
3. Restart Clarion IDE

### Building from Source

1. Clone this repository
2. Open `ClarionMarkdownEditor.sln` in Visual Studio
3. Update the reference paths in the `.csproj` if your Clarion installation differs:
   ```xml
   <HintPath>C:\Clarion12\bin\ICSharpCode.Core.dll</HintPath>
   <HintPath>C:\Clarion12\bin\ICSharpCode.SharpDevelop.dll</HintPath>
   ```
4. Build in Release configuration
5. Copy output files to the Clarion addins folder

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

- **UI Layer**: HTML/CSS/JavaScript embedded in a WebBrowser control
- **Native Toolbar**: WinForms ToolStrip for file operations (more reliable than JS-to-C# calls)
- **Markdown Parser**: Custom lightweight parser implemented in JavaScript
- **IDE Integration**: Uses reflection for compatibility across Clarion IDE versions

### Why WebBrowser Control?

The WebBrowser control allows for rich HTML rendering of the markdown preview, with proper styling for:
- Headers with border separators
- Syntax-highlighted code blocks
- Styled tables, blockquotes, and lists
- Responsive layout

### Settings Storage

User settings are stored in:
```
%APPDATA%\ClarionMarkdownEditor\settings.txt
```

## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.

## License

MIT License - see LICENSE file for details.

## Author

John Hickey

## Acknowledgments

- Built for the Clarion IDE (SharpDevelop-based)
- Inspired by popular markdown editors like Typora and Mark Text

```clarion
     MyProc PROCEDURE
     CODE
       MESSAGE('Hello from Clarion!')
       RETURN
```
```javascript
function test() {
  console.log('Hello');
}

```

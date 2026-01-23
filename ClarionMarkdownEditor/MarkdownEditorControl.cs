using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ClarionMarkdownEditor.Services;

namespace ClarionMarkdownEditor
{
    /// <summary>
    /// Main user control for the Markdown Editor addin.
    /// Uses WebBrowser control to display a split-pane markdown editor with live preview.
    /// </summary>
    public partial class MarkdownEditorControl : UserControl
    {
        private readonly EditorService _editorService;
        private readonly SettingsService _settingsService;
        private string _currentFilePath;

        public MarkdownEditorControl()
        {
            InitializeComponent();
            _editorService = new EditorService();
            _settingsService = new SettingsService();

            InitializeWebBrowser();
        }

        private void InitializeWebBrowser()
        {
            try
            {
                // Load the embedded HTML resource
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "ClarionMarkdownEditor.Resources.markdown-editor.html";

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            string html = reader.ReadToEnd();
                            webBrowser.DocumentText = html;
                        }
                    }
                    else
                    {
                        webBrowser.DocumentText = GetFallbackHtml();
                    }
                }
            }
            catch (Exception ex)
            {
                webBrowser.DocumentText = $"<html><body><h3>Error loading editor</h3><p>{ex.Message}</p></body></html>";
            }
        }

        private string GetFallbackHtml()
        {
            return @"<!DOCTYPE html>
<html>
<head>
    <style>
        body { font-family: Segoe UI, Arial, sans-serif; padding: 20px; }
        .error { color: #c00; }
    </style>
</head>
<body>
    <h3 class='error'>Resource not found</h3>
    <p>The markdown editor HTML resource could not be loaded.</p>
</body>
</html>";
        }

        #region Toolbar Button Handlers

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewMarkdownFile();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenMarkdownFile();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveMarkdownFile();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveMarkdownFileAs();
        }

        private void btnInsertToIDE_Click(object sender, EventArgs e)
        {
            InsertToIdeEditor();
        }

        #endregion

        #region File Operations

        private void NewMarkdownFile()
        {
            _currentFilePath = null;
            lblFileName.Text = "Untitled";
            SendContentToJs("", "Untitled");
        }

        private void OpenMarkdownFile()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Markdown files (*.md)|*.md|Text files (*.txt)|*.txt|All files (*.*)|*.*";
                dialog.Title = "Open Markdown File";

                // Try to get last folder from settings
                var lastFolder = _settingsService.Get("LastOpenFolder");
                if (!string.IsNullOrEmpty(lastFolder) && Directory.Exists(lastFolder))
                {
                    dialog.InitialDirectory = lastFolder;
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _currentFilePath = dialog.FileName;
                    _settingsService.Set("LastOpenFolder", Path.GetDirectoryName(dialog.FileName));

                    string content = File.ReadAllText(dialog.FileName);
                    lblFileName.Text = Path.GetFileName(dialog.FileName);
                    lblFileName.ToolTipText = dialog.FileName;
                    SendContentToJs(content, dialog.FileName);
                }
            }
        }

        private void SaveMarkdownFile()
        {
            if (string.IsNullOrEmpty(_currentFilePath))
            {
                SaveMarkdownFileAs();
                return;
            }

            string content = GetEditorContent();
            if (content != null)
            {
                File.WriteAllText(_currentFilePath, content);
                SendMessageToJs("fileSaved", _currentFilePath);
            }
        }

        private void SaveMarkdownFileAs()
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Markdown files (*.md)|*.md|Text files (*.txt)|*.txt|All files (*.*)|*.*";
                dialog.Title = "Save Markdown File";
                dialog.DefaultExt = "md";

                if (!string.IsNullOrEmpty(_currentFilePath))
                {
                    dialog.InitialDirectory = Path.GetDirectoryName(_currentFilePath);
                    dialog.FileName = Path.GetFileName(_currentFilePath);
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _currentFilePath = dialog.FileName;
                    string content = GetEditorContent();
                    if (content != null)
                    {
                        File.WriteAllText(dialog.FileName, content);
                        lblFileName.Text = Path.GetFileName(dialog.FileName);
                        lblFileName.ToolTipText = dialog.FileName;
                        SendMessageToJs("fileSaved", dialog.FileName);
                    }
                }
            }
        }

        private void InsertToIdeEditor()
        {
            string content = GetEditorContent();
            if (content != null)
            {
                var result = _editorService.InsertTextAtCaret(content);
                if (!result.Success)
                {
                    MessageBox.Show($"Could not insert text: {result.ErrorMessage}",
                        "Markdown Editor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        #endregion

        #region JavaScript Communication

        /// <summary>
        /// Gets the current content from the JavaScript editor.
        /// </summary>
        private string GetEditorContent()
        {
            if (webBrowser.Document != null)
            {
                try
                {
                    var result = webBrowser.Document.InvokeScript("getEditorContent");
                    return result as string;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        private void SendContentToJs(string content, string fileName)
        {
            // InvokeScript passes parameters directly - no string escaping needed
            // Just normalize line endings to \n for JavaScript
            string normalized = content.Replace("\r\n", "\n").Replace("\r", "\n");
            InvokeScript("loadContent", normalized, fileName);
        }

        private void SendMessageToJs(string messageType, string data)
        {
            InvokeScript("receiveMessage", messageType, data);
        }

        private void InvokeScript(string functionName, params string[] args)
        {
            if (webBrowser.Document != null)
            {
                try
                {
                    webBrowser.Document.InvokeScript(functionName, args);
                }
                catch
                {
                    // Script may not be ready yet
                }
            }
        }

        #endregion

        public void RefreshContent()
        {
            // Reload if we have a current file
            if (!string.IsNullOrEmpty(_currentFilePath) && File.Exists(_currentFilePath))
            {
                string content = File.ReadAllText(_currentFilePath);
                SendContentToJs(content, _currentFilePath);
            }
        }
    }
}

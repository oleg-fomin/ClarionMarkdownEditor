using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClarionMarkdownEditor.Services;

namespace ClarionMarkdownEditor
{
    /// <summary>
    /// Main user control for the Markdown Editor addin.
    /// Uses WebView2 control to display a split-pane markdown editor with live preview.
    /// </summary>
    public partial class MarkdownEditorControl : UserControl
    {
        private readonly EditorService _editorService;
        private readonly SettingsService _settingsService;
        private string _currentFilePath;
        private bool _isWebView2Ready = false;

        public MarkdownEditorControl()
        {
            InitializeComponent();
            _editorService = new EditorService();
            _settingsService = new SettingsService();

            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            try
            {
                // Initialize WebView2
                await webView.EnsureCoreWebView2Async(null);
                _isWebView2Ready = true;
                
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
                            
                            // Inject highlight.js from external files
                            html = InjectHighlightJs(html);
                            
                            // Debug: Save injected HTML to temp file
                            try
                            {
                                var tempPath = Path.Combine(Path.GetTempPath(), "markdown-editor-debug.html");
                                File.WriteAllText(tempPath, html);
                                System.Diagnostics.Debug.WriteLine($"DEBUG: HTML saved to {tempPath}");
                            }
                            catch { }
                            
                            webView.NavigateToString(html);
                        }
                    }
                    else
                    {
                        webView.NavigateToString(GetFallbackHtml());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing WebView2: {ex.Message}\n\nMake sure WebView2 Runtime is installed.", 
                    "WebView2 Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string InjectHighlightJs(string html)
        {
            var injectionLog = "INJECTION DEBUG: ";
            try
            {
                // Get the directory where the addin is deployed
                var addinDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                injectionLog += $"addinDir={addinDir}; ";
                
                // Load highlight.js CSS
                var cssPath = Path.Combine(addinDir, "Resources", "atom-one-dark.min.css");
                injectionLog += $"cssPath={cssPath}; ";
                if (File.Exists(cssPath))
                {
                    var css = File.ReadAllText(cssPath);
                    html = html.Replace("<!-- INJECT_HIGHLIGHTJS_CSS -->", $"<style>\n{css}\n    </style>");
                    injectionLog += "CSS injected; ";
                }
                else
                {
                    injectionLog += "CSS NOT FOUND; ";
                }
                
                // Load highlight.js JavaScript
                var jsPath = Path.Combine(addinDir, "Resources", "highlight.min.js");
                injectionLog += $"jsPath={jsPath}; ";
                if (File.Exists(jsPath))
                {
                    var js = File.ReadAllText(jsPath);
                    // Don't escape - the script block should handle it
                    // Just inject it as-is
                    html = html.Replace("<!-- INJECT_HIGHLIGHTJS_JS -->", $"<script>\n{js}\n    </script>");
                    injectionLog += "JS injected; ";
                }
                else
                {
                    injectionLog += "JS NOT FOUND; ";
                }
                
                // Inject Clarion language definition
                var clarionLang = GetClarionLanguageDefinition();
                html = html.Replace("<!-- INJECT_CLARION_LANG -->", $"<script>\n{clarionLang}\n    </script>");
                injectionLog += "Clarion lang injected; ";
                
                // Add diagnostic message in HTML
                html = html.Replace("</body>", $"<!-- {injectionLog} -->\n</body>");
                
                return html;
            }
            catch (Exception ex)
            {
                // If injection fails, add error comment
                injectionLog += $"EXCEPTION: {ex.Message}; ";
                html = html.Replace("</body>", $"<!-- {injectionLog} -->\n</body>");
                return html;
            }
        }

        private string GetClarionLanguageDefinition()
        {
            return @"
        // Clarion language definition for Highlight.js
        // Based on https://github.com/msarson/discourse-highlightjs-clarion
        hljs.registerLanguage('clarion', function(hljs) {
            const STRING_LITERAL = {
                className: 'string',
                begin: ""'"",
                end: ""'""
            };

            const COMMENTS = {
                className: 'comment',
                begin: '(\\!|\\|)',
                end: '$'
            };

            const NUMERIC_LITERALS = {
                className: 'number',
                begin: '\\b\\d+(\\.\\d+)?'
            };

            const LABELS = {
                className: 'label',
                begin: '^[A-Za-z_][A-Za-z0-9_]*(?::[A-Za-z0-9_]+)?',
                end: '(?=\\s)',
            };

            const CLASS_LABELS = {
                className: 'title class-',
                begin: '[A-Za-z_][A-Za-z0-9_]*\\.[A-Za-z_][A-Za-z0-9_]*',
                end: '(?=\\s)',
                relevance: 10
            };

            const BASE_TYPES = {
                className: 'type',
                begin: '\\b(?i:ANY|ASTRING|BOOL|BYTE|CSTRING|DATE|DECIMAL|DOUBLE|FLOAT4|LONG|PSTRING|REAL|SHORT|SIGNED|STRING|TIME|ULONG|UNSIGNED|USHORT)\\b'
            };

            const SPECIAL_TYPES = {
                className: 'type',
                begin: '\\b(?i:FILE|QUEUE|GROUP|ARRAY)\\b'
            };

            const ATTRIBUTES = {
                className: 'attr',
                begin: '\\b(?i:AT|AUTO|BINDABLE|BOXED|CENTER|COLOR|COLUMN|DEFAULT|DIM|DLL|DRIVER|DROP|ICON|INNER|LENGTH|MASK|NAME|ORDER|OVER|PAGE|PRIVATE|PROTECTED|PUBLIC|REQ|SCROLL|STATIC|TIP|USE|VALUE|VERTICAL|WIDTH)\\b'
            };

            const OPERATORS = {
                className: 'operator',
                begin: '(\\+|\\-|\\*|\\/|=|<>|<=|>=|~=|%|\\+=|\\-=|\\*=|\\\\=|:=:|~>|~<|\\^)'
            };

            const PUNCTUATION = {
                className: 'punctuation',
                begin: '[.,;]'
            };

            const CONSTANTS = {
                className: 'number',
                variants: [
                    { begin: '0[xX][0-9a-fA-F]+[hH]?' },
                    { begin: '[01]+[bB]' },
                    { begin: '[0-7]+[oO]' }
                ]
            };

            const PICTURE_NUMERIC_FORMAT = {
                className: 'string',
                begin: '@[Nn][\\-]?[0-9\\.]*\\~',
                end: '\\~'
            };

            const PREPROCESSOR_DIRECTIVES = {
                className: 'meta',
                begin: '#(pragma|include)',
                end: '$'
            };

            const HARD_RESERVED_KEYWORDS = {
                className: 'keyword',
                begin: '\\b(?i:ACCEPT|AND|BREAK|BY|CASE|CHOOSE|CYCLE|DO|ELSE|ELSIF|END|EXECUTE|EXIT|FUNCTION|GOTO|IF|LOOP|MEMBER|NEW|NOT|OF|OR|OROF|PARENT|PROCEDURE|PROGRAM|RETURN|ROUTINE|SELF|THEN|TIMES|TO|UNTIL|WHILE)\\b'
            };

            const SOFT_RESERVED_KEYWORDS = {
                className: 'keyword',
                begin: '\\b(?i:APPLICATION|CLASS|CODE|DATA|DETAIL|ENUM|FILE|FOOTER|FORM|GROUP|HEADER|INLINE|ITEM|JOIN|MAP|MENU|MENUBAR|MODULE|OLECONTROL|OPTION|QUEUE|RECORD|REPORT|ROW|SHEET|TAB|TABLE|TOOLBAR|VIEW|WINDOW|PROPERTY|INDEXER)\\b'
            };

            const LANG_FUNCTIONS = {
                className: 'built_in',
                begin: '\\b(?i:ADD|DISPOSE|ADDRESS|GET|PUT|OPEN|CLOSE|LOCK|UNLOCK|MESSAGE|CLEAR|FREE|SET|SEND|POST|FILEERROR|FILEERRORCODE|RANDOM|DAY|YEAR|MONTH|INSTRING|MATCH|LEN|UPPER|LOWER|LEFT|RIGHT|SUB|TODAY|FORMAT|INT|ABS)\\b'
            };

            const PROCEDURE_KEYWORD = {
                className: 'title function_',
                begin: '\\bPROCEDURE\\b'
            };

            return {
                name: 'Clarion',
                aliases: ['clarion', 'Clarion', 'CLARION', 'clw'],
                case_insensitive: true,
                contains: [
                    STRING_LITERAL,
                    COMMENTS,
                    NUMERIC_LITERALS,
                    CONSTANTS,
                    LABELS,
                    CLASS_LABELS,
                    BASE_TYPES,
                    SPECIAL_TYPES,
                    ATTRIBUTES,
                    HARD_RESERVED_KEYWORDS,
                    SOFT_RESERVED_KEYWORDS,
                    LANG_FUNCTIONS,
                    PROCEDURE_KEYWORD,
                    OPERATORS,
                    PUNCTUATION,
                    PREPROCESSOR_DIRECTIVES,
                    PICTURE_NUMERIC_FORMAT
                ]
            };
        });";
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
            if (_isWebView2Ready)
            {
                try
                {
                    var task = webView.ExecuteScriptAsync("getEditorContent()");
                    task.Wait();
                    var result = task.Result;
                    // Remove surrounding quotes from JSON string
                    if (result.StartsWith("\"") && result.EndsWith("\""))
                    {
                        result = result.Substring(1, result.Length - 2);
                        result = result.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\\"", "\"");
                    }
                    return result;
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
            // Normalize line endings to \n for JavaScript
            string normalized = content.Replace("\r\n", "\n").Replace("\r", "\n");
            InvokeScript("loadContent", normalized, fileName);
        }

        private void SendMessageToJs(string messageType, string data)
        {
            InvokeScript("receiveMessage", messageType, data);
        }

        private async void InvokeScript(string functionName, params string[] args)
        {
            if (_isWebView2Ready)
            {
                try
                {
                    // Build JavaScript call with arguments - properly escape strings
                    var escapedArgs = Array.ConvertAll(args, arg => {
                        arg = arg.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r");
                        return $"\"{arg}\"";
                    });
                    var argsStr = string.Join(",", escapedArgs);
                    await webView.ExecuteScriptAsync($"{functionName}({argsStr})");
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

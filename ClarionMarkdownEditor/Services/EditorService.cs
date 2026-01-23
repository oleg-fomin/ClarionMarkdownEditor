using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionMarkdownEditor.Services
{
    public class InsertResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public static InsertResult Succeeded() => new InsertResult { Success = true };
        public static InsertResult Failed(string message) => new InsertResult { Success = false, ErrorMessage = message };
    }

    /// <summary>
    /// Service to interact with the active text editor in the Clarion IDE.
    /// Uses reflection for version compatibility.
    /// </summary>
    public class EditorService
    {
        public bool HasActiveTextEditor()
        {
            try { return GetActiveTextArea() != null; }
            catch { return false; }
        }

        public string GetActiveDocumentContent()
        {
            try
            {
                var textArea = GetActiveTextArea();
                if (textArea == null) return null;

                var document = GetProperty(textArea, "Document");
                if (document == null) return null;

                return (GetProperty(document, "TextContent") ?? GetProperty(document, "Text")) as string;
            }
            catch { return null; }
        }

        public string GetActiveDocumentPath()
        {
            try
            {
                var workbench = WorkbenchSingleton.Workbench;
                if (workbench == null) return null;

                var activeWindow = GetProperty(workbench, "ActiveWorkbenchWindow") ?? GetProperty(workbench, "ActiveContent");
                if (activeWindow == null) return null;

                var viewContent = GetProperty(activeWindow, "ViewContent") ?? GetProperty(activeWindow, "ActiveViewContent") ?? activeWindow;
                return (GetProperty(viewContent, "FileName") ?? GetProperty(viewContent, "PrimaryFileName")) as string;
            }
            catch { return null; }
        }

        public InsertResult InsertTextAtCaret(string text)
        {
            try
            {
                var textArea = GetActiveTextArea();
                if (textArea == null) return InsertResult.Failed("No active text editor");

                var document = GetProperty(textArea, "Document");
                var caret = GetProperty(textArea, "Caret");
                if (document == null || caret == null) return InsertResult.Failed("Cannot access editor");

                var offset = (int)GetProperty(caret, "Offset");
                var insertMethod = document.GetType().GetMethod("Insert", new[] { typeof(int), typeof(string) });
                if (insertMethod == null) return InsertResult.Failed("Insert method not found");

                insertMethod.Invoke(document, new object[] { offset, text });
                SetProperty(caret, "Offset", offset + text.Length);

                try { textArea.GetType().GetMethod("Invalidate", Type.EmptyTypes)?.Invoke(textArea, null); } catch { }
                return InsertResult.Succeeded();
            }
            catch (Exception ex) { return InsertResult.Failed(ex.Message); }
        }

        private object GetActiveTextArea()
        {
            var workbench = WorkbenchSingleton.Workbench;
            if (workbench == null) return null;

            var activeWindow = GetProperty(workbench, "ActiveWorkbenchWindow") ?? GetProperty(workbench, "ActiveContent");
            if (activeWindow == null) return null;

            var viewContent = GetProperty(activeWindow, "ViewContent") ?? GetProperty(activeWindow, "ActiveViewContent") ?? activeWindow;

            // Try TextEditorControl
            var textEditor = GetProperty(viewContent, "TextEditorControl");
            if (textEditor != null)
            {
                var result = GetTextAreaFromEditor(textEditor);
                if (result != null) return result;
            }

            // Try Control property
            var control = GetProperty(viewContent, "Control");
            if (control != null)
            {
                var result = GetTextAreaFromEditor(control);
                if (result != null) return result;
                if (control is Control wc)
                {
                    result = FindTextAreaInControls(wc);
                    if (result != null) return result;
                }
            }

            // Try SecondaryViewContents (Clarion Embeditor)
            var secondary = GetProperty(viewContent, "SecondaryViewContents") as System.Collections.IEnumerable;
            if (secondary != null)
            {
                foreach (var svc in secondary)
                {
                    if (GetProperty(svc, "Control") is Control wc)
                    {
                        var result = FindTextAreaInControls(wc);
                        if (result != null) return result;
                    }
                }
            }
            return null;
        }

        private object GetTextAreaFromEditor(object editor)
        {
            if (editor == null) return null;
            var tac = GetProperty(editor, "ActiveTextAreaControl");
            if (tac != null)
            {
                var ta = GetProperty(tac, "TextArea");
                if (ta != null && GetProperty(ta, "Document") != null && GetProperty(ta, "Caret") != null) return ta;
            }
            if (GetProperty(editor, "Document") != null && GetProperty(editor, "Caret") != null) return editor;
            return null;
        }

        private object FindTextAreaInControls(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                var result = GetTextAreaFromEditor(child) ?? FindTextAreaInControls(child);
                if (result != null) return result;
            }
            return null;
        }

        private object GetProperty(object obj, string name)
        {
            if (obj == null) return null;
            try
            {
                var prop = obj.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
                if (prop != null) return prop.GetValue(obj, null);
                var field = obj.GetType().GetField(name, BindingFlags.Public | BindingFlags.Instance);
                return field?.GetValue(obj);
            }
            catch { return null; }
        }

        private void SetProperty(object obj, string name, object value)
        {
            try
            {
                var prop = obj?.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
                if (prop?.CanWrite == true) prop.SetValue(obj, value, null);
            }
            catch { }
        }
    }
}

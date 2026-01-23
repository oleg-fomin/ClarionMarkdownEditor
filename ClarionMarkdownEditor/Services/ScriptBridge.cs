using System;
using System.Runtime.InteropServices;

namespace ClarionMarkdownEditor.Services
{
    /// <summary>
    /// Bridge for JavaScript to C# communication via WebBrowser.ObjectForScripting.
    /// Call from JS: window.external.PerformAction(action, data)
    /// </summary>
    [ComVisible(true)]
    public class ScriptBridge
    {
        private readonly Action<string, string> _onAction;

        public ScriptBridge(Action<string, string> onAction)
        {
            _onAction = onAction;
        }

        /// <summary>
        /// Called from JavaScript to perform an action in C#.
        /// </summary>
        /// <param name="action">The action name (e.g., "openFile", "saveFile", "insertToEditor")</param>
        /// <param name="data">Optional data payload (JSON string or plain text)</param>
        public void PerformAction(string action, string data)
        {
            _onAction?.Invoke(action ?? "", data ?? "");
        }

        /// <summary>
        /// Overload for actions without data.
        /// </summary>
        public void PerformAction(string action)
        {
            PerformAction(action, "");
        }
    }
}

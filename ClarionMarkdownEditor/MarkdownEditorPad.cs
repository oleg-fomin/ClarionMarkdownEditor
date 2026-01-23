using System;
using System.Windows.Forms;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionMarkdownEditor
{
    /// <summary>
    /// Dockable pad for Markdown file viewer and editor for Clarion IDE.
    /// </summary>
    public class MarkdownEditorPad : AbstractPadContent
    {
        private MarkdownEditorControl _control;

        public override Control Control
        {
            get
            {
                if (_control == null)
                {
                    _control = new MarkdownEditorControl();
                }
                return _control;
            }
        }

        public override void Dispose()
        {
            if (_control != null)
            {
                _control.Dispose();
                _control = null;
            }
            base.Dispose();
        }

        public override void RedrawContent()
        {
            _control?.Refresh();
        }
    }
}

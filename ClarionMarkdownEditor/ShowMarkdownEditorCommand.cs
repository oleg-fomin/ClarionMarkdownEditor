using System;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Gui;

namespace ClarionMarkdownEditor
{
    /// <summary>
    /// Command to show the MarkdownEditor pad from the Tools menu.
    /// </summary>
    public class ShowMarkdownEditorCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            try
            {
                var workbench = WorkbenchSingleton.Workbench;
                if (workbench != null)
                {
                    // Use reflection for IDE version compatibility
                    var getPadMethod = workbench.GetType().GetMethod("GetPad", new Type[] { typeof(Type) });
                    if (getPadMethod != null)
                    {
                        var pad = getPadMethod.Invoke(workbench, new object[] { typeof(MarkdownEditorPad) });
                        if (pad != null)
                        {
                            var bringToFrontMethod = pad.GetType().GetMethod("BringPadToFront");
                            bringToFrontMethod?.Invoke(pad, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Error showing MarkdownEditor: " + ex.Message,
                    "Markdown Editor",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}

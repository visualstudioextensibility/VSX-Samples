using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace ContextMenu
{
   internal sealed class ToolWindowCommand
   {
      // Warning!!: Keep these values in sync with the .vsct file
      public const int CommandId = 0x0100;
      public static readonly Guid CommandSet = new Guid("c1a3a312-e25a-4cd1-b557-011428323a99");

      private readonly Package package;

      private ToolWindowCommand(Package package)
      {
         if (package == null)
         {
            throw new ArgumentNullException("package");
         }

         this.package = package;

         OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
         if (commandService != null)
         {
            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.ShowToolWindow, menuCommandID);
            commandService.AddCommand(menuItem);
         }
      }

      public static ToolWindowCommand Instance
      {
         get;
         private set;
      }

      private IServiceProvider ServiceProvider
      {
         get
         {
            return this.package;
         }
      }

      public static void Initialize(Package package)
      {
         Instance = new ToolWindowCommand(package);
      }

      private void ShowToolWindow(object sender, EventArgs e)
      {
         // Get the instance number 0 of this tool window. This window is single instance so this instance
         // is actually the only one.
         // The last flag is set to true so that if the tool window does not exists it will be created.
         ToolWindowPane window = this.package.FindToolWindow(typeof(MyToolWindow), 0, true);
         if ((null == window) || (null == window.Frame))
         {
            throw new NotSupportedException("Cannot create tool window");
         }

         IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
         Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
      }
   }
}

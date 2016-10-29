using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace ContextMenu
{
   internal sealed class Command2
   {
      // Warning!!: Keep these values in sync with the .vsct file
      public const int CommandId = 0x0102;
      public static readonly Guid CommandSet = new Guid("c1a3a312-e25a-4cd1-b557-011428323a99");

      private readonly Package package;

      private Command2(Package package)
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
            var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
            commandService.AddCommand(menuItem);
         }
      }

      public static Command2 Instance
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
         Instance = new Command2(package);
      }

      private void MenuItemCallback(object sender, EventArgs e)
      {
         string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
         string title = "Command2";

         // Show a message box to prove we were here
         VsShellUtilities.ShowMessageBox(
             this.ServiceProvider,
             message,
             title,
             OLEMSGICON.OLEMSGICON_INFO,
             OLEMSGBUTTON.OLEMSGBUTTON_OK,
             OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
      }
   }
}

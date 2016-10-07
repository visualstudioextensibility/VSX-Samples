using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace CommandWithArguments
{
   internal sealed class CommandWithArguments
   {
      public const int CommandId = 0x0100;

      public static readonly Guid CommandSet = new Guid("1ce3ba1f-07bd-41be-93c8-831fc64fd7d4");

      private readonly Package package;

      private CommandWithArguments(Package package)
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

            // Step 1: Add the <CommandFlag>AllowParams</CommandFlag> in the .vsct file for the command

            // Step 2: Use an OleMenuCommand, not a MenuCommand
            var oleMenuCommand = new OleMenuCommand(this.MenuItemCallback, menuCommandID);

            // Step 3: Add a non-empty description for the parameter
            // IMPORTANT: this line is required for a command to accept parameters
            oleMenuCommand.ParametersDescription = "Description for argument";

            commandService.AddCommand(oleMenuCommand);
         }
      }

      public static CommandWithArguments Instance
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
         Instance = new CommandWithArguments(package);
      }

      private void MenuItemCallback(object sender, EventArgs e)
      {
         string message;
         string title = "CommandWithArguments";
         string argumentValue = "";

         // Step 4: Cast EventArgs to OleMenuCmdEventArgs to get the argument value
         var oleMenuCmdEventArgs = e as OleMenuCmdEventArgs;

         if (oleMenuCmdEventArgs.InValue != null)
         {
            argumentValue = oleMenuCmdEventArgs.InValue.ToString();
         }
         message = $"Inside {this.GetType().FullName}.MenuItemCallback() called with value {argumentValue}'";

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

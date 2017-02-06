﻿using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace CommandProjectContextMenu
{
   internal sealed class Command1
   {
      public const int CommandId = 0x0100;

      public static readonly Guid CommandSet = new Guid("9a55a2b4-3e29-4359-882b-fa5f51c09300");

      private readonly Package package;

      private Command1(Package package)
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

      public static Command1 Instance
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
         Instance = new Command1(package);
      }

      private void MenuItemCallback(object sender, EventArgs e)
      {
         string message;
         string title = "Command1";
         EnvDTE.DTE dte;
         object[] activeSolutionProjects;
         EnvDTE.Project project;

         dte = (EnvDTE.DTE)this.ServiceProvider.GetService(typeof(EnvDTE.DTE));
         activeSolutionProjects = dte.ActiveSolutionProjects as object[];

         if (activeSolutionProjects != null)
         {
            foreach (object activeSolutionProject in activeSolutionProjects)
            {
               project = activeSolutionProject as EnvDTE.Project;

               if (project != null)
               {
                  message = $"Called on {project.FullName}";

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
      }
   }
}

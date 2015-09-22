using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;

namespace PackageCommandsCreatedByCode
{
   [PackageRegistration(UseManagedResourcesOnly = true)]
   [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
   [Guid(VSMyPackage.PackageGuidString)]
   [ProvideAutoLoad(Microsoft.VisualStudio.VSConstants.UICONTEXT.NoSolution_string)] // Note: the package must be loaded on startup to create and bind commands
   public sealed class VSMyPackage : Package
   {
      internal const string PackageGuidString = "6525510c-69c6-4d82-81e6-b20d2a69a5a2";
      private const string PackageGuidGroup = "AA61F99F-D83F-4AE1-8CF9-C97442674702";

      private int cmdId1;
      private int cmdId2;

      public VSMyPackage()
      {
      }

      protected override void Initialize()
      {
         IVsProfferCommands3 profferCommands3;
         OleMenuCommandService oleMenuCommandService;

         try
         {
            base.Initialize();

            profferCommands3 = base.GetService(typeof(SVsProfferCommands)) as IVsProfferCommands3;

            oleMenuCommandService = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            cmdId1 = AddCommand(profferCommands3, oleMenuCommandService, "MyCommand1", "My Command 1 Caption", "My Command 1 Tooltip");
            cmdId2 = AddCommand(profferCommands3, oleMenuCommandService, "MyCommand2", "My Command 2 Caption", "My Command 2 Tooltip");
         }
         catch (Exception ex)
         {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
         }
      }

      private int AddCommand(IVsProfferCommands3 profferCommands3, OleMenuCommandService oleMenuCommandService, string commandName, string commandCaption, string commandTooltip)
      {
         const uint CMD_FLAGS = 0;
         const string SATELLITE_DLL = "";
         const uint BITMAP_RESOURCE_ID = 0;
         const uint BITMAP_IMAGE_INDEX = 0;
         const int UI_CONTEXTS = 0;
         const Guid[] GUID_UI_CONTEXTS = null;

         Guid packageGuid = new Guid(PackageGuidString);
         Guid cmdGroupGuid = new Guid(PackageGuidGroup);
         int result;
         CommandID commandID;
         OleMenuCommand oleMenuCommand;
         uint cmdId = 0;

         result = profferCommands3.AddNamedCommand(ref packageGuid, ref cmdGroupGuid, commandName, out cmdId, commandName, commandCaption, commandTooltip,
            SATELLITE_DLL, BITMAP_RESOURCE_ID, BITMAP_IMAGE_INDEX, CMD_FLAGS, UI_CONTEXTS, GUID_UI_CONTEXTS);

         // Note: the result can be:
         // 1) Microsoft.VisualStudio.VSConstants.S_OK. This should be the case of the first creation, when the command didn't exist previously
         // 2) Microsoft.VisualStudio.VSConstants.S_FALSE. This can be because:
         //   2.1) The command with that name already exists. In this case the returned cmdId is valid.
         //   2.2) There is some error. In this case the returned cmdId remains with value 0.

         if (cmdId != 0)
         {
            if (oleMenuCommandService != null)
            {
               commandID = new CommandID(cmdGroupGuid, (int)cmdId);

               oleMenuCommand = new OleMenuCommand(OleMenuCommandCallback, commandID);
               oleMenuCommand.BeforeQueryStatus += this.OleMenuCommandBeforeQueryStatus;
               oleMenuCommandService.AddCommand(oleMenuCommand);
            }
         }
         else
         {
            throw new ApplicationException("Failed to add command");
         }
         return (int)cmdId;
      }

      private void OleMenuCommandBeforeQueryStatus(object sender, EventArgs e)
      {
         OleMenuCommand oleMenuCommand;
         CommandID commandId;

         try
         {
            oleMenuCommand = sender as OleMenuCommand;

            if (oleMenuCommand != null)
            {
               commandId = oleMenuCommand.CommandID;

               if (commandId != null)
               {
                  if (commandId.ID == cmdId1)
                  {
                     oleMenuCommand.Supported = true;
                     oleMenuCommand.Enabled = true;
                     oleMenuCommand.Visible = true;
                  }
                  else if (commandId.ID == cmdId2)
                  {
                     oleMenuCommand.Supported = true;
                     oleMenuCommand.Enabled = true;
                     oleMenuCommand.Visible = true;
                  }
               }
            }
         }
         catch (Exception ex)
         {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
         }
      }

      private void OleMenuCommandCallback(object sender, EventArgs e)
      {
         OleMenuCommand oleMenuCommand;
         CommandID commandId;

         try
         {
            oleMenuCommand = sender as OleMenuCommand;

            if (oleMenuCommand != null)
            {
               commandId = oleMenuCommand.CommandID;
               if (commandId != null)
               {
                  System.Windows.Forms.MessageBox.Show("Executed command with Id = " + commandId.ID.ToString());
               }
            }
         }
         catch (Exception ex)
         {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
         }
      }
   }
}

using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.IO;
using System.Diagnostics;

namespace VSIX
{
   [PackageRegistration(UseManagedResourcesOnly = true)]
   [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
   [Guid(VSIXPackage.PackageGuidString)]
   [ProvideAutoLoad(Microsoft.VisualStudio.VSConstants.UICONTEXT.NoSolution_string)]
   public sealed class VSIXPackage : Package
   {
      public const string PackageGuidString = "e9737e5e-4cbf-4c0c-91d7-d40f96839286";

      public VSIXPackage()
      {
      }

      // This method is called when VS loads because this package is marked with ProvideAutoLoad attribute
      protected override void Initialize()
      {
         const string PAYLOAD_FILE_NAME = "DebuggerVisualizer.dll";

         string sourceFolderFullName;
         string destinationFolderFullName;
         string sourceFileFullName;
         string destinationFileFullName;
         IVsShell shell;
         object documentsFolderFullNameObject = null;
         string documentsFolderFullName;

         try
         {
            base.Initialize();

            // The Visualizer dll is in the same folder than the package because its project is added as reference to this project,
            // so it is included inside the .vsix file. We only need to deploy it to the correct destination folder.
            sourceFolderFullName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            // Get the destination folder for visualizers
            shell = base.GetService(typeof(SVsShell)) as IVsShell;
            shell.GetProperty((int)__VSSPROPID2.VSSPROPID_VisualStudioDir, out documentsFolderFullNameObject);
            documentsFolderFullName = documentsFolderFullNameObject.ToString();
            destinationFolderFullName = Path.Combine(documentsFolderFullName, "Visualizers");

            sourceFileFullName = Path.Combine(sourceFolderFullName, PAYLOAD_FILE_NAME);
            destinationFileFullName = Path.Combine(destinationFolderFullName, PAYLOAD_FILE_NAME);

            CopyFileIfNewerVersion(sourceFileFullName, destinationFileFullName);
         }
         catch (Exception ex)
         {
            // TODO: Handle exception
            System.Diagnostics.Debug.WriteLine(ex.ToString());
         }        
      }

      private void CopyFileIfNewerVersion(string sourceFileFullName, string destinationFileFullName)
      {
         FileVersionInfo destinationFileVersionInfo;
         FileVersionInfo sourceFileVersionInfo;
         bool copy = false;

         if (File.Exists(destinationFileFullName))
         {
            sourceFileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(sourceFileFullName);
            destinationFileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(destinationFileFullName);
            if (sourceFileVersionInfo.FileMajorPart > destinationFileVersionInfo.FileMajorPart)
            {
               copy = true;
            }
            else if (sourceFileVersionInfo.FileMajorPart == destinationFileVersionInfo.FileMajorPart 
               && sourceFileVersionInfo.FileMinorPart > destinationFileVersionInfo.FileMinorPart)
            {
               copy = true;
            }
         }
         else
         {
            // First time
            copy = true;
         }

         if (copy)
         {
            File.Copy(sourceFileFullName, destinationFileFullName, true);
         }
      }
   }
}

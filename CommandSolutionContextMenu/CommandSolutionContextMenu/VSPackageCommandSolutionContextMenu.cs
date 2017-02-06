using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace CommandSolutionContextMenu
{
   [PackageRegistration(UseManagedResourcesOnly = true)]
   [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
   [Guid(VSPackageCommandSolutionContextMenu.PackageGuidString)]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
   [ProvideMenuResource("Menus.ctmenu", 1)]
   public sealed class VSPackageCommandSolutionContextMenu : Package
   {
      public const string PackageGuidString = "cffa6255-844b-403b-9a39-0361b700844d";

      public VSPackageCommandSolutionContextMenu()
      {
      }

      protected override void Initialize()
      {
         base.Initialize();
         Command1.Initialize(this);
      }
   }
}

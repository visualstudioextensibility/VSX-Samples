using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace CommandProjectContextMenu
{
   [PackageRegistration(UseManagedResourcesOnly = true)]
   [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
   [Guid(VSPackageCommandProjectContextMenu.PackageGuidString)]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
   [ProvideMenuResource("Menus.ctmenu", 1)]
   public sealed class VSPackageCommandProjectContextMenu : Package
   {
      public const string PackageGuidString = "069acb34-c12c-4e1c-ae69-1f9295046c84";

      public VSPackageCommandProjectContextMenu()
      {
      }

      protected override void Initialize()
      {
         base.Initialize();
          Command1.Initialize(this);
      }
   }
}

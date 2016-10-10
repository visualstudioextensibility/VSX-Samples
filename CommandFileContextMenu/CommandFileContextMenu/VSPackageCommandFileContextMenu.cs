using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace CommandFileContextMenu
{
   [PackageRegistration(UseManagedResourcesOnly = true)]
   [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
   [Guid(VSPackageCommandFileContextMenu.PackageGuidString)]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
   [ProvideMenuResource("Menus.ctmenu", 1)]
   public sealed class VSPackageCommandFileContextMenu : Package
   {
      public const string PackageGuidString = "669df51d-2588-4d30-962e-0070898e765e";

      public VSPackageCommandFileContextMenu()
      {
      }

      protected override void Initialize()
      {
         base.Initialize();
         Command1.Initialize(this);
      }
   }
}

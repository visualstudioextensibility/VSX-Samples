using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace CommandToolbar
{
   [PackageRegistration(UseManagedResourcesOnly = true)]
   [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
   [Guid(VSPackageCommandToolbar.PackageGuidString)]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
   [ProvideMenuResource("Menus.ctmenu", 1)]
   public sealed class VSPackageCommandToolbar : Package
   {
      public const string PackageGuidString = "4d39cbb8-8aad-4b40-b867-22082c266766";

      public VSPackageCommandToolbar()
      {
      }

      protected override void Initialize()
      {
         base.Initialize();
          Command1.Initialize(this);
          Command2.Initialize(this);
      }
   }
}

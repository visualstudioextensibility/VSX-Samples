using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace CommandTopMenu
{
   [PackageRegistration(UseManagedResourcesOnly = true)]
   [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
   [Guid(VSPackageCommandTopMenu.PackageGuidString)]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
   [ProvideMenuResource("Menus.ctmenu", 1)]
   public sealed class VSPackageCommandTopMenu : Package
   {
      public const string PackageGuidString = "cd803dc7-0b76-4fb6-b92e-6415591b66b8";

      public VSPackageCommandTopMenu()
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

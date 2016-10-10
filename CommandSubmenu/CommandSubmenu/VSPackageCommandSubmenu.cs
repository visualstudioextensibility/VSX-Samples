using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace CommandSubmenu
{
   [PackageRegistration(UseManagedResourcesOnly = true)]
   [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
   [Guid(VSPackageCommandSubmenu.PackageGuidString)]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
   [ProvideMenuResource("Menus.ctmenu", 1)]
   public sealed class VSPackageCommandSubmenu : Package
   {
      public const string PackageGuidString = "d2cc7f26-42fb-4c8d-858f-05f3fc3599d5";

      public VSPackageCommandSubmenu()
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

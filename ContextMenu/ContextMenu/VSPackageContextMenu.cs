using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace ContextMenu
{
   [PackageRegistration(UseManagedResourcesOnly = true)]
   [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
   [Guid(VSPackageContextMenu.PackageGuidString)]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
   [ProvideMenuResource("Menus.ctmenu", 1)]
   [ProvideToolWindow(typeof(MyToolWindow))]
   public sealed class VSPackageContextMenu : Package
   {
      public const string PackageGuidString = "9a1f0ba6-7996-44d1-bf23-712cf7adbcf6";

      public VSPackageContextMenu()
      {
      }

      protected override void Initialize()
      {
         base.Initialize();
         ToolWindowCommand.Initialize(this);
         Command1.Initialize(this);
         Command2.Initialize(this);
      }

   }
}

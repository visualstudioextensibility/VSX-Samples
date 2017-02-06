using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace CommandWithArguments
{
   [PackageRegistration(UseManagedResourcesOnly = true)]
   [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
   [Guid(VSPackageCommandWithArguments.PackageGuidString)]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
   [ProvideMenuResource("Menus.ctmenu", 1)]
   public sealed class VSPackageCommandWithArguments : Package
   {
      public const string PackageGuidString = "8c756339-2f14-43b7-becc-5da3c229ad49";

      public VSPackageCommandWithArguments()
      {
      }

      #region Package Members

      protected override void Initialize()
      {
            base.Initialize();
          CommandWithArguments.Initialize(this);
      }

      #endregion
   }
}

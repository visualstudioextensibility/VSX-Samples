using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;

namespace DtePackage
{
   [PackageRegistration(UseManagedResourcesOnly = true)]
   [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
   [Guid(MyPackage.PackageGuidString)]
   [ProvideAutoLoad(Microsoft.VisualStudio.VSConstants.UICONTEXT.NoSolution_string)]
   public sealed class MyPackage : Package
   {
      public const string PackageGuidString = "4d1225ef-ea14-4475-aa8d-19a148cf8c22";

      internal class DteInitializer : IVsShellPropertyEvents
      {
         private IVsShell shellService;
         private uint cookie;
         private Action callback;

         internal DteInitializer(IVsShell shellService, Action callback)
         {
            int hr;

            this.shellService = shellService;
            this.callback = callback;

            // Set an event handler to detect when the IDE is fully initialized
            hr = this.shellService.AdviseShellPropertyChanges(this, out this.cookie);

            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);
         }

         int IVsShellPropertyEvents.OnShellPropertyChange(int propid, object var)
         {
            int hr;
            bool isShellInitialized = false;

            switch (propid)
            {
               // This was for VS 2005, 2008
               //case (int) __VSSPROPID.VSSPROPID_Zombie:

               //   isShellInitialized = !(bool)var;
               //   break;

               // This is for VS 2010 and higher
               case (int)__VSSPROPID4.VSSPROPID_ShellInitialized:

                  isShellInitialized = (bool)var;
                  break;
            }

            if (isShellInitialized)
            {
               // Release the event handler to detect when the IDE is fully initialized
               hr = this.shellService.UnadviseShellPropertyChanges(this.cookie);

               Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);

               this.cookie = 0;

               this.callback();
            }
            return VSConstants.S_OK;
         }
      }

      private EnvDTE80.DTE2 m_dte;
      private DteInitializer m_dteInitializer;


      public MyPackage()
      {
      }

      protected override void Initialize()
      {
         IVsShell shellService;

         base.Initialize();

         // Try to retrieve the DTE instance at this point
         InitializeDte();

         // If not retrieved, we must wait for the Visual Studio Shell to be initialized
         if (m_dte == null)
         {
            // Note: if targetting only VS 2015 and higher, we could use this:
            // Microsoft.VisualStudio.Shell.KnownUIContexts.ShellInitializedContext.WhenActivated(() => this.InitializeDte());

            // For VS 2005 and higher, we use this:
            shellService = this.GetService(typeof(Microsoft.VisualStudio.Shell.Interop.SVsShell)) as IVsShell;

            m_dteInitializer = new DteInitializer(shellService, this.InitializeDte);
         }
      }

      private void InitializeDte()
      {
         m_dte = this.GetService(typeof(Microsoft.VisualStudio.Shell.Interop.SDTE)) as EnvDTE80.DTE2;

         m_dteInitializer = null;
      }
   }
}

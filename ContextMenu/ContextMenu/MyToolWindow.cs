using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace ContextMenu
{
   [Guid("f45e20ff-639e-4954-9e1a-d4767f5662ed")]
   public class MyToolWindow : ToolWindowPane
   {
      private ToolWindowControl m_toolWindowControl;

      public MyToolWindow() : base(null)
      {
         this.Caption = "My toolwindow";

         m_toolWindowControl = new ToolWindowControl();

         this.Content = m_toolWindowControl;
      }

      public override void OnToolWindowCreated()
      {
         VSPackageContextMenu package;

         base.OnToolWindowCreated();

         package = (VSPackageContextMenu)base.Package;

         m_toolWindowControl.Initialize(package);
      }
   }
}

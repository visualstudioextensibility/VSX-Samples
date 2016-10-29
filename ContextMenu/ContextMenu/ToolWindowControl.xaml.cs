using Microsoft.VisualStudio.Shell.Interop;
using System.Windows.Controls;

namespace ContextMenu
{
   public partial class ToolWindowControl : UserControl
   {
      private VSPackageContextMenu m_package;

      public ToolWindowControl()
      {
         this.InitializeComponent();
      }

      public void Initialize(VSPackageContextMenu package)
      {
         m_package = package;
      }

      private void frame_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
      {
         // Warning!!: Keep these values in sync with the .vsct file
         const string guidVSPackageContextMenuCmdSet = "c1a3a312-e25a-4cd1-b557-011428323a99";
         const int MyContextMenuId = 0x0200;

         System.IServiceProvider serviceProvider;
         IVsUIShell uiShell;
         System.Guid contextMenuGuid = new System.Guid(guidVSPackageContextMenuCmdSet);
         System.Windows.Point relativePoint;
         System.Windows.Point screenPoint;
         POINTS point;
         POINTS[] points;

         if (e.ChangedButton == System.Windows.Input.MouseButton.Right)
         {
            serviceProvider = (System.IServiceProvider)m_package;
            uiShell = serviceProvider.GetService(typeof(SVsUIShell)) as IVsUIShell;

            if (uiShell != null)
            {
               relativePoint = e.GetPosition(this);
               screenPoint = this.PointToScreen(relativePoint);

               point = new POINTS();
               point.x = (short)screenPoint.X;
               point.y = (short)screenPoint.Y;

               points = new[] { point };

               // TODO: error handling
               uiShell.ShowContextMenu(0, ref contextMenuGuid, MyContextMenuId, points, null);
            }
         }
      }
   }
}
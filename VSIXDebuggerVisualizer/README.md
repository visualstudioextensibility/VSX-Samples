VSIX Debugger Visualizer
========================

According to the MSDN documentation ([How to: Install a Visualizer](https://msdn.microsoft.com/en-us/library/sb2yca43.aspx)), to install a Visual Studio debugger visualizer you must copy the DLL to either of the following locations:

* \<VisualStudioInstallPath>\Common7\Packages\Debugger\Visualizers
* My Documents\<VisualStudioVersion>\Visualizers

While that approach is simple to do by hand, you may want something more automatic. This example shows an approach to install a Visual Studio debugger visualizer using VSIX, the same deployment technology used to install packages, MEF components, etc.

To approach to use VSIX is the following:

* You create a solution with two projects: one for the debugger visualizer and another project for a VSIX package
* You create a minimal package without user interface whose only purpose is to deploy the debugger visualizer dll in the suitable folder.
* The project with the package references the project with the debugger visualizer, so that the compiled dll is included in the VSIX.
* The package is marked to load on Visual Studio startup.
* When loaded, the package obtains the installation folder for visualizers (using the [IVsShell.GetProperty](https://msdn.microsoft.com/en-us/library/microsoft.visualstudio.shell.interop.ivsshell.getproperty.aspx) method with the [__VSSPROPID2.VSSPROPID_VisualStudioDir](https://msdn.microsoft.com/en-us/library/microsoft.visualstudio.shell.interop.__vsspropid2.aspx) property, concatenating the "Visualizers" subfolder).
* Then it checks if there is already a dll for the debugger visualizer in that folder: if there is no dll, copies it. Otherwise, checks if the version of the installed dll is older than the one being deployed.

HOWTO: Get the EnvDTE.DTE instance from a package
=================================================

Although a package can use Visual Studio services, sometimes it may need to use the 
automation model (EnvDTE). To get the EnvDTE.DTE instance, you can use the SDTE service:

```C#
EnvDTE80.DTE2 dte = this.GetService(typeof(Microsoft.VisualStudio.Shell.Interop.SDTE)) as EnvDTE80.DTE2;
```
However, you can get a null instance if your package is loaded very early, for example if it is marked to load on Visual Studio startup. So,
the package should wait until the Visual Studio shell is fully initialized. Across the several Visual Studio versions, the way to get notified
when the shell is fully initialized has changed slightly. This sample shows you the different variations:

* All Visual Studio versions: use the [IVsShell.AdviseShellPropertyChanges](https://msdn.microsoft.com/en-us/library/microsoft.visualstudio.shell.interop.ivsshell.adviseshellpropertychanges.aspx) method and wait for the [__VSSPROPID.VSSPROPID_Zombie](https://msdn.microsoft.com/en-us/library/microsoft.visualstudio.shell.interop.__vsspropid.aspx) property value changed.
* Visual Studio 2010 and higher: use the [IVsShell.AdviseShellPropertyChanges](https://msdn.microsoft.com/en-us/library/microsoft.visualstudio.shell.interop.ivsshell.adviseshellpropertychanges.aspx) method and wait for the [__VSSPROPID4.VSSPROPID_ShellInitialized](https://msdn.microsoft.com/en-us/library/microsoft.visualstudio.shell.interop.__vsspropid4.aspx) property value changed.
* Visual Studio 2015: use the [KnownUIContexts.ShellInitializedContext.WhenActivated](https://msdn.microsoft.com/en-us/library/microsoft.visualstudio.shell.knownuicontexts.shellinitializedcontext.aspx) method.

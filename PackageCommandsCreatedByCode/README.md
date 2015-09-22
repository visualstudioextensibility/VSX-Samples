HOWTO: Package with commands created by code
=====================================

While most of the time a package will create its commands using a .vsct file, so that they are also added to commandbars, menus, etc. there may be situations in which a package 
needs to create a command by code dynamically, even without knowing the command name at compile time.

If you have used add-ins (EnvDTE), you know that add-ins create commands always by code, using the [EnvDTE.Commands.AddNamedCommand](https://msdn.microsoft.com/en-us/library/envdte.commands.addnamedcommand.aspx). While you can get an instance 
even from a package, you can not use that method from a package because its first parameter is an EnvDTE.AddIn instance.

To create commands by code from a package, you need to use the native services, specifically the [AddNamedCommand method of the IVsProfferCommands interface](https://msdn.microsoft.com/en-us/library/envdte.ivsproffercommands.addnamedcommand.aspx).

In this sample a package will create two commands by code at run-time. Rather than using the EnvDTE.IVsProfferCommands or EnvDTE80.IVsProfferCommands2 interfaces, 
the [Microsoft.VisualStudio.Shell.Interop.IVsProfferCommands3](https://msdn.microsoft.com/en-us/library/microsoft.visualstudio.shell.interop.ivsproffercommands3.aspx) interface 
will be used, to avoid completely dependencies on the automation model references (EnvDTE).

**Note**: this sample only works on Visual Studio 2015. On previous versions of Visual Studio there is a bug that prevents enabling the commands, so they remain always disabled.
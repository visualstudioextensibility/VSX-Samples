HOWTO: Create a Visual Studio command that accepts arguments
============================================================

By default, the commands that you create for Visual Studio don't accept parameters

To accept parameters, four steps are required:

* Step 1: Add the AllowParams command flag in the .vsct file for the command
* Step 2: When creating the menu command, use an OleMenuCommand type, not a MenuCommand type
* Step 3: Fill the ParametersDescription property of the OleMenuCommand with the magical value "$"
* Step 4: In the MenuItemCallback method, cast EventArgs to OleMenuCmdEventArgs to get the argument value

HOWTO: Create a Visual Studio command that accept arguments
===========================================================

By default, the commands that you create for Visual Studio don't accept parameters

To accept parameters, four steps are required:

-- Step 1: Add the <CommandFlag>AllowParams</CommandFlag> in the .vsct file for the command
-- Step 2: Use an OleMenuCommand, not a MenuCommand
-- Step 3: Add a non-empty description for the parameter
-- Step 4: Cast EventArgs to OleMenuCmdEventArgs to get the argument value

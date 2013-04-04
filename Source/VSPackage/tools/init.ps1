# Runs the first time a package is installed in a solution, and every time the solution is opened.

param($installPath, $toolsPath, $package, $project)

# $installPath is the path to the folder where the package is installed.
# $toolsPath is the path to the tools directory in the folder where the package is installed.
# $package is a reference to the package object.
# $project is null in init.ps1

iex ("C:\Windows\Microsoft.NET\Framework\v4.0.30319\regasm.exe " + (join-path $installPath "\lib\net45\IronMeta.VSPlugin.dll") + " /codebase")

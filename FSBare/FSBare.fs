namespace VSPackagesInFS.FSBare
open System.Diagnostics

// After building the following files should be in 
// `%userprofile%\AppData\Local\Microsoft\VisualStudio\12.0Exp\Extensions\VSPinFS\FSBare\1.0`:
//   extension.vsixmanifest
//   FSBare.dll
//   FSBare.pdb
//   FSBare.pkgdef
//   FSharp.Core.dll

// This sample uses fully qualified names for system methods to make it easier
// for beginners to see where the methods are defined.
// Normally the following open statements would be used to make the code shorter.
// open System.Runtime.InteropServices
// open Microsoft.VisualStudio.Shell

module Guids =
  let [<Literal>] GuidFSBarePkgString = "1b8460f8-d08b-46cb-9420-79e98bdd998e"

/// <summary>
/// This is the class that implements the package exposed by this assembly.
///
/// The minimum requirement for a class to be considered a valid package for Visual Studio
/// is to implement the IVsPackage interface and register itself with the shell.
/// This package uses the helper classes defined inside the Managed Package Framework (MPF)
/// to do it: it derives from the Package class that provides the implementation of the 
/// IVsPackage interface and uses the registration attributes defined in the framework to 
/// register itself and its components with the shell.
/// </summary>

// This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
// a package.
[<Microsoft.VisualStudio.Shell.PackageRegistration(UseManagedResourcesOnly=true)>]

// This attribute is used to register the information needed to show this package
// in the Help/About dialog of Visual Studio.
// The actual Help/About information is defined in VSPackage.resx
[<Microsoft.VisualStudio.Shell.InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)>]
[<System.Runtime.InteropServices.Guid (Guids.GuidFSBarePkgString) >]
type FSBare() as self =
  inherit Microsoft.VisualStudio.Shell.Package()
  do
    /// <summary>
    /// Default constructor of the package.
    /// Inside this method you can place any initialization code that does not require 
    /// any Visual Studio service because at this point the package object is created but 
    /// not sited yet inside Visual Studio environment. The place to do all the other 
    /// initialization is the Initialize method.
    /// </summary>
    // Never actually called since this package only registers itself
    Debug.WriteLine(sprintf "Entering constructor for: %s", self.ToString())

  /// <summary>
  /// Initialization of the package; this method is called right after the package is sited, so this is the place
  /// where you can put all the initialization code that rely on services provided by VisualStudio.
  /// </summary>
  // Never actually called since this package only registers itself
  override __.Initialize() =
    Debug.WriteLine (sprintf "Entering Initialize() of: %s", self.ToString())
    base.Initialize()
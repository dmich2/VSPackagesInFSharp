namespace VSPackagesInFS.FSMenu
open System.Diagnostics

// After building the following files should be in 
// `%userprofile%\AppData\Local\Microsoft\VisualStudio\12.0Exp\Extensions\VSPinFS\FSMenu\1.0`:
//   extension.vsixmanifest
//   FSharp.Core.dll
//   FSMenu.dll
//   FSMenu.pdb
//   FSMenu.pkgdef

// This sample uses fully qualified names for system methods to make it easier
// for beginners to see where the methods are defined.
// Normally the following open statements would be used to make code shorter.
// open System.ComponentModel.Design
// open System.Globalization
// open System.Runtime.InteropServices
// open Microsoft.VisualStudio.Shell
// open System.Windows.Forms

[<AutoOpen>]
module Prelude =
  let inline isNull x = match x with null -> true | _ -> false

module Guids =
  // The following must match the "guidFSMenuPkg" Symbol defined in FSMenu.vsct.
  let [<Literal>] GuidFSMenuPkgString    = "cedc6e13-182b-4f7a-bffd-e4a09598ee63"

  // The following must match the "guidFSMenuCmdSet" Symbol defined in FSMenu.vsct.
  let [<Literal>] GuidFSMenuCmdSetString = "65903e4e-2bae-4f5b-9d9a-4d74484fdaa7"
  let             guidFSMenuCmdSet       = System.Guid GuidFSMenuCmdSetString

module CommandIDs =
  // The following must match the "cmdidFSMenuCommand" IDSymbol defined in FSMenu.vsct.
  let cmdidMyCommandFSMenu = 0x100

// This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
// a package.
[<Microsoft.VisualStudio.Shell.PackageRegistration(UseManagedResourcesOnly=true)>]

// This attribute is used to register the information needed to show this package
// in the Help/About dialog of Visual Studio.
// The actual Help/About information is defined in VSPackage.resx
[<Microsoft.VisualStudio.Shell.InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)>]

// This attribute is needed to let the shell know that this package exposes some menus.
// The actual menus and commands are defined in FSMenu.vsct.
// First arg "Menus.ctmenu" must match VSCTCompile ResourceName in FSMenu.fsproj file.
//  <VSCTCompile Include="FSMenu.vsct">
//    <ResourceName>Menus.ctmenu</ResourceName>
//  </VSCTCompile>
[<Microsoft.VisualStudio.Shell.ProvideMenuResource("Menus.ctmenu", 1)>]

[<System.Runtime.InteropServices.Guid (Guids.GuidFSMenuPkgString) >]

/// <summary>
/// F# primary constructor of the package.
/// Inside this method you can place any initialization code that does not require 
/// any Visual Studio service because at this point the package object is created but 
/// not sited yet inside Visual Studio environment. The place to do all the other 
/// initialization is the Initialize method.
/// </summary>
type FSMenu() as self =
  inherit Microsoft.VisualStudio.Shell.Package()
  do
    Debug.WriteLine(sprintf "Entering constructor for: %s", self.ToString())

  /// <summary>
  /// This function is the callback used to execute a command when the a menu item is clicked.
  /// See the Initialize method to see how the menu item is associated to this function using
  /// the OleMenuCommandService service and the MenuCommand class.
  /// </summary>
  member private __.MenuItemCallback =
    let uiShell = self.GetService(typeof<Microsoft.VisualStudio.Shell.Interop.SVsUIShell>) :?>
                    Microsoft.VisualStudio.Shell.Interop.IVsUIShell
    System.EventHandler (fun sender e -> 
      // Show a Message Box to prove we were here
      let clsid = ref System.Guid.Empty
      let success, result = 
        uiShell.ShowMessageBox(
          0u,
          clsid,
          "FSMenu",
          System.String.Format(System.Globalization.CultureInfo.CurrentCulture, 
                                "Inside {0}.MenuItemCallback()", self.ToString()),
          System.String.Empty,
          0u,
          Microsoft.VisualStudio.Shell.Interop.OLEMSGBUTTON.OLEMSGBUTTON_OK,
          Microsoft.VisualStudio.Shell.Interop.OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST,
          Microsoft.VisualStudio.Shell.Interop.OLEMSGICON.OLEMSGICON_INFO,
          0        // false
          )
      Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(success) |> ignore
      )

  /// <summary>
  /// Initialization of the package; this method is called right after the package is sited, so this is the place
  /// where you can put all the initialization code that rely on services provided by VisualStudio.
  /// </summary>
  override __.Initialize() =
    Debug.WriteLine (
      System.String.Format(System.Globalization.CultureInfo.CurrentCulture,
                           "Entering Initialize() of: {0}", self.ToString()))
    base.Initialize()

    // Add our command handlers for menu (commands must exist in the .vsct file)
    let menuService = self.GetService(typeof<System.ComponentModel.Design.IMenuCommandService>) :?>
                        Microsoft.VisualStudio.Shell.OleMenuCommandService
    if isNull menuService then () else
      // Create the command for the menu item.
      let commandId = System.ComponentModel.Design.CommandID(Guids.guidFSMenuCmdSet, CommandIDs.cmdidMyCommandFSMenu)
      let menuCommand = System.ComponentModel.Design.MenuCommand (self.MenuItemCallback, commandId)
      menuService.AddCommand(menuCommand)
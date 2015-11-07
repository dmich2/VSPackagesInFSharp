namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute "FSMenu">]
[<assembly: AssemblyProductAttribute "FSMenu">]
[<assembly: AssemblyDescriptionAttribute "FSMenu Description in assembly.fs">]
[<assembly: AssemblyVersionAttribute "0.0.0.01">]
[<assembly: AssemblyFileVersionAttribute "0.0.0.01">]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.0.0.01"
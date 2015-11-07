namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute "FSBare">]
[<assembly: AssemblyProductAttribute "FSBare">]
[<assembly: AssemblyDescriptionAttribute "FSBare Description in assembly.fs">]
[<assembly: AssemblyVersionAttribute "0.0.0.01">]
[<assembly: AssemblyFileVersionAttribute "0.0.0.01">]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.0.0.01"
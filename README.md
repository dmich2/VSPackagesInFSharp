## Visual Studio 2013 Visual Studio Packages in `F#`
---

This repo contains C# Visual Studio 2013 Extensibility Packages generated
by the Visual Studio Package wizard and their manually translated F#
counterparts.

* CSBare Project - Visual Studio Package with no "Select VSPackage Options"
  selected.

* FSBare Project - F# manual translation of CSBare

* CSMenu Project - Visual Studio Package with `Menu Command` "Select
  VSPackage Options" selected.

* FSMenu Project - F# manual translation of CSMenu

### Manually Creating F# translations of C# Visual Studio Packages

It will be easier to start off by copying the F# project from this repo
that implements the core features you want. See the relevant projects for
explicit examples, but the basic process is:

1. Create a F# Library project.

2. Copy the following files from either a C# generated package or from one of
   the example F# projects in this repo:

* `source.extension.vsixmanifest` to `source.extension.vsixmanifest`

* `[somepackage].vsct` to `[PACKAGENAME].vsct`

* `Resources\Assembly.fs` to `Resources\Assembly.fs`

* `VSPackage.resx` (or `Resources\VSPackage.resx`) to `Resources\VSPackage.resx`

* `Resources\Package.ico` to `Resources\Package.ico`

* `Resources\Images.png` to `Resources\Images.png`

3. Add:
```
  <VSToolsPath Condition="'$(VSToolsPath)' ==
''">$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v$(VisualStudioVersion)</VSToolsPath>
```
   to:
```
<PropertyGroup>
  <MinimumVisualStudioVersion Condition="'$(MinimumVisualStudioVersion)' == ''">12</MinimumVisualStudioVersion>
</PropertyGroup>
```
   to get:
```
<PropertyGroup>
  <MinimumVisualStudioVersion Condition="'$(MinimumVisualStudioVersion)' == ''">12</MinimumVisualStudioVersion>
  <VSToolsPath Condition="'$(VSToolsPath)' == ''">$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v$(VisualStudioVersion)</VSToolsPath>
</PropertyGroup>
```
   `VSToolsPath` will be used to load `Microsoft.VsSDK.targets` later.

4. Add:
```
<ProjectTypeGuids>{82b43b9b-a64c-4715-b499-d71e9ca2bd60};{F2A71F9B-5D33-465A-A702-920D77279786}</ProjectTypeGuids>
```
   right after:
```
<ProjectGuid>SOME GUID</ProjectGuid>
```
   to specify that this project is an F# Project and an Extensibility Project.

   See
   [INFO: List of known project type Guids](http://www.mztools.com/articles/2008/mz2008017.aspx)

5. To:
```
<PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
```
   add:
```
<StartAction>Program</StartAction>
<StartProgram>C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe</StartProgram>
<StartArguments>/rootsuffix Exp</StartArguments>
```
   to start the VS2013 Experimental instance when debugging.

6. At root level add:
```
<PropertyGroup>
  <UseCodebase>true</UseCodebase>
</PropertyGroup>
```
7. At root level, right after:

```
<Import Project="$(FSharpTargetsPath)" />
```
   add:
```
<Import Project="$(VSToolsPath)\VSSDK\Microsoft.VsSDK.targets" Condition="'$(VSToolsPath)' != ''" />
```
   This is how `source.extension.vsixmanifest` is processed and the `.vsct`
   file is converted to `.pkgdef` and `.vsix` files.

8. Make sure you have something that looks like this (where `[PACKAGENAME]`
   is replaced by your actual package name):
```
<ItemGroup>
  <Compile Include="[PACKAGENAME].fs" />
  <VSCTCompile Include="[PACKAGENAME].vsct">
    <ResourceName>Menus.ctmenu</ResourceName>
  </VSCTCompile>
  <None Include="source.extension.vsixmanifest">
    <SubType>Designer</SubType>
  </None>
<!--
    <None Include="Key.snk" />
-->
  <EmbeddedResource Include="Resources\VSPackage.resx">
    <MergeWithCTO>true</MergeWithCTO>
    <ManifestResourceName>VSPackage</ManifestResourceName>
  </EmbeddedResource>
  <Compile Include="Resources\Assembly.fs" />
  <Content Include="Resources\Package.ico" />
  <None Include="Resources\Images.png" />
</ItemGroup>
```

9. Include the following references (others may be needed depending on your
    package):
```
<ItemGroup>
  <Reference Include="Microsoft.VisualStudio.OLE.Interop" />
  <Reference Include="Microsoft.VisualStudio.Shell.Interop" />
  <Reference Include="Microsoft.VisualStudio.Shell.Interop.8.0" />
  <Reference Include="Microsoft.VisualStudio.Shell.Interop.9.0" />
  <Reference Include="Microsoft.VisualStudio.Shell.Interop.10.0" />
  <Reference Include="Microsoft.VisualStudio.Shell.Interop.11.0">
    <EmbedInteropTypes>true</EmbedInteropTypes>
  </Reference>
  <Reference Include="Microsoft.VisualStudio.Shell.Interop.12.0">
    <EmbedInteropTypes>true</EmbedInteropTypes>
  </Reference>
  <Reference Include="Microsoft.VisualStudio.Shell.12.0" />
  <Reference Include="Microsoft.VisualStudio.Shell.Immutable.10.0" />
  <Reference Include="mscorlib" />
  <Reference Include="FSharp.Core, Version=$(TargetFSharpCoreVersion), Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a">
    <Private>True</Private>
  </Reference>
  <Reference Include="System" />
  <Reference Include="System.Core" />
  <Reference Include="System.Design" />
  <Reference Include="System.Numerics" />
  <Reference Include="System.Windows.Forms" />
</ItemGroup>
<ItemGroup>
  <COMReference Include="stdole">
    <Guid>{00020430-0000-0000-C000-000000000046}</Guid>
    <VersionMajor>2</VersionMajor>
    <VersionMinor>0</VersionMinor>
    <Lcid>0</Lcid>
    <WrapperTool>primary</WrapperTool>
    <Isolated>False</Isolated>
    <EmbedInteropTypes>False</EmbedInteropTypes>
  </COMReference>
</ItemGroup>
```

10. Double-click on `source.extension.vsixmanifest` and  `VSPackage.resx`
    to open up visual editors for those files.

11. Double-click on `[PACKAGENAME].vsct` and edit using the `.xml` editor.

12. Double-click on `Assembly.fs` and edit.

<!--
   Local Variables:
   coding: utf-8
   mode: markdown
   mode: auto-fill
   indent-tabs-mode: nil
   sentence-end-double-space: t
   fill-column: 75
   standard-indent: 3
   tab-stop-list: (3 6 9 12 15 18 21 24 27 30 33 36 39 42 45 48 51 54 57 60)
   End:
-->

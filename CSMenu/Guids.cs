// Guids.cs
// MUST match guids.h
using System;

namespace VSPackagesInFS.CSMenu
{
    static class GuidList
    {
        public const string guidCSMenuPkgString = "865444a5-ca16-4005-8a2d-51334bea4660";
        public const string guidCSMenuCmdSetString = "3c413c7a-5732-45c0-89be-e42378ca94ea";

        public static readonly Guid guidCSMenuCmdSet = new Guid(guidCSMenuCmdSetString);
    };
}
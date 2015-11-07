// Guids.cs
// MUST match guids.h
using System;

namespace VSPackagesInFS.CSBare
{
    static class GuidList
    {
        public const string guidCSBarePkgString = "5fbd925b-9063-4abe-b8f0-1e0e7a51e4ad";
        public const string guidCSBareCmdSetString = "c4b24b5a-c4a9-4c2c-95f7-037101ed5d87";

        public static readonly Guid guidCSBareCmdSet = new Guid(guidCSBareCmdSetString);
    };
}
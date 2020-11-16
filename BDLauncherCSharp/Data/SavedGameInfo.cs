using System;
using System.IO;

namespace BDLauncherCSharp.Data
{
    public class SavedGameInfo
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public FileInfo RealFile { get; set; }
    }
}
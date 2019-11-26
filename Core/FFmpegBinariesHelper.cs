﻿using System;
using System.IO;
using System.Runtime.InteropServices;

namespace FFmpeg.AutoGen.Example
{
    public class FFmpegBinariesHelper
    {
        private const string LD_LIBRARY_PATH = "LD_LIBRARY_PATH";

        public static void RegisterFFmpegBinaries()
        {
            string libraryPath = "";
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                    string current = Environment.CurrentDirectory;
                    string probe = Path.Combine(Environment.Is64BitProcess ? "x64" : "x86");
                    while (current != null)
                    {
                        string ffmpegDirectory = Path.Combine(current, probe);
                        if (Directory.Exists(ffmpegDirectory))
                        {
                            Console.WriteLine($"FFmpeg binaries found in: {ffmpegDirectory}");
                            RegisterLibrariesSearchPath(ffmpegDirectory);
                            return;
                        }
                        current = Directory.GetParent(current)?.FullName;
                    }
                    break;

                case PlatformID.Unix:
                    libraryPath = "/usr/lib/x86_64-linux-gnu";
                    RegisterLibrariesSearchPath(libraryPath);
                    break;

                case PlatformID.MacOSX:
                    libraryPath = Environment.GetEnvironmentVariable(LD_LIBRARY_PATH);
                    RegisterLibrariesSearchPath(libraryPath);
                    break;
            }
        }

        private static void RegisterLibrariesSearchPath(string path)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                    NativeMethods.SetDllDirectory(path);
                    break;

                case PlatformID.Unix:
                case PlatformID.MacOSX:
                    string currentValue = Environment.GetEnvironmentVariable(LD_LIBRARY_PATH);
                    if (string.IsNullOrWhiteSpace(currentValue) == false && currentValue.Contains(path) == false)
                    {
                        string newValue = currentValue + Path.PathSeparator + path;
                        Environment.SetEnvironmentVariable(LD_LIBRARY_PATH, newValue);
                    }
                    break;
            }
        }

        internal static class NativeMethods
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool SetDllDirectory(string lpPathName);
        }
    }
}
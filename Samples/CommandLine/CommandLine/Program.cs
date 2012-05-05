using System;
using System.Diagnostics;

namespace CommandLine
{
    class Program
    {
        public static void Main(string[] args)
        {
            StartDicom2();
        }

        private static void StartDicom2()
        {
            const string fileName = @"E:\Temp\Projects\FYP\SCE11-0353\Converter\dicom2.exe";
            const string workDir = @"E:\Temp\Projects\FYP\Samples\Images";
            const string commandLineArguments = @"-p *.dcm";

            var info = new ProcessStartInfo
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = fileName,
                Arguments = commandLineArguments,
                WorkingDirectory = workDir
            };

            try
            {
                var dicom2 = Process.Start(info);
                dicom2.WaitForExit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

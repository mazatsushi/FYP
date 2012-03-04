using System;
using System.Diagnostics;

namespace CommandLine
{
    class Program
    {
        /// <summary>
        /// A private string that contains the absolute path for the dicom2 program.
        /// This is the main program that RIS uses for file conversion.
        /// </summary>
        private static readonly UriBuilder Dicom2AbsolutePath = new UriBuilder(@"C:\Program Files\XMedCon\bin\medcon.exe");

        /// <summary>
        /// The command string that we pass into dicom2 for file conversion.
        /// Make sure to append the DICOM file name (with the .dcm extension) before invoking the program.
        /// </summary>
        private const string Dicom2CommandString = "dicom2.exe -p ";

        private static readonly string[] DicomDelimiter = new[] { ".dcm" };
        private const string PngExtension = ".png";

        public static void Main(string[] args)
        {
            StartDicom2();
        }

        private static void StartDicom2()
        {
            Process dicom2 = new Process();
            string fileName = "cmd";
            string commandLineArguments = @"-p *.dcm";
            //string commandLineArguments = @"-c png -f 'e:\temp\projects\fyp\sce11-0353\uploads*.dcm'";

            try
            {
                dicom2.StartInfo.FileName = fileName;
                dicom2.StartInfo.WorkingDirectory = @"e:\temp\projects\fyp\sce11-0353\uploads\dicom2.exe";
                dicom2.StartInfo.Arguments = commandLineArguments;
                dicom2.StartInfo.CreateNoWindow = true;
                dicom2.StartInfo.UseShellExecute = false;
                dicom2.Start();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

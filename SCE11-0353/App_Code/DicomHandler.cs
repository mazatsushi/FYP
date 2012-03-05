using System;
using System.Diagnostics;
using System.IO;

/// <summary>
/// This class handles file conversion from the DICOM to PNG formats.
/// It is necessary as DICOM cannot be natively displayed in a web browser unless using a DICOM plugin for Silverlight.
/// 
/// The class offloads the actual conversion to a command line program called "dicom2".
/// However dicom2 only accepts valid DICOM files. As such, invalid DICOM files cannot be converted.
/// </summary>
public class DicomHandler
{
    /// <summary>
    /// A private string that contains the absolute path for the dicom2 program.
    /// This is the main program that RIS uses for file conversion.
    /// </summary>
    private const string Dicom2Executable = @"E:\Temp\Projects\FYP\SCE11-0353\Uploads\dicom2.exe";

    /// <summary>
    /// A private string that contains the working directory for the dicom2 program.
    /// This is the main program that RIS uses for file conversion.
    /// </summary>
    private const string WorkingDirectory = @"E:\Temp\Projects\FYP\SCE11-0353\Uploads";

    private static readonly string[] DicomExtension = new[] { ".dcm" };
    private const string PngExtension = ".png";

    /// <summary>
    /// The command string that we pass into dicom2 for file conversion.
    /// Make sure to append the DICOM file name (with the .dcm extension) before invoking the program.
    /// </summary>
    private const string ProgramArguments = "-p ";

    /// <summary>
    /// Method for converting DICOM to PNG files
    /// </summary>
    /// <param name="dicomFileName">The name of the DICOM file to be converted</param>
    /// <returns>True if the conversion is successful. False otherwise.</returns>
    public static bool Convert(string dicomFileName)
    {
        bool convertResult;

        // Provide information about the execution context to dicom2
        var program = new ProcessStartInfo
                          {
                              CreateNoWindow = true,
                              FileName = Dicom2Executable,
                              UseShellExecute = false,
                              WorkingDirectory = WorkingDirectory
                          };

        // Get the filename, and append it to the program arguments
        var fileName = dicomFileName.Split(DicomExtension, 2, StringSplitOptions.RemoveEmptyEntries)[0];
        program.Arguments = (ProgramArguments + fileName);

        // Invoke dicom2 to convert the file with the specified file name to PNG format

        /*
         * Check whether conversion was successful.
         * We do so by checking if there is a file with the same name, but with a .png extension.
         */
        var pngFileName = fileName + PngExtension;

        /* Using the variable pngFilePath created above, we check whether conversion is successful by
         * checking whether the file exists in the same directory that we uploaded the DICOM file to */
        try { }
        catch (NullReferenceException e) { }

        return false; // TODO: Remember to change this line
    }
}
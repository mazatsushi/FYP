using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

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
    private const string Dicom2Path = @"E:\Temp\Projects\FYP\SCE11-0353\Converter\dicom2.exe";

    /// <summary>
    /// A private string that contains the working directory for the dicom2 program.
    /// This is the main program that RIS uses for file conversion.
    /// </summary>
    private const string WorkDirectory = @"E:\Temp\Projects\FYP\SCE11-0353\Uploads";

    private static readonly string[] DicomExtension = new string[] { ".dcm", ".DCM" };
    
    /// <summary>
    /// The command string that we pass into dicom2 for file conversion.
    /// Make sure to append the DICOM file name (with the .dcm extension) before invoking the program.
    /// </summary>
    private const string ProgramArguments = "-p ";

    public static bool Convert(string dicomFileName, out string fileNameOnly)
    {
        var success = false;
        fileNameOnly = null;
        // Provide information about the execution context to dicom2
        var startInfo = new ProcessStartInfo
                          {
                              Arguments = (ProgramArguments + dicomFileName),
                              CreateNoWindow = true,
                              FileName = Dicom2Path,
                              UseShellExecute = false,
                              WorkingDirectory = WorkDirectory
                          };

        // Get the filename without any file extensions
        var fileName = dicomFileName.Split(DicomExtension, StringSplitOptions.RemoveEmptyEntries)[0];
        var dirInfo = new DirectoryInfo(WorkDirectory);

        try
        {
            // Invoke dicom2 to convert the file with the specified file name to PNG format
            using (var process = Process.Start(startInfo))
            {
                process.WaitForExit();
            }
            var files = dirInfo.GetFiles("*.png");
            if (files.Length >= 1 && files.Select(fileInfo => fileInfo.Name).All(name => name.Contains(fileName)))
            {
                fileNameOnly = fileName;
                success = true;
            }
        }
        catch (ObjectDisposedException) { }
        catch (InvalidOperationException) { }
        catch (ArgumentException) { }
        catch (FileNotFoundException) { }
        catch (Win32Exception) { }
        return success;
    }

    public static bool IsDicomFile(HttpPostedFile file)
    {
        var valid = false;
        var fileExt = Path.GetExtension(file.FileName);
        if (null != fileExt)
        {
            if (fileExt.Equals(".DCM") || fileExt.Equals(".dcm"))
            {
                valid = true;
            }
        }
        return valid;
    }
}
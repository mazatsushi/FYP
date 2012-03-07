using System;
using System.ComponentModel;
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
    private const string Dicom2Path = @"E:\Temp\Projects\FYP\SCE11-0353\Uploads\dicom2.exe";

    /// <summary>
    /// A private string that contains the working directory for the dicom2 program.
    /// This is the main program that RIS uses for file conversion.
    /// </summary>
    private const string WorkDirectory = @"E:\Temp\Projects\FYP\SCE11-0353\Uploads";

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
    /// <param name="dicomFileName">The name of the DICOM file (with the .dcm extension) to be converted</param>
    /// <returns>True if the conversion is successful. False otherwise.</returns>
    public static bool Convert(string dicomFileName)
    {
        var convertResult = false;

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

        // Perform some rudimentary string processing to get the expected output filename (with .png extension).
        var pngFilePath = WorkDirectory + fileName + PngExtension;

        try
        {
            // Invoke dicom2 to convert the file with the specified file name to PNG format
            using (var process = Process.Start(startInfo))
            {
                process.WaitForExit();
                /*
                 * Using the variable pngFilePath created above, we check whether conversion is successful by
                 * checking whether the file exists in the same directory that we uploaded the DICOM file to.
                 */
                convertResult = File.Exists(pngFilePath);
            }
        }
        catch (ObjectDisposedException e)
        {
            //The process object has already been disposed.
        }
        catch (InvalidOperationException e)
        {
            /*
             * No file name was specified in the startInfo parameter's FileName property.
             * -or-
             * The UseShellExecute property of the startInfo parameter is true and the RedirectStandardInput, RedirectStandardOutput, or RedirectStandardError property is also true.
             * -or-
             * The UseShellExecute property of the startInfo parameter is true and the UserName property is not Nothing or empty or the Password property is not Nothing.
             */
        }
        catch (ArgumentException e)
        {
            // The startInfo parameter is Nothing.
        }
        catch (FileNotFoundException e)
        {
            // The file specified in the startInfo parameter's FileName property could not be found.
        }
        catch (Win32Exception e)
        {
            /*
             * An error occurred when opening the associated file.
             * -or-
             * The sum of the length of the arguments and the length of the full path to the process exceeds 2080. The error message associated with this exception can be one of the following: "The data area passed to a system call is too small." or "Access is denied."
             */
        }
        return convertResult;
    }
}
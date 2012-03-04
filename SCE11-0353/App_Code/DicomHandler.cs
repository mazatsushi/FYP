using System;
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
	private static readonly UriBuilder Dicom2AbsolutePath = new UriBuilder(@"E:\Temp\Projects\FYP\SCE11-0353\Uploads");

	/// <summary>
	/// A private string that contains the absolute path for the IrfanView program.
	/// This is a backup program for conversion. Do not use unless necessary as it is
	/// unable to process more than single frame DICOM files.
	/// </summary>
	private UriBuilder _irfanViewAbsolutePath = new UriBuilder(@"C:\Program Files (x86)\IrfanView");

	private static readonly string[] DicomDelimiter = new[] {".dcm"};
	private const string PngExtension = ".png";

	/// <summary>
	/// The command string that we pass into dicom2 for file conversion.
	/// Make sure to append the DICOM file name (with the .dcm extension) before invoking the program.
	/// </summary>
	private const string Dicom2CommandString = "dicom2.exe -p ";

	/// <summary>
	/// Method for converting DICOM to PNG files
	/// </summary>
	/// <param name="dicomFileName">The name of the DICOM file to be converted</param>
	/// <returns>True if the conversion is successful. False otherwise.</returns>
	public static bool Convert(string dicomFileName)
	{
		bool convertResult;
		
		// Invoke dicom2 to convert the file with the specified file name to PNG

		// Remove the .dcm file extension from the DICOM file, and append the .png extension
		var pngFileName = (dicomFileName.Split(DicomDelimiter, 2, StringSplitOptions.RemoveEmptyEntries))[0] + PngExtension;
		var pngFilePath = Dicom2AbsolutePath + pngFileName;

		/* Using the variable pngFilePath created above, we check whether conversion is successful by
		 * checking whether the file exists in the same directory that we uploaded the DICOM file to */

		return false; // TODO: Remember to change this line
	}
}
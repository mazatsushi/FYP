using System;
using System.Drawing;
using System.Drawing.Imaging;
using EvilDicom.Image;

namespace Evil_DICOM
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
            Console.ReadLine();
        }

        private static void Test()
        {
            try
            {
                ImageMatrix im = new ImageMatrix(@"e:\temp\projects\fyp\samples\dicom images\CT-MONO2-16-brain.dcm");
                Image temp = im.GetImage(12);
                temp.Save(@"e:\test.jpg", ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

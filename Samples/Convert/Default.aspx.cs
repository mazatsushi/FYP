using System;
using EvilDicom.Image;
using ImageResizer;

public partial class _Default : System.Web.UI.Page
{
    private const string file = @"E:\Temp\Projects\FYP\Samples\Images\SPOTMGAN.dcm";

    protected void Page_Load(object sender, EventArgs e)
    {
        ImageBuilder.Current.Build(new ImageMatrix(file).GetImage(0), @"E:\Test.jpg", new ResizeSettings("maxwidth=300&maxheight=300"));
    }
}

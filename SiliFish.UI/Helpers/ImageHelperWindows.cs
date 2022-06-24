using System.Drawing.Drawing2D;

namespace SiliFish.UI
{
    public class ImageHelperWindows
    {
        public static Image CreateBlankImage(int width, int height)
        {
            Image img = new Bitmap(width, height);
            return img;
        }

        public static Image MergeImages(List<Image> ImageList, int nRow, int nCol, bool rowsFirst = true)
        {
            if (ImageList == null || ImageList.Count == 0 || ImageList.Count(img => img != null) == 0)
                return null;
            int width = ImageList.Max(img => img.Width);
            int height = ImageList.Max(img => img.Height);
            Image imgMatrix = CreateBlankImage(width * nCol, height * nRow);
            int rowInd = 0;
            int colInd = 0;
            using (var canvas = Graphics.FromImage(imgMatrix))
            {
                canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                foreach (Image img in ImageList)
                {
                    canvas.DrawImage(img, colInd * width, rowInd * height);
                    if (rowsFirst)
                    {
                        colInd++;
                        if (colInd == nCol)
                        {
                            rowInd++;
                            colInd = 0;
                        }
                    }
                    else
                    {
                        rowInd++;
                        if (rowInd == nRow)
                        {
                            colInd++;
                            rowInd = 0;
                        }
                    }
                    if (colInd == nCol || rowInd == nRow) break;
                }
                canvas.Save();
            }
            return imgMatrix;
        }
    }
}

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;
using System.Linq;

namespace Zebrafish.Helpers
{
    internal class ImageHelper
    {
        public static Image<Rgba32> CreateBlankImage(int width, int height)
        {
            Image<Rgba32> img = new(width, height);
            img.Mutate(ig => ig.BackgroundColor(Color.White));
            return img;
        }

        public static Image<Rgba32> MergeImages(List<Image> ImageList, int nRow, int nCol, bool rowsFirst = true)
        {
            if (ImageList == null || ImageList.Count == 0 || ImageList.Count(img => img != null) == 0)
                return null;
            int width = ImageList.Max(img => img.Width);
            int height = ImageList.Max(img => img.Height);
            Image<Rgba32> imgMatrix = CreateBlankImage(width * nCol, height * nRow);
            int rowInd = 0;
            int colInd = 0;
            foreach (Image img in ImageList)
            {
                imgMatrix.Mutate(ig => ig.DrawImage(img, location: new Point(colInd * width, rowInd * height), 1));
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
            return imgMatrix;
        }
    }
}

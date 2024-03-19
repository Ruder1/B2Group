using PresentationLayer.Models;
using System.Drawing;

namespace PresentationLayer
{
    public class Service
    {
        public bool IsPointInside(PointViewModel point, List<PointViewModel> polygon)
        {
            bool isInside = false;
            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                if ((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y) &&
                    (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                {
                    isInside = !isInside;
                }
            }
            return isInside;
        }
    }
}

using System.Text.Json.Serialization;

namespace PresentationLayer.Models
{
    public class PolygonViewModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Id { get; set; }
        
        public string Name { get; set; }

        public List<PointViewModel> Points { get; set; }
    }
}

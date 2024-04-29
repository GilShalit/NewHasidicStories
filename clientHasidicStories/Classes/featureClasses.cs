using static System.Runtime.InteropServices.JavaScript.JSType;

namespace clientHasidicStories.Classes
{

    public class clsGeoJson
    {
        public string type { get; set; } = "geojson";
        public Data data { get; set; } = new Data();
    }

    public class Data
    {
        private List<Feature> lFeatures = new List<Feature>();
        public string type { get; set; } = "FeatureCollection";
        public Feature[] features { get => lFeatures.ToArray(); }
        public void Add(Feature feature) { lFeatures.Add(feature); }
        public float[] Center
        {
            get
            {
                float lat = 0;
                float lon = 0;
                foreach (Feature f in lFeatures)
                {
                    lat += f.geometry.coordinates[0]; 
                    lon += f.geometry.coordinates[1];
                }
                lat = lat / lFeatures.Count();
                lon=lon / lFeatures.Count();
                return [lat, lon];
            }
        }
    }

    public class Feature
    {
        public string type { get; set; } = "Feature";
        public Geometry geometry { get; set; } = new Geometry();
        public Properties properties { get; set; } = new Properties();
    }

    public class Geometry
    {
        public string type { get; set; } = "Point";
        public float[] coordinates { get; set; } = new float[2];
    }

    public class Properties
    {
        public string name { get; set; }
        public string link { get; set; }
        public string xmlid { get; set; }
    }
}

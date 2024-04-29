namespace clientHasidicStories.Classes
{

    public class clsGeoJson
    {
        public string type { get; set; } = "geojson";
        public Data data { get; set; }
    }

    public class Data
    {
        public string type { get; set; } = "FeatureCollection";
        public Feature[] features { get; set; }
    }

    public class Feature
    {
        public string type { get; set; } = "Feature";
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; } = "Point";
        public float[] coordinates { get; set; }
    }

    public class Properties
    {
        public string name { get; set; }
    }
}

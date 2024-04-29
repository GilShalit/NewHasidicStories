namespace clientHasidicStories.Classes
{

    public class clsGeoJson
    {
        public string type { get; set; } = "geojson";
        public Data data { get; set; } = new Data();
    }

    public class Data
    {
        private List<Feature> lFeatures=new List<Feature>();
        public string type { get; set; } = "FeatureCollection";
        public Feature[] features { get => lFeatures.ToArray(); }
        public void Add(Feature feature) {  lFeatures.Add(feature); }
    }

    public class Feature
    {
        public string type { get; set; } = "Feature";
        public Geometry geometry { get; set; }=new Geometry();
        public Properties properties { get; set; }=new Properties();
    }

    public class Geometry
    {
        public string type { get; set; } = "Point";
        public float[] coordinates { get; set; }= new float[2];
    }

    public class Properties
    {
        public string name { get; set; }
        public string link { get; set; }
        public string xmlid { get; set; }
    }
}

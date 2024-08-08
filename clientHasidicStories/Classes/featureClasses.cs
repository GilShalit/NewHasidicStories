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
        public List<Feature> lFeatures = new List<Feature>();//turned public so it is serialized between tabs
        public string type { get; set; } = "FeatureCollection";
        public Feature[] features { get => lFeatures.Where(f => f.selected).ToArray(); }
        public void Add(Feature feature) { lFeatures.Add(feature); }
        public Feature featureById(string id) => lFeatures.Where(f => f.properties.xmlid == id).FirstOrDefault();
        public float[] Center
        {
            get
            {
                float lat = 0;
                float lon = 0;
                foreach (Feature f in lFeatures.Where(f => f.geometry != null))
                {
                    lat += f.geometry.coordinates[0];
                    lon += f.geometry.coordinates[1];
                }
                lat = lat / lFeatures.Count();
                lon = lon / lFeatures.Count();
                return [lat, lon];
            }
        }
        public void selectFeature(string xmlid)
        {
            Feature feature = lFeatures.Where(f => f.properties.xmlid == xmlid).FirstOrDefault();
            //not selecting features with a 0,0 coordinate
            if (feature != null && (feature.geometry.coordinates[0] != 0 || feature.geometry.coordinates[1] != 0)) feature.selected = true;
        }
        public void unselectAllFeatures()
        {
            foreach (Feature feature in lFeatures) feature.selected = false;
        }
    }

    public class Feature
    {
        public string type { get; set; } = "Feature";
        public Geometry geometry { get; set; } = new Geometry();
        public Properties properties { get; set; } = new Properties();
        public bool selected { get; set; } = true;
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

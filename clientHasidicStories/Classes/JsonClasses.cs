using System.Text.Json.Serialization;

namespace clientHasidicStories.Classes
{


    public class clsFileList
    {
        public string _class { get; set; }
        public string datatemplate { get; set; }
        public string datatemplateperpage { get; set; }
        public string datapaginationstart { get; set; }
        public string datapaginationtotal { get; set; }
        public Li[] li { get; set; }
    }

    public class Li
    {
        public string _class { get; set; }
        public string[] text { get; set; }
        public Header header { get; set; }
        [JsonPropertyName("pb-restricted")] public PbRestricted pbrestricted { get; set; }
    }

    public class Header
    {
        public string datatemplate { get; set; }
        public Div div { get; set; }
    }

    public class Div
    {
        public string _class { get; set; }
        public string[] text { get; set; }
        public Div1[] div { get; set; }
    }

    public class Div1
    {
        public string _class { get; set; }
        public A a { get; set; }
        public object div { get; set; }
    }

    public class A
    {
        public string href { get; set; }
        public string _class { get; set; }
        public string target { get; set; }
        public H5 div { get; set; }
    }

    public class H5
    {
        public string _class { get; set; }
        [JsonPropertyName("#text")] public string text { get; set; }
    }

    public class PbRestricted
    {
        public string group { get; set; }
        public string[] text { get; set; }
        [JsonPropertyName("pb-ajax")] public PbAjax pbajax { get; set; }
    }

    public class PbAjax
    {
        public string url { get; set; }
        public string method { get; set; }
        public string emit { get; set; }
        public string _event { get; set; }
        public string confirm { get; set; }
        public string quiet { get; set; }
        public string[] text { get; set; }
        public PaperIconButton papericonbutton { get; set; }
        public H3 h3 { get; set; }
    }

    public class PaperIconButton
    {
        public string icon { get; set; }
    }

    public class H3
    {
        public string slot { get; set; }
        public string[] text { get; set; }
        public PbI18n pbi18n { get; set; }
    }

    public class PbI18n
    {
        public string key { get; set; }
    }


}

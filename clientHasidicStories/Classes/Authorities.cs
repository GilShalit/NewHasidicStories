namespace clientHasidicStories.Classes
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.tei-c.org/ns/1.0", IsNullable = false)]
    public partial class TEI
    {
        public clsTEITeiHeader teiHeader { get; set; }
        public TEIText text { get; set; }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class clsTEITeiHeader
    {
        public TEITeiHeaderFileDesc fileDesc { get; set; }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDesc
    {
        public TEITeiHeaderFileDescTitleStmt titleStmt { get; set; }
        public TEITeiHeaderFileDescPublicationStmt publicationStmt { get; set; }
        public TEITeiHeaderFileDescSourceDesc sourceDesc { get; set; }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDescTitleStmt
    {
        public string title { get; set; }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDescPublicationStmt
    {
        public string p { get; set; }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDescSourceDesc
    {
        [System.Xml.Serialization.XmlArrayItemAttribute("person", IsNullable = false)]
        public TEITeiHeaderFileDescSourceDescPerson[] listPerson { get; set; }
        [System.Xml.Serialization.XmlArrayItemAttribute("place", IsNullable = false)]
        public TEITeiHeaderFileDescSourceDescPlace[] listPlace { get; set; }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDescSourceDescPerson
    {
        public string name { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute("xml:id")]
        public string xmlid { get; set; }
        public TEITeiHeaderFileDescSourceDescPlaceIdno idno { get; set; }

    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDescSourceDescPlace
    {
        public TEITeiHeaderFileDescSourceDescPlacePlaceName placeName { get; set; }
        public TEITeiHeaderFileDescSourceDescPlaceLocation location { get; set; }
        public TEITeiHeaderFileDescSourceDescPlaceIdno idno { get; set; }
        [System.Xml.Serialization.XmlAttributeAttribute("xml:id")]
        public string xmlid { get; set; }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDescSourceDescPlacePlaceName
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type { get; set; }

        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value { get; set; }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDescSourceDescPlaceLocation
    {
        public string geo { get; set; }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDescSourceDescPlaceIdno
    {

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type { get; set; }

        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value { get; set; }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEIText
    {
        public TEITextBody body { get; set; }
    }

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITextBody
    {
        public string p { get; set; }
    }


}

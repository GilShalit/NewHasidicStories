namespace clientHasidicStories.Classes
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.tei-c.org/ns/1.0", IsNullable = false)]
    public partial class TEI
    {

        /// <remarks/>
        public TEITeiHeader teiHeader { get; set; }

        /// <remarks/>
        public TEIText text { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeader
    {
        /// <remarks/>
        public TEITeiHeaderFileDesc fileDesc { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDesc
    {

        /// <remarks/>
        public TEITeiHeaderFileDescTitleStmt titleStmt { get; set; }
        /// <remarks/>
        public TEITeiHeaderFileDescPublicationStmt publicationStmt { get; set; }
        /// <remarks/>
        public TEITeiHeaderFileDescSourceDesc sourceDesc { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDescTitleStmt
    {

        /// <remarks/>
        public string title { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDescPublicationStmt
    {

        /// <remarks/>
        public string p { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDescSourceDesc
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("person", IsNullable = false)]
        public TEITeiHeaderFileDescSourceDescPerson[] listPerson { get; set; }
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("place", IsNullable = false)]
        public TEITeiHeaderFileDescSourceDescPlace[] listPlace { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDescSourceDescPerson
    {

        /// <remarks/>
        public string name { get; set; }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string xmlid { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDescSourceDescPlace
    {

        /// <remarks/>
        public TEITeiHeaderFileDescSourceDescPlacePlaceName placeName { get; set; }
        /// <remarks/>
        public TEITeiHeaderFileDescSourceDescPlaceLocation location { get; set; }
        /// <remarks/>
        public TEITeiHeaderFileDescSourceDescPlaceIdno idno { get; set; }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string xmlid { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDescSourceDescPlacePlaceName
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type { get; set; }
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value { get; set; }
    }
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDescSourceDescPlaceLocation
    {

        /// <remarks/>
        public string geo { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITeiHeaderFileDescSourceDescPlaceIdno
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type { get; set; }
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEIText
    {

        /// <remarks/>
        public TEITextBody body { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tei-c.org/ns/1.0")]
    public partial class TEITextBody
    {

        /// <remarks/>
        public string p { get; set; }
    }


}

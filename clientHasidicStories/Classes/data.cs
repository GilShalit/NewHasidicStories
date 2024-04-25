
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute("editions", Namespace = "", IsNullable = false)]
public partial class clsEditionsData
{

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("edition")]
    public clsEditionData[] edition { get; set; }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute("edition", Namespace = "", IsNullable = false)]
public partial class clsEditionData
{
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("story", IsNullable = false)]
    public clsStory[] stories { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string editionTitle { get; set; }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class clsStory
{
    /// <remarks/>
    public string ana { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("placeRef", IsNullable = false)]
    public string[] places { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("persRef", IsNullable = false)]
    public string[] persons { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Id { get; set; }
}


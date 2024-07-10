
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute("editions",Namespace = "", IsNullable = false)]
public partial class clsEditionsStoryTexts
{
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("edition")]
    public editionStoryTexts[] editions { get; set;}
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class editionStoryTexts
{
    /// <remarks/>
    public string name { get; set;}

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("story", IsNullable = false)]
    public story[] stories { get; set; }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class story
{

    /// <remarks/>
    public string id { get; set;}

    /// <remarks/>
    public string text { get; set; }
}


using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class TestSerialize {
    [XmlAttribute("Id")]
    public int Id { get; set; }
    
    [XmlAttribute("Name")]
    public string Name { get; set; }
    
    [XmlElement("List")]
    public List<int> ListTest { get; set; }
    
    [XmlAttribute("PrivateId")]
    private int PrivateId { get; set; }
    
    [XmlAttribute("PrivateName")]
    private string PrivateName { get; set; }
    
    [XmlElement("PrivateList")]
    private List<int> PrivateList { get; set; }

    public void SetPrivateId(int _id) {
        PrivateId = _id;
    }
    
    public void SetPrivateName(string _name) {
        PrivateName = _name;
    }
    
    public void SetPrivateList(int _value) {
        if (PrivateList == null) {
            PrivateList = new List<int>();
        }
        PrivateList.Add(_value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextAsset", menuName = "TextAsset")]
public class Asset_ViewText : ScriptableObject {
    [SerializeField, Multiline(3)]
    private List<string> _Strings = new List<string>();
    public List<string> Strings
    {
        get { return _Strings; }
    }
}

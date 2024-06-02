using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MeshProperties
{
    public string filepath;
}

[Serializable]
public class MeshModalities
{
    public Dictionary<string, MeshProperties> modalities = new Dictionary<string, MeshProperties>();
}

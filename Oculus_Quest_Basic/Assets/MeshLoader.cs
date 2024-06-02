using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MeshLoader : MonoBehaviour
{
    [SerializeField]
    private string filePath = "Assets/Resources/DictMockup/exp_XR_salento.json";
    [SerializeField]
    private GameObject containerManipulator;
    // Start is called before the first frame update
    void Start()
    {
        string jsonString = File.ReadAllText(filePath);
        JObject jsonObject = JObject.Parse(jsonString);

        //MeshModalities meshModalities = JsonUtility.FromJson<MeshModalities>(jsonString);
        Debug.Log("data");

        foreach (var locationProperty in jsonObject)
        {
            // Accessing key and value
            Debug.Log("Key: " + locationProperty.Key);
            string nameObj = locationProperty.Key;
            Debug.Log("Value: " + locationProperty.Value);
            JObject jsonObjectInner = JObject.Parse(locationProperty.Value.ToString());

            foreach (var method_Metadata in jsonObjectInner)
            {
                //Debug.Log("Key: " + method_Metadata.Key);
                //Debug.Log("Value: " + method_Metadata.Value);
                Dictionary<string, string> dict_model = method_Metadata.Value.ToObject<Dictionary<string, string>>();

                string prefabFilePath = dict_model["filepath"];
                //prefabFilePath = prefabFilePath.Replace("Assets/Resources/Custom_Mesh/GT/", "");
                //prefabFilePath = prefabFilePath.Replace(".obj", "");

                //LoadObj(prefabFilePath);
                var obj = Resources.Load<GameObject>(prefabFilePath);
                Debug.Log("test");
                //Instantiate Main prefab object container
                GameObject containerManipulatorObj = Instantiate(this.containerManipulator, Vector3.zero, Quaternion.identity);
                containerManipulatorObj.name = nameObj  + "_" + method_Metadata.Key;
                GameObject containerManipulatorObjVisuals = containerManipulatorObj.transform.Find("Visuals").gameObject;

                //instantiate obj
                GameObject instantiated = Instantiate(obj, Vector3.zero, Quaternion.identity);
                instantiated.transform.parent = containerManipulatorObjVisuals.transform;

                BoxCollider meshChild = instantiated.transform.Find("default").gameObject.AddComponent<BoxCollider>();
                //meshChild.convex = true;
            }
        }
    }


    void LoadObj(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("File does not exist: " + filePath);
            return;
        }

        // Read all lines from the file
        string[] lines = File.ReadAllLines(filePath);

        // Parse the .obj file and create a mesh
        Mesh mesh = new Mesh();
        // Implement your own .obj file parser here

        // Create a GameObject
        GameObject obj = new GameObject();
        obj.AddComponent<MeshFilter>().mesh = mesh;
        obj.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Standard")); // Or use your own shader

        // Set position, rotation, scale if needed
        obj.transform.position = Vector3.zero;

        // (Optional) If you want to automatically calculate normals and apply them
        mesh.RecalculateNormals();
    }

}

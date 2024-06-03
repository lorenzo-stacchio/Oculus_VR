using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

public class MeshLoader : MonoBehaviour
{
    [SerializeField]
    private string filePath = "Assets/Resources/DictMockup/exp_XR_salento.json";
    [SerializeField]
    private GameObject containerManipulator;

    [SerializeField]
    private GameObject groundFloor;
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

            float offset_x = 2f;
            int count = 0;
            foreach (var method_Metadata in jsonObjectInner)
            {
                //Debug.Log("Key: " + method_Metadata.Key);
                //Debug.Log("Value: " + method_Metadata.Value);
                JObject jsonObjectInnerValues = JObject.Parse(method_Metadata.Value.ToString());

                //Dictionary<string, string> dict_model = method_Metadata.Value.ToObject<Dictionary<string, string>>();

                string prefabFilePath = jsonObjectInnerValues["filepath"].ToString();
                string typeFormat = jsonObjectInnerValues["type"].ToString();
                Vector3 vectorRotation = JsonUtility.FromJson<Vector3>(jsonObjectInnerValues["rotation"].ToString());
                float scaleMetric = float.Parse(jsonObjectInnerValues["scale_metric"].ToString(), CultureInfo.InvariantCulture);
                float offset_factor = float.Parse(jsonObjectInnerValues["offset_factor"].ToString(), CultureInfo.InvariantCulture);

                //prefabFilePath = prefabFilePath.Replace("Assets/Resources/Custom_Mesh/GT/", "");
                //prefabFilePath = prefabFilePath.Replace(".obj", "");

                //LoadObj(prefabFilePath);
                var obj = Resources.Load<GameObject>(prefabFilePath);
                Debug.Log("test");

                //Instantiate Main prefab object container
                GameObject containerManipulatorObj = Instantiate(this.containerManipulator, Vector3.zero, Quaternion.identity);
                containerManipulatorObj.transform.parent = this.gameObject.transform;
                containerManipulatorObj.name = nameObj  + "_" + method_Metadata.Key;
                containerManipulatorObj.transform.position = new Vector3(count * offset_x, 0.0f, 0.0f);
                containerManipulatorObj.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                GameObject containerManipulatorObjVisuals = containerManipulatorObj.transform.Find("Visuals").gameObject;
               

                //instantiate INNER obj
                GameObject instantiated = Instantiate(obj, Vector3.zero, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                instantiated.transform.parent = containerManipulatorObjVisuals.transform;

                // set as prefixed in the json
                Quaternion rotation = Quaternion.Euler(vectorRotation.x, vectorRotation.y, vectorRotation.z);
                //instantiated.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                instantiated.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                instantiated.transform.localRotation = rotation;


                GameObject TargetVisuals = containerManipulatorObjVisuals.transform.Find("Target").gameObject;
                //Attach fix position with respect to target
                //FixPositionWithRespectTo instantiatedFixed = instantiated.AddComponent<FixPositionWithRespectTo>();
                //instantiatedFixed.Init(TargetVisuals, -1f);


                if (typeFormat == "obj") {
                 
                    GameObject child = instantiated.transform.Find("default").gameObject;
                    FixPositionWithRespectTo instantiatedFixed = child.AddComponent<FixPositionWithRespectTo>();
                    instantiatedFixed.Init(TargetVisuals, -1 * offset_factor, groundFloor);

                    BoxCollider meshChild = child.gameObject.AddComponent<BoxCollider>();
                    // Attach fix scale to make all the meshes equally greater
                    FixSize instantiatedFixedSizeObj = child.AddComponent<FixSize>();
                    instantiatedFixedSizeObj.Init(scaleMetric);
                    //DestroyImmediate(child.gameObject.GetComponent<BoxCollider>());
                    //child.gameObject.AddComponent<BoxCollider>();

                }
                else if (typeFormat == "glb")
                { // does not have default child
                    FixPositionWithRespectTo instantiatedFixed = instantiated.AddComponent<FixPositionWithRespectTo>();
                    instantiatedFixed.Init(TargetVisuals, -1 * offset_factor, groundFloor);
                    BoxCollider meshChild = instantiated.AddComponent<BoxCollider>();
                    // Attach fix scale to make all the meshes equally greater
                    FixSize instantiatedFixedSizeGlb = instantiated.AddComponent<FixSize>();
                    instantiatedFixedSizeGlb.Init(scaleMetric);
                    //DestroyImmediate(meshChild.gameObject.GetComponent<BoxCollider>());
                    //meshChild.gameObject.AddComponent<BoxCollider>();

                }


                count++;
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

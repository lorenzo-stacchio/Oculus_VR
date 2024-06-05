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

    [SerializeField]
    private List<GameObject> spawnedVisuals;

    //public ListGa
    // Start is called before the first frame update
    void Start()
    {
        string jsonString = File.ReadAllText(filePath);
        JObject jsonObject = JObject.Parse(jsonString);
        this.spawnedVisuals = new List<GameObject>();
        //MeshModalities meshModalities = JsonUtility.FromJson<MeshModalities>(jsonString);
        Debug.Log("data");

        float offset_x = 3f;
        int count = 0; // hypothesize only one series of objects



        foreach (var locationProperty in jsonObject) // this should contain only one key e.g. Statue
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
                
                this.spawnedVisuals.Add(containerManipulatorObjVisuals);

                //instantiate INNER obj
                GameObject instantiated = Instantiate(obj, Vector3.zero, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                instantiated.transform.parent = containerManipulatorObjVisuals.transform;

                // set as prefixed in the json
                Quaternion rotation = Quaternion.Euler(vectorRotation.x, vectorRotation.y, vectorRotation.z);
                //instantiated.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                instantiated.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                instantiated.transform.localRotation = rotation;


                GameObject TargetVisuals = containerManipulatorObj.transform.FindChildRecursive("Target").gameObject;

                //Attach fix position with respect to target
                //FixPositionWithRespectTo instantiatedFixed = instantiated.AddComponent<FixPositionWithRespectTo>();
                //instantiatedFixed.Init(TargetVisuals, -1f);


                if (typeFormat == "obj") {
                 
                    GameObject child = instantiated.transform.Find("default").gameObject;
                    BoxCollider meshChild = child.gameObject.AddComponent<BoxCollider>();
                    // Attach fix scale to make all the meshes equally greater
                    FixSize instantiatedFixedSizeObj = child.AddComponent<FixSize>();
                    instantiatedFixedSizeObj.Init(scaleMetric);
                    //DestroyImmediate(child.gameObject.GetComponent<BoxCollider>());
                    //child.gameObject.AddComponent<BoxCollider>();

                    FixPositionWithRespectTo instantiatedFixed = child.AddComponent<FixPositionWithRespectTo>();
                    instantiatedFixed.Init(TargetVisuals, -1 * offset_factor, groundFloor);

                   

                }
                else if (typeFormat == "glb")
                { // does not have default child
                    BoxCollider meshChild = instantiated.AddComponent<BoxCollider>();
                    // Attach fix scale to make all the meshes equally greater
                    FixSize instantiatedFixedSizeGlb = instantiated.AddComponent<FixSize>();
                    instantiatedFixedSizeGlb.Init(scaleMetric);
                    //DestroyImmediate(meshChild.gameObject.GetComponent<BoxCollider>());
                    //meshChild.gameObject.AddComponent<BoxCollider>();

                    FixPositionWithRespectTo instantiatedFixed = instantiated.AddComponent<FixPositionWithRespectTo>();
                    instantiatedFixed.Init(TargetVisuals, -1 * offset_factor, groundFloor);
                    

                }


                count++;

                //fix original transform
                //containerManipulatorObjVisuals.GetComponent<NerfMeshManager>().SetFirstTransform(containerManipulatorObjVisuals.transform.position, containerManipulatorObjVisuals.transform.localScale, containerManipulatorObjVisuals.transform.rotation);
            }
        }

        //// put this objectat the center location
        Vector3 tempTransform = this.transform.position;
        float countOffset = count % 2 == 0 ? (count / 2) + 0.5f : (float)(count / 2);
        countOffset += -1;
        tempTransform.x -= offset_x * countOffset;
        this.transform.position = tempTransform;

        foreach(GameObject SV in spawnedVisuals)
        {
            SV.GetComponent<NerfMeshManager>().SetFirstTransform(SV.transform.position, SV.transform.localScale, SV.transform.rotation);

        }
    }

}

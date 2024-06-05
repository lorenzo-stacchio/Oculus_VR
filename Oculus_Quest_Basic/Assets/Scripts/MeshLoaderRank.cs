using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

public class MeshLoaderRank : MonoBehaviour
{
    [SerializeField]
    private string filePath = "Assets/Resources/DictMockup/exp_XR_salento_Rank.json";
    [SerializeField]
    private GameObject containerManipulator;

    [SerializeField]
    private GameObject groundFloor;

    [SerializeField]
    private GameObject RankManager;

    // Start is called before the first frame update
    void Start()
    {
        string jsonString = File.ReadAllText(filePath);
        JObject jsonObject = JObject.Parse(jsonString);

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
                JObject jsonObjectInnerValues = JObject.Parse(method_Metadata.Value.ToString());

                //Dictionary<string, string> dict_model = method_Metadata.Value.ToObject<Dictionary<string, string>>();

                string prefabFilePath = jsonObjectInnerValues["filepath"].ToString();
                string typeFormat = jsonObjectInnerValues["type"].ToString();
                Vector3 vectorRotation = JsonUtility.FromJson<Vector3>(jsonObjectInnerValues["rotation"].ToString());
                float scaleMetric = float.Parse(jsonObjectInnerValues["scale_metric"].ToString(), CultureInfo.InvariantCulture);


                var obj = Resources.Load<GameObject>(prefabFilePath);

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
                instantiated.transform.localPosition = new Vector3(0, 2.0f, -2.0f);  

                // set as prefixed in the json
                Quaternion rotation = Quaternion.Euler(vectorRotation.x, vectorRotation.y, vectorRotation.z);
                //instantiated.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                instantiated.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                instantiated.transform.localRotation = rotation;


                if (typeFormat == "obj") {
                    GameObject child = instantiated.transform.Find("default").gameObject;
                    BoxCollider meshChild = child.gameObject.AddComponent<BoxCollider>();
                    // Attach fix scale to make all the meshes equally greater
                    FixSize instantiatedFixedSizeObj = child.AddComponent<FixSize>();
                    instantiatedFixedSizeObj.Init(scaleMetric);

                }
                else if (typeFormat == "glb")
                { // does not have default child
                    BoxCollider meshChild = instantiated.AddComponent<BoxCollider>();
                    // Attach fix scale to make all the meshes equally greater
                    FixSize instantiatedFixedSizeGlb = instantiated.AddComponent<FixSize>();
                    instantiatedFixedSizeGlb.Init(scaleMetric);
  

                }


                count++;
            }
        }

        // put this objectat the center location
        Vector3 tempTransform = this.transform.position;
        float countOffset = count % 2 == 0 ? (count / 2) + 0.5f : (float)(count / 2);
        countOffset += -1;
        tempTransform.x -= offset_x * countOffset;
        this.transform.position = tempTransform;
    }

    public void PickNearestScorer(GameObject toScore)
    {

        var scorers = this.RankManager.GetComponent<RankManager>().getScorers();
        var minScorer = new GameObject(); //default
        var minDistance = float.PositiveInfinity;
        
        foreach (GameObject scorer in scorers)
        {
            float distance = Vector3.Distance(scorer.transform.position, toScore.transform.position);

            if (distance < minDistance)
            {
                minScorer = scorer;
                minDistance = distance;
            } 
        }

        minScorer.GetComponentInChildren<BaseRankManager>().placeObject(toScore);
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = minScorer.transform.position;

        GameObject sphere2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere2.transform.position = toScore.transform.position;
        
        Debug.DrawRay(minScorer.transform.position, toScore.transform.position, Color.green);

        //this.scoringRanks = scoringRanks;
    }


}

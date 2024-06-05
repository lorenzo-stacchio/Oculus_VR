using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRankManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

   

    public void placeObject(GameObject objectToPlace)
    {
        GameObject baseRank = this.transform.FindChildRecursive("RankBase").gameObject;
        float distance = Vector3.Distance(baseRank.transform.position, objectToPlace.transform.position);
        if (distance < 5)
        {
            //objectToPlace.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);//baseRank.transform.position;
            //objectToPlace.transform.position = new Vector3(3.0f, 0.0f, 3.0f);//baseRank.transform.position;
            objectToPlace.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);//baseRank.transform.position;
            objectToPlace.transform.position = baseRank.transform.position;//baseRank.transform.position;

            objectToPlace.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);//baseRank.transform.rotation;
            objectToPlace.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);//baseRank.transform.rotation;

        }
        //otherwise do not place

    }

}

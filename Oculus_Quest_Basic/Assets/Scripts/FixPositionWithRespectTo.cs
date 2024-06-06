using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPositionWithRespectTo : MonoBehaviour
{
    private GameObject withRespectTo;
    private GameObject groundFloor;
    private float forwardOffset;
    private float heigth = 2.0f;

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(this.name + "   " + this.transform.rotation.eulerAngles.ToString());

        MeshFilter meshFilter = GetComponent<MeshFilter>();

        Mesh mesh = meshFilter.sharedMesh;

        Bounds bounds = mesh.bounds;

        Vector3 min = bounds.min;
        Debug.Log(this.name + "   " + min.ToString());

    }


    public void Init(GameObject withRespectTo, float forwardOffset, GameObject groundFloor)
    {
        this.withRespectTo = withRespectTo;
        this.forwardOffset = forwardOffset;
        this.groundFloor = groundFloor;
        // centered and back with respect to target
        this.Reset();
    }


    public void Reset()
    {
        // centered and back with respect to target
        this.transform.position = withRespectTo.transform.position;
        //then move position backward with a fixed offset
        this.transform.position += withRespectTo.transform.forward * this.forwardOffset;

        BoxCollider boxCollider = this.GetComponent<BoxCollider>();

        if (boxCollider != null)
        {
            // Calculate the bottom of the BoxCollider relative to the object's position
            float bottomOffset = boxCollider.center.y - (boxCollider.size.y / 2);

            // Adjust the position so the bottom of the BoxCollider is at floorHeight
            Vector3 spawnPosition = this.transform.position;
            //spawnPosition.y = this.groundFloor.transform.position.y - bottomOffset;
            spawnPosition.y = heigth; // bad fix until not solving

            // Set the adjusted position
            this.transform.position = spawnPosition;
        }
        else
        {
            Debug.LogError("The prefab does not have a BoxCollider component.");
        }
    }
}

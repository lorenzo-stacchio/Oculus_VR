using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixSize : MonoBehaviour
{
    // Desired dimensions in world units
    public float desiredHeight; // it will all be scaled according to this parameter

    public void Init(float desiredHeight)
    {
        this.desiredHeight = desiredHeight;
        this.Reset();
    }


    public void Reset()
    {
        // Get the MeshFilter component
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter != null)
        {

            Mesh mesh = meshFilter.sharedMesh;

            // Get the mesh bounds
            //Bounds bounds = meshFilter.sharedMesh.bounds;

            // Get the current dimensions of the mesh
            //Vector3 currentDimensions = bounds.size;

            Vector3 scale = this.transform.lossyScale;
            Bounds bounds = mesh.bounds;

            float realWidth = bounds.size.x;
            float realHeight = bounds.size.y;
            float realDepth = bounds.size.z;

            //Vector3 size = transform.localToWorldMatrix.MultiplyVector(currentDimensions);

            //calculate scale
            float scaleY = desiredHeight / realHeight;

            // Calculate the scale factors
            //float scaleX = desiredWidth / currentDimensions.x;
            //float scaleY = desiredHeight / currentDimensions.y;
            //float scaleZ = desiredDepth / currentDimensions.z;

            // Apply the scale factors to the transform
            transform.localScale = new Vector3(scaleY, scaleY, scaleY);
        }
        else
        {
            Debug.LogError("MeshFilter component not found!");
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPositionWithRespectTo : MonoBehaviour
{
    private GameObject withRespectTo;
    private float forwardOffset;

    // Update is called once per frame
    void Update()
    {

        Debug.Log(this.name + "   " + this.transform.rotation.eulerAngles.ToString());
    }


    public void Init(GameObject withRespectTo, float forwardOffset)
    {
        this.withRespectTo = withRespectTo;
        this.forwardOffset = forwardOffset;
        // centered and back with respect to target
        this.Reset();
    }


    public void Reset()
    {
        // centered and back with respect to target
        this.transform.position = withRespectTo.transform.position;
        //then move position backward with a fixed offset
        this.transform.position += withRespectTo.transform.forward * this.forwardOffset;
    }
}

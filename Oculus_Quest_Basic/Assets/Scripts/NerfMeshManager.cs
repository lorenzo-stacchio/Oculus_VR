using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Oculus.Interaction;
using Oculus.Haptics;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction.Input;

public class NerfMeshManager : MonoBehaviour
{
    Handedness currentHand;
    bool interaction_started = false;
    Vector3 lastPosition;
    Quaternion lastRotation;
    Vector3 lastScale;

    private void LateUpdate()
    {
        if (currentHand == Handedness.Left && this.interaction_started)
        {
            //this.StopMotion();
            this.AssignCompleteTransform(lastPosition, lastRotation,lastScale);

        }
    }

    void AssignCompleteTransform(Vector3 newPosition, Quaternion newRotation, Vector3 newScale)
    {
        // Set the position
        this.transform.position = newPosition;
        // Set the rotation
        this.transform.rotation = newRotation;
        // Set the scale
        this.transform.localScale = newScale;
    }

    public void StopMotion()
    {

        Rigidbody rbdy = this.gameObject.GetComponent<Rigidbody>();
        //Stop Moving/Translating
        rbdy.velocity = Vector3.zero;
        //Stop rotating
        rbdy.angularVelocity = Vector3.zero;
        rbdy.constraints = RigidbodyConstraints.FreezeAll;
        Debug.Log(rbdy);
    }


    public void InteractionCather(PointerEvent evt)
    {
        //check hand is what we wanted
        HandGrabInteractor handGrabInteractor = (HandGrabInteractor)evt.Data;
        currentHand = handGrabInteractor.Hand.Handedness;
        if (currentHand == Handedness.Left && ! this.interaction_started)
        {
            Debug.Log("HERE START");
            this.interaction_started = true;
            this.lastPosition = this.gameObject.transform.position;
            this.lastRotation = this.gameObject.transform.rotation;
            this.lastScale = this.gameObject.transform.localScale;
        }
        
    }

    public void InteractionOuter(PointerEvent evt)
    {
        Debug.Log("HERE EHD");

        StartCoroutine(WaitForSeconds(5f));

    }

    IEnumerator WaitForSeconds(float seconds)
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(seconds);

        // After 3 seconds, continue executing the code below this line
        Debug.Log("Three seconds have passed!");

        this.interaction_started = false;

    }




}

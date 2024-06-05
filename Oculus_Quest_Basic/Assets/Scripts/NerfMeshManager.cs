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
using System;

public class NerfMeshManager : MonoBehaviour
{
    Handedness currentHand;
    bool interaction_started = false;
    Vector3 lastPosition;
    Quaternion lastRotation;
    Vector3 lastScale;
    Vector3 FirstTransform;
    Vector3 FirstScale;
    Quaternion FirstRotation;

    string Name;

    public void Start()
    {
        this.Name = this.gameObject.transform.parent.gameObject.name;
    }

    private void LateUpdate()
    {
        if (currentHand == Handedness.Left && this.interaction_started)
        {
            this.StopMotion();
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
        rbdy.useGravity = false;
        //Stop Moving/Translating
        rbdy.velocity = Vector3.zero;
        //Stop rotating
        rbdy.angularVelocity = Vector3.zero;
        rbdy.constraints = RigidbodyConstraints.FreezeAll;
        Debug.Log(rbdy);
    }


    public void UnStopMotion()
    {

        Rigidbody rbdy = this.gameObject.GetComponent<Rigidbody>();
        rbdy.useGravity = true;
        //Stop Moving/Translating
        rbdy.velocity = Vector3.zero;
        //Stop rotating
        rbdy.angularVelocity = Vector3.zero;
        rbdy.constraints = RigidbodyConstraints.None;
        Debug.Log(rbdy);
    }

    public void StopPosition()
    {

        Rigidbody rbdy = this.gameObject.GetComponent<Rigidbody>();
        rbdy.useGravity = false;
        //Stop Moving/Translating
        rbdy.velocity = Vector3.zero;
        //Stop rotating
        rbdy.angularVelocity = Vector3.zero;
        rbdy.constraints = RigidbodyConstraints.FreezePosition;
        Debug.Log(rbdy);
    }


    public void InteractionCather(PointerEvent evt)
    {
        //check hand is what we wanted
        HandGrabInteractor handGrabInteractor = (HandGrabInteractor)evt.Data;
        currentHand = handGrabInteractor.Hand.Handedness;
        if (currentHand == Handedness.Left)
        {
            Debug.Log("HERE START");
            this.interaction_started = true;
            this.lastPosition = this.gameObject.transform.position;
            this.lastRotation = this.gameObject.transform.rotation;
            this.lastScale = this.gameObject.transform.localScale;
        }
        else
        {
            this.interaction_started = false;
        }
        
    }


    public void OnInteractionStart(PointerEvent evt)
    {
        this.StopPosition();
        string RotoTranslationValue = this.TransformToString(this.gameObject.transform);

        DataModel test = new DataModel(Logger.logger.FirstName, Logger.logger.LastName, Logger.logger.ID,
                        "Base",  this.Name, "Exploration", "Interaction Started", "RotoTranslation", System.DateTime.Now, RotoTranslationValue);

        Logger.logger.Add_Row_Log(test.ToString());
    }


    public void OnInteractionEnd(PointerEvent evt)
    {
        this.StopMotion();
        string RotoTranslationValue = this.TransformToString(this.gameObject.transform);

        DataModel test = new DataModel(Logger.logger.FirstName, Logger.logger.LastName, Logger.logger.ID,
                        "Base",  this.Name, "Exploration", "Interaction Ended", "RotoTranslation", System.DateTime.Now, RotoTranslationValue);

        Logger.logger.Add_Row_Log(test.ToString());
        //this.UnStopMotion();
    }


    public void OnInteractionEndRank(PointerEvent evt)
    {
        //ONLY FOR RANK MODE
        GameObject sphere2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sphere2.transform.position = this.gameObject.transform.position;
        sphere2.name = "Before unstop";

        this.StopMotion();
        MeshLoaderRank comp = GameObject.Find("Meshes").GetComponentInParent<MeshLoaderRank>();

        //DataModel test = new DataModel(Logger.logger.FirstName, Logger.logger.LastName, Logger.logger.ID,
        //                "Base", this.Name, "Exploration", "Interaction Ended", "RotoTranslation", System.DateTime.Now, RotoTranslationValue);

        //Logger.logger.Add_Row_Log(test.ToString());
        this.UnStopMotion();
        GameObject sphere3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sphere3.transform.position = this.gameObject.transform.position;
        sphere3.name = "After unstop";
        //comp.PickNearestScorer(this.gameObject.transform.parent.gameObject);
        comp.PickNearestScorer(this.gameObject);
        this.StopMotion();

    }



    public void OnInteraction(PointerEvent evt)
    {

        string RotoTranslationValue = this.TransformToString(this.gameObject.transform);

        DataModel test = new DataModel(Logger.logger.FirstName, Logger.logger.LastName, Logger.logger.ID,
                        "Base",  this.Name, "Exploration", "Movement Interaction", "RotoTranslation", System.DateTime.Now, RotoTranslationValue);

        Logger.logger.Add_Row_Log(test.ToString());
    }


    public void Hovering(PointerEvent evt)
    {

        string RotoTranslationValue = this.TransformToString(this.gameObject.transform);

        DataModel test = new DataModel(Logger.logger.FirstName, Logger.logger.LastName, Logger.logger.ID,
                        "Base",  this.Name, "Exploration", "Hovering Interaction", "RotoTranslation", System.DateTime.Now, RotoTranslationValue);

        Logger.logger.Add_Row_Log(test.ToString());
    }

    public void unHovering(PointerEvent evt)
    {

        string RotoTranslationValue = this.TransformToString(this.gameObject.transform);

        DataModel test = new DataModel(Logger.logger.FirstName, Logger.logger.LastName, Logger.logger.ID,
                        "Base",  this.Name, "Exploration", "UnHovering Interaction", "RotoTranslation", System.DateTime.Now, RotoTranslationValue);

        Logger.logger.Add_Row_Log(test.ToString());
    }

    public void SetFirstTransform(Vector3 transform2, Vector3 localScale, Quaternion rotation)
    {
        this.FirstTransform = transform2;
        this.FirstScale = localScale;
        this.FirstRotation = rotation;
    }

    public void ResetTransform()
    {
        this.UnStopMotion();
        this.transform.position= FirstTransform;
        this.transform.rotation = FirstRotation;
        this.transform.localScale = FirstScale;

    }

    private string TransformToString(Transform transform)
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        Vector3 scale = transform.localScale;

        string to_ret =  $"Pos_{position.x}---{position.y}---{position.z}|" +
               $"Rot_{rotation.x}---{rotation.y}---{rotation.z}---{rotation.w}|" +
               $"Sc_{scale.x}---{scale.y}---{scale.z}";

        to_ret = to_ret.Replace(',', '.');

        return to_ret;

    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class testbutton : MonoBehaviour
{
    private bool state = true;
    [SerializeField]
    private TextMeshPro text;
    [SerializeField]
    private Material material;
    [SerializeField]
    private GameObject toGrab;
    public void changeState()
    {
        this.state = ! this.state;

        if (this.state)
        {
            this.text.text = "Grabbable";
            this.material.color= new Color(0f, 255f, 0f);
            this.toGrab.GetComponent<Grabbable>().enabled = true;
            this.toGrab.GetComponent<PointableUnityEventWrapper>().enabled = true;
            this.toGrab.GetComponentInChildren<GrabInteractable>().enabled = true;
            this.toGrab.GetComponentInChildren<HandGrabInteractable>().enabled = true;
        }
        else
        {
            this.text.text = "Denied Grabbable";
            this.material.color = new Color(255f, 0f, 0f);
            this.toGrab.GetComponent<Grabbable>().enabled = false;
            this.toGrab.GetComponent<PointableUnityEventWrapper>().enabled = false;
            this.toGrab.GetComponentInChildren<GrabInteractable>().enabled = false;
            this.toGrab.GetComponentInChildren<HandGrabInteractable>().enabled = false;
        }
    }
    
}

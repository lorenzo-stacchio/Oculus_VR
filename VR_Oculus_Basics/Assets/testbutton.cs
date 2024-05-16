using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction;

public class testbutton : MonoBehaviour
{
    private bool state = false;

    [SerializeField]
    private TextMeshPro text;

    [SerializeField]
    private Material material;

    public void test()
    {
        Debug.Log("Aldo");
    }


    public void changeText()
    {
        this.state = ! this.state;

        if (this.state)
        {
            this.text.text = "Active";
            this.material.color= new Color(0f, 255f, 0f);

        }
        else
        {
            this.text.text = "Non active";
            this.material.color = new Color(255f, 0f, 0f);

        }
    }
}

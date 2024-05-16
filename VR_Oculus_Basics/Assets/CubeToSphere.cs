using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeToSphere : MonoBehaviour
{
    //[SerializeField]

    private GameObject to_transform;
    private GameObject to_transform2;


    // Start is called before the first frame update
    void Start()
    {
        this.to_transform = this.gameObject.transform.Find("ToTransform").gameObject;
        this.to_transform2 = this.gameObject.transform.Find("ToTransform2").gameObject;

    }



    public void grabSelect()
    {

        Renderer renderer = this.to_transform2.GetComponent<Renderer>();
        renderer.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        this.to_transform.SetActive(false);
        this.to_transform2.SetActive(true);

        
    }


    public void grabUnSelect()
    {
        Renderer renderer = this.to_transform.GetComponent<Renderer>();
        renderer.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        this.to_transform.SetActive(true);
        this.to_transform2.SetActive(false);
      
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using TMPro;
public class ScoreManager : MonoBehaviour
{

    private int selectedScore;
    private GameObject lastSelected;
    
    [SerializeField]
    private GameObject prefabButton;


    [SerializeField]
    private GameObject targetObject;
    private GameObject scoreObject;

    [SerializeField]
    private int maxScore = 1;

    private string MeshName;

    // Start is called before the first frame update
    private float offsetScore = +0.36f;

    void Start()
    {
        this.scoreObject = Instantiate(this.prefabButton, new Vector3(0, 0, 0), Quaternion.identity);
        this.scoreObject.transform.parent = this.gameObject.transform.parent;
        this.MeshName = this.gameObject.transform.parent.name;
        this.scoreObject.transform.position = new Vector3(0, 0, 0); //center at button 3
        this.scoreObject.transform.position = this.scoreObject.transform.position + transform.right * offsetScore;
        
        this.scoreObject.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

        this.scoreObject.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));

        Debug.Log(this.scoreObject.name);

        for (int i = 0; i < this.scoreObject.transform.childCount; i++)
        {
            // Get the child transform at index i
            Transform childTransform = this.scoreObject.transform.GetChild(i);

            // Print the name of the child GameObject
            Debug.Log("Child " + i + ": " + childTransform.gameObject.name);
            GameObject childScoreButton = childTransform.gameObject;
            int index = int.Parse(childScoreButton.name);
            childScoreButton.transform.FindChildRecursive("Visuals").gameObject.name = "Visuals_" + (index + 1).ToString();
            childScoreButton.GetComponentInChildren<InteractorValue>().SetValues(int.Parse(childScoreButton.name),this.gameObject);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (this.targetObject != null)
        {
            // Set this object as a child of the target object
            //transform.SetParent(target);

            // Optionally, reset the local position and rotation if needed
            this.scoreObject.transform.position = this.targetObject.transform.position;// new Vector3(this.targetObject.transform.position.x, this.targetObject.transform.position.y, this.targetObject.transform.position.z + offsetScore);
            this.scoreObject.transform.position = this.scoreObject.transform.position + transform.right * offsetScore;

            //FAST FIX
            this.scoreObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            this.targetObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            //this.scoreObject.transform.rotation = new Quaternion(0.0f, 0.0, 0.0f, 0.0f);
        }
        else
        {
            Debug.LogError("Target is not assigned.");
        }
    }


    public void AddMark(GameObject scoreClicked)
    {
        
        scoreClicked.transform.FindChildRecursive("Panel").gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 255.0f, 0.0f);
        string actionState = "";

        if (this.lastSelected == null)
        {
            //scoreClicked.transform.FindChildRecursive("Panel").gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 255.0f, 0.0f);
            actionState = "FirstMark";
        }
        else
        {
            if (this.lastSelected.Equals(scoreClicked))
            {
                actionState = "ClickedSameValue";
            }
            else
            {
                this.lastSelected.transform.FindChildRecursive("Panel").gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f);
                actionState = "MarkChanged";
                this.lastSelected = scoreClicked;
                //this.lastSelected.transform.FindChildRecursive("Panel").gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 255.0f, 0.0f);
            }
        }
        

        this.lastSelected = scoreClicked;
        this.lastSelected.transform.FindChildRecursive("Panel").gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 255.0f, 0.0f);
        this.selectedScore = scoreClicked.GetComponentInChildren<InteractorValue>().getScoreValue();



        DataModel test = new DataModel(Logger.logger.FirstName, Logger.logger.LastName,
                        "Base", this.MeshName, "Scoring", "Novel Score", actionState, System.DateTime.Now, this.selectedScore.ToString());

        Logger.logger.Add_Row_Log(test.ToString());
    }


}

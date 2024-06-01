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
    private int maxScore = 1;

    // Start is called before the first frame update
    
    void Start()
    {
        //generate buttons
        //for (int i=0; i < this.maxScore; i++)
        //{
        //    GameObject button = Instantiate(this.prefabButton, new Vector3(0, 0, 0), Quaternion.identity);
        //    button.transform.parent = this.gameObject.transform;
        //    button.transform.position = new Vector3(0, 0, i * (-0.18f));
        //    button.name = (i + 1).ToString();
        //    InteractorValue comp = button.AddComponent<InteractorValue>();
        //    comp.Init(i + 1, this);
        //    //IPointable test = new IPointable();

        //    // set clicked function
        //    button.transform.FindChildRecursive("Visual").gameObject.name= "Visual_" + (i + 1).ToString();

        //    button.transform.FindChildRecursive("Text").GetComponent<TextMeshPro>().text = (i + 1).ToString();
        //}

        for (int i = 0; i < this.transform.childCount; i++)
        {
            // Get the child transform at index i
            Transform childTransform = this.transform.GetChild(i);

            // Print the name of the child GameObject
            Debug.Log("Child " + i + ": " + childTransform.gameObject.name);
            GameObject childScoreButton = childTransform.gameObject;
            int index = int.Parse(childScoreButton.name);
            childScoreButton.transform.FindChildRecursive("Visuals").gameObject.name = "Visuals_" + (index + 1).ToString();
            childScoreButton.GetComponentInChildren<InteractorValue>().SetValues(int.Parse(childScoreButton.name),this.gameObject);
        }

        //    foreach (GameObject score in this.transform.FindChildren())
        //{
        //    GameObject button = Instantiate(this.prefabButton, new Vector3(0, 0, 0), Quaternion.identity);
        //    button.transform.parent = this.gameObject.transform;
        //    button.transform.position = new Vector3(0, 0, i * (-0.18f));
        //    button.name = (i + 1).ToString();
        //    InteractorValue comp = button.AddComponent<InteractorValue>();
        //    comp.Init(i + 1, this);
        //    //IPointable test = new IPointable();

        //    // set clicked function
        //    button.transform.FindChildRecursive("Visual").gameObject.name = "Visual_" + (i + 1).ToString();

        //    button.transform.FindChildRecursive("Text").GetComponent<TextMeshPro>().text = (i + 1).ToString();
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
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
                        "Base", this.gameObject.name, "Scoring", "Novel Score", actionState, System.DateTime.Now, this.selectedScore.ToString());

        Logger.logger.Add_Row_Log(test.ToString());
    }


}

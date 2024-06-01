using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction; 

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
        for (int i=0; i < this.maxScore; i++)
        {
            //GameObject button = Instantiate(this.prefabButton, new Vector3(0,0,0), Quaternion.identity);
            //button.transform.parent = this.gameObject.transform;
            //button.transform.position = new Vector3(0, 0, i * (-0.18f));
            //button.name = (i+1).ToString();
            //InteractorValue comp = button.AddComponent<InteractorValue>();
            //comp.Init(i+1, this);
            //IPointable test = new IPointable();

            // set clicked function

            //button.transform.Find("Visuals").GetComponent<PointableUnityEventWrapper>().WhenSelect += comp.Clicked();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddMark(GameObject scoreClicked)
    {
        
        scoreClicked.transform.FindChildRecursive("Panel").gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 255.0f, 0.0f);
        string actionState = "";

        if (this.lastSelected.Equals(null))
        {
            this.lastSelected.transform.FindChildRecursive("Panel").gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 255.0f, 0.0f);
            actionState = "FirstMark";
        } else
        {
            this.lastSelected.transform.FindChildRecursive("Panel").gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f);
            actionState = "MarkChanged";

        }
       

        this.lastSelected = scoreClicked;

        this.selectedScore = scoreClicked.GetComponent<InteractorValue>().getScoreValue();



        DataModel test = new DataModel(Logger.logger.FirstName, Logger.logger.LastName,
                        "Base", this.gameObject.name, "Scoring", "Novel Score", actionState, System.DateTime.Now, this.selectedScore.ToString());

        Logger.logger.Add_Row_Log(test.ToString());
    }


}

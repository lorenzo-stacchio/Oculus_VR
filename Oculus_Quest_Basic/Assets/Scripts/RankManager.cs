using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using TMPro;
public class RankManager : MonoBehaviour
{

    private int selectedScore;
    private GameObject lastSelected;
    
    [SerializeField]
    private GameObject prefabButton;

    [SerializeField]
    private GameObject floor;

    private List<GameObject> scorers;


    // Start is called before the first frame update
    private float offsetScore = 6.0f;

    void Start()
    {

        //Debug.Log(IDManager.Instance.GetSubjectID());
        var test = 4;
        var offset = 0.005f;
       
        scorers = new List<GameObject>();
       

        for (int i = 0; i < test; i++)
        {
            // Get the child transform at index i
            var scoreObject = Instantiate(this.prefabButton, new Vector3(0, 0, 0), Quaternion.identity);
            scoreObject.name = "Score" + (i+1).ToString();
            scoreObject.transform.parent = this.gameObject.transform;
            //scoreObject.transform.position = new Vector3(0, 0, 0);
            //scoreObject.transform.localPosition = new Vector3(0, 0, 0);

            var y_coordinate = this.floor.transform.position.y + (this.floor.transform.position.y - scoreObject.transform.FindChildRecursive("RankBase").transform.position.y);
            y_coordinate += offset;

            scoreObject.transform.position = new Vector3(0, y_coordinate, 0); //center at button 3
            scoreObject.transform.position = scoreObject.transform.position + transform.right * (offsetScore*i);
            scoreObject.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

            scoreObject.transform.FindChildRecursive("Text").GetComponent<TextMeshPro>().text = (i + 1).ToString() + "°";

            scoreObject.AddComponent<BaseRankManager>();
            Debug.Log(scoreObject.name);
            scorers.Add(scoreObject);

        }

        Vector3 pos = this.transform.position;
        pos.x = -1 * ((test / 2) * offsetScore);
        this.transform.position = pos;


    }

    public List<GameObject> getScorers()
    {
        return this.scorers;
    }


    public void AddMark(GameObject scoreClicked)
    {
        
        //scoreClicked.transform.FindChildRecursive("Panel").gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 255.0f, 0.0f);
        //string actionState = "";

        //if (this.lastSelected == null)
        //{
        //    //scoreClicked.transform.FindChildRecursive("Panel").gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 255.0f, 0.0f);
        //    actionState = "FirstMark";
        //}
        //else
        //{
        //    if (this.lastSelected.Equals(scoreClicked))
        //    {
        //        actionState = "ClickedSameValue";
        //    }
        //    else
        //    {
        //        this.lastSelected.transform.FindChildRecursive("Panel").gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f);
        //        actionState = "MarkChanged";
        //        this.lastSelected = scoreClicked;
        //        //this.lastSelected.transform.FindChildRecursive("Panel").gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 255.0f, 0.0f);
        //    }
        //}
        

        //this.lastSelected = scoreClicked;
        //this.lastSelected.transform.FindChildRecursive("Panel").gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 255.0f, 0.0f);
        //this.selectedScore = scoreClicked.GetComponentInChildren<InteractorValue>().getScoreValue();



        //DataModel test = new DataModel(Logger.logger.FirstName, Logger.logger.LastName, Logger.logger.ID,
        //                "Base", this.MeshName, "Scoring", "Novel Score", actionState, System.DateTime.Now, this.selectedScore.ToString());

        //Logger.logger.Add_Row_Log(test.ToString());
    }


}

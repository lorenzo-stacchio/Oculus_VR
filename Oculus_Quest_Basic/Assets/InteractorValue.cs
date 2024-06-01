using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using System;

public class InteractorValue : MonoBehaviour
{
    // Start is called before the first frame update
    private int scoreValue;

    private ScoreManager scoreManager;

    //public event Action<PointerEvent> WhenPointerEventRaised;

    public void Init(int scoreValue, ScoreManager manager)
    {
        this.scoreValue = scoreValue;
        this.scoreManager = manager;
    }


    public int getScoreValue()
    {
        return scoreValue;
    }


    public void Clicked()
    {
        this.scoreManager.AddMark(this.gameObject);
    }


}

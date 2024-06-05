using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;

public class IDManager : MonoBehaviour
{
    public static IDManager Instance { get; private set; }

    // Start is called before the first frame update
    [SerializeField]
    private InputField OVRKeyboardTextField;

    private string subjectID;
    private string FirstName;
    private string LastName;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    public void StoreIDname()
    {
        this.subjectID = this.CleanFilePath(this.OVRKeyboardTextField.text);
        this.FirstName = "userFirstName";
        this.LastName = "userLastName";
        SceneManager.LoadScene("TestManipulation_SanGinesio_Score");

    }

    public string GetSubjectID()
    {
        return this.subjectID;
    }

    public string GetSubjectFirstName()
    {
        return this.FirstName;
    }

    public string GetSubjectLastName()
    {
        return this.LastName;
    }

    public string CleanFilePath(string input)
    {
        // Define invalid characters based on the file system
        char[] invalidChars = Path.GetInvalidFileNameChars();

        // Remove invalid characters
        string cleaned = new string(input.Where(c => !invalidChars.Contains(c)).ToArray());

        // Trim leading and trailing whitespace
        cleaned = cleaned.Trim();

        // Optionally, replace spaces with underscores
        cleaned = cleaned.Replace(" ", "_");

        // Ensure the length of the filename is within acceptable limits
        if (cleaned.Length > 255)
        {
            cleaned = cleaned.Substring(0, 255);
        }

        return cleaned;
    }

}

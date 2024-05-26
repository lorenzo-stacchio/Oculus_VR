using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Reflection;
using System;
using System.Text.RegularExpressions;

public class Logger : MonoBehaviour
{

    [SerializeField]
    public string folderPath;

    private string dirsavePath;
    private string savePath;



    [SerializeField]
    public string FirstName;

    [SerializeField]
    public string LastName;


    [SerializeField]
    public int Id;


    // Private and not editor-visible variables
    private List<string> csv_logs;
    public static Logger logger;



    // Start is called before the first frame update
    void Start()
    {
        logger = this;
        logger.csv_logs = new List<string>();
        logger.dirsavePath = logger.folderPath + $"{this.FirstName}_{this.LastName}/";
        logger.savePath = logger.dirsavePath + $"{this.FirstName}_{this.LastName}_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}_test.csv";

        if (!Directory.Exists(logger.dirsavePath))
        {
            Directory.CreateDirectory(logger.dirsavePath);
        }

        List<String> names = GetFields(typeof(DataModel));
        //create user folder if not exist

        logger.Add_Row_Log(string.Join(',', names));
    }


    public void Add_Row_Log(string row)
    {

        logger.csv_logs.Add(row);
        logger.WriteCSV("updated");
    }


    private void WriteCSV(string message)
    {
        // Create a new StreamWriter to write to the CSV file.
        StreamWriter writer = new StreamWriter(logger.savePath);

        // Write the CSV header (column names).
        foreach (string row in logger.csv_logs)
        {
            writer.WriteLine(row);

        }

        // Close the StreamWriter to save the file.
        writer.Close();
    }


    private List<string> GetFields(Type type)
    {
        FieldInfo[] myFieldInfo = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance
           | BindingFlags.Public);
        string pattern = @"\<(.*?)\>"; // Matches text enclosed within square brackets
        List<String> names = new List<string>();
        // Display the field information of FieldInfoClass.
        for (int i = 0; i < myFieldInfo.Length; i++)
        {
            MatchCollection matches = Regex.Matches(myFieldInfo[i].Name, pattern);
            string temp_name = matches[0].Value;
            temp_name = temp_name.Replace("<", "");
            temp_name = temp_name.Replace(">", "");
            names.Add(temp_name);

        }

        return names;
    }

}
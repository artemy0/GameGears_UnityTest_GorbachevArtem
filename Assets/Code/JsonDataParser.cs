using System.IO;
using UnityEngine;

public class JsonDataParser : IDataParser
{
    private readonly string filePath;

    public JsonDataParser()
    {
        filePath = Application.dataPath + "/Resources/data.txt";
    }

    public Data GetData()
    {
        string json = "";
        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                json += line;
            }
        }

        if (string.IsNullOrEmpty(json))
        {
            return new Data();
        }

        return JsonUtility.FromJson<Data>(json);
    }
}

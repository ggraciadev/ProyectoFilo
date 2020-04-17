using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVToJSON : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<string> GetCSVAttributes(string csv)
    {
        string temp = "";
        List<string> attributes = new List<string>();
        for (int i = 0; csv[i] != '\n'; i++)
        {

            if (csv[i] == ',')
            {
                attributes.Add(temp);
                temp = "";
            }
            else if (csv[i] != '\r')
            {
                temp += csv[i];
            }
        }
        attributes.Add(temp);
        return attributes;
    }

    public static string CSVToJson(string csv)
    {
        string json = "[\n{\n";
        List<string> attributes = GetCSVAttributes(csv);
        string temp = "";
        int currentIteratorX = 0;

        int tempIterator = 0;
        while (csv[tempIterator] != '\n') ++tempIterator;

        for(int i = tempIterator+1; i < csv.Length; ++i)
        {
            if (csv[i] == '\n')
            {
                if (json[json.Length - 1] != '}')
                    json += "\"" + attributes[currentIteratorX++] + "\": " + "\"" + temp + "\"\n}";
                currentIteratorX = 0;
                temp = "";
            }
            else if (csv[i] == ',')
            {
                if (json[json.Length - 1] != '}')
                    json += "\"" + attributes[currentIteratorX] + "\": " + "\"" + temp + "\",";
                currentIteratorX++;
                temp = "";
            }
            else if (csv[i] != '\r')
            {
                if(json[json.Length - 1] == '}')
                {
                    json += ",\n{\n";
                }
                temp += csv[i];
            }
        }

        json += "\"" + attributes[currentIteratorX++] + "\": " + "\"" + temp + "\"\n}\n]";

        return json;
    }
}

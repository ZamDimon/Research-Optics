using UnityEngine;
using UnityEditor;
using System.IO;

public class TextManager : MonoBehaviour
{
    
    /* [MenuItem("Tools/Write file")]
    public static void WriteString(string line)
    {
        string path = "Assets/Text_output/Output.txt";

        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(line);
        writer.Close();

        AssetDatabase.ImportAsset(path); 
        TextAsset asset = (TextAsset)Resources.Load("test");

        Debug.Log(asset.text);
    } */
    

    /*[MenuItem("Tools/Read file")]
    public static void ReadString()
    {
        string path = "Assets/Resources/test.txt";

        StreamReader reader = new StreamReader(path); 
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    } */
}

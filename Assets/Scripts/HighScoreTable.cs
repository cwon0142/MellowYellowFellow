using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{

    [SerializeField]
    string highscoreFile = "scores.txt";

    // Serialised member variable to hold the font it will be rendered with
    [SerializeField]
    Font scoreFont;

    // Storing high scores
    struct HighScoreEntry
    {
        public int score;
        public string name;
    }

    List<HighScoreEntry> allScores = new List<HighScoreEntry>();

    // Start is called before the first frame update
    void Start()
    {
        LoadHighScoreTable();
        SortHighScoreEntries();
        CreateHighScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Reading high scores
    public void LoadHighScoreTable()
    {
        using (TextReader file = File.OpenText(highscoreFile))
        {
            string text = null;
            while ((text = file.ReadLine()) != null)
            {
                Debug.Log(text);
                string[] splits = text.Split(' ');
                HighScoreEntry entry;
                entry.name = splits[0];
                entry.score = int.Parse(splits[1]);
                allScores.Add(entry);
            }
        }
    }

    // Displaying high schores
    void CreateHighScoreText()
    {
        for (int i = 0; i < allScores.Count; ++i)
        {
            // Instantiate a Unity GameObject for each high score entry
            GameObject o = new GameObject();
            o.transform.parent = transform;

            // Add text component
            Text t = o.AddComponent<Text>();
            t.text = allScores[i].name + "\t\t" + allScores[i].score;
            t.font = scoreFont;
            t.fontSize = 50;

            o.transform.localPosition = new Vector3(0, -(i) * 6, 0);

            o.transform.localRotation = Quaternion.identity;
            o.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            o.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 100);
        }
    }

    // Sorting high schores
    public void SortHighScoreEntries()
    {
        allScores.Sort((x, y) => y.score.CompareTo(x.score));
    }
}

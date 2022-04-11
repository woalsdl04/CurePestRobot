using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Rank 
{
    public string name = "";
    public float score = 0f;

    public Rank(string _name, float _score) 
    {
        name = _name;
        score = _score;
    }
}

public class Ranking : MonoBehaviour
{
    public List<Text> ScoreText = new List<Text>();
    public void AddRank(Rank newRank) 
    {
        V.Rank.Add(newRank);

        for (int i = 0; i < V.Rank.Count; i++)
        {
            int Count = 0;

            for (int j = 0; j < V.Rank.Count; j++)
            {
                if(V.Rank[i].score < V.Rank[j].score || 
                   (V.Rank[i].score == V.Rank[j].score && i < j)) 
                {
                    Count++;
                }
            }

            if(Count >= ScoreText.Count) 
            {
                V.Rank.RemoveAt(i);
                continue;
            }

            ScoreText[Count].text = V.Rank[i].name + " " + string.Format("{0:#,###}", V.Rank[i].score);
        }
    }

    public void SetRank() 
    {
        for (int i = 0; i < V.Rank.Count; i++)
        {
            int Count = 0;

            for (int j = 0; j < V.Rank.Count; j++)
            {
                if (V.Rank[i].score < V.Rank[j].score ||
                   (V.Rank[i].score == V.Rank[j].score && i < j))
                {
                    Count++;
                }
            }

            if (Count >= ScoreText.Count)
            {
                V.Rank.RemoveAt(i);
                continue;
            }

            ScoreText[Count].text = V.Rank[i].name + " " + string.Format("{0:#,###}", V.Rank[i].score);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ScoreText = V.FInd_Child_Component_List<Text>(gameObject);

        SetRank();
        //AddRank(new Rank("BBB", 10000));
        //AddRank(new Rank("AAA", 100000));
        //AddRank(new Rank("CCC", 1000));
        //AddRank(new Rank("cscs", 321541));
        //AddRank(new Rank("wdq", 1000032));
        //AddRank(new Rank("dwqd", 100002));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

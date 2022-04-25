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
    public void AddRank(Rank newRank) //랭킹 추가
    {
        V.Rank.Add(newRank);

        SetRank();
    }

    public void SetRank() //랭킹 정리
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
        ScoreText = V.Find_Child_Component_List<Text>(gameObject);

        SetRank();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

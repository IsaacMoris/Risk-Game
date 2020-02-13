using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using TMPro;


public class Graph : MonoBehaviour
{
    // Start is called before the first frame update

    bool[,] v = new bool[50, 50];
    GlobalClass obj;
    private void Awake()
    {
       
        obj = GetComponent<GlobalClass>();
        string line;
        string path = "Assets/Graph.txt";
        System.IO.StreamReader file =
            new System.IO.StreamReader(path);
        while ((line = file.ReadLine()) != null)
        {

            int node = -1, sum = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ' ')
                {
                    if (node == -1)
                        node = sum;
                    else
                        v[node, sum] = true;
                    sum = 0;
                    continue;
                }
                sum = sum * 10 + Convert.ToInt32(line[i]) - Convert.ToInt32('0');
            }
        }

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       


    }


    public List<int> can_attack(int selected , int cur_player)
    {
      //  Debug.Log(selected + " hhhhh  "+ cur_player)
        List<int> countries = new List<int>();
        for (int i = 1; i <= 33; i++)
            if (v[selected,i] && obj.country_owner[i]!= cur_player)
            { countries.Add(i);
              //  Debug.Log(i);
            }
        return countries; 
    }

    public List<int> move_soldier(int selected, int cur_player)
    {
        Queue<int> q = new Queue<int>();
        List<int> countries = new List<int>();
         bool [] vis = new bool[50];
         q.Enqueue(selected);
         while (q.Count>0)
         {
             int cur = q.Peek();
             q.Dequeue();
            if (vis[cur])
                continue;
            vis[cur] = true;
            countries.Add(cur);
            for (int i = 1; i <= 33; i++)
                if (v[cur, i] && obj.country_owner[i]==cur_player)
                    q.Enqueue(i);
         }
        return countries;
    }
 
 }


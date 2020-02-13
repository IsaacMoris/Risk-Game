using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalClass : MonoBehaviour
{
    // Start is called before the first frame update

    public  int[]  country_owner   = new int[50];
    public  int[] country_soliders  = new int[50];
    public  List<Player> players = new List<Player>();
    public  Material[] matrials = new Material[3];
    public  int nofplayers=3;
    public int cur_phase;
    public Queue<int> players_turns = new Queue<int>();

     void Awake()
    {

        distribute_country_material();
        change_tags();
        distributePlayers();
        cur_phase = 2;
      //  players_turns = new Queue<int>();
    }
    void Start()
    {
        update_material();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {  for (int i = 1; i <= 33; i++)
        {
            GameObject.FindGameObjectWithTag("T" + i.ToString()).GetComponent<TextMesh>().text = country_soliders[i].ToString();
            Debug.Log(i.ToString());


            }
            Debug.Log("stop");



        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
      

    }

    public void update_material()
    {
        for (int i = 1; i <= 33; i++)
        {
            GameObject.FindGameObjectWithTag(i.ToString()).GetComponent<SpriteRenderer>().material =players[country_owner[i]].matrial;
        }
    }
    public void distributePlayers()
    {
        try
        {

            List<int> cur_countries = new List<int>();
            System.Random rnd = new System.Random();
            for (int i = 1; i <= 33; i++)
                cur_countries.Add(i);
            int cur_p = 0;
            for (int i = 1; i <= 33; i++)
            {
                // choose random country 
                int x = rnd.Next(0, 33 - i); 
                int cun = cur_countries[x]; 
                country_soliders[i] = 5;

                // assign it to player cur_p

                country_owner[cun] = cur_p;  
                players[cur_p].countries.Add(cur_countries[x]); 

                // remove it
                cur_countries.RemoveAt(x);

                // next player
                cur_p = (cur_p + 1) % nofplayers;
            }

        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }
    void change_tags()
    {
        for(int i=1;i<=33;i++)
        {
            GameObject.FindGameObjectWithTag(i.ToString()).transform.GetChild(0).GetChild(0).tag = "T" + i.ToString();
        }
    }
   void distribute_country_material()
    {
        for (int i = 0; i < nofplayers; i++)
        {
            players.Add(new Player());
            players_turns.Enqueue(i);
            players[i].matrial = matrials[i];
        }
    }

}
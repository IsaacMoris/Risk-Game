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
    int cnt = 33;
    Camera cam;
    public static bool question, attack_more_bool = false;
    int currentplayer = 1;
    public GameObject counter;
    public GameObject question_canvas;
    public GameObject counter_text;
    public GameObject attack_more_canvas;

    int soldiers, numberofcountriesOfenemy;
    public Material new_material;
    public Material default_material;
    public Material shadow;
    public Material originalMat;
    public static int selected_country, attacked_country;
    public static int counter_attack, counter_soldier;

    string s = "";
    bool[,] v = new bool[50, 50];
    int attack_phase = 1;

    GlobalClass obj;
    private void Awake()
    {
        cam = GetComponent<Camera>();
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

        if (obj.cur_phase == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {

                    if (attack_phase == 1)
                    {

                        selected_country = Convert.ToInt32(hit.collider.gameObject.tag);
                        if (obj.country_owner[selected_country] == currentplayer && obj.country_soliders[selected_country] > 1)
                        {
                            attack_phase++;

                            available_to_attack(selected_country, can_attack(selected_country, currentplayer));
                            talker.say_instruction("select country to attack");
                        }


                    }
                    if (attack_phase == 2)
                    {
                        attacked_country = Convert.ToInt32(hit.collider.gameObject.tag);

                        if (can_attack(selected_country, currentplayer).Contains(attacked_country) == true)
                        {
                            attack_phase++;

                            counter.SetActive(true);
                            talker.say_instruction("Choose number of soldiers you attack with big number comes with a big Risk");
                            counter_attack = obj.country_soliders[selected_country];


                        }

                    }




                }

            }
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("right click");

                close_attack();


            }
            if (attack_phase == 3)
            {
                if (counter.activeSelf == false)
                {

                    soldiers = Convert.ToInt32(counter_text.GetComponent<TextMeshPro>().text);
                    question_canvas.SetActive(true);
                    Questiones objectt = GetComponent<Questiones>();
                    objectt.next_qestion();
                    attack_phase++;
                    talker.say_instruction("Answer Question if you answer correctly the enemy will lose the number you attacked with if your answer was wrong you will lose your soldiers");


                }
            }
            if (attack_phase == 4)
                if (!question_canvas.activeSelf)
                {
                    if (question)
                    {
                        obj.country_soliders[attacked_country] -= soldiers;
                        if (obj.country_soliders[attacked_country] <= 0)
                        {
                            Debug.Log(obj.country_owner[attacked_country]);

                            obj.country_owner[attacked_country] = currentplayer;
                            obj.country_soliders[selected_country]--;
                        }
                        talker.say_instruction("attack success", question);


                    }
                    else
                    {

                        int x = (soldiers * numberofcountriesOfenemy + 32) / 33;
                        obj.country_soliders[selected_country] -= x;
                        if (obj.country_soliders[selected_country] <= 0)
                        {
                            obj.country_owner[selected_country] = obj.country_owner[attacked_country];
                            if (obj.country_soliders[attacked_country] > 1)
                                obj.country_soliders[attacked_country]--;
                        }
                        talker.say_instruction("Attack failed", question);
                        numberofcountriesOfenemy = obj.players[obj.country_owner[attacked_country]].countries.Count;

                    }
                    obj.update_material();
                    attack_phase++;
                    attack_more_canvas.SetActive(true);
                }
            if (attack_phase == 5)
            {
                if (!attack_more_canvas.activeSelf)
                {
                    if (attack_more_bool)
                    {
                        close_attack();
                    }
                }


            }

        }


    }


    public List<int> can_attack(int selected, int cur_player)
    {
        List<int> countries = new List<int>();
        for (int i = 1; i <= 33; i++)
            if (v[selected, i] && obj.country_owner[i] != cur_player)
                countries.Add(i);
        return countries;
    }

    public List<int> move_soldier(int selected, int cur_player)
    {
        Queue<int> q = new Queue<int>();
        List<int> countries = new List<int>();
        bool[] vis = new bool[50];
        q.Enqueue(selected);
        while (q.Count > 0)
        {
            int cur = q.Peek();
            q.Dequeue();
            if (vis[cur])
                continue;
            vis[cur] = true;
            countries.Add(cur);
            for (int i = 1; i <= 33; i++)
                if (v[cur, i] && obj.country_owner[i] == cur_player)
                    q.Enqueue(i);
        }
        return countries;
    }
    void available_to_attack(int selected, List<int> country_list)
    {

        for (int i = 1; i <= 33; i++)
        {
            GameObject ob = GameObject.FindGameObjectWithTag(i.ToString());
            if (country_list.Contains(i) == true)
            {
                // ob.GetComponent<SpriteRenderer>().material = new_material;
            }
            else
            {
                if (!ob.CompareTag(selected_country.ToString()))
                {
                    ob.GetComponent<SpriteRenderer>().material = shadow;
                    // ob.transform.GetChild(0).gameObject.SetActive(false);
                }



            }


        }
        GameObject.FindGameObjectWithTag(selected.ToString()).GetComponent<SpriteRenderer>().material = default_material;
    }
    void close_attack()
    {
        obj.update_material();
        attack_phase = 1;
        question_canvas.SetActive(false);
        counter.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using TMPro;
public class attack_phase : MonoBehaviour
{
    Camera cam;

    public static bool question, attack_more_bool = false;
    int currentplayer ;
    public GameObject counter;
    public GameObject question_canvas;
    public GameObject counter_text;
    public GameObject attack_more_canvas;

    int soldiers, numberofcountriesOfenemy;
    public Material new_material;
    public Material default_material;
    public Material shadow;
    public static int selected_country, attacked_country;
    public static int counter_attack, counter_soldier;
    public int curr_ph;

    int attack_phases = 1;
    Graph graph_obj;
    

   public GlobalClass obj;
    // Start is called before the first frame update
    void Start()
    {
        graph_obj = GetComponent<Graph>();
        cam = GetComponent<Camera>();
        obj = GetComponent<GlobalClass>();
        curr_ph = obj.cur_phase;
        numberofcountriesOfenemy = obj.players[currentplayer].countries.Count;
        currentplayer = obj.players_turns.Peek();

    }

    // Update is called once per frame
    void Update()
    {
        if (obj.cur_phase == 1)
        {
            main_attack_phase();
        }

    }
    void main_attack_phase()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("had mouse click");

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                if (attack_phases == 1)
                {
                    Debug.Log("inside attack phase 1");


                    selected_country = Convert.ToInt32(hit.collider.gameObject.tag);
                    if (obj.country_owner[selected_country] == currentplayer && obj.country_soliders[selected_country] > 1)
                    {
                        attack_phases++;

                        coloring_near_by(selected_country, graph_obj.can_attack(selected_country, currentplayer));
                        talker.say_instruction("select country to attack");
                    }


                }
                if (attack_phases == 2)
                {
                    attacked_country = Convert.ToInt32(hit.collider.gameObject.tag);

                    if (graph_obj.can_attack(selected_country, currentplayer).Contains(attacked_country) == true)
                    {
                        attack_phases++;

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
        if (attack_phases == 3)
        {
            if (counter.activeSelf == false)
            {

                soldiers = Convert.ToInt32(counter_text.GetComponent<TextMeshPro>().text);
                question_canvas.SetActive(true);
                Questiones objectt = GetComponent<Questiones>();
                attack_phases++;
                talker.say_instruction("Answer Question if you answer correctly the enemy will lose the number you attacked with if your answer was wrong you will lose your soldiers");


            }
        }
        if (attack_phases == 4)
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
                        obj.country_soliders[attacked_country]=1;

                    }
                    talker.say_instruction("attack success", question);


                }
                else
                {

                    int xfailed = (soldiers * numberofcountriesOfenemy + 32) / 33;
                    
                    obj.country_soliders[selected_country] -= xfailed;
                    if (obj.country_soliders[selected_country] <= 0)
                    {
                       
                            obj.country_soliders[selected_country]=1;
                    }
                    talker.say_instruction("Attack failed", question);
                    numberofcountriesOfenemy = obj.players[obj.country_owner[attacked_country]].countries.Count;

                }
                obj.update_material();
                attack_phases++;
                attack_more_canvas.SetActive(true);
            }
        if (attack_phases == 5)
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
    public void close_attack()
    {
        obj.update_material();
        attack_phases = 1;
        question_canvas.SetActive(false);
        counter.SetActive(false);
       obj.cur_phase++;
       obj.cur_phase %= 3;
    }
    void coloring_near_by(int selected, List<int> country_list)
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
    }
}

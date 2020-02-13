using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Fortify : MonoBehaviour
{


    Graph obj;
    GlobalClass gClass;
    playingfns playerfn_obj;
    public GameObject CounterPanel;
    GameObject scroller;
    public static int confirmed;
    Camera cam;
    public List<int> my_countries;
    public int selected_country;
    public int move_phase = 0;
    int pastCountry;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        obj = GetComponent<Graph>();
        gClass = GetComponent<GlobalClass>();
        playerfn_obj = GetComponent<playingfns>();
        scroller = GameObject.FindGameObjectWithTag("Scroller");
        confirmed = 0;
        pastCountry = 0;
    }


    void Update()
    {
        if (gClass.cur_phase == 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (move_phase == 0)
                    {
                        
                        selected_country = Convert.ToInt32(hit.collider.gameObject.tag);
                        if (gClass.players[gClass.players_turns.Peek()].countries.Contains(selected_country))
                        {

                            my_countries = obj.move_soldier(selected_country, gClass.players_turns.Peek());
                            coloringContries();
                            move_phase++;
                            pastCountry = selected_country;

                        }
                    }
                    if (move_phase == 1)
                    {

                        selected_country = Convert.ToInt32(hit.collider.gameObject.tag);
                        if (my_countries.Contains(selected_country) && selected_country != pastCountry)
                        {
                           
                            GameObject ob = GameObject.FindGameObjectWithTag("Scroller");
                            ob.SetActive(true);
                            talker.say_instruction("Choose number of soldiers you want to move");
                            int numberofsolider = Convert.ToInt32(GameObject.FindGameObjectWithTag("counter text").GetComponent<TextMeshPro>().text);
                          //  if (confirmed == 1 || confirmed == -1)
                         //   {
                               // ob.SetActive(false);
                                gClass.country_soliders[pastCountry] -= numberofsolider;
                                gClass.country_soliders[selected_country] += numberofsolider;
                                confirmed = 0;
                                closeFortify();
                        //    }

                        }
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                closeFortify();

            }
        }
    }
    void coloringContries()
    {
        for (int i = 1; i <= 33; i++)
        {
            GameObject ob = GameObject.FindGameObjectWithTag(i.ToString());
            if (my_countries.Contains(i) == true)
            {
                ob.GetComponent<SpriteRenderer>().material = obj.new_material;
            }
            else
            {
                ob.GetComponent<SpriteRenderer>().material = obj.shadow;
                // ob.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    void closeFortify()
    {
        move_phase = 0;
        gClass.update_material();
    }
    // Update is called once per frame

}

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
    Camera cam;
    public List<int> my_countries;
    public int selected_country;
    public int move_phase = 0;
     public int pastCountry;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        obj = GetComponent<Graph>();
        gClass = GetComponent<GlobalClass>();
        selected_country = 0;
        move_phase = 0;
        pastCountry = 0;
    }


    void Update()
    {
        if (gClass.cur_phase == 2)
        {
            my_countries = gClass.players[gClass.players_turns.Peek()].countries;
            Debug.Log("fortifaaaaaaaaaaaay");
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (move_phase == 0)
                    {
                        
                        coloringContries(my_countries);
                        selected_country = Convert.ToInt32(hit.collider.gameObject.tag);
                        if (my_countries.Contains(selected_country))
                        {

                            move_phase++;
                            pastCountry = selected_country;
                            return;
                        }
                    }
                    if (move_phase == 1)
                    {
                        List<int> tomove = obj.move_soldier(pastCountry, gClass.players_turns.Peek());
                        coloringContries(tomove);
                        if (tomove.Contains(Convert.ToInt32(hit.collider.gameObject.tag)))
                        {
                            selected_country = Convert.ToInt32(hit.collider.gameObject.tag);
                            if (tomove.Contains(selected_country))
                            {
                                talker.say_instruction("Choose number of soldiers you want to move");

                            }
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
    void coloringContries(List<int> country)
    {
        for (int i = 1; i <= 33; i++)
        {
            GameObject ob = GameObject.FindGameObjectWithTag(i.ToString());
            if (!country.Contains(i))
            {
                ob.GetComponent<SpriteRenderer>().material = gClass.shadow;
            }
        }
    }
    void closeFortify()
    {
        gClass.cur_phase = 0;
        gClass.players_turns.Enqueue(gClass.players_turns.Peek());
        gClass.players_turns.Dequeue();
        gClass.update_material();
        Debug.Log("End Fortify ");
    }
    public void move_soldiers()
    {
        int numberofsolider = Convert.ToInt32(GameObject.FindGameObjectWithTag("counter text").GetComponent<TextMeshPro>().text);
        Debug.Log("Move soliders " + numberofsolider);
        gClass.country_soliders[pastCountry] -= numberofsolider;
        gClass.country_soliders[selected_country] += numberofsolider;
        closeFortify();
    }
    // Update is called once per frame

}

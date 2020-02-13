using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class buttons_controller : MonoBehaviour
{
    // Start is called before the first frame update
    int counter=0;
    Camera cam;
    GlobalClass Gobject;
    Fortify fortify_obj;
    int max;



    public GameObject cnvs;
    void Start()
    {

        Gobject = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlobalClass>();
        fortify_obj = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Fortify>();
    }

    // Update is called once per frame
    void Update()
    {

    }
   public void increase()
    {

        getmax();

        
        counter = Convert.ToInt32(GameObject.FindGameObjectWithTag("counter text").GetComponent<TextMeshPro>().text);
        counter++;
        if (counter > max)
        {
            counter = 1;
        }

        GameObject.FindGameObjectWithTag("counter text").GetComponent<TextMeshPro>().text = counter.ToString();
    }
  public void decrease()
    {
        getmax();


            counter = Convert.ToInt32(GameObject.FindGameObjectWithTag("counter text").GetComponent<TextMeshPro>().text);
            counter--;
            if (counter == 0)
            {
                counter = max;
            }
            GameObject.FindGameObjectWithTag("counter text").GetComponent<TextMeshPro>().text = counter.ToString();
      
    }
    public void ok()
    {
        cnvs.SetActive(false);
    }
    public void cancel()
    {

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<attack_phase>().close_attack();
  
        cnvs.SetActive(false);
    }        
    void getmax()

    {
        if (Gobject.cur_phase == 1)
        {
            max = attack_phase.counter_attack;
        }
        else if (Gobject.cur_phase == 0)
        {
            max = Gobject.players[Gobject.players_turns.Peek()].soldiers_of_draft;
        }
        else
        {
            max = Gobject.country_soliders[fortify_obj.pastCountry] - 1;
        }
    }
}

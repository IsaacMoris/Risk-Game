using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Draftphase : MonoBehaviour
{
    // Lists for each country
    ArrayList NAmercian = new ArrayList() { 28, 29, 30, 31, 32, 33 }; //+3
    ArrayList SAmercian = new ArrayList() { 21, 22, 23, 24, 25, 26, 27 }; //+4
    ArrayList Asia = new ArrayList() { 3, 4, 5, 6, 7, 8, 9, 10, 13 }; //+7
    ArrayList Europe1 = new ArrayList() { 16, 17, 18, 19, 20 }; //+4
    ArrayList Europe2 = new ArrayList() { 1, 2, 11, 12, 14, 15 }; //+5
    public int selected_country=0;
    public int draft_phase = 0;
    // Start is called before the first frame update
    GlobalClass gClassObj;

    // Start is called before the first frame update
    void Start()
    {
        gClassObj = GetComponent<GlobalClass>();
    }

    public int increased_soldiers_number() //: countryNum = list.size();
    {
        List<int> playerCountries = gClassObj.players[gClassObj.players_turns.Peek()].countries;
        bool contain = true;
        int increasedNum = playerCountries.Capacity / 3;
        foreach (int country in NAmercian) if (!playerCountries.Contains(country)) { contain = false; break; }
        if (contain) increasedNum += 3; contain = true;

        foreach (int country in SAmercian) if (!playerCountries.Contains(country)) { contain = false; break; }
        if (contain) increasedNum += 4; contain = true;

        foreach (int country in Asia) if (!playerCountries.Contains(country)) { contain = false; break; }
        if (contain) increasedNum += 7; contain = true;

        foreach (int country in Europe1) if (!playerCountries.Contains(country)) { contain = false; break; }
        if (contain) increasedNum += 4; contain = true;

        foreach (int country in Europe2) if (!playerCountries.Contains(country)) { contain = false; break; }
        if (contain) increasedNum += 5; contain = true;


        if (increasedNum < 3) return 3;
        else return increasedNum;
    }



    void Update()
    {
        if (gClassObj.cur_phase == 0)
        {
            coloringContries();
            if(draft_phase==0)
            {
                gClassObj.players[gClassObj.players_turns.Peek()].soldiers_of_draft = increased_soldiers_number();
                Debug.Log("draft soliders = " + gClassObj.players[gClassObj.players_turns.Peek()].soldiers_of_draft);
                draft_phase++;
            }
            if(draft_phase==1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (gClassObj.players[gClassObj.players_turns.Peek()].countries.Contains(Convert.ToInt32(hit.collider.gameObject.tag)))
                            selected_country = Convert.ToInt32(hit.collider.gameObject.tag);
                    }
                }
                if (gClassObj.players[gClassObj.players_turns.Peek()].soldiers_of_draft == 0)
                {
                    Debug.Log("End Draft Phase");
                    gClassObj.cur_phase = (gClassObj.cur_phase + 1) % 3;
                    gClassObj.update_material();
                    selected_country = 0;
                    draft_phase = 0;
                }
            }
        }
    }

    public void add_soliders()
    {
        int counter = Convert.ToInt32(GameObject.FindGameObjectWithTag("counter text").GetComponent<TextMeshPro>().text);
        gClassObj.players[gClassObj.players_turns.Peek()].soldiers_of_draft -= counter;
        gClassObj.country_soliders[selected_country] += counter;
    }
    void coloringContries()
    {
        for (int i = 1; i <= 33; i++)
        {
            GameObject ob = GameObject.FindGameObjectWithTag(i.ToString());
            if (gClassObj.players[gClassObj.players_turns.Peek()].countries.Contains(i) == false)
                ob.GetComponent<SpriteRenderer>().material = gClassObj.shadow;

        }

    }
}

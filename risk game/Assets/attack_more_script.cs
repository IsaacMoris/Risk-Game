using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class attack_more_script : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public GameObject want_to_attack_more_canvas;
    public Button clicked_button;
    GlobalClass Gobject;
    Fortify fortify_obj;
    Draftphase draft_phase;
    attack_phase attk;
    void Start()
    {

        Gobject = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlobalClass>();
        fortify_obj = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Fortify>();
        draft_phase = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Draftphase>();
        attk= GameObject.FindGameObjectWithTag("MainCamera").GetComponent<attack_phase>();

    }
    public void want_to_attack_more()
    {            
        if(Gobject.cur_phase==0)
        {
            draft_phase.add_soliders();
        }
        else if (Gobject.cur_phase == 1)
        {
            if (clicked_button.CompareTag("yes attack more"))
            {
                attack_phase.attack_more_bool = true;
                Debug.Log("attack more");

            }
            else
            {
                attack_phase.attack_more_bool = false;
                Debug.Log("do not attack");

            }
        }
        else if (Gobject.cur_phase == 2)
        {
            fortify_obj.move_soldiers();
            fortify_obj.move_phase = 0;
        }

    }
    public void end_attack()
    {


        Gobject.cur_phase++;
        Gobject.cur_phase %= 3;
        attk.close_attack();
    }
}

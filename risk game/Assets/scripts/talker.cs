using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class talker : MonoBehaviour
{
    // Start is called before the first frame update
   static Animator anim;
    public  GameObject text_balloon;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //anim.SetBool("talk", false);

    }
    public static void say_instruction(string instruction)
    {
        anim.SetBool("talk", true);
        GameObject.FindGameObjectWithTag("instruction").GetComponent<Text>().text = instruction;
        GameObject.FindGameObjectWithTag("instruction").GetComponent<Text>().color = Color.black;


    }
    public static void say_instruction(string instruction,bool question_answer)
    {
        if (question_answer == true)
        {
            anim.SetBool("correct", true);
            anim.SetInteger("attack_success", 1);
            GameObject.FindGameObjectWithTag("instruction").GetComponent<Text>().color = Color.green;
        }
        else
        {
            anim.SetBool("wrong", true);
            GameObject.FindGameObjectWithTag("instruction").GetComponent<Text>().color = Color.red;


        }

        GameObject.FindGameObjectWithTag("instruction").GetComponent<Text>().text = instruction;


    }
    public void stop_talking()
    {
        anim.SetBool("talk", false);
        anim.SetBool("correct", false);
        anim.SetBool("wrong", false);


    }
   
}

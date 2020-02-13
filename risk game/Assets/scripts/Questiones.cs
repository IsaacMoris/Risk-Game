using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System;


public class Questiones : MonoBehaviour
{
 
    public struct question
    {
        public string question_text;
        public List<string> options;
        public string correct_ans;
    }
    public Button ans1, ans2, ans3, ans4;
    public Button clicked_button;
    public TextMeshProUGUI paneltxt;
    public GameObject Question_canvas;
    Queue Questions_queue = new Queue();
    StreamReader reader;
    string text = " ";
    public TextMeshProUGUI tell_him;
    Queue<question> Question_list=new Queue<question>();

    void Start()
    {
        FileInfo theSourceFile = new FileInfo(@"Assets\Questions.txt");
        reader = theSourceFile.OpenText();
        while (text != null)
        {
            text = reader.ReadLine();
            question newq = new question();
            newq.options = new List<string>();
            newq.question_text = text;
            for(int i=0; i < 4; i++)
            {
                text = reader.ReadLine();
                newq.options.Add(text);
            }
            text = reader.ReadLine();
            newq.correct_ans = text;

            Question_list.Enqueue(newq);
            Questions_queue.Enqueue(text);
        }
        update_question();
      
    }

   public void next_qestion()
    {
        string correct = Question_list.Peek().correct_ans;

        if (correct == clicked_button.GetComponentInChildren<TextMeshProUGUI>().text)
        {
            attack_phase.question = true;
            Debug.Log("correct");
        }
        else
        {
            attack_phase.question = false;
            Debug.Log("wrong");

        }
        update_question();
        Question_canvas.SetActive(false);
   

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void update_question()
    {
        Question_list.Enqueue(Question_list.Peek());
        Question_list.Dequeue();
        paneltxt.text = Question_list.Peek().question_text.ToString();

        ans1.GetComponentInChildren<TextMeshProUGUI>().text = Question_list.Peek().options[0].ToString();

        ans2.GetComponentInChildren<TextMeshProUGUI>().text = Question_list.Peek().options[1].ToString();

        ans3.GetComponentInChildren<TextMeshProUGUI>().text = Question_list.Peek().options[2].ToString();


        ans4.GetComponentInChildren<TextMeshProUGUI>().text = Question_list.Peek().options[3].ToString();

       
        

       
    }
    //void fill_question_list()
    //{
    //    while (Questions_queue.Count > 12)
    //    {
    //        question one_question = new question();

    //        one_question.question_text = Questions_queue.Peek().ToString();
    //        Questions_queue.Dequeue();
    //         for(int i = 0; i < 4; i++)
    //         {
    //             Debug.Log(Questions_queue.Peek().ToString());
    //            one_question.options.Add(Questions_queue.Peek().ToString());
    //             Questions_queue.Dequeue();
             
    //        }

    //        one_question.correct_ans = Questions_queue.Peek().ToString();
    //        Questions_queue.Dequeue();
    //        Question_list.Enqueue(one_question);

    //    }
    //}
}

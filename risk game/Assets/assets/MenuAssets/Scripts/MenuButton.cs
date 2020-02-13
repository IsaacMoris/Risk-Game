using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;

  
    // Update is called once per frame
    public void Update()
    {
		if(menuButtonController.index == thisIndex)
		{
			animator.SetBool ("selected", true);
			if(Input.GetAxis("Submit") == 1)
            {
                
                if (Input.GetKeyDown("space")|| Input.GetKeyDown(KeyCode.Return)) { 
				animator.SetBool ("pressed", true);
                if (menuButtonController.index == 0)
                {
                    SceneManager.LoadScene("Game");
                }
                if (menuButtonController.index == 1)
                {
                    StartCoroutine(ExampleCoroutine());

                }
                if (menuButtonController.index == 2)
                {
                    Application.Quit();
                }
                }
            }
            else if (animator.GetBool ("pressed")){
				animator.SetBool ("pressed", false);
				animatorFunctions.disableOnce = true;
			}
		}else{
			animator.SetBool ("selected", false);
		}
    }
    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;
    
    }
    IEnumerator ExampleCoroutine()
    {

        Mute();
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);
       

    }

}

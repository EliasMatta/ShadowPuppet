using UnityEngine;

public class Exit : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if(collision.CompareTag("Player"))
        {


            if (GameManager.instance.HasCollectedMemo())
            {


                FadeManager.instance.FadeToNextLevel();



            }
            else
            {
                Debug.Log("You need to collect the Memo first!");

            }


        }



    }




}

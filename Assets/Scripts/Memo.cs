using UnityEngine;

public class Memo : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {


            GameManager.instance.CollectMemo();


            Destroy(gameObject);

        }




    }







}

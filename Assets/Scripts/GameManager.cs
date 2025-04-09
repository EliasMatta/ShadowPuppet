using UnityEngine;

public class GameManager : MonoBehaviour
{
   
    public static GameManager instance;

    private bool memoCollected = false;

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }



    }

    public void CollectMemo()
    {

        memoCollected = true;
        Debug.Log("Memo Collected");


    }


    public bool HasCollectedMemo()
    {



        return memoCollected;





    }





}

using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    int health = 100;

    public void TakeDemage(int demage)
    {

        health -= demage;
       
        if (health <= 0)
        {
            Time.timeScale = 0;
            Debug.Log("Morreu");
        }
        
    }
}

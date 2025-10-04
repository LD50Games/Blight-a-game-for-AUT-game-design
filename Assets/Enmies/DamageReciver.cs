using UnityEngine;

public class DamageReciver : MonoBehaviour
{
    public GameObject enemyContoller;
    public float healthMultipleir;

    public void TakeDamage(int damage)
    {
        enemyContoller.SendMessage("TakeDamage",damage * healthMultipleir);
        
    }
}

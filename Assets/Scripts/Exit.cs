using UnityEngine;

public class Exit : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Level Completed!");
            /*TODO FAIRE UNE POPUP DE NIVEAU TERMINE
            HIDE LE GAMEROOT ET METTRE EN PAUSE LE JEU
            PROPOSER DE GAGNER DES CONSERVES EN REGARDANT UNE PUB
            En cliquant sur niveau suivant, charger la nouvelle map en detruistant tout les anciens zombies et marchand*/
        }
    }
}


using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public bool isActive;

    private int nbZombie = 5;

    public GameObject shopGo;

    public GameObject exitArenaGo;
    public GameObject exitArenaTextGo;
    public Vector3 posEnter = new Vector3(0, 0, 0);

    public Vector3 posEnterArena = new Vector3(5, 0, 0);

    public List<GameObject> arenaObjects = new List<GameObject>();

    void Awake()
    {
        isActive = false;
    }

    public int getZombieCount()
    {
        nbZombie += 1;
        nbZombie = nbZombie * 2;
        return nbZombie;
    }

    public bool getIsBoss()
    {
        return true;
    }

    public void endArena()
    {
        // Faire spawn un coffre à la fin de l'arène
        shopGo.SetActive(true);
        // Faire spawn la fleche vers le coffre

    }

    public void canExit()
    {
        // Afficher la sortie de l'arène
        exitArenaGo.SetActive(true);
        exitArenaTextGo.SetActive(true);
        

        // Afficher la fleche qui dirige vers la sortie de l'arène
    }
}

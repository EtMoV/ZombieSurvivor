
public class Round
{
    public int numberRound;
    public int nbZombieSpawn;
    public int maxZombies;
    public int pvZombie;
    public bool isBoss;
    public bool bossHasSpawn;
    public bool isActive;
    public int currentNbZombieSpawn;

    public Round(int nR, int nZ, int mZ, int pZ, bool iB, bool iA)
    {
        numberRound = nR;
        nbZombieSpawn = nZ;
        maxZombies = mZ;
        pvZombie = pZ;
        isBoss = iB;
        bossHasSpawn = false;
        isActive = iA;
        currentNbZombieSpawn = 0;
    }
}

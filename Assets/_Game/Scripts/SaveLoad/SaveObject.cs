using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SaveObject 
{
    public SaveObject(){}
    public SaveObject(int coins, int highScore, ShipStats shipStats)
    {
        Coins = coins;
        HighScore = highScore;
        ShipStats = shipStats;
    }

    public int Coins { get; set; }
    public int HighScore { get; set; }
    public ShipStats ShipStats { get; set; }


}

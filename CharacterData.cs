using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData 
{
    [Header("Character Init")]
    public string characterType;

    [Header("Character Attributes")]
    public int id;
    public bool isMarried;

    public PlayerAttributes playerAttributes = new PlayerAttributes();
    public MajorNPCAttributes majorNPCAttributes = new MajorNPCAttributes();
}

[System.Serializable]
public class PlayerAttributes
{
    public string charName;
    public string race;
    public string gender;
    public int birthDay;
    public string birthMonth;
    public string charDescription;
    [Header("Player Attributes")]
    public int level;
    public int health;
    public int stamina;
    public int endurance;
    public int enduranceExp;
    public int gathering;
    public int gatheringExp;
    public int cooking;
    public int cookingExp;
    public int social;
    public int socialExp;
    public List<int> equipmentIDList = new List<int>();
}

[System.Serializable]
public class MajorNPCAttributes
{
    [Header("MajorNPC Attributes")]
    public string relationshipWithPlayer;
}

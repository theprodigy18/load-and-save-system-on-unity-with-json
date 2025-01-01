using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "Character/Player")]
public class Player : Character
{
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
    
    public PlayerEquipment equipment;
}
// this is sub-class from character

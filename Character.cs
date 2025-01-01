using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/BaseCharacter")]
public class Character : ScriptableObject
{
    private int id;
    [Header("Character Attributes")]
    public string charName;
    public Race race;
    public Gender gender;
    public GameDate birthDate;
    public string charDescription;
    public bool isMarried;

    public int GetId()
    {
        return id;
    }
    public void SetId(int id)
    {
        this.id = id;
    }
}
// This is base class of character

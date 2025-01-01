using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMajorNPC", menuName = "Character/MajorNPC")]
public class MajorNPC : Character
{
    [Header("MajorNPC Attributes")]
    public List<Item> favoriteGiftList;
    public List<Item> dislikeGiftList;
    public Relationship relationshipWithPlayer;
}

// this is sub-class from character

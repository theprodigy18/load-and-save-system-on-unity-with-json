using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class CharacterDatabase : MonoBehaviour
{
    public static CharacterDatabase Instance { get; set; }
    
    [SerializeField] private List<Character> baseCharacterList;
    public List<Character> gameCharacterList;
    public bool isCharacterDatabaseReady = false;
    public bool save = false;
    public bool load = false;
    public string jsonPath = Application.dataPath + Path.AltDirectorySeparatorChar;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        isCharacterDatabaseReady = false;
        gameCharacterList.Clear();
        AssignCharactersIDAndClone();
    }

    private void Update()
    {
        if (save)
        {
            save = false;
            SaveGameData();
        }
        if (load)
        {
            load = false;
           AllGameData gameData = LoadGameData();
           AssignCharacterData(gameData.characterList.list);
        }
    }

    private void AssignCharactersIDAndClone()
    {
        for (int i = 0; i < baseCharacterList.Count; i++)
        {
            baseCharacterList[i].SetId(i);
            Character newCharacter = Instantiate(baseCharacterList[i]);
            newCharacter.SetId(i);
            gameCharacterList.Add(newCharacter);
            // Debug.Log(baseCharacterList[i].GetId() + " " + gameCharacterList[i].GetId());
        }

        isCharacterDatabaseReady = true;
    }

    public void SaveGameData()
    {
        List<CharacterData> characterDatas = new List<CharacterData>();

        for (int i = 0; i < gameCharacterList.Count; i++)
        {
            CharacterData characterData = CopyCharacterData(gameCharacterList[i]);
            characterDatas.Add(characterData);
        }

        CharacterList characterList = new CharacterList(characterDatas);
        AllGameData allGameData = new AllGameData(characterList);

        string json = JsonUtility.ToJson(allGameData);
        string encryptedJson = EncryptAndDecrypt(json);

        using (StreamWriter writer = new StreamWriter(jsonPath + "SaveGame.json"))
        {
            writer.Write(encryptedJson);
            Debug.Log("Game Saved at " + jsonPath +"SaveGame.json");
        };
    }
    public AllGameData LoadGameData()
    {
        string json = File.ReadAllText(jsonPath + "SaveGame.json");
        string decryptedJson = EncryptAndDecrypt(json);
        AllGameData allGameData = JsonUtility.FromJson<AllGameData>(decryptedJson);

        return allGameData;        
    }

    public void AssignCharacterData(List<CharacterData> characterDatas)
    {
        foreach (CharacterData characterData in characterDatas)
        {
            Character gameCharacter = gameCharacterList.FirstOrDefault(character => character.GetId() == characterData.id);
            gameCharacter.isMarried = characterData.isMarried;
            Debug.Log(gameCharacter.charName);

            if (gameCharacter is Player player)
            {
                player.charName = characterData.playerAttributes.charName;
                player.race = Enum.Parse<Race>(characterData.playerAttributes.race);
                player.gender = Enum.Parse<Gender>(characterData.playerAttributes.gender);
                player.birthDate.day = characterData.playerAttributes.birthDay;
                player.birthDate.month = Enum.Parse<Month>(characterData.playerAttributes.birthMonth);
                player.charDescription = characterData.playerAttributes.charDescription;

                player.level = characterData.playerAttributes.level;
                player.health = characterData.playerAttributes.health;
                player.stamina = characterData.playerAttributes.stamina;
                player.endurance = characterData.playerAttributes.endurance;
                player.enduranceExp = characterData.playerAttributes.enduranceExp;
                player.gathering = characterData.playerAttributes.gathering;
                player.gatheringExp = characterData.playerAttributes.gatheringExp;
                player.cooking = characterData.playerAttributes.cooking;
                player.cookingExp = characterData.playerAttributes.cookingExp;
                player.social = characterData.playerAttributes.social;
                player.socialExp = characterData.playerAttributes.socialExp;

                player.equipment.head = ItemDatabase.Instance.itemList.FirstOrDefault(item => item.GetId() == characterData.playerAttributes.equipmentIDList[0]) as EquipmentItem;
                player.equipment.torso = ItemDatabase.Instance.itemList.FirstOrDefault(item => item.GetId() == characterData.playerAttributes.equipmentIDList[1]) as EquipmentItem;
                player.equipment.lower = ItemDatabase.Instance.itemList.FirstOrDefault(item => item.GetId() == characterData.playerAttributes.equipmentIDList[2]) as EquipmentItem;
                player.equipment.gadget = ItemDatabase.Instance.itemList.FirstOrDefault(item => item.GetId() == characterData.playerAttributes.equipmentIDList[3]) as EquipmentItem;
                player.equipment.boots = ItemDatabase.Instance.itemList.FirstOrDefault(item => item.GetId() == characterData.playerAttributes.equipmentIDList[4]) as EquipmentItem;
            }
            
            if (gameCharacter is MajorNPC majorNPC)
            {
                majorNPC.relationshipWithPlayer = Enum.Parse<Relationship>(characterData.majorNPCAttributes.relationshipWithPlayer);
            }
        }
    }
    private CharacterData CopyCharacterData(Character character)
    {
        CharacterData characterData = new CharacterData();

        characterData.characterType = "Character";
        characterData.id = character.GetId();
        characterData.isMarried = character.isMarried;

        if (character is Player player)
        {
            characterData.characterType = "Player";
            characterData.playerAttributes.charName = player.charName;
            characterData.playerAttributes.race = player.race.ToString();
            characterData.playerAttributes.gender = player.gender.ToString();
            characterData.playerAttributes.birthDay = player.birthDate.day;
            characterData.playerAttributes.birthMonth = player.birthDate.month.ToString();
            characterData.playerAttributes.charDescription = player.charDescription;

            characterData.playerAttributes.level = player.level;
            characterData.playerAttributes.health = player.health;
            characterData.playerAttributes.stamina = player.stamina;
            characterData.playerAttributes.endurance = player.endurance;
            characterData.playerAttributes.enduranceExp = player.enduranceExp;
            characterData.playerAttributes.gathering = player.gathering;
            characterData.playerAttributes.gatheringExp = player.gatheringExp;
            characterData.playerAttributes.cooking = player.cooking;
            characterData.playerAttributes.cookingExp = player.cookingExp;
            characterData.playerAttributes.social = player.social;
            characterData.playerAttributes.socialExp = player.socialExp;

            characterData.playerAttributes.equipmentIDList.Add(player.equipment.head.GetId());
            characterData.playerAttributes.equipmentIDList.Add(player.equipment.torso.GetId());
            characterData.playerAttributes.equipmentIDList.Add(player.equipment.lower.GetId());
            characterData.playerAttributes.equipmentIDList.Add(player.equipment.gadget.GetId());
            characterData.playerAttributes.equipmentIDList.Add(player.equipment.boots.GetId());
        }

        if (character is MajorNPC majorNPC)
        {
            characterData.characterType = "MajorNPC";
            characterData.majorNPCAttributes.relationshipWithPlayer = majorNPC.relationshipWithPlayer.ToString();
        }


        return characterData;
    }

    private string EncryptAndDecrypt(string data)
    {
        string key = "pukimak190";
        string result = "";

        for (int i = 0; i < data.Length; i++)
        {
            result += (char)(data[i] ^ key[i % key.Length]);
        }

        return result;

        // XOR method to see the difference beetween 2 binary
        // if it have different then return 1 and if there is same return 0
        // and then the binary code will represent a new string character 
    }
}


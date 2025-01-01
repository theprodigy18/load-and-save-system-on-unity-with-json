using System.Collections.Generic;

[System.Serializable]
public class AllGameData 
{
    public CharacterList characterList;

    public AllGameData(CharacterList characterList)
    {
        this.characterList = characterList;
    }
}

[System.Serializable]
public class CharacterList 
{
    public List<CharacterData> list;

    public CharacterList(List<CharacterData> list)
    {
        this.list = list;
    }
}

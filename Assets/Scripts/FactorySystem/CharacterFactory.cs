using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FactorySystem
{
    public class CharacterFactory
    {
        //public T CreateCharacter<T>(ICharacter_Model character_Model) where T : ICharacter, new()
        //{
        //    T t = new T();
        //    t.SetICharacterModel(character_Model);
        //    GameObject go = AssetSystem.AssetManager.Instance.ResourcesAssetLoad.LoadCharacters(character_Model.prefabName);
        //    go.name = character_Model.name;
        //    t.SetGameObject(go);
        //    return t;
        //}

        //public Player CreatePlayer(ICharacter_Model character_Model, Player_Model player_Model)
        //{
        //    Player player = new Player();
        //    player.SetICharacterModel(character_Model);
        //    player.SetPlayerModel(player_Model);
        //    GameObject go = AssetSystem.AssetManager.Instance.ResourcesAssetLoad.LoadCharacters(character_Model.prefabName);
        //    go.name = character_Model.prefabName;
        //    player.SetGameObject(go);
        //    return player;
        //}
    }
}

using Behaviours.UI;
using Unity.Cinemachine;
using UnityEngine;

namespace Behaviours.Levels
{
    public static class LevelLoader{
        public const string LevelPath = "Levels/Level ";

        public static Level GetLevel(int index,out GameObject levelObj){
            GameObject prefab = Resources.Load<GameObject>($"{LevelPath}{index}");
            levelObj = Object.Instantiate(prefab);
            levelObj.name = prefab.name;
            return levelObj.GetComponent<Level>();
        }
    }
}
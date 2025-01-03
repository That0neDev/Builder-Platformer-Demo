using System.Collections;
using Behaviours.Levels;
using Behaviours.UI;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class GameGlobal : MonoBehaviour{
    public GameObject Player;
    public CinemachineBrain CinemachineBrain;
    public GameUI GameUI;
    public Level ActiveLevel;
    [SerializeField] CanvasGroup fadeCanvas;
    [SerializeField] Transform levelParent;
    [SerializeField] Transform playerBuildingParent;
    [SerializeField] int startIndex;

    public static GameGlobal GetGlobal(){
        return FindFirstObjectByType<GameGlobal>();
    }
    public IEnumerator LoadLevel(int diff){
        if(ActiveLevel != null){
            string name = ActiveLevel.gameObject.name.Remove(0,6);
            startIndex = int.Parse(name);
        }
        yield return FadeOut();
        if(ActiveLevel != null)
            Destroy(ActiveLevel.gameObject);
        Player.SetActive(false);
        GameUI.ResetUI();
        foreach (Transform building in playerBuildingParent)
            Destroy(building.gameObject);

        ActiveLevel = LevelLoader.GetLevel(startIndex + diff,out GameObject obj);
        obj.transform.SetParent(levelParent);
        ActiveLevel.LoadLevel(this);
        yield return FadeIn();
    }

    public void LoadSameLevel(){
        StartCoroutine(LoadLevel(0));
    }

    public void LoadNextLevel(){
        StartCoroutine(LoadLevel(1));
    }

    private IEnumerator FadeIn(){
        const float Delta = 1f;
        yield return fadeCanvas.DOFade(0,Delta).WaitForCompletion();
    }

    private IEnumerator FadeOut(){
        const float Delta = 1f;
        yield return fadeCanvas.DOFade(1,Delta).WaitForCompletion();
    }

    private void Awake(){
        Player.SetActive(false);
        LoadSameLevel();
    }
}
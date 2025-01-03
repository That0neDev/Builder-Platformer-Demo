using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Behaviours.Levels;

namespace Behaviours.UI.Building{

    public class BuildPanel : UIElement
    {
        public Level activeLevel;
        public GameObject selectablePrefab;
        public Transform selectableContainer;
        public BuildTool buildTool;
        public GameUI gameUI;
        public int dataIndex;

        public void DeductItem(){
            var data = activeLevel.Objects[dataIndex];
            data.Amount -= 1;
            selectableContainer.Find(data.playerObj.name)
                .GetChild(2)
                .GetComponent<TextMeshProUGUI>().text = 
                data.Amount.ToString();
        }

        public void ReDraw(){
            Close();
            gameUI.OpenUI(this);
        }


        private void Clear(){
            foreach (Transform item in selectableContainer)
                Destroy(item.gameObject);

            print(selectableContainer.childCount);
        }

        public override UIElement Close(){
            Clear();
            return base.Close();
        }

        public override UIElement Open()
        {
            int i = 0;
            activeLevel = GameGlobal.GetGlobal().ActiveLevel;
            var objList = activeLevel.Objects;

            if(selectableContainer.childCount > 0)
                Clear();

            foreach (var item in objList)
            {
                print(selectableContainer.childCount);
                Transform newObj = Instantiate(selectablePrefab,selectableContainer).transform;
                BuildElement buildElement = newObj.GetComponent<BuildElement>();
                buildElement.panel = this;
                buildElement.objData = item.playerObj;
                newObj.name = item.playerObj.name;
                newObj.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.playerObj.name;
                newObj.GetChild(1).GetComponent<Image>().sprite = 
                    item.playerObj.Prefab.GetComponent<SpriteRenderer>().sprite;
                newObj.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.Amount.ToString();
                if(i == 0)
                    EventSystem.current.SetSelectedGameObject(newObj.gameObject);
                
                i++;
            }
            return base.Open();
        }

        public void OnSelectedElement(int index){
            EventSystem.current.SetSelectedGameObject(buildTool.gameObject);
            dataIndex = index;
            gameUI.OpenUI(buildTool);
        }
    }
}
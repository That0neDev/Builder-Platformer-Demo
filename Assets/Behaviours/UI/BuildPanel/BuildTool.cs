using System;
using Behaviours.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviours.UI.Building{

    public class BuildTool : UIElement , ISelectable
    {
        private bool building;
        public bool Building {get {return building;} 
            set{
                if(value == true){
                    var data =  buildPanel.activeLevel.Objects[buildPanel.dataIndex];
                    if(data == null) 
                        return;

                    displayImage.sprite = data.playerObj.Prefab.GetComponent<SpriteRenderer>().sprite;
                }
                building = value;
            }
        }
        private bool canBuild;

        public Vector2 targetScreen;
        public Vector2 targetWorldRaw;
        public Vector3 targetWorld;
        public Vector2 snapUnit;
        private Vector2 colliderSize;

        private RectTransform rectTransform;
        [SerializeField] Transform playerBlocks;

        public BuildPanel buildPanel;
        [SerializeField] Image displayImage;
        [SerializeField] Image borderImage;

        [SerializeField] GameUI gameUI;
        [SerializeField] float navigateSpeed;
        [SerializeField] float PPU;

        private Camera gameCam;

        public override void Awake(){
            gameCam = Camera.main;
            rectTransform = GetComponent<RectTransform>();
            base.Awake();
        }

        private void Update(){
            bool CanBuild(){
                var data =  buildPanel.activeLevel.Objects[buildPanel.dataIndex];
                if(data.Amount <= 0) 
                    return false;

                return data.playerObj.CalculateCollision(colliderSize,targetWorld);
            }
            if(!Building)
                return;
            
            MoveToTargetScreen();
            canBuild = CanBuild();
            targetWorldRaw += gameUI.inputDirection * Time.deltaTime * navigateSpeed;
            if(canBuild){
                borderImage.color = Color.white;
                SetCanvasVisibility(1);
            }else{
                borderImage.color = Color.red;
                SetCanvasVisibility(0.8f);
            }
        }

        private void MoveToTargetScreen(){
            targetScreen = new(targetWorldRaw.x,targetWorldRaw.y);
            targetScreen = Snapping.Snap(targetScreen,snapUnit);
            rectTransform.anchoredPosition = targetScreen;
            Vector3 p = new(rectTransform.position.x,rectTransform.position.y,gameCam.nearClipPlane);
            targetWorld = (Vector2)gameCam.transform.position + (targetScreen / PPU);
        }   

        public void Select()
        {
            if(!canBuild)
                return;

            buildPanel.DeductItem();
            var data = buildPanel.activeLevel.Objects[buildPanel.dataIndex];

            GameObject newObject = Instantiate(data.playerObj.Prefab);
            newObject.transform.SetParent(playerBlocks);
            newObject.transform.position = targetWorld;
            if(data.playerObj.isStatic)
                newObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            else newObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            newObject.name = data.playerObj.name;
        }

        public override UIElement Open()
        {
            Building = true;
            var data = buildPanel.activeLevel.Objects[buildPanel.dataIndex];
            var obj = data.playerObj.Prefab;
            displayImage.sprite = obj.GetComponent<SpriteRenderer>().sprite;
            colliderSize = obj.GetComponent<BoxCollider2D>().size;
            return base.Open();
        }

        public override UIElement Close()
        {
            Building = false;
            return base.Close();
        }
    }
}
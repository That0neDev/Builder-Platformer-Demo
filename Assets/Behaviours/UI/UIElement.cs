using System;
using System.Collections.Generic;
using Behaviours.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviours.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIElement : MonoBehaviour
    {
        [SerializeField] List<UIElement> dependencyElements;
        private CanvasGroup canvas;
        private Image image;
        public bool isOpen;
        public bool grabAttention;
        
        public UIElement returnElement;

        public virtual void Awake(){
            canvas = GetComponent<CanvasGroup>();
            TryGetComponent(out image);
        }
        public virtual UIElement Open(){
            canvas.alpha = 1;
            canvas.blocksRaycasts = true;
            canvas.interactable = true;
            isOpen = true;
            return this;
        }
        public virtual UIElement Close(){
            if(isOpen == false)
                return null;
            
            canvas.alpha = 0;
            canvas.blocksRaycasts = false;
            canvas.interactable = false;
            isOpen = false;
            return this;
        }        
        public UIElement Restart(){
            return Open();
        }

        public void GetDependencies(ref HashSet<UIElement> visitedElements){
            HashSet<UIElement> retVal = new();

            if(dependencyElements == null)
                return;

            foreach (var element in dependencyElements)
            {
                if(visitedElements.Contains(element))
                    continue;
                visitedElements.Add(element);
                element.GetDependencies(ref visitedElements);
            }
        }
        public void SetCanvasVisibility(bool value){
            if(value) 
                canvas.alpha = 1;
            else canvas.alpha = 0;
        }
        public void SetCanvasVisibility(float value){
            canvas.alpha = Mathf.Clamp(value,0,1);
        }
        public void SetColor(Color value){
            if(image == null)
                return;
            
            image.color = value;
        }
        public void SetColor(Vector3 value){
            if(image == null)
                return;
            
            image.color = new Color(value.x,value.y,value.z);
        }
        public void SetColor(Vector4 value){
            image.color = new Color(value.x,value.y,value.z,value.w);
        }
        public void SetAlpha(float value){
            if(image == null)
                return;
            
            var c = new Color(image.color.r,image.color.g,image.color.b);
            image.color = new(c.r,c.g,c.b,value);
        }
        public void SetAlpha(bool value){
            if(image == null)
                return;
            int a = value ? 1 : 0;
            var c = new Color(image.color.r,image.color.g,image.color.b);
            image.color = new(c.r,c.g,c.b,a);          
        }
    }
}
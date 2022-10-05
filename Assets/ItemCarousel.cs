using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCarousel : MonoBehaviour
{
    
    public ItemCarrouselHolder uiItemPrefab;
    public float movingSpeed = 5f;
    public float rotationPercent = 1f;
    public float rotationScreenPercent = 1f;
    public float offsetScreenPercent = 1f;
    public float positionOffset = 1f;
    private float startTime;
    private LinkedList<ItemCarrouselHolder> uiItemHolders;

    void Start()
    {
        startTime = Time.time;
        RectTransform rectT = gameObject.GetComponent<RectTransform>();
        uiItemHolders = new LinkedList<ItemCarrouselHolder>();
        
        
        
        for(int i = 0; i < 10; i++)
        {
            ItemCarrouselHolder uiItem = Instantiate(uiItemPrefab, transform);
            uiItem.Set(uiItemHolders.Count*positionOffset);
            UIConstraint constraint = uiItem.GetComponent<UIConstraint>();
            constraint.target = rectT;
            RectTransform rectTransform = uiItem.GetComponent<RectTransform>();
            uiItemHolders.AddLast(uiItem);
        }
    }
    
    void Update()
    {
        
        float size = rotationScreenPercent * Screen.height;
        float yOffset = offsetScreenPercent * Screen.height;
        foreach(var uiItemHolder in uiItemHolders)
        {
            uiItemHolder.UpdatePosition(movingSpeed*Time.deltaTime,size,yOffset,positionOffset);
        }
    }
}

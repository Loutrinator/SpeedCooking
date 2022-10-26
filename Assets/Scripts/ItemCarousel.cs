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
    public float respawnThreshold = 0.125f;
    public float itemSlotAmount = 7;
    private float startTime;
    private float direction = 1;
    private LinkedList<ItemCarrouselHolder> uiItemHolders;

    void Start()
    {
        startTime = Time.time;
        RectTransform rectT = gameObject.GetComponent<RectTransform>();
        uiItemHolders = new LinkedList<ItemCarrouselHolder>();
        
        for(int i = 0; i < itemSlotAmount; i++)
        {
            ItemCarrouselHolder uiItem = Instantiate(uiItemPrefab, transform);
            float position = (i / (itemSlotAmount-1) * 2 - 1)*respawnThreshold;
            uiItem.Set(position);
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
        direction = (int)((startTime - Time.time) / 2f)%2*2+1;
        foreach(var uiItemHolder in uiItemHolders)
        {
            uiItemHolder.UpdatePosition(direction*movingSpeed*Time.deltaTime,size,yOffset, respawnThreshold);
        }
    }
}

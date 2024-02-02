using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TriggerController : MonoBehaviour, IPointerDownHandler
{
    public GameObject goLeftTrigger;
    public GameObject goRightTrigger;
    public GameObject JumpTrigger;
    public GameObject attackTrigger;
    public GameObject useTrigger;
    public GameObject potionHealTrigger;
    public GameObject potionSanityTrigger;
    public GameObject torchTrigger;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter == goLeftTrigger)
        {
            Debug.Log("Left Trigger Tocado");
        }
        else if (eventData.pointerEnter == goRightTrigger)
        {
            Debug.Log("Right Trigger Tocado");
        } else if (eventData.pointerEnter == JumpTrigger) {

        } else if (eventData.pointerEnter == attackTrigger) {
            
        } else if (eventData.pointerEnter == useTrigger) {
            
        } else if (eventData.pointerEnter == potionHealTrigger) {
            
        } else if (eventData.pointerEnter == potionSanityTrigger) {
            
        } else if (eventData.pointerEnter == torchTrigger) {
            
        }
    }
}

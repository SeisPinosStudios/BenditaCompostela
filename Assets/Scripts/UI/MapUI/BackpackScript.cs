using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BackpackScript : MonoBehaviour, IPointerClickHandler
{
    GameObject mitopedia, deckBuilder, equipment;
    Vector3 mitopediaOP, deckBuilderOP, equipmentOP;
    bool open = false;

    public float lerpDuration;
    public int separation;

    public GameObject equipmentPrefab;
    public GameObject deckPrefab;
    public Transform pivot;

    public void Awake()
    {
        mitopedia = transform.GetChild(0).gameObject;
        mitopediaOP = mitopedia.transform.localPosition;

        deckBuilder = transform.GetChild(1).gameObject;
        deckBuilderOP = deckBuilder.transform.localPosition;

        equipment = transform.GetChild(2).gameObject;
        equipmentOP = equipment.transform.localPosition;
    }

    public void OnPointerClick(PointerEventData pointerEvent)
    {
        if (!open) StartCoroutine(OpenBackpack());
        else if (open) StartCoroutine(CloseBackpack());
    }

    public IEnumerator OpenBackpack()
    {
        float timeElapsed = 0;
        var mitopediaPosition = mitopedia.transform.localPosition;
        var deckBuilderPosition = deckBuilder.transform.localPosition;
        var equipmentPosition = equipment.transform.localPosition;

        while (timeElapsed < lerpDuration)
        {
            #region Mitopedia
            mitopediaPosition.y = Mathf.Lerp(mitopediaOP.y, mitopediaOP.y + separation, timeElapsed / lerpDuration);
            mitopedia.transform.localPosition = mitopediaPosition;
            #endregion

            #region DeckBuilder
            deckBuilderPosition.x = Mathf.Lerp(deckBuilderOP.x, deckBuilderOP.x + (separation * Mathf.Cos(Mathf.PI / 4)), timeElapsed / lerpDuration);
            deckBuilderPosition.y = Mathf.Lerp(deckBuilderOP.y, deckBuilderOP.y + (separation * Mathf.Cos(Mathf.PI / 4)), timeElapsed / lerpDuration);
            deckBuilder.transform.localPosition = deckBuilderPosition;
            #endregion

            #region Equipment
            equipmentPosition.x = Mathf.Lerp(equipmentOP.x, equipmentOP.x + separation, timeElapsed / lerpDuration);
            equipment.transform.localPosition = equipmentPosition;
            #endregion

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        open = true;
    }

    public IEnumerator CloseBackpack()
    {
        float timeElapsed = 0;
        var mitopediaPosition = mitopedia.transform.localPosition;
        var deckBuilderPosition = deckBuilder.transform.localPosition;
        var equipmentPosition = equipment.transform.localPosition;

        while(timeElapsed < lerpDuration)
        {
            #region Mitopedia
            mitopediaPosition.y = Mathf.Lerp(mitopediaOP.y + separation, mitopediaOP.y, timeElapsed / lerpDuration);
            mitopedia.transform.localPosition = mitopediaPosition;
            #endregion

            #region DeckBuilder
            deckBuilderPosition.x = Mathf.Lerp(deckBuilderOP.x + (separation * Mathf.Cos(Mathf.PI / 4)), deckBuilderOP.x, timeElapsed / lerpDuration);
            deckBuilderPosition.y = Mathf.Lerp(deckBuilderOP.y + (separation * Mathf.Cos(Mathf.PI / 4)), deckBuilderOP.y, timeElapsed / lerpDuration);
            deckBuilder.transform.localPosition = deckBuilderPosition;
            #endregion

            #region Equipment
            equipmentPosition.x = Mathf.Lerp(equipmentOP.x + separation, equipmentOP.x, timeElapsed / lerpDuration);
            equipment.transform.localPosition = equipmentPosition;
            #endregion

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        open = false;
    }

    public void Equipment()
    {
        if (pivot.transform.childCount > 0) Destroy(pivot.transform.GetChild(0).gameObject);
        StartCoroutine(CloseBackpack());
        Instantiate(equipmentPrefab, pivot);
    }

    public void DeckBuilder()
    {
        if (pivot.transform.childCount > 0) Destroy(pivot.transform.GetChild(0).gameObject);
        StartCoroutine(CloseBackpack());
        Instantiate(deckPrefab, pivot);
    }
}

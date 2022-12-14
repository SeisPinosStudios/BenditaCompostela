using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;
    public List<ResponseEvent> responseEvents;

    List<GameObject> tempResponseButtons = new List<GameObject>();

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }
    public void AddResponseEvent(List<ResponseEvent> responseEvents) {
        this.responseEvents = responseEvents;
    }
    public void ShowResponses(Response[] responses)
    {
        GetComponent<TypeWriterEffect>().Stop();
        float responseBoxHeight = 0;
        for(int i = 0; i<responses.Length; i++)
        {
            Response response = responses[i];
            int responseIndex = i;

            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponentInChildren<TMP_Text>().text = response.ResponseText;

            if(response.CheckConditions()) responseButton.GetComponent<Button>().onClick.AddListener(() => onPickedResponse(response, responseIndex));
            else { responseButton.GetComponent<Button>().enabled = false; responseButton.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f); }

            tempResponseButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }
        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }
    private void onPickedResponse(Response response, int responseIndex)
    {
        responseBox.gameObject.SetActive(false);
        foreach(GameObject button in tempResponseButtons){
            Destroy(button);
        }
        tempResponseButtons.Clear();

        if (responseEvents != null && responseIndex <= responseEvents.Count) {
            //responseEvents[responseIndex].OnPickedResponse?.Invoke();
        }

        //responseEvents = null;
        if (response.DialogueObject) {
            Debug.Log("RESPONSE EVENTS LENGTH" + responseEvents.Count);
            Debug.Log("RESPONSE PICKED" + responseEvents[responseIndex].name);
            dialogueUI.ShowResponseDialog(response.DialogueObject, responseEvents[responseIndex]);
        }
        else
        {
            Debug.Log("RESPONSE EVENTS LENGTH" + responseEvents.Count);
            Debug.Log("RESPONSE PICKED" + responseEvents[responseIndex].name);
            responseEvents[responseIndex].onPickedResponse.Invoke();
            dialogueUI.CloseDialogBox();
        }
    }
}

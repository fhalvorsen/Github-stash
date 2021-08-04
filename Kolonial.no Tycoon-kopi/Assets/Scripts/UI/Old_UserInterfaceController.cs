using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Old_UserInterfaceController : MonoBehaviour
{

    public Text timeDisplay;
    public Text moneyDisplay;
    public Text popupDisplay;
    public Button cameraBtn;

    // private TimeController timeController;
    // private Money_Sys moneyController;      

    private GameObject cityCamera;

    private bool isInCityView = false;

    public bool PopUpReady;

    // Use this for initialization
    void Start()
    {
        // Henter kontroller scripts
        // timeController = GetComponentInParent<TimeController>();
        // moneyController = GetComponentInParent<Money_Sys>();

        // Legger til funksjon til kamera svitsj knappen
        cameraBtn.onClick.AddListener(ChangeCameraView);
        cityCamera = GameObject.Find("City").gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // timeDisplay.text = timeController.GetTimeToString();
        // moneyDisplay.text = moneyController.GetKroner() + " Kr";
    }

    void ChangeCameraView()
    {
        isInCityView = !isInCityView;
        cityCamera.SetActive(!cityCamera.gameObject.activeInHierarchy);

    }

    public void PopUp(string message)
    {
        StartCoroutine(ShowPopUp(message));
    }

    IEnumerator ShowPopUp(string message)
    {
        popupDisplay.text = message;
        popupDisplay.enabled = true;
        yield return new WaitForSeconds(3);
        popupDisplay.enabled = false;
    }
}
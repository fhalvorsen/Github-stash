using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResearchMenu : MonoBehaviour 
{
    [SerializeField]
    private SimpleObjectPool researchTogglePool;
    [SerializeField]
    private GameObject toggleParent;
    [SerializeField]
    private ToggleGroup toggleGroup;
    [HideInInspector]
    public Character researchCharacter;
    private List<GameObject> toggles = new List<GameObject>();
	
    private void OnEnable()
    {
        toggleGroup.SetAllTogglesOff();
        while (toggles.Count > 0)
        {
            researchTogglePool.ReturnObject(toggles[0]);
            toggles.RemoveAt(0);
        }
        foreach (Service service in GameManager.Instance.gameData.playerCompany.ResearchedServices)
        {
            if (service.Type == ServiceType.PhysicalOrder)
            {
                continue;
            }
            for (int i = 1; i < 4; i++)
            {
                GameObject toggle = researchTogglePool.GetObject();
                toggles.Add (toggle);
                toggle.transform.SetParent(toggleParent.transform);
                ResearchToggle toggleScript = toggle.GetComponent<ResearchToggle>();
                toggleScript.Setup(service.Type, i);
                Toggle toggleComponent = toggle.GetComponent<Toggle>();
                toggleComponent.group = toggleGroup;

                if(service.Level + 1== i)
                {
                    toggleComponent.interactable = true;
                }
                else
                {
                    toggleComponent.interactable = false;
                }   
            }
        }
    }

    public void HandleResearchButtonPress()
    {
        foreach(Toggle toggle in toggleGroup.ActiveToggles())
        {
            if (toggle.isOn)
            {
                ResearchToggle toggleScript = toggle.GetComponent<ResearchToggle>();
                Service service = new Service(toggleScript.Type, 0, toggleScript.Lvl);
                InteractableComputer computer = Interactable.FindObjectOfType<InteractableComputer>();
                GameManager.Instance.PlayerScript.AssignJob(CharacterData.Job.Unassigned);
                GameManager.Instance.PlayerScript.StartResearch(service);
            }
        }
    }
}

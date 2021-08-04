using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerCharacter : Character 
{
    [SerializeField]
    private int researchInterval;
    [HideInInspector]
    public bool clickToMoveEnabled = true;

	protected override void Update()
	{
        base.Update();
        
		 if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

		if (Input.GetMouseButtonDown(0) && clickToMoveEnabled)
        {
            MoveToClick();
        }

        if (Input.GetMouseButton(1))
        {
            OpenActionMenu();
        }
    }


    public void StartResearch(Service service)
    {
        StartCoroutine(DoResearch(service));
    }

    private IEnumerator DoResearch(Service service)
    {
        InteractableComputer computer = FindObjectOfType<InteractableComputer>();
        movement.SetInteractionTarget(computer);
        clickToMoveEnabled = false;
        
        yield return new WaitUntil(()=> computer.HasInteractedWith(this));

        transform.position = computer.forSits.transform.position;
        transform.rotation = computer.forSits.transform.rotation;
        movement.animator.SetBool("Computing", true);
        movement.agent.enabled = false;
        
        int lastInterval = 0;
        while (characterData.progress < 999)
        {
            characterData.progress += 101;
            lastInterval = GameManager.Instance.time.GetTime();
            yield return new WaitUntil(()=> GameManager.Instance.time.GetTime() - lastInterval > researchInterval);
        }
         List<Service> services = GameManager.Instance.gameData.playerCompany.ResearchedServices;
        for (int i = 0; i < services.Count; i++)
        {
            if (services[i].Type == service.Type)
            {
                services.RemoveAt(i);
                services.Insert(i, service);
                continue;
            }
        }
        clickToMoveEnabled = true;
        movement.animator.SetBool("Computing", false);
        movement.agent.enabled = true;
        characterData.progress = 0;
    }

    private void OpenActionMenu()
    {
        Ray ray = movement.warehouseCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            interactable = hit.collider.GetComponent<Interactable>();


            if (interactable != null)
            {
                if (interactable is InteractableComputer)
                {
                    UserInterfaceController.Instance.ClearAllMenus();
                    UserInterfaceController.Instance.DisplayComputerActionMenu(hit.point);
                }
                if (interactable is InteractableWarehouseStation)
                {
                    UserInterfaceController.Instance.ClearAllMenus();
                    UserInterfaceController.Instance.DisplayWarehouseActionMenu(hit.point);
                }
                if (interactable is InteractableDeliveryStation)
                {
                    UserInterfaceController.Instance.ClearAllMenus();
                    UserInterfaceController.Instance.DisplayDeliveriesActionMenu(hit.point);
                }

            }

        }
    }

    public void HandleActionButtonPress()
    {
        movement.SetInteractionTarget(interactable);
        UserInterfaceController.Instance.ClearAllMenus();
    }

    private void MoveToClick()
    {
        UserInterfaceController.Instance.ClearAllMenus();
        Ray ray = movement.warehouseCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
               if(interactable is InteractableComputer)
               {
                   UserInterfaceController.Instance.ClearAllMenus();
                   UserInterfaceController.Instance.DisplayComputerMenu();
               } 
               else if(interactable is InteractableWarehouseStation)
               {
                   UserInterfaceController.Instance.ClearAllMenus();
                   UserInterfaceController.Instance.DisplayWarehouseInfoMenu();
               } 
               else if (interactable is InteractableDeliveryStation)
               {
                   UserInterfaceController.Instance.ClearAllMenus();
               }
            }
            else
            {
                AssignJob(CharacterData.Job.Unassigned);
                movement.RemoveInteractionTarget();
                movement.MoveToPoint(hit.point);
            }

            // stop focusing any object
        }
    }
}

using UnityEngine;

public class InteractableBoxes : Interactable {

    public GameObject InventoryMenu;

  

    void OpenInventoryMenu()
    {
        InventoryMenu.SetActive(true);
    }
}

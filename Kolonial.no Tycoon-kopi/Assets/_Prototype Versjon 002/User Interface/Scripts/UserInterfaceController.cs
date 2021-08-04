using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UserInterfaceController : Singleton<UserInterfaceController> 
{
	private int currentDisplayedOrders;
	[SerializeField]
	private TextMeshProUGUI notificationTitle, notificationBody, characterInfoTitle, 
	assignMenuName, wholesalerInventoryTitle, wholesalerInventoryTotal,
	timeLabel, cashLabel, customerCountLabel;
	[SerializeField]
	private Image playerInfoPortrait, assignMenuPortrait;
	[SerializeField]
	private Sprite unselectedSprite, selectedSprite;
	[SerializeField]
	private GameObject companyCreationMenu, notificationPanel, characterInfoMenu, 
	assignMenu, warehouseSelect, deliverySelect, wholesalerInventoryCardGrid, warehouseStaffCardGrid,
	computerActionMenu, deliveriesActionMenu, warehouseActionMenu, staffActionMenu, 
	playerActionMenu,warehouseInfoMenu, wholesalersMenu, wholesalerInventoryMenu, warehouseStaffMenu,
	buildMenu, orderIndicatorPanel, researchMenu, computerInfoMenu, gameMenusParent,
	mainMenusParent, actionMenusParent, deliveryIndicatorPanel;
	[SerializeField]
	private SimpleObjectPool wholesalerItemCardPool, 
	staffCardPool, orderIndicatorPool, deliveryIndicatorPool;
	[SerializeField]
	private Camera warehouseCamera;
	private List<WholesalerItemCard> wholsalerCardComponents = new List<WholesalerItemCard>();
	private List<StaffCard> staffCardComponents = new List<StaffCard>();
	private List<GameObject> orderIndicators = new List<GameObject>(), deliveryIndicators = new List<GameObject>();
	private ItemDictionary itemsToBuy = new ItemDictionary();
	// public ItemData[] testitems;
	// public ItemDictionary testDictionary;
	
	public static UserInterfaceController Instance {
		get {
			return ((UserInterfaceController)mInstance);
		} set {
			mInstance = value;
		}
	}

	private void Start()
	{
		orderIndicatorPool = orderIndicatorPanel.GetComponent<SimpleObjectPool>();
		// testDictionary = new ItemDictionary();
		// foreach (ItemData item in testitems)
		// {
		// 	testDictionary.AddItem(item, 3);
		// }
		// DisplayInventoryMenu(testDictionary);
		
		
	}

	private void Update ()
	{
		UpdateTimeLabel();
		UpdateCashLabel();
		UpdateCustomerCountLabel();
	}

	public void DisplayCompanyCreationMenu()
	{
		companyCreationMenu.SetActive (true);
	}

	public void DisplayNotification(string title, string body)
	{
		notificationPanel.SetActive(true);
		notificationTitle.text = title;
		notificationBody.text = body;
	}
	
	public void DisplayCharacterInfoMenu(CharacterData data)
	{
		characterInfoMenu.SetActive(true);

	}
	
	public void DisplayAssignMenu(CharacterData data)
	{
		assignMenu.SetActive(true);
	}

	public void DisplayWarehouseInfoMenu()
	{
		warehouseInfoMenu.SetActive(true);
	}

	public void DisplayWholesalerInventoryMenu(WholesalerData wholesaler)
	{
		wholesalerInventoryMenu.SetActive(true);
		wholesalerInventoryTitle.text = wholesaler.name;
		
		while (wholsalerCardComponents.Count > 0)
		{
			wholesalerItemCardPool.ReturnObject(wholsalerCardComponents[0].gameObject);
			wholsalerCardComponents.RemoveAt(0);
		}

		foreach (ItemData item in wholesaler.Inventory)
		{
			GameObject itemCard = wholesalerItemCardPool.GetObject();
			itemCard.transform.SetParent(wholesalerInventoryCardGrid.transform);
			WholesalerItemCard cardScript = itemCard.GetComponent<WholesalerItemCard>();
			cardScript.Setup(item);
			wholsalerCardComponents.Add(cardScript);
		}
		wholesalerInventoryTotal.text = "0";
	}

	public void UpdateOrder(ItemData item, int amount)
	{
		itemsToBuy.Items[item] = amount;
		float cost = 0;

		foreach (ItemData data in itemsToBuy.Items.Keys)
		{
			cost += itemsToBuy.Items[data] * data.wholesalerPrice;
		}
		wholesalerInventoryTotal.text = cost.ToString();
	}

	public void BuyOrder()
	{
		foreach(ItemData data in itemsToBuy.Items.Keys)
		{
			GameManager.Instance.playerBalance -= data.wholesalerPrice;
			GameManager.Instance.gameData.playerCompany.inventory.AddItem(data, itemsToBuy.Items[data]);
		}
		itemsToBuy.Items.Clear();
	}

	public void DisplayComputerMenu()
	{
		computerInfoMenu.SetActive(true);
	}

	public void DisplayWarehouseStaffMenu()
	{
		warehouseStaffMenu.SetActive(true);

		while (staffCardComponents.Count > 0)
		{
			staffCardPool.ReturnObject(staffCardComponents[0].gameObject);
			staffCardComponents.RemoveAt(0);
		}

		foreach (CharacterData staff in GameManager.Instance.Staff)
		{
			if(staff.AssignedJob == CharacterData.Job.WarehouseWorker)
			{
				GameObject staffCard = staffCardPool.GetObject();
				staffCard.transform.SetParent(warehouseStaffCardGrid.transform);
				StaffCard cardScript = staffCard.GetComponent<StaffCard>();
				cardScript.Setup(staff);
				staffCardComponents.Add(cardScript);
			}
		}

		while (staffCardComponents.Count < GameManager.Instance.Warehouse.StaffSlots)
		{
			GameObject staffCard = staffCardPool.GetObject();
			staffCard.transform.SetParent(warehouseStaffCardGrid.transform);
			StaffCard cardScript = staffCard.GetComponent<StaffCard>();
			cardScript.SetupStaffSlot();
			staffCardComponents.Add(cardScript);
			Debug.Log("Warehouse Slot");
		}
	}

	public void DisplayResearchMenu(Character c)
	{
		researchMenu.SetActive(true);
		ResearchMenu rMenu = researchMenu.GetComponent<ResearchMenu>();
		rMenu.researchCharacter = c;
	}

	public void ClearAllMenus()
	{
		foreach(Transform child in actionMenusParent.transform)
		{
			child.gameObject.SetActive(false);
		}
		foreach (Transform child in gameMenusParent.transform)
		{
			child.gameObject.SetActive(false);
		}
		foreach (Transform child in mainMenusParent.transform)
		{
			child.gameObject.SetActive(false);
		}
	}

	public void DisplayComputerActionMenu(Vector3 position)
	{
		computerActionMenu.SetActive(true);
		computerActionMenu.transform.position = warehouseCamera.WorldToScreenPoint(position);
	}

	public void DisplayDeliveriesActionMenu(Vector3 position)
	{
		deliveriesActionMenu.SetActive(true);
		deliveriesActionMenu.transform.position = warehouseCamera.WorldToScreenPoint(position);
	}
	public void DisplayWarehouseActionMenu(Vector3 position)
	{
		warehouseActionMenu.SetActive(true);
		warehouseActionMenu.transform.position = warehouseCamera.WorldToScreenPoint(position);
	}
	public void DisplayStaffActionMenu(Vector3 position)
	{
		staffActionMenu.SetActive(true);
		staffActionMenu.transform.position = warehouseCamera.WorldToScreenPoint(position);
	}
	public void DisplayPlayerActionMenu(Vector3 position)
	{
		playerActionMenu.SetActive(true);
		playerActionMenu.transform.position = warehouseCamera.WorldToScreenPoint(position);
	}

	public void DisplayBuildMenu()
	{
		// buildMenuDropdown.AddOptions(List<string> options);
	}

	public void RemoveOrderIndicator()
	{
		orderIndicatorPool.ReturnObject(orderIndicators[0]);
		orderIndicators.RemoveAt(0);
	}
	public void AddOrderIndicator()
	{
		GameObject orderIndicator = orderIndicatorPool.GetObject();
		orderIndicator.transform.localScale = Vector3.one;
		orderIndicator.transform.SetParent(orderIndicatorPanel.transform);
		orderIndicators.Add(orderIndicator);
	}

	public void RemoveDeliveryIndicator()
	{
		deliveryIndicatorPool.ReturnObject(deliveryIndicators[0]);
		deliveryIndicators.RemoveAt(0);
	}

	public void AddDeliveryIndicator()
	{
		GameObject deliveryIndicator = deliveryIndicatorPool.GetObject();
		deliveryIndicator.transform.localScale = Vector3.one;
		deliveryIndicator.transform.SetParent(deliveryIndicatorPanel.transform);
		deliveryIndicators.Add(deliveryIndicator);

	}
	public void ToggleAssign()
	{
		Image warehouseSelectImage = warehouseSelect.GetComponent<Image>();
		Image deliverySelectImage = deliverySelect.GetComponent<Image>();

		if (warehouseSelectImage.sprite == selectedSprite)
		{
			warehouseSelectImage.sprite = unselectedSprite;
			deliverySelectImage.sprite = selectedSprite;
		} 
		else
		{
			warehouseSelectImage.sprite = selectedSprite;
			deliverySelectImage.sprite = unselectedSprite;
		}
	}

	public void HandleDeliveryActionButton()
	{
		GameManager.Instance.PlayerScript.AssignJob(CharacterData.Job.Driver);
	}

	private void UpdateTimeLabel()
	{
		int hours = GameManager.Instance.time.GetTime();
		 
		// TODO finish this shit
		int days = (int)Mathf.Floor(hours / 24);
		string day = Mathf.Floor((days % 365) % 30) .ToString();
		string month = Mathf.Floor((days % 365) / 30) .ToString();
		string year = Mathf.Floor(days / 365) .ToString();
		
		
		timeLabel.text = string.Format("Y{0} M{1} D{2}", year, month, day);
	}

	private void UpdateCashLabel()
	{
		cashLabel.text = GameManager.Instance.playerBalance.ToString("n");
	}

	private void UpdateCustomerCountLabel()
	{
		customerCountLabel.text = GameManager.Instance.customerCount.ToString();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholesalersMenu : MonoBehaviour 
{
	private SimpleObjectPool wholesalerCardPool;
	[SerializeField]
	private GameObject wholesalersCardGrid;

	public void OnEnable()
	{
		wholesalerCardPool = GetComponent<SimpleObjectPool>();
		foreach (WholesalerData wholesaler in GameManager.Instance.gameData.Wholesalers)
		{
			GameObject wholesalerCard = wholesalerCardPool.GetObject();
			wholesalerCard.transform.SetParent(wholesalersCardGrid.transform);
			WholesalerCard cardScript = wholesalerCard.GetComponent<WholesalerCard>();
			cardScript.Setup(wholesaler);
		}
	}
}

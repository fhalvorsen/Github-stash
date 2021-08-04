using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWarehouseStation : Interactable {

	

	public override void OnFocused(Character character)
	{
		base.OnFocused(character);
		GameManager.Instance.AvailableWarehousePoints.Remove(this);
	}

	public override void OnDefocused(Character character)
	{
		base.OnDefocused(character);
		GameManager.Instance.AvailableWarehousePoints.Add(this);
	}
}

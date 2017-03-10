using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Menu;
using Types;

public class PurchaseItem : ButtonEvent {

	public Text costText;
	public PurchaseType purchaseType;
	public float disabledAlpha = 0.5f;
	public CanvasGroup canvas;

	public override void OnItemUp() {
		base.OnItemUp();
		switch (purchaseType) {
			case PurchaseType.SINGLE_LIFE: BuySingleLife(); break;
			case PurchaseType.FULL_LIFE_REFILL: BuyFullRefill(); break;
			case PurchaseType.SHIP: BuyShip(); break;
			case PurchaseType.IAP_RESOURCE_500: BuyIAPResources(500); break;
			case PurchaseType.IAP_RESOURCE_1500: BuyIAPResources(1500); break;
			case PurchaseType.IAP_RESOURCE_4500: BuyIAPResources(4500); break;
			case PurchaseType.IAP_RESOURCE_10000: BuyIAPResources(10000); break;
		}
	}

	public virtual void BuySingleLife() {}
	public virtual void BuyFullRefill() {}
	public virtual void BuyShip() {}
	public virtual void BuyIAPResources(int amount) {}

	protected void SetCostText(int cost) {

		int mag = (int)(Mathf.Floor(Mathf.Log10(cost))/3); 
		double divisor = Mathf.Pow(10, mag*3);

		double shortNumber = cost / divisor;

		string numSuffix = string.Empty;
		switch(mag)
		{
		    case 1:
		        numSuffix = "K";
		        break;
		    case 2:
		        numSuffix = "M";
		        break;
		    case 3:
		        numSuffix = "B";
		        break;
		}
		string decimals = cost >= 1000 ? "N2" : "N0";
		string result = shortNumber.ToString(decimals) + numSuffix; 
		if (result.ToLower() == "nan") { result = "0"; }
		costText.text = result;
	}
}

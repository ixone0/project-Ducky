using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RanButtonn : MonoBehaviour
{
	[SerializeField] GameObject[] buttons;
	[SerializeField] GameObject[] lightArray;
	[SerializeField] int[] lightOrder;
	[SerializeField] GameObject panel;

	int level = 0;
	int buttonclicked = 0;
	int colorOrderRunCount = 0;
	bool passed = false;
	bool won = false;
	Color32 red = new Color32(255, 39, 0, 255);
	Color32 green = new Color32(4, 204, 0, 255);
	public float lightspeed;

	private void OnEnable()
	{
		level = 0;
		buttonclicked = 0;
		colorOrderRunCount = -1;
		won = false;
		for(int i = 0; i < lightOrder.Length; i++)
		{
			lightOrder[i] = (Random.Range(0, 8));
		}
		level = 1;

		StartCoroutine(ColorOrder());
	}

	public void ButtonClickOrder(int button)
	{
		buttonclicked++;
		if(button == lightOrder[buttonclicked-1])
		{
			Debug.Log("pass");
			passed = true;
		}
		else
		{
			Debug.Log("fail");
			won = false;
			passed = false;
			StartCoroutine(ColorBlink(red));
		}
		if(buttonclicked == level && passed == true && buttonclicked != 5)
		{
			level++;
			passed = false;
			StartCoroutine(ColorOrder());
		}
		if(buttonclicked == level && passed == true && buttonclicked == 5)
		{
			Debug.Log("fail");
			won = true;
			StartCoroutine(ColorBlink(green));
		}
	}

	public void ClosePanel()
	{
		panel.SetActive(false);
	}

	public void Openpanel()
	{
		panel.SetActive(true);
	}

	IEnumerator ColorBlink(Color32 colorToBlink)
	{
		DisableInteractableButton();
		for(int j = 0; j < 3; j++)
		{
			for(int i = 0; i < buttons.Length; i++)
			{
				buttons[i].GetComponent<Image>().color = colorToBlink;
			}
			yield return new WaitForSeconds(0.5f);

		}
		if(won == true)
		{
			ClosePanel();
		}
		EnableInteractableButtons();
		OnEnable();
	}

	IEnumerator ColorOrder()
	{
		buttonclicked = 0;
		colorOrderRunCount++;
		DisableInteractableButton();
		for(int i = 0; i <= colorOrderRunCount; i++)
		{
			if(level >= colorOrderRunCount)
			{
				lightArray[lightOrder[i]].GetComponent<Image>().color = green;
				yield return new WaitForSeconds(lightspeed);
			}
		}
		EnableInteractableButtons();
	}

	void DisableInteractableButton()
	{
		for(int i = 0; i < buttons.Length; i++)
		{
			buttons[i].GetComponent<Button>().interactable = false;
		}
	}

	void EnableInteractableButtons()
	{
		for(int i = 0; i < buttons.Length; i++)
		{
			buttons[i].GetComponent<Button>().interactable = true;
		}
	}

}

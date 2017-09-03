using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryControl : MonoBehaviour {

	public int choiceTimes = 3;
	public int[] choicePages =new int[3];
    //Image for choice button
    public string[] choiceHint = new string[3];
	public Sprite[] posPic=new Sprite[3];
	public Sprite[] negPic=new Sprite[3];
	//Contents to show in diary
	public TextAsset[] Text1=new TextAsset[2];
	public TextAsset[] Text2=new TextAsset[4];
    public TextAsset[] Text3= new TextAsset[8];
    public GameObject choiceCanvas;
    public GameObject choiceText;
	public GameObject posButton;
	public GameObject negButton;
	public GameObject TargetBook;


	private Book BScript;
	private BookReader BR;
	[SerializeField] private bool canActiveCanvas = false;
	[SerializeField] private int currentChoiceNum = 0;
	[SerializeField] private bool[] isFirstTime;
	[SerializeField] private bool[] isPositive;

	void Start () {
		//isFirstTime all false
		isFirstTime=new bool[choiceTimes];
		for (int i = 0; i < choiceTimes; i++) {
			isFirstTime [i] = true;
		}

		isPositive = new bool[choiceTimes];
		for (int i = 0; i < choiceTimes; i++) {
			isPositive [i] = false;
		}
		//init BookReader
		BR=gameObject.GetComponent<BookReader>();
		BScript = TargetBook.GetComponent<Book> ();
	}

	void Update () {
		//Update canActiveCanvas
		//if nextPage == choicePages[X] && isFirstTime[X]  then set CanActiveCanvas
		bool flag=false;
		for (int i = 0; i < choiceTimes; i++) {
			if (isFirstTime [i]) {
				if (choicePages [i] == (BR.getPageNo() + 2)) {
					currentChoiceNum = i;
					canActiveCanvas = true;
					flag = true;
					break;
				}
			}
		}

		if (!flag) {
			canActiveCanvas = false;
		}
	}

	public bool getCanActiveCanvas()
	{
		return canActiveCanvas;
	}

	//Used in BookReader.cs to Active Canvas when needed
	public void ActiveCanvas()
	{
		//isFirstTime =false;    //then can Active is False Automatically
		isFirstTime[currentChoiceNum]=false;
		//Activate Canvas;
		choiceCanvas.SetActive(true);
		//Set UI
        choiceText.GetComponent<Text>().text= choiceHint[currentChoiceNum];
        posButton.GetComponent<Image>().sprite=posPic[currentChoiceNum];
		negButton.GetComponent<Image>().sprite=negPic[currentChoiceNum];
	}

	//Used in Canvas UI for Click
	public void setCurrentPositive()
	{
		//Update Book Content
		isPositive[currentChoiceNum]=true;
        if(currentChoiceNum==0)
        {
            int pos = (isPositive[0] ? 1 : 0) ;
            BScript.pages[currentChoiceNum + 1] = Text1[pos];
        }
        else if (currentChoiceNum == 1)
        {
            int pos = (isPositive[0] ? 1 : 0) * 2 + (isPositive[1] ? 1 : 0) ;
            BScript.pages[currentChoiceNum + 1] = Text2[pos];
        }
        else if (currentChoiceNum == 2)
        {
            int pos = (isPositive[0] ? 1 : 0) * 4 + (isPositive[1] ? 1 : 0) * 2 + (isPositive[2] ? 1 : 0);
            BScript.pages[currentChoiceNum + 1] = Text3[pos];
        }
        BR.refreshBookContent();
		//Deactivate Canvas
		choiceCanvas.SetActive(false);
	}

	//Used in Canvas UI for Click
	public void setCurrentNegative()
	{
		//Update Book Content
		isPositive[currentChoiceNum]=false;
        if (currentChoiceNum == 0)
        {
            int pos = (isPositive[0] ? 1 : 0);
            BScript.pages[currentChoiceNum + 1] = Text1[pos];
        }
        else if (currentChoiceNum == 1)
        {
            int pos = (isPositive[0] ? 1 : 0) * 2 + (isPositive[1] ? 1 : 0);
            BScript.pages[currentChoiceNum + 1] = Text2[pos];
        }
        else if (currentChoiceNum == 2)
        {
            int pos = (isPositive[0] ? 1 : 0) * 4 + (isPositive[1] ? 1 : 0) * 2 + (isPositive[2] ? 1 : 0);
            BScript.pages[currentChoiceNum + 1] = Text3[pos];
        }
        BR.refreshBookContent();
		//Deactivate Canvas
		choiceCanvas.SetActive(false);
	}

    public int getEnding()
    {
        return isPositive[choiceTimes - 1] ? 0:1;
    }

    public bool canEnd()
    {
        if (BR)
            return BR.isEnd;
        else
            return false;
    }

}
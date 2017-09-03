using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Book : MonoBehaviour {
	public List<TextAsset> pages = new List<TextAsset>();
	public bool twoPages;
	public bool isNumbered;
	public Font font;
	public int fontSize = 6;
	public Color fontColor = new Color(0,0,0,255);
	public Texture2D pageTexture;
	public Texture2D backgroundTexture;
	public Texture2D foregroundTexture;
	public float pageWidth = 0.4f;			//1 = full width of screen
	public float pageHeight = 1.2f;			//multiplication of pageWidth to determine height
	public float backgroundArea = 1.05f;	//1 = same area as page
	public float contentArea = 0.8f;		//1 = same area as page

    private int lastTimePage = 0;
	 
	public void AddPages(Book book){
		pages.AddRange(book.pages);
	}

    public int getLastTimePage()
    {
        return lastTimePage;
    }

    public void settLastTimePage(int pageNo)
    {
        lastTimePage = pageNo;
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class ProloguePlay : MonoBehaviour {

    public float videoFadeTime = 2.0f;
    public float hintHoldTime = 6.0f;
    public float hintFadeTime = 6.0f;
	public MovieTexture prologue;
	public AudioClip audio;
	public GameObject firstPersonController;
	public GameObject soundControl;
	public GameObject camera;
    public Graphic black;
    public Graphic transPic;

	private AudioSource AS;
    private float endTime;
    [SerializeField]
    private bool coroutineOn=false;

    void OnGUI()
    {
		//绘制电影纹理
		GUI.DrawTexture (new Rect (0,0, Screen.width, Screen.height),prologue,ScaleMode.StretchToFill);

        if (GUILayout.Button("Skip"))
        {
            //停止播放
            prologue.Stop();
        }
    }

	void Awake(){
		AS = gameObject.GetComponent<AudioSource> ();
        endTime = Time.time + prologue.duration - videoFadeTime;
        AS.clip = audio;
        prologue.loop = false;
        prologue.Play();
        AS.Play();
        firstPersonController.SetActive(false);
        camera.SetActive(true);
        soundControl.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
        if (!coroutineOn)
        {
            if (Time.time > endTime)
            {
                this.enabled = false;
                camera.SetActive(false);
                firstPersonController.SetActive(true);
                soundControl.SetActive(true);
                StartCoroutine(Transition1());
                coroutineOn = true;
            }
            else if (!prologue.isPlaying)
            {
                this.enabled = false;
                camera.SetActive(false);
                firstPersonController.SetActive(true);
                soundControl.SetActive(true);
                StartCoroutine(Transition2());
                coroutineOn = true;
            }
        }
	}

    IEnumerator Transition1()
    {
        black.CrossFadeAlpha(0.0f, 2.0f, true);
        yield return new WaitForSeconds(videoFadeTime);
        yield return new WaitForSeconds(hintHoldTime);
        transPic.CrossFadeAlpha(0.0f, hintFadeTime, true);
    }

    IEnumerator Transition2()
    {
        black.canvasRenderer.SetAlpha(0.0f);
        transPic.canvasRenderer.SetAlpha(0.0f);
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPlay : MonoBehaviour {

    public DiaryControl DC;
    public MovieTexture[] endingVideos;
    public AudioClip[] endingAudios;
    public GameObject firstPersonController;
    public GameObject soundControl;
    public GameObject alterCamera;

    [SerializeField]
    private AudioSource AS;
    [SerializeField]
    private MovieTexture videoToPlay;
    [SerializeField]
    private bool played = false;

    void Awake()
    {
        AS = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
    }

    void OnGUI()
    {
        //Debug.Log(DC.canEnd());
        //绘制电影纹理
        if (DC.canEnd())
        {
            //Debug.Log("canEnd");
            firstPersonController.SetActive(false);
            alterCamera.SetActive(true);
            soundControl.SetActive(false);

            videoToPlay = endingVideos[DC.getEnding()];
            AS.clip= endingAudios[DC.getEnding()];
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), videoToPlay, ScaleMode.StretchToFill);

            if (played && videoToPlay.isPlaying)
            {
                if (GUILayout.Button("Skip"))
                {
                    //停止播放
                    videoToPlay.Stop();
                    AS.Stop();
                }
            }

            if(played && !videoToPlay.isPlaying)
            {
                if (GUILayout.Button("Replay"))
                {
                    Application.LoadLevel("LoadScene");
                }
            }

            if (!played && !videoToPlay.isPlaying)
            {
                videoToPlay.Play();
                AS.Play();
                played = true;
            }
        }

    }
}

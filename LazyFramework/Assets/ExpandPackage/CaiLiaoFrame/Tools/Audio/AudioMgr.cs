using UnityEngine;
using System.Collections;
using System;

public class AudioMgr : MonoBehaviour 
{
    static AudioMgr _singleton;
    public static AudioMgr singleton
    {
        get
        {
            return _singleton;
        }
    }
    AudioSource selected;
    AudioSource custom;
    AudioSource teacher;
    AudioSource mEvent;
	AudioSource mBackGround;
    AudioClip teacher_clip;

    void Awake()
    {
        _singleton = this;
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
		PlayBackGround ("beijing", 0.2f, true);
    }
    string lastAudio = "";
    string lastEventAudio = "";
    public void PlayCustomAudio(string path, float volume, bool ifloop, out AudioSource outCustom)
    {
        if (custom == null)
        {
            GameObject obj = new GameObject("CustomAudio");
            obj.transform.parent = transform;
            obj.transform.localPosition = Vector3.zero;
            custom = obj.AddComponent<AudioSource>();
            custom.playOnAwake = false;
        }
        custom.volume = volume;
        custom.loop = ifloop;
        AudioClip clip = Resources.Load("Audio/" + path) as AudioClip;
        if (clip != null)
        {
            custom.clip = clip;
        }
        outCustom = custom;
        custom.Play();
    }
    
    public void EventAudio(string path, float volume = 1, bool ifloop = false) 
    {
        if (mEvent == null)
        {
            GameObject obj = new GameObject("EventAudio");
            obj.transform.parent = transform;
            obj.transform.localPosition = Vector3.zero;
            mEvent = obj.AddComponent<AudioSource>();
            mEvent.playOnAwake = false;
        }
        mEvent.volume = volume;
        mEvent.loop = ifloop;

        if (!lastEventAudio.Equals(path))
        {
			AudioClip clip = Resources.Load("Audio/"+path) as AudioClip;
            if (clip != null)
            {
                mEvent.clip = clip;
            }
            lastEventAudio = path;
        }
        mEvent.Play();
    }
    public void PlayAudio(string path, float volume, bool ifloop, out AudioSource OutSelected)
    {
        if (selected == null)
        {
            GameObject obj = new GameObject("SelectedAudio");
            obj.transform.parent = transform;
            obj.transform.localPosition = Vector3.zero;
            selected = obj.AddComponent<AudioSource>();
            selected.playOnAwake = false;
        }
        selected.volume = volume;
        selected.loop = ifloop;

        if (!lastAudio.Equals(path))
        {
            AudioClip clip = Resources.Load("Audio/" + path) as AudioClip; 
            if (clip != null)
            {
                selected.clip = clip;
            }
            else
            {
                OutSelected = selected;
                return;
            }
            lastAudio = path;
        }
        OutSelected = selected;
        selected.Play();
    }
    public void PlayAudioVolume(float volume = 1) 
    {
        selected.volume = volume;
    }
	public void PlayBackGround(string path, float volume=1,bool ifloop = false)
	{
        if (mBackGround == null)
		{
			GameObject obj = new GameObject("BackGroundAudio");
			obj.transform.parent = transform;
			obj.transform.localPosition = Vector3.zero;
			mBackGround = obj.AddComponent<AudioSource>();
			mBackGround.playOnAwake = false;
		}
		mBackGround.volume = volume;
		mBackGround.loop = ifloop;

		if (!lastAudio.Equals(path))
		{
			AudioClip clip = Resources.Load("Audio/"+path) as AudioClip;
			if (clip != null)
			{
				mBackGround.clip = clip;
			}
			lastAudio = path;
		}
		mBackGround.Play();
	}
    public void Clear()
    {
        if (selected != null)
        {
            selected.Stop();
            selected.clip = null;
        }
        if (mEvent!=null)
        {
            mEvent.Stop();
            mEvent.clip = null;
        }
        lastEventAudio = "";
        if (custom != null)
        {
            custom.Stop();
            custom.clip = null;
        }
        lastAudio = "";
    }
    public void PlayTeacherAudio(byte[] val, int samples, int channels, int frequency)
    {
        //byte[] data = Zip.DeCompress(val);
        //int length = data.Length / 4;
        //float[] _data = new float[length];
        //Buffer.BlockCopy(data, 0, _data, 0, data.Length);

        //if(teacher_clip == null)
        //    teacher_clip = AudioClip.Create("teacherClip", samples, channels, frequency, false);
        //teacher_clip.SetData(_data, 0);

        //if (teacher == null)
        //{
        //    GameObject obj = new GameObject("TeacherAudio");
        //    obj.transform.parent = transform;
        //    obj.transform.localPosition = Vector3.zero;
        //    teacher = obj.AddComponent<AudioSource>();
        //    teacher.playOnAwake = false;
        //}
        //teacher.clip = teacher_clip;
        //teacher.Play();
    }
}

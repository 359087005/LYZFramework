using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class UGUISpriteAnimation : MonoBehaviour
{
    //[HideInInspector]
    public Image ImageSource;
        
    private int mCurFrame = 0;
    public bool IsResouresFile=true;
    
    /// <summary>    /// 在Resoures中文件名称    /// </summary>
    public string ResouresFileName;
    /// <summary>    /// 要加载图片的长度    /// </summary>
    public int FileCount;
    [Range(1,100)]
    /// <summary>    /// 隔开几个加载一个图片    /// </summary>
    public int FileInterval=1;

    private float mDelta = 0;
    /// <summary>    /// 播放次数    /// </summary>
    public float FPS = 5;
    //[HideInInspector]
    /// <summary>    /// 图片集    /// </summary>
    public List<Sprite> SpriteFrames=new List<Sprite>();
    /// <summary>    /// 是否播放    /// </summary>
    public bool IsPlaying = false;
    /// <summary>    /// 图片正常播放 还是倒着播放    /// </summary>
    public bool Foward = true;
    /// <summary>    /// 是否开始播放    /// </summary>
    public bool AutoPlay = false;
    /// <summary>    /// 是否循环    /// </summary>
    public bool Loop = false;

    /// <summary>    /// 结束事件    /// </summary>
    public UnityAction ExitEvent;
    public int FrameCount
    {
        get
        {
            return SpriteFrames.Count;
        }
    }

    void Awake()
    {
        ImageSource = GetComponent<Image>();
        if (!IsResouresFile)
            return;
        for (int i = 0; i < FileCount; i += FileInterval)
        {
            
            UnityEngine.Object obj = Resources.Load(ResouresFileName + "/"+ResouresFileName+" (" + i.ToString()+")");
            if (obj)
            {
                Texture2D tex = obj as Texture2D;
                Sprite spr = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);


                if (spr)
                    SpriteFrames.Add(spr);
            }
        }
    }

    void Start()
    {
        
        //if (AutoPlay)
        //{
        //    Play();
        //}
        //else
        //{
        //    IsPlaying = false;
        //}
    }
    void OnEnable()
    {
       
        mCurFrame = 0;
        SetSprite(0);
        if (AutoPlay)
        {
            Play();
        }
        else
        {
            IsPlaying = false;
        }
    }
    public void SetSprite(int idx)
    {
        if (idx<SpriteFrames.Count&&idx>-1)
        {
            ImageSource.sprite = SpriteFrames[idx];
        }
    }

    public void Play(UnityAction action=null)
    {
        ExitEvent = action;
        if (!ImageSource.enabled)
            ImageSource.enabled = true;
        IsPlaying = true;
        Foward = true;
    }

    public void PlayReverse()
    {
        IsPlaying = true;
        Foward = false;
    }

    void FixedUpdate()
    {
        if (!IsPlaying || 0 == FrameCount)
        {
            return;
        }

        mDelta += Time.deltaTime;
        if (mDelta > 1 / FPS)
        {
            mDelta = 0;
            if (Foward)
            {
                mCurFrame++;
            }
            else
            {
                mCurFrame--;
            }

            if (mCurFrame >= FrameCount)
            {
                if (Loop)
                {
                    mCurFrame = 0;
                }
                else
                {
                    if (ExitEvent!=null)
                        ExitEvent.Invoke();
                    
                    IsPlaying = false;
                    return;
                }
            }
            else if (mCurFrame < 0)
            {
                if (Loop)
                {
                    mCurFrame = FrameCount - 1;
                }
                else
                {
                    if (ExitEvent != null)
                        ExitEvent.Invoke();
                    IsPlaying = false;
                    return;
                }
            }

            SetSprite(mCurFrame);
        }
    }

    public void Pause(bool isPause=true)
    {
        IsPlaying = false;
        ImageSource.enabled = isPause; 
    }

    public void Resume()
    {
        if (!IsPlaying)
        {
            IsPlaying = true;
        }
    }

    public void Stop()
    {
        mCurFrame = 0;
        SetSprite(mCurFrame);
        IsPlaying = false;
    }

    public void Rewind()
    {
        mCurFrame = 0;
        SetSprite(mCurFrame);
        Play();
    }
}

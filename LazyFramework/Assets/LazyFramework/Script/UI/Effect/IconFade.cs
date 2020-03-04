using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Lazy
{
    public class IconFade : MonoBehaviour
    {
        [SerializeField] bool isPlay = false;
        [SerializeField] Image image;
        private float max = 0;
        private float min = 0;

        public void Start()
        {
            if (image == null)
            {
                if (GetComponent<Image>() != null)
                {
                    image = GetComponent<Image>();
                }
            }
        }
        /// <summary>
        /// 开始闪烁
        /// </summary>
        /// <param name="max">a最大值</param>
        /// <param name="min">a最小值</param>
        /// <param name="time">闪烁速度</param>
        public void StartLoopFade(float max, float min, float time)
        {
            isPlay = true;
            this.max = max;
            this.min = min;
            StartCoroutine(StartLoopFadeIE(time));
        }
        /// <summary>
        /// 开始闪烁
        /// </summary>
        /// <param name="max">a最大值</param>
        /// <param name="min">a最小值</param>
        /// <param name="time">闪烁速度</param>
        /// <param name="fadetime">连续闪烁时间</param>
        public void StartLoopFade(float max, float min, float time, float fadetime)
        {
            isPlay = true;
            this.max = max;
            this.min = min;
            StartCoroutine(StartLoopFadeTimeIE(time, fadetime));
        }
        /// <summary>
        /// 停止闪烁
        /// </summary>
        public void StopLoopFade()
        {
            isPlay = false;
            image.color = new Color(image.color.r, image.color.g, image.color.b, max);
        }
        IEnumerator StartLoopFadeTimeIE(float time, float fadetime)
        {
            float a = max;
            bool isZ = true;
            float curtime = 0;
            while (isPlay)
            {

                if (isZ)
                {
                    a = Mathf.MoveTowards(a, min, time * Time.deltaTime);
                    if (a == min)
                    {
                        isZ = false;
                    }
                }
                else
                {
                    a = Mathf.MoveTowards(a, max, time * Time.deltaTime);
                    if (a == max)
                    {
                        isZ = true;
                    }
                }
                curtime += Time.deltaTime;
                if (curtime >= fadetime)
                {
                    isPlay = false;
                    a = max;
                }
                image.color = new Color(image.color.r, image.color.g, image.color.b, a);
                yield return new WaitForFixedUpdate();
            }
        }
        IEnumerator StartLoopFadeIE(float time)
        {
            float a = max;
            bool isZ = true;
            while (isPlay)
            {
                if (isZ)
                {
                    a = Mathf.MoveTowards(a, min, time * Time.deltaTime);
                    if (a == min)
                    {
                        isZ = false;
                    }
                }
                else
                {
                    a = Mathf.MoveTowards(a, max, time * Time.deltaTime);
                    if (a == max)
                    {
                        isZ = true;
                    }
                }
                image.color = new Color(image.color.r, image.color.g, image.color.b, a);
                yield return new WaitForFixedUpdate();
            }
        }
    }

}

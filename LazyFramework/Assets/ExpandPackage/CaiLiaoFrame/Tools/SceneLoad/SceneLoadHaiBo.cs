using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SceneLoadHaiBo : MonoBehaviour {

    public GameObject JinDuTiaoUI;
    public Slider loadingSlider;
    public Text loadingText;
    bool isShowJinDuTiao;
    float targetValue;
	void Start () {

        //test
        //LoadScene("HongWaiGuangPuYi", () => Debug.Log("jiazaiwancheng"));
	}
    public void LoadScene(string sceneName, Action action=null)
    {
        StartCoroutine(LoadSceneI(sceneName,action));
    }

    AsyncOperation operation;
    IEnumerator LoadSceneI(string sceneName, Action action)
    {
        JinDuTiaoUI.SetActive(true);
        isShowJinDuTiao = true;
        loadingSlider.value = 0;
        loadingText.text = "";
        operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        yield return operation;
        JinDuTiaoUI.SetActive(false);
        isShowJinDuTiao = false;
        //yield return new WaitForEndOfFrame();
        action();
    }


	void Update () {
        if (isShowJinDuTiao)
        {
            targetValue = operation.progress;

            if (operation.progress >= 0.9f)
            {
                //operation.progress的值最大为0.9
                targetValue = 0.99f;
            }

            if (targetValue != loadingSlider.value)
            {
                //插值运算
                loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime * 1);
                if (Mathf.Abs(loadingSlider.value - targetValue) < 0.01f)
                {
                    loadingSlider.value = targetValue;
                }
            }

            loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";

            if ((int)(loadingSlider.value * 100) >= 99)
            {
                //允许异步加载完毕后自动切换场景
                operation.allowSceneActivation = true;
                Debug.Log("允许异步加载完毕后自动切换场景");
            }
        }
	}
}

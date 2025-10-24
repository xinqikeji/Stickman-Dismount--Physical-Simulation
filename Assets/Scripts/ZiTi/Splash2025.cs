using System.Collections;
using System.Collections.Generic;
using TTSDK;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Splash2025 : MonoBehaviour
{

    public float displayTime = 2.0f; // UI 显示的时间，默认 2 秒
    private Image uiImage; // 假设你的 UI 是 Image 组件，根据实际情况修改

    void Start()
    {
       
        if (DataManager.Instance.GameAdvice!=1)
        {
            this.gameObject.SetActive(false);
        }
        // 获取 UI 对象的 Image 组件，如果 UI 是其他类型（如 Text 等），修改对应组件类型
        uiImage = GetComponent<Image>();
        if (uiImage != null)
        {
            // 确保 UI 初始状态为可见
            uiImage.enabled = true;
            // 启动协程，等待一段时间后隐藏 UI
            StartCoroutine(HideAfterDelay());
        }
        else
        {
            //Debug.LogError("UITimer 脚本需要附加到一个带有 Image 组件的 UI 对象上！");
        }
    }

    private IEnumerator HideAfterDelay()
    {
        // 等待指定时间
        yield return new WaitForSeconds(displayTime);
        // 隐藏 UI
        if (uiImage != null)
        {
            uiImage.enabled = false;
            DataManager.Instance.GameAdvice = 0;
        }
    }
   
}

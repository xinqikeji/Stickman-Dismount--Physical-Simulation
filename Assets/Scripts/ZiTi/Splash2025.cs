using System.Collections;
using System.Collections.Generic;
using TTSDK;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Splash2025 : MonoBehaviour
{

    public float displayTime = 2.0f; // UI ��ʾ��ʱ�䣬Ĭ�� 2 ��
    private Image uiImage; // ������� UI �� Image ���������ʵ������޸�

    void Start()
    {
       
        if (DataManager.Instance.GameAdvice!=1)
        {
            this.gameObject.SetActive(false);
        }
        // ��ȡ UI ����� Image �������� UI ���������ͣ��� Text �ȣ����޸Ķ�Ӧ�������
        uiImage = GetComponent<Image>();
        if (uiImage != null)
        {
            // ȷ�� UI ��ʼ״̬Ϊ�ɼ�
            uiImage.enabled = true;
            // ����Э�̣��ȴ�һ��ʱ������� UI
            StartCoroutine(HideAfterDelay());
        }
        else
        {
            //Debug.LogError("UITimer �ű���Ҫ���ӵ�һ������ Image ����� UI �����ϣ�");
        }
    }

    private IEnumerator HideAfterDelay()
    {
        // �ȴ�ָ��ʱ��
        yield return new WaitForSeconds(displayTime);
        // ���� UI
        if (uiImage != null)
        {
            uiImage.enabled = false;
            DataManager.Instance.GameAdvice = 0;
        }
    }
   
}

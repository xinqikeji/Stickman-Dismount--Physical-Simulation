using System.Collections;
using System.Collections.Generic;
using StarkSDKSpace;
using UnityEngine;
using UnityEngine.UI;
using TTSDK;
using TTSDK.UNBridgeLib.LitJson;

public class SDKTest : MonoBehaviour
{
    //public static SDKTest instance;

    //public Image tip;
   
    //public Button startButton;
    //public Button stopButton;
    //public Button shareBtn;


    //public void OPenpop(string pop)
    //{
    //    tip.gameObject.SetActive(true);
    //    tip.GetComponentInChildren<Text>().text = pop;
       
    //    Invoke("closepop", 1f);
    //}
    //void closepop()
    //{
    //    tip.gameObject.SetActive(false);
    //}

    //[SerializeField] private bool m_IsRecordAudio = true;
    //[SerializeField] private int m_MaxRecordTime = 300; // ���¼��ʱ�����룩

    //private TTGameRecorder m_TTGameRecorder;

    //void Start()
    //{
    //    tip.gameObject.SetActive(false);
    //    //startButton.onClick.AddListener(OnStartButtonTapped);
    //    //stopButton.onClick.AddListener(OnStopButtonTapped);       // ��ʼ��SDK¼�����
    //    m_TTGameRecorder = TT.GetGameRecorder();
    //    //shareBtn.onClick.AddListener(share);
    //    // ��ʼ����ť״̬
    //    //SetUIState(m_TTGameRecorder.GetEnabled());
    //}

    public void share()
    {
        //if(videopath==null)
        //{
        //    OPenpop("¼��·��Ϊ��");return;
        //}
        //if(lupintime<3)
        //{
        //    OPenpop("С��Ϸ¼��ʱ��С�� 3 "); return;
        //}
        JsonData shareJson = new JsonData();
        shareJson["channel"] = "";//video
        shareJson["desc"] = " �����İ�";
        //shareJson["extra"] = new JsonData();
        //shareJson["extra"]["videoPath"] = videopath ; ;//¼������Ļ���·���� OnRecordComplete �õ���·��
        JsonData videoTopics = new JsonData();
        videoTopics.SetJsonType(JsonType.Array);


        //shareJson["extra"]["hashtag_list"] = videoTopics;
        TT.ShareAppMessage(shareJson, (data) =>
        {
            Debug.Log($"ShareAppMessage success: {data}");
            //OPenpop("����ɹ�");
        },
        (errMsg) =>
        {
            Debug.Log($"ShareAppMessage failed: {errMsg}");
            //OPenpop(errMsg);
        },
        () =>
        {
            Debug.Log($"ShareAppMessage cancel");
            //OPenpop("����ȡ��");
        });

    }
    //// ��ʼ¼����ť���
    //public void OnStartButtonTapped()
    //{
    //    if (m_TTGameRecorder.GetVideoRecordState() != TTGameRecorder.VideoRecordState.RECORD_STARTED)
    //    {
    //        m_TTGameRecorder.Start(
    //            m_IsRecordAudio,
    //            m_MaxRecordTime,
    //            OnRecordStart,
    //            OnRecordError,
    //            OnRecordTimeout
    //        );
    //    }
    //    else
    //    {
    //        Debug.LogWarning("¼���Ѿ���ʼ�������ظ����");
    //    }
    //}

    //// ֹͣ¼����ť���
    //public void OnStopButtonTapped()
    //{
    //    m_TTGameRecorder.Stop(OnRecordComplete, OnRecordError);
    //}

    //// ¼����ʼ�ص�
    //private void OnRecordStart()
    //{
    //    OPenpop("¼����ʼ");
    //    SetUIState(false);
    //}
    //public string videopath;
    //// ¼����ɻص�
    //private void OnRecordComplete(string videoPath)
    //{
    //    videopath = videoPath; lupintime = m_TTGameRecorder.GetRecordDuration();
    //    OPenpop($"¼�����: ʱ��: {m_TTGameRecorder.GetRecordDuration() }��");
    //    SetUIState(true);
    //}
    //public int lupintime;
    //// ¼����ʱ�ص�
    //private void OnRecordTimeout(string videoPath)
    //{
    //    videopath = videoPath; lupintime = m_TTGameRecorder.GetRecordDuration();
    //    OPenpop($"¼����ʱ: {videoPath}\n��¼��ʱ��: {m_TTGameRecorder.GetRecordDuration()}��");
    //    SetUIState(true);
    //}

    //// �������ϲ��ظ�������
    //private void OnRecordError(int errCode, string errMsg)
    //{
    //    OPenpop($"¼������: [{errCode}] {errMsg}");
    //    SetUIState(true);
    //}

    //// ͳһ����UI״̬
    //private void SetUIState(bool isRecording)
    //{
    //    // ȷ�������̸߳���UI
        
    //        startButton.gameObject.SetActive(isRecording);
    //        stopButton.gameObject.SetActive(!isRecording);

    //        // ���ְ�ť������
    //        startButton.interactable = isRecording;
    //        stopButton.interactable = !isRecording;
        
    //}
}
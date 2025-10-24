using System;
using System.Collections;
using System.Collections.Generic;
using TTSDK;
using TTSDK.UNBridgeLib.LitJson;
using UnityEngine;
using UnityEngine.UI;

public class cebianlan : MonoBehaviour
{
    public Button guanbibtn;
    public Button opencebianlan;
    public Button linqujiangli;
    public Button showbtn;
    public Text lingqujieshu;

 
    private const string PREFS_KEY_LAST_CLAIMED = "LastClaimedTime";
    public void Awake()
    {
        if (CanClaimReward())
        {
            PlayerPrefs.SetString("IsCebianlan", "True");
        }
        showbtn.onClick.AddListener(() => { this.gameObject.SetActive(true); });

        lingqujieshu.gameObject.SetActive(false);
        guanbibtn.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });
        opencebianlan.onClick.AddListener(() =>
        {
            TTosidebar();
        });

        //��ȡ������ť
        linqujiangli.onClick.AddListener(() =>
        {
            //���������
            CoinManager.coin += 200;
            Debug.Log(CoinManager.coin);
            CoinManager.UpCoin();





            //���ù�
            opencebianlan.gameObject.SetActive(false);
            linqujiangli.gameObject.SetActive(false);
            lingqujieshu.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            this.showbtn.gameObject.SetActive(false);
            // ���������ȡʱ��
            PlayerPrefs.SetString(PREFS_KEY_LAST_CLAIMED, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            PlayerPrefs.SetString("IsCebianlan", "false");
            PlayerPrefs.Save();
            
        });
        TTSidebarinit();
    }
    private bool CanClaimReward()
    {
        // ��ȡ��ǰʱ��
        DateTime now = DateTime.Now;

        // ��ȡ�ϴ���ȡ��ʱ���
        if (!PlayerPrefs.HasKey(PREFS_KEY_LAST_CLAIMED))
        {
            // ���û��ʱ�������ʾ��δ��ȡ������
            return true;
        }

        // ����ʱ���
        DateTime lastClaimed = DateTime.Parse(PlayerPrefs.GetString(PREFS_KEY_LAST_CLAIMED));
        Debug.LogError(now.Day);
        Debug.LogError(lastClaimed.Day);
        // ����ʱ���
        //TimeSpan timeSpan = now - lastClaimed;
        if (now.Day != lastClaimed.Day)
        {
            return true;
        }
        return false;
        // �������24Сʱ�������ٴ���ȡ����
        // return timeSpan.TotalHours >= 24;
    }
    public void TTSidebarinit()
    {
        TT.InitSDK((code, env) =>
        { });

        if (PlayerPrefs.GetString("IsCebianlan") == "True")
        {
            showbtn.gameObject.SetActive(true);
        }
        else
        {
            showbtn.gameObject.SetActive(false);
        }
        if ((PlayerPrefs.GetString("Launch From") == "homepage") && (PlayerPrefs.GetString("location") == "homepage_expand"))
        {
            opencebianlan.gameObject.SetActive(false);

            if (CanClaimReward())
            {
                linqujiangli.gameObject.SetActive(true);
                lingqujieshu.gameObject.SetActive(false);
            }
            else
            {
                lingqujieshu.gameObject.SetActive(true);
                linqujiangli.gameObject.SetActive(false);
            }
        }
        else
        {
            opencebianlan.gameObject.SetActive(true);
            linqujiangli.gameObject.SetActive(false);
        }

    }

    public void TTosidebar()
    {
        var data = new JsonData
        {
            ["scene"] = "sidebar",
            //["activityId"] = "cacheActivityId",
        };
        TT.NavigateToScene(data, () =>
        {
            Debug.Log("navigate to scene success");
        }, () =>
        {
            opencebianlan.gameObject.SetActive(false);


            linqujiangli.gameObject.SetActive(true);

            Debug.Log("navigate to scene complete");
        }, (errCode, errMsg) =>
        {
            Debug.Log($"navigate to scene error, errCode:{errCode}, errMsg:{errMsg}");
        });
    }
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }
}

using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine.UI;
public class ShareScreenShot : MonoBehaviour 
{
	private bool isProcessing = false;
	public static ShareScreenShot instance;
	public string ScreenshotName = "screenshot.png";
	private string shareScore,shareTotalCoins;
	//Public string subject for share
	public string subject = "Stickman Dismouting-Ragdoll Physics";
	string destination;
	private float width, height, offsetX, offsetY;

	#if UNITY_ANDROID
	public string AndroidUrl = "";
	#elif UNITY_IOS
	public string iOSUrl = "";
	#endif

	void Awake()
	{
		if (instance == null)
			instance = this;

	}
	public void ButtonShare()
	{

		GameObject.FindObjectOfType<SoundMusicManager> ().CameraSound ();
		shareScore = "" +PlayerPrefs.GetInt (BadLogic.nameMap + "BESTSCORE");
		shareTotalCoins = "" + PlayerPrefs.GetInt ("COIN");

		#if UNITY_ANDROID
		if(!isProcessing)
			StartCoroutine( ShareScreenshot() );
		#elif UNITY_IOS
		if(!isProcessing)
		{
		    StartCoroutine( ShareScreenshot() );
		}
		#else
		Debug.Log("No sharing set up for this platform.");
		#endif
	}
		#if UNITY_ANDROID
		public IEnumerator ShareScreenshot()
		{

		isProcessing = true;
		// wait for graphics to render
		yield return new WaitForEndOfFrame();

		string screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
		ScreenCapture.CaptureScreenshot(ScreenshotName);
		yield return new WaitForSeconds(1f);
		if(!Application.isEditor)
		{
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
		AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + screenShotPath);
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
		intentObject.Call<AndroidJavaObject>("setType", "image/png");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"),  "LOL!!! ,Break all physical rules with -Stickman Destruction Story- |My Best Score| " + shareScore+" |"+" |My TotalCoins| "+ shareTotalCoins+" |"+ " download " + AndroidUrl);
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "YO! I Love This Game!");
		currentActivity.Call("startActivity", jChooser);
		}


		isProcessing = false;
		}
		#endif

		#if UNITY_IOS
		public IEnumerator ShareScreenshot()
		{
		width = Screen.width * 1.0f;
		height = Screen.height * 1.0f;
		offsetX = Screen.width * 0.0f;
		offsetY = Screen.height * 0.0f;

		isProcessing = true;
		// wait for graphics to render
		yield return new WaitForEndOfFrame();
		//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
		// create the texture
		Texture2D screenTexture = new Texture2D((int)width, (int)height, TextureFormat.RGB24, true);
		// put buffer into texture
		screenTexture.ReadPixels(new Rect(offsetX, offsetY, width, height), 0, 0);
		// apply
		screenTexture.Apply();
		//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
		byte[] dataToSave = screenTexture.EncodeToPNG();
		destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
		File.WriteAllBytes(destination, dataToSave);
		ShareMethode();
		isProcessing = false;
		}
		#endif
		void ShareMethode()
		{
			if (!Application.isEditor)
			{
				#if UNITY_IOS
				CallSocialShareAdvanced("LOL!!! ,Break all physical rules with -Stickman Destruction Story- |My Best Score| " + shareScore+" |"+"|My TotalCoins|"+shareTotalCoins+" |", subject, iOSUrl, destination);
				#endif
				}
		}
		#if UNITY_IOS
		public struct ConfigStruct
		{
		public string title;
		public string message;
		}

		[DllImport ("__Internal")] private static extern void showAlertMessage(ref ConfigStruct conf);

		public struct SocialSharingStruct
		{
		public string text;
		public string url;
		public string image;
		public string subject;
		}

		[DllImport ("__Internal")] private static extern void showSocialSharing(ref SocialSharingStruct conf);

		public static void CallSocialShare(string title, string message)
		{
		ConfigStruct conf = new ConfigStruct();
		conf.title  = title;
		conf.message = message;
		showAlertMessage(ref conf);
		}

		public static void CallSocialShareAdvanced(string defaultTxt, string subject, string url, string img)
		{

		//--------Remove line code Shift+Ctrl+C----------
		//Share Other Imager if you dont want to capture this scene's image
		//-----------------------------
		SocialSharingStruct conf = new SocialSharingStruct();
		conf.text = defaultTxt; 
		conf.url = url;
		conf.image = img;
		conf.subject = subject;

		showSocialSharing(ref conf);
		}
		#endif

	}
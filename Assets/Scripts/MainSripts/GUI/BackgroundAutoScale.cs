using UnityEngine;
using System.Collections;

public class BackgroundAutoScale : MonoBehaviour
{
	public static BackgroundAutoScale instance;
	public bool noneX = false;
	public bool noneY = false;
	//public bool _update = false;

	void Start ()
	{
		StartCoroutine (delay ());
		/*
		Vector2 sprite_size = GetComponent<SpriteRenderer>().sprite.rect.size;
		Vector2 local_sprite_size = sprite_size /  GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
		Vector3 world_size = local_sprite_size;
		world_size.x *= transform.lossyScale.x;
		world_size.y *= transform.lossyScale.y;

		//convert to screen space size
		Vector3 screen_size = 0.5f * world_size / Camera.main.orthographicSize;
		screen_size.y *= Camera.main.aspect;

		//size in pixels
		Vector3 in_pixels = new Vector3(screen_size.x * Camera.main.pixelWidth, screen_size.y * Camera.main.pixelHeight, 0) * 0.5f;

		print (Screen.width);
		transform.localScale = new Vector3 (Camera.main.pixelWidth/in_pixels.x,Camera.main.pixelHeight/in_pixels.y,0);

		Debug.Log(string.Format("World size: {0}, Screen size: {1}, Pixel size: {2}",world_size,screen_size,in_pixels));
	*/
	}

	Vector3 xWidth;
	Vector3 yHeight;
	public void autoScale ()
	{
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		if (sr == null)
			return;
		transform.localScale = new Vector3 (1, 1, 1);
		float width = sr.sprite.bounds.size.x;
		float height = sr.sprite.bounds.size.y;
		float worldScreenHeight = Camera.main.orthographicSize * 2f;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
		if (!noneX) {
			xWidth = transform.localScale;
			xWidth.x = worldScreenWidth / width;
			transform.localScale = xWidth;
		}
		//transform.localScale.x = worldScreenWidth / width;
		if (!noneY) {
			yHeight = transform.localScale;
			yHeight.y = worldScreenHeight / height;
			transform.localScale = yHeight;
		}
		//transform.localScale.y = worldScreenHeight / height;
	}

	IEnumerator delay ()
	{
		yield return new WaitForSecondsRealtime (0.05f);
		autoScale ();
	}

//	void OnEnable ()
//	{
//		autoScale ();
//	}

//	void LateUpdate ()
//	{
//		if (_update) {
//			autoScale ();
//		}
//	}
}

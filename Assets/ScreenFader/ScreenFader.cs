using UnityEngine;

/// <summary> Implements screen fading effect (fade in/out). </summary>
[RequireComponent (typeof(SpriteRenderer))]
public class ScreenFader : MonoBehaviour {

	public float fadeTime = 2f;
	public bool startWithOverlay;

	private SpriteRenderer overlay;
	private float elapsedTime;
	private bool isFadingIn;
	private bool isFadingOut;

	public void RequestFadeOut() {
		if (CanExecuteOperation(0f)) {
			isFadingOut = true;
			RepositionSelfToCamera();
		}
		else
			Debug.LogWarning("Cannot execute fade out at the moment");
	}

	public void RequestFadeIn() {
		if (CanExecuteOperation(1f)) {
			isFadingIn = true;
			RepositionSelfToCamera();
		}
		else
			Debug.LogWarning("Cannot execute fade in at the moment");
	}

	public bool IsIdle() {
		return (!isFadingIn && !isFadingOut);
	}

	private bool CanExecuteOperation(float alpha) {
		return (!isFadingIn && !isFadingOut && overlay.color.a == alpha);
	}

	void Awake() {
		FindOverlay();
		InitOverlay();
		ResizeOverlayToScreenSize();
	}

	void Update() {
		if (isFadingIn)
			Fade(1f - elapsedTime/fadeTime, 0f, ref isFadingIn);
		else if (isFadingOut)
			Fade(elapsedTime/fadeTime, 1f, ref isFadingOut);
	}

	private void RepositionSelfToCamera() {
		transform.position = new Vector3(Camera.main.transform.position.x,
		                                 Camera.main.transform.position.y,
		                                 transform.position.z);
	}

	private void FindOverlay() {
		overlay = GetComponent<SpriteRenderer>();
		if (!overlay)
			Debug.LogError("[ScreenFader]FindOverlay: could not find the sprite renderer component");
		else if (!overlay.sprite)
			Debug.LogError("[ScreenFader]FindOverlay: overlay has no sprite");
	}

	private void InitOverlay() {
		Color newOverlayColor = overlay.color;
		newOverlayColor.a = (startWithOverlay ? 1f : 0f);
		overlay.color = newOverlayColor;
	}

	private void ResizeOverlayToScreenSize() {
		float worldScreenHeight = Camera.main.orthographicSize * 2;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		transform.localScale = new Vector3(worldScreenWidth / overlay.sprite.bounds.size.x,
		                                   worldScreenHeight / overlay.sprite.bounds.size.y, 1);
	}

	private void Fade(float alphaPercentage, float alphaLimit, ref bool fadeFlag) {
		alphaPercentage = Mathf.Clamp01(alphaPercentage);
		overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, alphaPercentage);
		elapsedTime += Time.deltaTime;
		if (overlay.color.a == alphaLimit) {
			fadeFlag = false;
			elapsedTime = 0;
		}
	}
}

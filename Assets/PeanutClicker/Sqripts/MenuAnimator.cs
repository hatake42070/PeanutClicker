using UnityEngine;
using System.Collections;

public class MenuAnimator : MonoBehaviour
{
    [Header("Settings")]
    public float slideSpeed = 1500f;
    public bool slideLeft = true;

    public bool IsOpen { get; private set; } = false;

    private RectTransform rectTransform;
    private Vector2 closePos;
    private Vector2 openPos;
    private Coroutine currentCoroutine;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        closePos = rectTransform.anchoredPosition;

        float width = rectTransform.sizeDelta.x;
        float direction = slideLeft ? -1 : 1;

        openPos = new Vector2(closePos.x + (width * direction), closePos.y);
    }

    // 通常のアニメーション付き移動
    public void SetMenuState(bool open)
    {
        if (IsOpen == open) return;
        IsOpen = open;

        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(SlideCoroutine(open ? openPos : closePos));
    }

    // ★追加: アニメーションなしで瞬時に移動（切り替え用）
    public void JumpToState(bool open)
    {
        IsOpen = open;
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);

        // 即座に座標を書き換える
        rectTransform.anchoredPosition = open ? openPos : closePos;
    }

    IEnumerator SlideCoroutine(Vector2 targetPos)
    {
        while (Vector2.Distance(rectTransform.anchoredPosition, targetPos) > 1f)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(
                rectTransform.anchoredPosition,
                targetPos,
                slideSpeed * Time.deltaTime
            );
            yield return null;
        }
        rectTransform.anchoredPosition = targetPos;
    }
}
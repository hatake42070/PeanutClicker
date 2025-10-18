using UnityEngine;
using System.Collections;

public class MenuAnimator : MonoBehaviour
{
    public RectTransform menuPanel;
    public RectTransform mainButton;
    public float slideSpeed = 1000f;

    [HideInInspector] public Vector2 menuOpenPos;
    [HideInInspector] public Vector2 menuClosePos;
    [HideInInspector] public Vector2 buttonOpenPos;
    [HideInInspector] public Vector2 buttonClosePos;

    private Coroutine panelAnimationCoroutine;
    private Coroutine buttonAnimationCoroutine;

    // パネルだけを開閉させるメソッド
    public void AnimatePanel(bool isOpen)
    {
        // アニメーションが実行中出ないか確認、実行中であれば止める
        if (panelAnimationCoroutine != null)
        {
            StopCoroutine(panelAnimationCoroutine);
        }
        Vector2 targetPos = isOpen ? menuOpenPos : menuClosePos;
        panelAnimationCoroutine = StartCoroutine(Slide(menuPanel, menuPanel.anchoredPosition, targetPos));
    }

    // ボタンだけを開閉させるメソッド
    // true: ボタンを『開いた状態の位置』へアニメーションさせる
    // false: ボタンを『閉じた状態の位置』へアニメーションさせる
    public void AnimateButton(bool isOpen)
    {
        if (buttonAnimationCoroutine != null)
        {
            StopCoroutine(buttonAnimationCoroutine);
        }
        Vector2 targetPos = isOpen ? buttonOpenPos : buttonClosePos;
        buttonAnimationCoroutine = StartCoroutine(Slide(mainButton, mainButton.anchoredPosition, targetPos));
    }

    public void SetPanelState(bool isOpen)
    {
        // 実行中のパネルアニメーションがあれば停止
        if (panelAnimationCoroutine != null)
        {
            StopCoroutine(panelAnimationCoroutine);
        }
        // パネルの位置を直接設定する
        menuPanel.anchoredPosition = isOpen ? menuOpenPos : menuClosePos;
    }

    // RectTransformをスライドさせる汎用コルーチン
    private IEnumerator Slide(RectTransform target, Vector2 from, Vector2 to)
    {
        float t = 0;
        float distance = Vector2.Distance(from, to);

        if (distance <= 0)
        {
            yield break;
        }

        while (t < 1)
        {
            t += Time.deltaTime * slideSpeed / distance;
            target.anchoredPosition = Vector2.Lerp(from, to, t);
            yield return null;
        }
        target.anchoredPosition = to;
    }
}
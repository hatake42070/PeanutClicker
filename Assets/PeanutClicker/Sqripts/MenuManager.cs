using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    [System.Serializable]
    public struct MenuPair
    {
        public string name;
        public Button menuButton;
        public MenuAnimator menuPanel;
    }

    [Header("Menu Setup")]
    public List<MenuPair> menus = new List<MenuPair>();

    [Header("Button Container Animation")]
    public RectTransform buttonContainer;

    // ★ここが重要: ボタンをどれくらい動かすか。
    // パネルの幅が300なら、ここも300くらいにすると丁度よくなります。
    public float containerSlideDistance = 220f;

    public float slideSpeed = 1500f;

    private MenuAnimator currentOpenMenu = null;
    private Vector2 containerDefaultPos;
    private Vector2 containerMovedPos;
    private Coroutine containerCoroutine;

    void Start()
    {
        if (buttonContainer != null)
        {
            containerDefaultPos = buttonContainer.anchoredPosition;
            // ボタンコンテナを左へずらす
            containerMovedPos = new Vector2(containerDefaultPos.x - containerSlideDistance, containerDefaultPos.y);
        }

        foreach (var pair in menus)
        {
            if (pair.menuButton != null && pair.menuPanel != null)
            {
                MenuAnimator targetPanel = pair.menuPanel;
                pair.menuButton.onClick.AddListener(() => OnMenuButtonClicked(targetPanel));
            }
        }
    }

    private void OnMenuButtonClicked(MenuAnimator targetMenu)
    {
        targetMenu.transform.SetAsLastSibling();

        // ケースA: 同じボタンを押した -> 閉じる（アニメーションあり）
        if (currentOpenMenu == targetMenu)
        {
            targetMenu.SetMenuState(false);
            MoveButtonContainer(false); // ボタンも戻す
            currentOpenMenu = null;
        }
        // ケースB: 別のメニューに切り替え -> 瞬時に切り替え（アニメーションなし）
        else if (currentOpenMenu != null)
        {
            // 開いているものを一瞬で閉じて、新しいのを一瞬で開く
            currentOpenMenu.JumpToState(false);
            targetMenu.JumpToState(true);

            currentOpenMenu = targetMenu;
            // ボタンコンテナは「開いた位置」のままキープ（動かさない）
        }
        // ケースC: 新しく開く -> 開く（アニメーションあり）
        else
        {
            targetMenu.SetMenuState(true);
            MoveButtonContainer(true); // ボタンをずらす
            currentOpenMenu = targetMenu;
        }
    }

    private void MoveButtonContainer(bool isOpen)
    {
        if (buttonContainer == null) return;

        if (containerCoroutine != null) StopCoroutine(containerCoroutine);

        Vector2 targetPos = isOpen ? containerMovedPos : containerDefaultPos;
        containerCoroutine = StartCoroutine(SlideContainer(targetPos));
    }

    IEnumerator SlideContainer(Vector2 targetPos)
    {
        while (Vector2.Distance(buttonContainer.anchoredPosition, targetPos) > 1f)
        {
            buttonContainer.anchoredPosition = Vector2.MoveTowards(
                buttonContainer.anchoredPosition,
                targetPos,
                slideSpeed * Time.deltaTime
            );
            yield return null;
        }
        buttonContainer.anchoredPosition = targetPos;
    }
}
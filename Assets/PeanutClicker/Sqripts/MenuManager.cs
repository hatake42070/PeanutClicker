using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // インスペクターで、連動させたい全てのMenuAnimatorを登録する
    public MenuAnimator[] menus;

    // 現在のメニューの開閉状況
    private MenuAnimator currentOpenMenu = null;

    void Start()
    {
        // 登録されている全てのメニューの位置を計算し、初期化する
        foreach (MenuAnimator menu in menus)
        {
            CalculatePositions(menu);
            // anchoredPositionのset機能
            menu.menuPanel.anchoredPosition = menu.menuClosePos;
            menu.mainButton.anchoredPosition = menu.buttonClosePos;
        }
    }

    // メニューとボタンの位置を計算
    private void CalculatePositions(MenuAnimator menu)
    {
        menu.menuClosePos = menu.menuPanel.anchoredPosition;
        menu.buttonClosePos = menu.mainButton.anchoredPosition;
        menu.menuOpenPos = new Vector2(menu.menuClosePos.x - menu.menuPanel.sizeDelta.x, menu.menuClosePos.y);
        menu.buttonOpenPos = new Vector2(menu.buttonClosePos.x - menu.menuPanel.sizeDelta.x, menu.buttonClosePos.y);
    }

    // メニューボタンが押された時に実行され、
    public void ToggleMenu(MenuAnimator targetMenu)
    {
        // ケース1: 何も開いていない状態で、新しいメニューを開く
        if (currentOpenMenu == null)
        {
            // パネルをアニメーションで開く
            targetMenu.AnimatePanel(true);
            // 全てのボタンをアニメーションで開く位置へ
            foreach (MenuAnimator menu in menus)
            {
                menu.AnimateButton(true);
            }
            currentOpenMenu = targetMenu;
        }
        // ケース2: 開いているメニューを再度クリックして閉じる
        else if (currentOpenMenu == targetMenu)
        {
            // パネルをアニメーションで閉じる
            currentOpenMenu.AnimatePanel(false);
            // 全てのボタンをアニメーションで閉じる位置へ
            foreach (MenuAnimator menu in menus)
            {
                menu.AnimateButton(false);
            }
            currentOpenMenu = null;
        }
        // ケース3: あるメニューが開いている状態で、別のメニューに切り替える
        else
        {
            // 現在開いているパネルを【瞬時に】閉じる
            currentOpenMenu.SetPanelState(false);
            // ターゲットのパネルを【瞬時に】開く
            targetMenu.SetPanelState(true);

            // ボタンはすでに開いた位置にあるので、何もしない

            // 現在開いているメニューの情報を更新
            currentOpenMenu = targetMenu;
        }
    }
}
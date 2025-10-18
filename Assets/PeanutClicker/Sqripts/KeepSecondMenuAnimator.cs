using UnityEngine;
using System.Collections;

public class KeepSecondMenuAnimator : MonoBehaviour
{
    // メニューパネル
    public RectTransform menuPanel;
    // メニューを開くボタン
    public RectTransform mainButton;
    // メニュー展開スピード
    private float slideSpeed;
    // メニュー閉じたポジション
    private Vector2 menuClosePos;
    // メニュー開いたポジション
    private Vector2 menuOpenPos;
    // メニューを閉じたときのボタンのポジション
    private Vector2 buttonClosePos;
    // メニューを開いときのボタンのポジション
    private Vector2 buttonOpenPos;

    public bool isSecondMenuOpen = false;

    void Start()
    {
        // メニュースライドスピード
        slideSpeed = 1000f;
        // 初期位置と終了位置を定義
        // ゲーム開始時はメニューを隠しておく
        menuClosePos = menuPanel.anchoredPosition;
        // menuClosePos = menuClosePos = new Vector2(110.0f, 0.0f);
        Debug.Log(menuClosePos);
        buttonClosePos = mainButton.anchoredPosition;
        // buttonClosePos =  new Vector2(-20.0f, -40.0f);
        Debug.Log(buttonClosePos);

        // メニューの幅に合わせて、移動後の位置を計算
        // 右側のメニューの場合、X座標をメニューの幅分だけずらす
        // menuOpenPos = new Vector2(menuClosePos.x + menuPanel.sizeDelta.x, menuClosePos.y);
        menuOpenPos = new Vector2(menuClosePos.x - menuPanel.sizeDelta.x, menuClosePos.y);
        // buttonOpenPos = new Vector2(buttonClosePos.x + menuPanel.sizeDelta.x, buttonClosePos.y);
        buttonOpenPos = new Vector2(buttonClosePos.x - menuPanel.sizeDelta.x, buttonClosePos.y);

        isSecondMenuOpen = false;
    }

    public void OnToggleButtonClicked()
    {

        StopAllCoroutines(); // 既存のアニメーションを停止

        if (isSecondMenuOpen)
        {
            StartCoroutine(SlideMenu(menuOpenPos, menuClosePos, buttonOpenPos, buttonClosePos));
        }
        else
        {
            StartCoroutine(SlideMenu(menuClosePos, menuOpenPos, buttonClosePos, buttonOpenPos));
        }
        isSecondMenuOpen = !isSecondMenuOpen; // ステートを切り替える
    }

    IEnumerator SlideMenu(Vector2 menuFrom, Vector2 menuTo, Vector2 buttonFrom, Vector2 buttonTo)
    {
        float t = 0;
        while (t < 1)
        {
            // フレームに関係なく同じスピードでメニューが開くようにする
            t += Time.deltaTime * slideSpeed / Vector2.Distance(menuFrom, menuTo);
            // Lerp(補完の開始値, 補完の終了値, 補完の進行度(0~1));
            menuPanel.anchoredPosition = Vector2.Lerp(menuFrom, menuTo, t);
            mainButton.anchoredPosition = Vector2.Lerp(buttonFrom, buttonTo, t);
            yield return null;
        }
    }
}
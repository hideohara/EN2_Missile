using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// GameManagerクラス外でusing
using UnityEngine.Assertions;



public class GameManager : MonoBehaviour
{
    // プレハブの設定
    [SerializeField, Header("Prefabs")]
    // 爆発のプレハブ
    private Explosion explosionPrefab_;

    // メインカメラ
    private Camera mainCamera_;

    // Start is called before the first frame update
    private void Start()
    {
        // 「MainCamera」というタグを持つゲームオブジェクトを検索
        GameObject mainCameraObject =
          GameObject.FindGameObjectWithTag("MainCamera");
        // Cameraコンポーネントが取得できることを確認
        bool isGetComponent =
          mainCameraObject.TryGetComponent(out mainCamera_);
        // Cameraコンポーネントが取得できていなければ止める
        Assert.IsTrue(isGetComponent,
          "MainCameraにCameraコンポーネントがありません");
    }


    // Update is called once per frame
    void Update()
    {
        // クリックしたら爆発を生成
        if (Input.GetMouseButtonDown(0)) { GenerateExplosion(); }
    }


    /// <summary>
    /// 爆発の生成
    /// </summary>
    private void GenerateExplosion()
    {
        // クリックしたスクリーン座標の取得し、ワールド座標に変換する
        Vector3 clickPosition =
          mainCamera_.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.z = 0;

        // クリックした座標に爆発を生成
        Explosion explosion = Instantiate(
          explosionPrefab_,
          clickPosition,
          Quaternion.identity
        );
    }

}

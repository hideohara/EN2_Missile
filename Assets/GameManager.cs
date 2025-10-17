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


    // 隕石のプレハブ
    [SerializeField]
    private Meteor meteorPrefab_;

    //隕石の生成関係
    [SerializeField, Header("MeteorSpawner")]
    // 隕石がぶつかる地面
    private BoxCollider2D ground_;
    // 隕石の生成の時間間隔
    [SerializeField]
    private float meteorInterval_ = 1;
    // 隕石の生成までの時間
    private float meteorTimer_;


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
        if (Input.GetMouseButtonDown(0)) 
        { 
            GenerateExplosion(); 
        }

        UpdateMeteorTimer();
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

    /// <summary>
    /// スコアの加算
    /// </summary>
    /// <param name="point">加算するスコア</param>
    public void AddScore(int point) { }

    /// <summary>
    /// ライフを減らす
    /// </summary>
    /// <param name="damage">減らす値</param>
    public void Damage(int point) { }

    /// <summary>
    /// 隕石タイマの更新
    /// </summary>
    private void UpdateMeteorTimer()
    {
        meteorTimer_ -= Time.deltaTime;
        if (meteorTimer_ > 0) { return; }
        meteorTimer_ += meteorInterval_;
        GenerateMeteor();
    }


    /// <summary>
    /// 隕石の生成
    /// </summary>
    private void GenerateMeteor()
    {
        Vector3 spawnPosition = new Vector3(0, 6, 0);
        Meteor meteor =
          Instantiate(meteorPrefab_,
          spawnPosition, Quaternion.identity);
        meteor.Setup(ground_, this, explosionPrefab_);
    }







}

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
    [SerializeField]
// レティクルのプレハブ
private GameObject reticlePrefab_;

[SerializeField]
// ミサイルのプレハブ
private Missile missilePrefab_;

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
    // 隕石の生成位置
    [SerializeField]
    private List<Transform> spawnPositions_;


    // スコア関係
    [SerializeField, Header("ScoreUISettings")]
    // スコア表示用テキスト
    private ScoreText scoreText_;
    // スコア
    private int score_;

    // ライフ関係
    [SerializeField, Header("LifeUISettings")]
    // ライフゲージ
    private LifeBar lifeBar_;
    // 最大体力
    [SerializeField]
    private float maxLife_ = 10;
    // 現在体力
    private float life_;



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

        // 生成位置Listの要素数が1以上あることを確認
        Assert.IsTrue(
          spawnPositions_.Count > 0,
          "spawnPositions_に要素が一つもありません。"
        );
        foreach (Transform t in spawnPositions_)
        {
            // 各要素にNullが含まれていないことを確認
            Assert.IsNotNull(
              t,
              "spawnPositions_にNullが含まれています"
            );
        }

        // 体力の初期化
        ResetLife();
    }


    // Update is called once per frame
    void Update()
    {
        // クリックしたら爆発を生成
        if (Input.GetMouseButtonDown(0)) 
        { 
            GenerateMissile(); 
        }

        UpdateMeteorTimer();
    }


    /// <summary>
    /// ミサイルの生成(追加)
    /// </summary>
    private void GenerateMissile()
    {
        Vector3 clickPosition =
          mainCamera_.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.z = 0;
        GameObject reticle = Instantiate(
          reticlePrefab_, clickPosition, Quaternion.identity);

        Vector3 launchPosiion = new Vector3(0, -3, 0);
        Missile missile = Instantiate(
          missilePrefab_, launchPosiion, Quaternion.identity);
        missile.Setup(reticle);
    }



    /// <summary>
    /// スコアの加算
    /// </summary>
    /// <param name="point">加算するスコア</param>
    public void AddScore(int point)
    {
        score_ += point;
        scoreText_.SetScore(score_);
    }



    /// <summary>
    /// ライフを減らす
    /// </summary>
    /// <param name="point">減らす値</param>
    public void Damage(float point)
    {
        life_ -= point;
        // UIの更新
        UpdateLifeBar();
    }


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
        int max = spawnPositions_.Count;
        int posIndex = Random.Range(0, max);
        Vector3 spawnPosition =
          spawnPositions_[posIndex].position;
        //Vector3 spawnPosition = new Vector3(0, 6, 0);

        Meteor meteor =
          Instantiate(meteorPrefab_,
          spawnPosition, Quaternion.identity);
        meteor.Setup(ground_, this, explosionPrefab_);
    }



    /// <summary>
    /// ライフの初期化
    /// </summary>
    private void ResetLife()
    {
        life_ = maxLife_;
        // UIの更新
        UpdateLifeBar();
    }


    /// <summary>
    /// ライフUIの更新
    /// </summary>
    private void UpdateLifeBar()
    {
        // 最大体力と現在体力の割合で何割かを算出
        float lifeRatio = Mathf.Clamp01(
          life_ / maxLife_
        );
        // 割合をlifeBar_へ伝え、UIに反映してもらう
        lifeBar_.SetGaugeRatio(lifeRatio);
    }





}

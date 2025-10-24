using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ItemBaseを継承
public class ClusterBombItem : ItemBase
{
    [SerializeField]
    private Explosion explosionPrefab_;
    // 取得して爆発状態かどうかを判断する
    bool isGet = false;
    // 爆発し続ける時間
    private float explosionEmmitionTimer_ = 3;
    // 細かな爆発を生成する間隔
    private float explosionInterval_ = 0.2f;
    private float explosionTimer_ = 0.0f;

    public override void Get()
    {
        // Rendererを取得し、無効にする
        //if (TryGetComponent(out renderer_))
        //{
        //    renderer_.enabled = false;
        //}
        // ColliderはItemBaseで取得済みなので、無効にするだけで済む
        collider_.enabled = false;
        // 子オブジェクト(TextMesh)を無効にする
        transform.GetChild(0).gameObject.SetActive(false);
        // 取得されたことを記憶する
        isGet = true;
    }



    protected override void Update()
    {
        // 取得されていなければ通常のUpdate、つまり基底クラスのUpdateを呼ぶ
        if (!isGet)
        {
            // ここでいうbaseは基底クラスのこと。
            // これは基底クラスのUpdateを呼び出している。
            base.Update();
            return;
        }
        // 爆発持続タイマーを減らす
        explosionTimer_ -= Time.deltaTime;
        // 爆発持続タイマーが切れたら消滅
        if (explosionEmmitionTimer_ <= 0) { Destroy(gameObject); }
        // 細かな爆発について計算する
        UpdateClusterExplosion();

    }






    // 小さな爆発を起こす
    private void UpdateClusterExplosion()
    {
        // クラスター爆発のタイマーを減らし、まだであれば早期リターン
        explosionEmmitionTimer_ -= Time.deltaTime;
        if (explosionTimer_ > 0) { return; }
        // 爆発範囲を決めて、ランダムでoffsetを決める
        float randomWidth = 2;
        Vector3 offset = new Vector3(
            Random.Range(-randomWidth, randomWidth),
            Random.Range(-randomWidth, randomWidth),
            0
          );
        // 自身の位置+offsetの位置に爆発を生成。タイマーにインターバル加算。
        Instantiate(
          explosionPrefab_,
          transform.position + offset,
          Quaternion.identity
        );
        explosionTimer_ += explosionInterval_;
    }



}



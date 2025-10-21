using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Missile : MonoBehaviour
{
    // Explosionのプレハブ。MissileはExplosionを生成する。
    [SerializeField]
    private Explosion explosionPrefab_;
    // 移動速度
    [SerializeField]
    private float speed_;
    // 移動量
    private Vector3 velocity_;
    // レティクル保存用
    private GameObject reticle_;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position  += new Vector3(0,0.1f,0);

        // 距離の二乗の計算。距離の比較用なので二乗のまま使う。
        float distanceSqr = Vector3.SqrMagnitude(
          reticle_.transform.position - transform.position
        );
        Vector3 velocityDeltaTime =
          velocity_ * Time.deltaTime;
        float velocityDistanceSqr =
          Vector3.SqrMagnitude(velocityDeltaTime);
        // 充分に遠かったら移動
        if (distanceSqr >= velocityDistanceSqr)
        {
            transform.position += velocityDeltaTime;
            return;
        }
        // 近かったらレティクルと重なって爆破
        transform.position = reticle_.transform.position;
        Explosion();
    }





    public void Setup(GameObject reticle)
    {
        // レティクルの格納
        reticle_ = reticle;
        // 生成位置とレティクルが一致していたら即爆発
        if (
          reticle.transform.position != transform.position
        )
        {
            // 1. 移動量の計算
            SetupVelocity();
            // 2. 向きの計算
            LookAtReticle();
        }
        else
        {
            // 3. 爆発
            Explosion();
        }
    }

    private void SetupVelocity()
    {
        // 座標の差分をとり、ベクトルを算出
        Vector3 direction = (
          reticle_.transform.position - transform.position
        );
        // ベクトルがゼロでないことを。
        Assert.IsTrue(direction != Vector3.zero);
        // ベクトルを正規化
        direction = direction.normalized;
        // ベクトルに移動速度を掛け移動量とする。
        velocity_ = direction * speed_;


        //velocity_ = new Vector3(0, 0.1f, 0);
    }

    private void LookAtReticle()
    {
        // 角度の算出。Atan2はRadで返ってくるので、Degreeに直す。
        float angle = Mathf.Atan2(
          velocity_.y,
          velocity_.x
        ) * Mathf.Rad2Deg;
        // 角度を90度傾ける
        angle += 90;
        // ゲームオブジェクトを回転させる。
        // この時の単位はDegreeなので注意。
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Explosion()
    {
        // 爆発の生成
        Instantiate(
          explosionPrefab_,
          transform.position,
          Quaternion.identity
        );
        // 爆発とともにレティクルも消す
        Destroy(reticle_);
        // 自身も消す。
        Destroy(gameObject);
    }




}

//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

// ItemBaseを継承
public class GiganticBombItem : ItemBase
{
    // 大爆発プレハブを設定
    [SerializeField]
    Explosion giganticExplosionPrefab_;



    // 抽象クラスを実装。自身の位置にgitanticExplosionPrefgabを
    // 生成し、自身は即削除するのみである。
    public override void Get()
    {
        Instantiate(
          giganticExplosionPrefab_,
          transform.position,
          Quaternion.identity
        );
        Destroy(gameObject);
    }

}

//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

// ItemBase���p��
public class GiganticBombItem : ItemBase
{
    // �唚���v���n�u��ݒ�
    [SerializeField]
    Explosion giganticExplosionPrefab_;



    // ���ۃN���X�������B���g�̈ʒu��gitanticExplosionPrefgab��
    // �������A���g�͑��폜����݂̂ł���B
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

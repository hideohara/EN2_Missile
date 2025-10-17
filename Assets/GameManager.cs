using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// GameManager�N���X�O��using
using UnityEngine.Assertions;



public class GameManager : MonoBehaviour
{
    // �v���n�u�̐ݒ�
    [SerializeField, Header("Prefabs")]
    // �����̃v���n�u
    private Explosion explosionPrefab_;

    // ���C���J����
    private Camera mainCamera_;

    // Start is called before the first frame update
    private void Start()
    {
        // �uMainCamera�v�Ƃ����^�O�����Q�[���I�u�W�F�N�g������
        GameObject mainCameraObject =
          GameObject.FindGameObjectWithTag("MainCamera");
        // Camera�R���|�[�l���g���擾�ł��邱�Ƃ��m�F
        bool isGetComponent =
          mainCameraObject.TryGetComponent(out mainCamera_);
        // Camera�R���|�[�l���g���擾�ł��Ă��Ȃ���Ύ~�߂�
        Assert.IsTrue(isGetComponent,
          "MainCamera��Camera�R���|�[�l���g������܂���");
    }


    // Update is called once per frame
    void Update()
    {
        // �N���b�N�����甚���𐶐�
        if (Input.GetMouseButtonDown(0)) { GenerateExplosion(); }
    }


    /// <summary>
    /// �����̐���
    /// </summary>
    private void GenerateExplosion()
    {
        // �N���b�N�����X�N���[�����W�̎擾���A���[���h���W�ɕϊ�����
        Vector3 clickPosition =
          mainCamera_.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.z = 0;

        // �N���b�N�������W�ɔ����𐶐�
        Explosion explosion = Instantiate(
          explosionPrefab_,
          clickPosition,
          Quaternion.identity
        );
    }

}

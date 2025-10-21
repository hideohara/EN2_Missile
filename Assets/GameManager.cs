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
    [SerializeField]
// ���e�B�N���̃v���n�u
private GameObject reticlePrefab_;

[SerializeField]
// �~�T�C���̃v���n�u
private Missile missilePrefab_;

    // ���C���J����
    private Camera mainCamera_;


    // 覐΂̃v���n�u
    [SerializeField]
    private Meteor meteorPrefab_;

    //覐΂̐����֌W
    [SerializeField, Header("MeteorSpawner")]
    // 覐΂��Ԃ���n��
    private BoxCollider2D ground_;
    // 覐΂̐����̎��ԊԊu
    [SerializeField]
    private float meteorInterval_ = 1;
    // 覐΂̐����܂ł̎���
    private float meteorTimer_;
    // 覐΂̐����ʒu
    [SerializeField]
    private List<Transform> spawnPositions_;


    // �X�R�A�֌W
    [SerializeField, Header("ScoreUISettings")]
    // �X�R�A�\���p�e�L�X�g
    private ScoreText scoreText_;
    // �X�R�A
    private int score_;

    // ���C�t�֌W
    [SerializeField, Header("LifeUISettings")]
    // ���C�t�Q�[�W
    private LifeBar lifeBar_;
    // �ő�̗�
    [SerializeField]
    private float maxLife_ = 10;
    // ���ݑ̗�
    private float life_;



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

        // �����ʒuList�̗v�f����1�ȏ゠�邱�Ƃ��m�F
        Assert.IsTrue(
          spawnPositions_.Count > 0,
          "spawnPositions_�ɗv�f���������܂���B"
        );
        foreach (Transform t in spawnPositions_)
        {
            // �e�v�f��Null���܂܂�Ă��Ȃ����Ƃ��m�F
            Assert.IsNotNull(
              t,
              "spawnPositions_��Null���܂܂�Ă��܂�"
            );
        }

        // �̗͂̏�����
        ResetLife();
    }


    // Update is called once per frame
    void Update()
    {
        // �N���b�N�����甚���𐶐�
        if (Input.GetMouseButtonDown(0)) 
        { 
            GenerateMissile(); 
        }

        UpdateMeteorTimer();
    }


    /// <summary>
    /// �~�T�C���̐���(�ǉ�)
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
    /// �X�R�A�̉��Z
    /// </summary>
    /// <param name="point">���Z����X�R�A</param>
    public void AddScore(int point)
    {
        score_ += point;
        scoreText_.SetScore(score_);
    }



    /// <summary>
    /// ���C�t�����炷
    /// </summary>
    /// <param name="point">���炷�l</param>
    public void Damage(float point)
    {
        life_ -= point;
        // UI�̍X�V
        UpdateLifeBar();
    }


    /// <summary>
    /// 覐΃^�C�}�̍X�V
    /// </summary>
    private void UpdateMeteorTimer()
    {
        meteorTimer_ -= Time.deltaTime;
        if (meteorTimer_ > 0) { return; }
        meteorTimer_ += meteorInterval_;
        GenerateMeteor();
    }


    /// <summary>
    /// 覐΂̐���
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
    /// ���C�t�̏�����
    /// </summary>
    private void ResetLife()
    {
        life_ = maxLife_;
        // UI�̍X�V
        UpdateLifeBar();
    }


    /// <summary>
    /// ���C�tUI�̍X�V
    /// </summary>
    private void UpdateLifeBar()
    {
        // �ő�̗͂ƌ��ݑ̗͂̊����ŉ��������Z�o
        float lifeRatio = Mathf.Clamp01(
          life_ / maxLife_
        );
        // ������lifeBar_�֓`���AUI�ɔ��f���Ă��炤
        lifeBar_.SetGaugeRatio(lifeRatio);
    }





}

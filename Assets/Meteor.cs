using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Meteor : MonoBehaviour
{
    /// <summary>
    /// �Œᗎ�����x
    /// </summary>
    [SerializeField] private float fallSpeedMin_ = 1;
    /// <summary>
    /// �ō��������x
    /// </summary>
    [SerializeField] private float fallSpeedMax_ = 3;

    /// <summary>
    /// �����v���n�u�B����������󂯎��
    /// </summary>
    private Explosion explosionPrefab_;
    /// <summary>
    /// �n�ʂ̃R���C�_�[�B����������󂯎��
    /// </summary>
    private BoxCollider2D groundCollider_;
    private Rigidbody2D rb_;
    private GameManager gameManager_;

    // Start is called before the first frame update
    private void Start()
    {
        rb_ = GetComponent<Rigidbody2D>();
        SetupVlocity();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ����������K�v�ȏ��������p��
    /// </summary>
    public void Setup(BoxCollider2D ground, GameManager gameManager, Explosion explosionPrefab)
    {
        gameManager_ = gameManager;
        groundCollider_ = ground;
        explosionPrefab_ = explosionPrefab;
    }


    /// <summary>
    /// �ړ��ʂ̐ݒ�
    /// </summary>
    private void SetupVlocity()
    {
        // �n�ʂ̏㉺���E�̈ʒu���擾
        float left = groundCollider_.bounds.center.x - groundCollider_.bounds.size.x / 2;
        float right = groundCollider_.bounds.center.x + groundCollider_.bounds.size.x / 2;
        float top = groundCollider_.bounds.center.y + groundCollider_.bounds.size.y / 2;
        float bottom = groundCollider_.bounds.center.y - groundCollider_.bounds.size.y / 2;


        float targetX = Mathf.Lerp(left, right, Random.Range(0.0f, 1.0f));

        Vector3 target = new Vector3(targetX, top, 0);
        Vector3 direction = (target - transform.position).normalized;
        float fallSpeed = Random.Range(fallSpeedMin_, fallSpeedMax_);
        rb_.velocity = direction * fallSpeed;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Explosion"))
        { Explosion(); }
        if (collision.gameObject.CompareTag("Ground"))
        { Fall(); }
    }


    /// <summary>
    /// ����
    /// </summary>
    private void Explosion()
    {
        // GameManager��score���Z��ʒm
        gameManager_.AddScore(100);
        // �����𐶐���
        Instantiate(explosionPrefab_, transform.position,
          Quaternion.identity);
        // ���g�����ł�����
        Destroy(gameObject);
    }


    /// <summary>
    /// �n�ʂɗ���
    /// </summary>
    private void Fall()
    {
        // GameManager�Ƀ_���[�W�̒ʒm
        gameManager_.Damage(1);
        // ���g�͏���
        Destroy(gameObject);
    }

}

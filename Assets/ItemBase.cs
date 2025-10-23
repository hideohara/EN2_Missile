using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
abstract public class ItemBase : MonoBehaviour
//public class ItemBase : MonoBehaviour

{

    // �ړ����x�B�h���N���X�ł��g����悤��protected
    [SerializeField]
    protected float speed_ = 3;
    // ��ʃT�C�Y�m�F�p�B�h���N���X�ł��g����悤��Protected
    protected Camera camera_;
    // ���g�̃T�C�Y�m�F�p�B�h���N���X�ł��g����悤��Protected
    protected Collider2D collider_;
    
    // ������
    private void Awake()
    {
        camera_ = Camera.main;
        collider_ = GetComponent<Collider2D>();
    }


    // �X�V�����B�h���N���X�Ŕ�����������悤��virtual������B
    protected virtual void Update()
    {
        // �ړ�����
        transform.Translate(Vector3.right * speed_ * Time.deltaTime);

        // ��ʊO�̊m�F
        // ���[���h���W��̃J�����E�[���J��������Z�o
        float worldScreenRight =
          camera_.orthographicSize * camera_.aspect;
        // �A�C�e���̂����蔻��̃T�C�Y
        float boundsSize = collider_.bounds.size.x;
        // �����蔻��܂ߊ��S�ɉ�ʊO�ɏo�Ă�����Destroy
        if (transform.position.x > worldScreenRight + boundsSize)
        {
            Destroy(gameObject);
        }
    }




    // �Փ˔���

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion")) { Get(); }
    }



    // �A�C�e���擾�̏����̒��ۃ��\�b�h�B�h���N���X�Ŏ������K�{�B
    // ���N���X�ł͎������Ȃ��̂ŁA�Ōオ�u;�v�ł��邱�Ƃɒ��ӁB
    public abstract void Get();


    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}



}

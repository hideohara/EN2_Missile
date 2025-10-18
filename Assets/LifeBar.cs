using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class LifeBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Slider�̊���
    private float ratio_;
    // Slider�R���|�[�l���g
    private Slider slider_;


    private void Awake()
    {
        // Slider�R���|�[�l���g�̎擾
        slider_ = GetComponent<Slider>();
    }

    // Slider�̊�����ݒ�
    public void SetGaugeRatio(float ratio)
    {
        // 0�`1�͈̔͂Ő؂�l�߂�
        ratio_ = Mathf.Clamp01(ratio);
        // UI�ɔ��f
        slider_.value = ratio_;
    }


}

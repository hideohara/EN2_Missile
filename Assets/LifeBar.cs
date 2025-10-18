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

    // Sliderの割合
    private float ratio_;
    // Sliderコンポーネント
    private Slider slider_;


    private void Awake()
    {
        // Sliderコンポーネントの取得
        slider_ = GetComponent<Slider>();
    }

    // Sliderの割合を設定
    public void SetGaugeRatio(float ratio)
    {
        // 0～1の範囲で切り詰める
        ratio_ = Mathf.Clamp01(ratio);
        // UIに反映
        slider_.value = ratio_;
    }


}

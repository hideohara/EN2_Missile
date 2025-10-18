//using System.Collections;
//using System.Collections.Generic;


using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreText : MonoBehaviour
{
    /// <summary>
    /// �\���p�̃X�R�A
    /// </summary>
    private int score_;
    /// <summary>
    /// �e�L�X�g�{��
    /// </summary>
    private TMP_Text scoreText_;

    /// <summary>
    /// �X�^�[�g
    /// </summary>
    private void Start()
    {
        score_ = 0;
        scoreText_ = GetComponent<TMP_Text>();
    }

    /// <summary>
    /// �X�R�A���X�V�ƃe�L�X�g�ւ̓K�p
    /// </summary>
    /// <param name="score">�V�����X�R�A�l</param>
    public void SetScore(int score)
    {
        score_ = score;
        UpdateScoreText();
    }



    /// <summary>
    /// �e�L�X�g�̍X�V
    /// </summary>
    private void UpdateScoreText()
    {
        // ���l��8��0�l��
        scoreText_.text = $"SCORE:{score_:0000000}";
    }


}


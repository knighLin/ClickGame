using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]//確保執行此程式時，會自動添加該元件，但變數還是要自己設定
[RequireComponent(typeof(MeshFader))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(HealthComponent))]
public class EnemyBehavior : MonoBehaviour {
    private MeshFader meshFader;
    private Animator animator;
    private AudioSource audioSource;
    private HealthComponent healthComponent;

    [SerializeField]private AudioClip hurtClip;
    [SerializeField]private AudioClip deadClip;
 
    public bool IsDead
    {
        get
        {
            return healthComponent.Death;
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        meshFader = GetComponent<MeshFader>();
        audioSource = GetComponent<AudioSource>();
        healthComponent = GetComponent<HealthComponent>();
    }

    private void OnEnable()
    {
        StartCoroutine(meshFader.FadeIn());
    }


    [ContextMenu("Test Execute")]
    private void TestExecute()
    {
        StartCoroutine(Execute());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            DoDamage(10);
    }

    #region Public Method
    /// <summary>
    /// 請呼叫我，開始敵人生命
    /// </summary>
    /// <returns></returns>
    public IEnumerator Execute()
    {
        healthComponent.Init(100);
        while(IsDead == false)//當還活著，死了跳出迴圈
        {
            yield return null;
        }
        animator.SetTrigger("die");
        audioSource.clip = deadClip;
        audioSource.Play();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);//當前播放的animator裡的動畫片段的長度
        yield return StartCoroutine(meshFader.FadeOut());//呼叫淡出
    }
    
    
    public void DoDamage(int attack)
    {
        healthComponent.Hurt(attack);
        animator.SetTrigger("hurt");
        audioSource.clip = hurtClip;
        audioSource.Play();
    }
    #endregion
}

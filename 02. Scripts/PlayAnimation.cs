using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAnimation : MonoBehaviour {
    public GameObject m_FireGameObject;
    public GameObject Bullet_Prefab;
    public GameObject UI;
    private Text txtAmmo;
    public Transform FirePos;
    



    public int Magazine;
    public int Ammo;
    public float ShootCoolTime;
    public float Reload_CoolTime;
    private float time;
    private float Reload_time;

    private MeshRenderer m_FireEffect;
    private Animator m_Animator;
	// Use this for initialization
	void Start () {
        m_Animator = gameObject.GetComponent<Animator>();
        m_FireEffect = m_FireGameObject.GetComponent<MeshRenderer>();
        txtAmmo = UI.GetComponent<Text>();
        // Default
        Magazine = 3;
        Ammo = 30;
        ShootCoolTime = 0.3f;
        Reload_CoolTime = 3f;

        time = Time.time;
        Reload_time = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
        txtAmmo.text = string.Format("Ammo : {0} Magazine : {1}", Ammo, Magazine);
        if (Input.GetKeyDown(KeyCode.R) && Ammo < 30 && Reload_time <= Time.time&&Magazine>=1) 
        {
            StartCoroutine("Reload");
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0)&&time <= Time.time &&
        m_Animator.GetBool("Reload")==false)
        {
            if (Ammo > 0)
                StartCoroutine("FireShoot");
            else
                Debug.Log(Ammo);
        }
	}

    IEnumerator FireShoot()
    {
        m_Animator.SetTrigger("M4A1 Attack");
        StartCoroutine("Fire");
        m_FireGameObject.SetActive(true);
        time += ShootCoolTime;
        Debug.Log(Ammo);
        Ammo--;
        yield return new WaitForSeconds(0.2f);
        m_FireGameObject.SetActive(false);
    }
    
    IEnumerator Reload()
    {
        m_Animator.SetBool("Reload", true);
        Magazine--;
        Reload_time += Reload_CoolTime;
        Debug.Log("Reload"+Magazine);
        yield return new WaitForSeconds(3f);
        m_Animator.SetBool("Reload", false);
        Ammo = 30;
    }

    IEnumerator Fire()
    {
        GameObject Bullet = Instantiate(Bullet_Prefab,FirePos) as GameObject;
        Bullet.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 100f), ForceMode.Force);
        yield return null; 
    }
}

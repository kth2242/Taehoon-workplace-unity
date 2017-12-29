using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYER_STATE
{
    IDLE,
    WALK,
    RUN,
    ATTACK,
    DEAD
}

public class Player_Ctrl : MonoBehaviour {

    public PLAYER_STATE PS;

    public Vector3 lookDirection;
    public float speed = 0f;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;

    Animation animation;
    public AnimationClip idleAni;
    public AnimationClip walkAni;
    public AnimationClip runAni;

    public GameObject bullet;
    public Transform shotPoint;
    public GameObject shotFX;
    public AudioClip shotSound;

    void KeyboardInput()
    {
        float xx = Input.GetAxisRaw("Vertical");
        float zz = Input.GetAxisRaw("Horizontal");

        if(Input.GetKey(KeyCode.W))
        {
            speed = walkSpeed;
            PS = PLAYER_STATE.WALK;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
                PS = PLAYER_STATE.RUN;
            }
            transform.Translate(0, 0, speed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey(KeyCode.A))
        {
            speed = walkSpeed;
            PS = PLAYER_STATE.WALK;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
                PS = PLAYER_STATE.RUN;
            }
            transform.Translate(-speed * Time.deltaTime, 0, 0, Space.World);
        }
        if(Input.GetKey(KeyCode.S))
        {
            speed = walkSpeed;
            PS = PLAYER_STATE.WALK;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
                PS = PLAYER_STATE.RUN;
            }
            transform.Translate(0, 0, -speed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey(KeyCode.D))
        {
            speed = walkSpeed;
            PS = PLAYER_STATE.WALK;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
                PS = PLAYER_STATE.RUN;
            }
            transform.Translate(speed * Time.deltaTime, 0, 0, Space.World);
        }        
        
        if (xx == 0 && zz == 0 && PS != PLAYER_STATE.IDLE)
        {
            PS = PLAYER_STATE.IDLE;
            speed = 0f;
        }

        if(Input.GetKeyDown(KeyCode.Space) && PS != PLAYER_STATE.DEAD)
        {
            StartCoroutine("Shot");
        }
    }

    void LookUpdate()
    {
        Quaternion R = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, R, 10f);
    }

    void Start()
    {
        animation = this.GetComponent<Animation>();
    }

    void Update()
    {
        if (PS != PLAYER_STATE.DEAD)
        {
            KeyboardInput();
            LookUpdate();
        }

        AnimationUpdate();
    }

    void AnimationUpdate()
    {
        if(PS == PLAYER_STATE.IDLE)
        {
            animation.CrossFade(idleAni.name, 0.2f);
        }
        else if (PS == PLAYER_STATE.WALK)
        {
            animation.CrossFade(walkAni.name, 0.2f);
        }
        else if (PS == PLAYER_STATE.RUN)
        {
            animation.CrossFade(runAni.name, 0.2f);
        }
        else if (PS == PLAYER_STATE.ATTACK)
        {
            animation.CrossFade(idleAni.name, 0.2f);
        }
        else if (PS == PLAYER_STATE.DEAD)
        {
            animation.CrossFade(idleAni.name, 0.2f);
        }
    }

    public IEnumerator Shot()
    {
        GameObject bulletClone = Instantiate(bullet, shotPoint.position,
                                            Quaternion.LookRotation(shotPoint.forward)) as GameObject;

        Physics.IgnoreCollision(bulletClone.GetComponent<Collider>(), GetComponent<Collider>());

        GetComponent<AudioSource>().clip = shotSound;
        GetComponent<AudioSource>().Play();

        shotFX.SetActive(true);

        PS = PLAYER_STATE.ATTACK;
        speed = 0f;

        yield return new WaitForSeconds(0.15f);
        shotFX.SetActive(false);

        yield return new WaitForSeconds(0.15f);
        PS = PLAYER_STATE.IDLE;
    }
}

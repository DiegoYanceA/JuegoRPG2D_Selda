using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    private static SFXManager sharedInstance = null;

    public static SFXManager SharedInstance
    {
        get
        {
            return sharedInstance;
        }
    }

    private void Awake()
    {
        if(sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }

        sharedInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    public enum SFTType
    {
        ATTACK, DIE, HIT, KNOCK
    }

    public AudioSource attack;
    public AudioSource die;
    public AudioSource hit;
    public AudioSource knock;
    
    public void PlaySFX(SFTType type)
    {
        switch (type)
        {
            case SFTType.ATTACK:
                attack.Play();
                break;
            case SFTType.DIE:
                die.Play();
                break;
            case SFTType.HIT:
                hit.Play();
                break;
            case SFTType.KNOCK:
                knock.Play();
                break;

        }
    }
}

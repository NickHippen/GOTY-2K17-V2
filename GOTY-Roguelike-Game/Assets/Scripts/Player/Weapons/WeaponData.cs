using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour {

	public string baseName;
	public string desc;
	public float damage = 10f;
	public Vector3 rotation;
	public GameObject model;
	public Sprite icon;
	public float cost;

	public WeaponModifier modifier;
	public WeaponEmotion emotion;
    public GameObject Player { get; set; }

    protected ParticleSystem particleEffect;
    protected ParticleSystem.MainModule particleMain;

    SoundData sfx;
	//private AudioSource audioSource;
    protected float damageMultiplier = 1f;

	protected virtual void Start() {
		//audioSource = GetComponent<AudioSource>();
		//effect.transform.parent = transform.parent;
		//effect.transform.position = transform.position;
		sfx = GetComponent<SoundData>();
		Player = GameObject.Find("remy");

        particleEffect = this.GetComponentInChildren<ParticleSystem>(true);
        if (particleEffect != null)
        {
            particleMain = particleEffect.main;
            ApplyWeaponParticleEffect();
        }
    }

	public string FullName {
		get {
			string fullName = "";
			if (modifier != 0) {
				fullName += modifier.ToString() + " ";
			}
			fullName += baseName;
			if (emotion != 0) {
				fullName += " of " + emotion.ToString();
			}
			return fullName;
		}
	}

	public virtual void Attack() {
		PlayAttackAudio(0);
		//if (particleEffect != null) {
		//	//effect.transform.position = transform.position + transform.forward;
		//	particleEffect.Emit(10);
		//}
	}

    public void ApplyDamageMultiplier(float duration, float damageMultiplier)
    {
        this.damageMultiplier = damageMultiplier;
        StartCoroutine(ExtraDamage(duration));
    }

    private IEnumerator ExtraDamage(float duration)
    {
        yield return new WaitForSeconds(duration);
        damageMultiplier = 1f;
    }

    void ApplyWeaponParticleEffect()
    {
        switch(emotion)
        {
            case WeaponEmotion.Sorrow:
                particleMain.startColor = new Color(Color.grey.r, Color.grey.b, Color.grey.g, 0.4588f);
                particleEffect.gameObject.SetActive(true);
                break;
            case WeaponEmotion.Rage:
                particleMain.startColor = new Color(Color.red.r, Color.red.b, Color.red.g, 0.4588f);
                particleEffect.gameObject.SetActive(true);
                break;
            case WeaponEmotion.Inspiration:
                particleMain.startColor = new Color(Color.white.r, Color.white.b, Color.white.g, 0.4588f);
                particleEffect.gameObject.SetActive(true);
                break;
            case WeaponEmotion.Confidence:
                particleMain.startColor = new Color(Color.blue.r, Color.blue.b, Color.blue.g, 0.4588f);
                particleEffect.gameObject.SetActive(true);
                break;
            case WeaponEmotion.Shame:
                particleMain.startColor = new Color(Color.green.r, Color.green.b, Color.green.g, 0.4588f);
                particleEffect.gameObject.SetActive(true);
                break;
            case WeaponEmotion.Anxiety:
                particleMain.startColor = new Color(Color.magenta.r, Color.magenta.b, Color.magenta.g, 0.4588f);
                particleEffect.gameObject.SetActive(true);
                break;
            case WeaponEmotion.Envy:
                particleMain.startColor = new Color(Color.yellow.r, Color.yellow.b, Color.yellow.g, 0.4588f);
                particleEffect.gameObject.SetActive(true);
                break;
            default: break;
        }
    }

    //Index 0 = fired, index 1 = impact
    protected virtual void PlayAttackAudio(int index) {
		if (sfx != null) {
			sfx.playSound(index);
		}
	}
}

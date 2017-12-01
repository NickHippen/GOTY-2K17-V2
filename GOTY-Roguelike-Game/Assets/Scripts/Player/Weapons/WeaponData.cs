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

	public ParticleSystem effect;

	private SoundData sfx;
	//private AudioSource audioSource;
    protected float damageMultiplier = 1f;

	protected virtual void Start() {
		//audioSource = GetComponent<AudioSource>();
		//effect.transform.parent = transform.parent;
		//effect.transform.position = transform.position;
		sfx = GetComponent<SoundData>();
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
		if (effect != null) {
			//effect.transform.position = transform.position + transform.forward;
			effect.Emit(10);
		}
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

	//Index 0 = fired, index 1 = impact
	protected virtual void PlayAttackAudio(int index) {
		if (sfx != null) {
			sfx.playSound(index);
		}
	}
}

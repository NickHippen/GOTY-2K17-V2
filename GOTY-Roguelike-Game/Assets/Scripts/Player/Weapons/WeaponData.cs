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
    public float lightMoveSpeed = 1.3f;
    public float inspirationAtkRate = 1.3f;
    public float compassionCdRate = 1.3f;

    public WeaponModifier modifier;
    public WeaponEmotion emotion;
    public GameObject Player { get; set; }


    protected ParticleSystem particleEffect;
    protected ParticleSystem.MainModule particleMain;
    protected float damageMultiplier = 1f;

    SoundData sfx;
	//private AudioSource audioSource;

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

    protected virtual void OnEnable()
    {
        if (this.tag == "Equipped")
        {
            // enable passive modifier weapon traits
            switch (modifier)
            {
                case WeaponModifier.Light:
                    Player.GetComponent<Animator>().SetFloat("MoveSpeed", lightMoveSpeed);
                    break;
                default: break;
            }
            // enable passive emotion weapon traits
            switch (emotion)
            {
                case WeaponEmotion.Compassion:
                    List<Ability> abilities = Player.GetComponent<AbilityController>().getAbilityList();
                    foreach (Ability ability in abilities)
                    {
                        ability.CooldownMultiplier *= compassionCdRate;
                    }
                    break;
                case WeaponEmotion.Inspiration:
                    Player.GetComponent<Animator>().SetFloat("AttackSpeed", inspirationAtkRate);
                    break;
                default: break;
            }
        }
    }

    protected virtual void OnDisable()
    {
        if (this.tag == "Equipped")
        {
            cost = 0;
            // enable passive modifier weapon traits
            switch (modifier)
            {
                case WeaponModifier.Light:
                    Player.GetComponent<Animator>().SetFloat("MoveSpeed", 1f); // default speed
                    break;
                default: break;
            }
            // disable passive emotion weapon traits
            switch (emotion)
            {
                case WeaponEmotion.Compassion:
                    List<Ability> abilities = Player.GetComponent<AbilityController>().getAbilityList();
                    foreach (Ability ability in abilities)
                    {
                        if (ability.CooldownMultiplier / compassionCdRate > 1.01f)
                        {
                            ability.CooldownMultiplier /= compassionCdRate;
                        }
                        else ability.CooldownMultiplier = 1;
                    }
                    break;
                case WeaponEmotion.Inspiration:
                    Player.GetComponent<Animator>().SetFloat("AttackSpeed", 1f); // default speed
                    break;
                default: break;
            }
        }
    }

    // call this method in subclasse's OnEnable function. Does OnDisable method when weapon is dropped
    protected virtual IEnumerator WaitForDrop()
    {
        yield return new WaitWhile(() => this.tag == "Equipped");
        OnDisable();
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

    public bool RandomChanceHit(float percent)
    {
        float chance;
        if (this is GunData || this is DaggerData)
        {
            chance = percent/3f;
        }
        else
        {
            chance = percent;
        }
        return Random.Range(0f, 1f) < chance;
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
                particleMain.startColor = new Color(Color.blue.r, Color.blue.b, Color.blue.g, 0.4588f);
                particleEffect.gameObject.SetActive(true);
                break;
            case WeaponEmotion.Isolation:
                particleMain.startColor = new Color(Color.gray.r, Color.gray.b, Color.gray.g, 0.4588f);
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
            case WeaponEmotion.Compassion:
                particleMain.startColor = new Color(Color.cyan.r, Color.cyan.b, Color.cyan.g, 0.4588f);
                particleEffect.gameObject.SetActive(true);
                break;
            case WeaponEmotion.Confidence:
                particleMain.startColor = new Color(255f, 165f, 0f, 0.4588f); // orange
                particleEffect.gameObject.SetActive(true);
                break;
            case WeaponEmotion.Shame:
                particleMain.startColor = new Color(Color.green.r, Color.green.b, Color.green.g, 0.4588f);
                particleEffect.gameObject.SetActive(true);
                break;
            case WeaponEmotion.Anxiety:
                particleMain.startColor = new Color(238f, 130f, 238f, 0.4588f); // violet
                particleEffect.gameObject.SetActive(true);
                break;
            case WeaponEmotion.Envy:
                particleMain.startColor = new Color(165f, 42f, 42f, 0.4588f); // brown
                particleEffect.gameObject.SetActive(true);
                break;
            case WeaponEmotion.Elation:
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

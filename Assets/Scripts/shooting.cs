using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class shooting : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;

    //bullet and supershot reference
    public GameObject bullet;
    //transform from where the bullet will be instantiated
    public Transform bulletTransform;

    [SerializeField] private AudioSource bulletShotSoundEffect;

    //supershot prefab and the number of uses
    public GameObject superShot;
    public int superShotUse =10;
    public TextMeshProUGUI superShotText;

    [SerializeField] private AudioSource superShotSoundEffect;

    //machine gun amo, and the number of uses for maching gun ability 
    public int machineGunAmo = 20;
    public int machineGunUses = 10;
    public TextMeshProUGUI machineGunText;

    [SerializeField] private AudioSource machineGunSoundEffect;

    //cooldown duration in seconds
    public float machineGunCooldown = 1.0f; 
    private float machineGunCooldownTimer = 0f;
    private bool isMachineGunOnCooldown = false;

    //flags to check if they can fire
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;



    void Start()
    {
        //update the ablity uses by adding the value if in the boss scene
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "BOSS")
        {
            superShotUse+= mathAbilityManager.div;
            machineGunUses += mathAbilityManager.rand;
        }

        //intitialize camera & the UI texts
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        superShotText.text = superShotUse.ToString();
        machineGunText.text = machineGunUses.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        //aim towards the mouse position
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
       
       
        //allows for a break between shots
        if(!canFire){
            timer += Time.deltaTime;
            if(timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }

        }
        //if left mouse click then normal bullet shot
        if (Input.GetMouseButton(0) && canFire)
        {
            canFire =false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            bulletShotSoundEffect.Play();
        }
       
        if (isMachineGunOnCooldown)
        {
            machineGunCooldownTimer += Time.deltaTime;
            if (machineGunCooldownTimer >= machineGunCooldown)
            {
                isMachineGunOnCooldown = false;
                machineGunCooldownTimer = 0f;
            }
        }

        //if right mouse click then machine gun ability
        //check to see if can fire, updating uses
        if (Input.GetMouseButton(1) && machineGunUses >0 && !isMachineGunOnCooldown)
        {
            if(machineGunAmo>0)
            {
                Instantiate(bullet, bulletTransform.position, Quaternion.identity);
                machineGunAmo--;
                StartCoroutine(PlayMachineGunSound());
                
            }
            else
            {
                machineGunAmo=20;
                machineGunUses--;
                isMachineGunOnCooldown = true;

            }
        }
        //update UI
        machineGunText.text = machineGunUses.ToString();

        //super shot ability if E ket pressed and can fire
        if (Input.GetKeyDown(KeyCode.E) && canFire && superShotUse > 0 )
        {
            canFire =false;
            Instantiate(superShot, bulletTransform.position, Quaternion.identity);
            
            superShotUse--;
            superShotSoundEffect.Play();
        }
        //updating UI text
        superShotText.text = superShotUse.ToString();
    }

    //playing the sound effect in a loop to sound like a machine gun
    IEnumerator PlayMachineGunSound()
    {
        for(int i = 0; i < 7; i++)
        {
            machineGunSoundEffect.Play();
            yield return new WaitForSeconds(.17f);
        }
    }
    
}

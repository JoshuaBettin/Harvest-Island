using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private float health = 100;

    public float Health { get => health; /*set => health = value;*/ }

    private Slider slider;

    [SerializeField]
    private AudioClip hurtSFX, deathSFX;

    [SerializeField]
    private float respawnMinX, respawnMaxX, respawnMinY, respawnMaxY;

    public void Start()
    { slider = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        float distract = -10;
        ChangeHealth(distract);
    }

    /// <summary>
    /// returns true if player health reached 0 or below
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool ChangeHealth(float value)
    {
        health += value;

        PhotonView pv = PhotonView.Get(this);
        int viewID = pv.ViewID;

        // hurt sound if value < 0 
        if (value < 0 && health > 0)
        {
            StartCoroutine(PlayerHurtCoroutine(viewID));
            AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(hurtSFX);
        }
        if (value < 0 && health <= 0)
        {
            AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(deathSFX);
        }

        // hurt animation trigger
        slider.value = health;

        if (health <= 0)
        {
            // deathsound;

            pv.RPC("KillPlayer", RpcTarget.All, viewID);

            ActivateDeathPanel();

            StartCoroutine(RevivePlayerCoroutine(viewID));

            return true;
        }

        return false;
    }

    public IEnumerator PlayerHurtCoroutine(int viewID)
    {
        PhotonView pv = PhotonView.Get(this);
        yield return new WaitForSeconds(0.25f);
        pv.RPC("ChangeColor", RpcTarget.All, viewID, "red");
        yield return new WaitForSeconds(0.2f);
        pv.RPC("ChangeColor", RpcTarget.All, viewID, "white");
    }

    [PunRPC]
    public void ChangeColor(int viewID, string color)
    {
        PhotonView pv = PhotonNetwork.GetPhotonView(viewID);
        GameObject player = pv.gameObject;
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        if (color == "red") spriteRenderer.color = Color.red;
        if (color == "white") spriteRenderer.color = Color.white; 
    }

    [PunRPC]
    public void KillPlayer(int viewID)
    {
        PhotonView pv = PhotonNetwork.GetPhotonView(viewID);
        GameObject player = pv.gameObject;

        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
        sr.enabled = false;
        Collider2D collider = player.GetComponent<Collider2D>();
        collider.enabled = false;
    }

    [PunRPC]
    public void RevivePlayer(int viewID)
    {
        PhotonView pv = PhotonNetwork.GetPhotonView(viewID);
        GameObject player = pv.gameObject;
        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
        sr.enabled = true;
        Collider2D collider = player.GetComponent<Collider2D>();
        collider.enabled = true;
    }

    private void ActivateDeathPanel()
    {
        GameObject deathPanelParent = GameObject.FindGameObjectWithTag("DeathPanelParent");
        RectTransform deathPanel = deathPanelParent.GetComponentInChildren<RectTransform>(true);
        deathPanel.gameObject.SetActive(true);
    }

    private void DeactivateDeathPanel()
    {
        GameObject deathPanelParent = GameObject.FindGameObjectWithTag("DeathPanelParent");
        RectTransform deathPanel = deathPanelParent.GetComponentInChildren<RectTransform>(true);
        deathPanel.gameObject.SetActive(false);
    }

    private IEnumerator RevivePlayerCoroutine(int viewID)
    {
        TMP_Text counter = GameObject.FindGameObjectWithTag("CounterText").GetComponent<TMP_Text>();

        for (int i = 10; i > 0 ; i--)
        {
            counter.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        PhotonView pv = PhotonView.Get(this);
        pv.RPC("RevivePlayer", RpcTarget.All, viewID);

        pv.gameObject.transform.position = new Vector3(UnityEngine.Random.Range(respawnMinX, respawnMaxX), UnityEngine.Random.Range(respawnMinY, respawnMaxY), 0);
        ChangeHealth(100);
        DeactivateDeathPanel();
    }
}
    
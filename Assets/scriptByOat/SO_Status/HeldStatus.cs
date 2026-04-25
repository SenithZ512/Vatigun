using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class HeldStatus : MonoBehaviour,IElement
{
    public SO_Status status;
    public float _health;
    public float _armor ; 
    public float _speed ;
    private IOndeath ondeath;
    public AudioSource auido;
    public AudioClip clip;
    public IStatusSource _statusSource { get; private set; }
    public bool isPlayer = false;
    private void Start()
    {
        auido = GetComponent<AudioSource>();
        auido.spatialBlend = 0.63f;
        if (isPlayer)
            _statusSource = new PlayerUpgradeAdapter(status);
        else
            _statusSource = new SO_StatusAdapter(status);
        _health = _statusSource.GetHealth();
        _armor = _statusSource.GetArmor();
        _speed = status.speed;
        GameEvent.UpdatePLayerStatus?.Invoke();
        ondeath = GetComponent<IOndeath>();
    }
    private void Update()
    {
        if (_health <= 0)
        {
            if(ondeath != null) ondeath.OnDeath();

        }
    }

    public void Accept(IVisitor visitor)
    {
       visitor.Visit(this);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalusManager : SingletonBehaviour<MalusManager> {

    Malus[] _malus = new Malus[2];

    public void Give<T>(byte player) where T : Malus, new()
    {
        if (_malus[player] != null)
            _malus[player].End();
        Malus malus = new T();
        malus.player = player;
        malus.Start();
        _malus[player] = malus;
    }

    public void RemoveMalus(byte player)
    {
        if (_malus[player] != null)
        {
            Malus malus = _malus[player];
            _malus[player] = null;
            malus.End();
        }
    }

	// Use this for initialization
	void Start () {
        _malus = new Malus[Enum.GetNames(typeof(Food.Player)).Length];
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Malus malus in _malus)
            if(malus != null)
                malus.Update();
	}
}

public abstract class Malus
{
    private bool _removed = false;
    public byte player;

    public abstract void Start();
    public abstract void Update();
    protected abstract void OnEnd();

    public void End()
    {
        if (!_removed)
        {
            _removed = true;
            OnEnd();
            MalusManager.Instance.RemoveMalus(player);
        }
    }
}

public class AccelerateFallMalus : Malus
{
    public override void Start()
    {
        GameManager.Instance.PlayerStats[player].FallSpeed = 2f;
    }

    public override void Update() { }

    protected override void OnEnd()
    {
        GameManager.Instance.PlayerStats[player].FallSpeed = 1f;
    }
}

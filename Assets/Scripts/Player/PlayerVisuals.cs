using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerVisuals : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Player _player;

    private readonly int IsRunning = Animator.StringToHash("IsRunning");
    private readonly int IsJumping = Animator.StringToHash("IsJumping");

    private void Reset()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _player.PlayerMoves += OnPlayerMoves;
        _player.PlayerIdles += OnPlayerIdles;
        _player.PlayerJumps += OnPlayerJumps;
        _player.PlayerLands += OnPlayerLands;
    }

    private void OnPlayerLands()
    {
        Debug.Log("test");
    }

    private void OnPlayerJumps()
    {
        _animator.SetBool(IsJumping, true);
    }

    private void OnPlayerIdles()
    {
        _animator.SetBool(IsRunning, false);
    }

    private void OnPlayerMoves()
    {
        _animator.SetBool(IsRunning, true);
    }

    private void OnDisable()
    {
        _player.PlayerMoves -= OnPlayerMoves;
        _player.PlayerIdles -= OnPlayerIdles;
    }
}

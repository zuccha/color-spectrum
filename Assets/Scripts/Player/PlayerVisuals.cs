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
    private readonly int IsInAir = Animator.StringToHash("IsInAir");

    private void Reset()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _player.PlayerMoves += OnPlayerMoves;
        _player.PlayerIdles += OnPlayerIdles;
    }

    private void OnPlayerIdles()
    {
        _animator.SetBool(IsRunning, false);
    }

    private void OnPlayerMoves()
    {
        _animator.SetBool(IsRunning, true);
    }

    private void Update()
    {
        _animator.SetBool(IsInAir, !_player.IsGrounded);
    }

    private void OnDisable()
    {
        _player.PlayerMoves -= OnPlayerMoves;
        _player.PlayerIdles -= OnPlayerIdles;
    }
}

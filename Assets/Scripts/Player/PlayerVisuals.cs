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
    [SerializeField]
    private ParticleSystem _dustParticles;

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
        _player.PlayerChangedDirection += OnPlayerChangedDirection;
        _player.PlayerJumps += OnPlayerJumps;
    }

    private void OnPlayerJumps()
    {
        _dustParticles.Play();
    }

    private void OnPlayerChangedDirection()
    {
        _dustParticles.Play();
    }

    private void OnPlayerIdles()
    {
        _animator.SetBool(IsRunning, false);
    }

    private void OnPlayerMoves()
    {
        _animator.SetBool(IsRunning, true);
    }

    public void EmitDustParticles()
    {
        _dustParticles.Play();
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

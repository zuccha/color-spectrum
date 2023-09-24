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

    [Header("Audio")]
    [SerializeField]
    private AudioClip _footstepClip;
    [SerializeField]
    private AudioClip _jumpClip;

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
        PlayJumpClip();
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

    public void PlayFootstepClip()
    {
        AudioManager.Instance.PlayEffect(_footstepClip, 0.4f);
    }

    public void PlayJumpClip()
    {
        AudioManager.Instance.PlayEffect(_jumpClip, 0.4f);
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

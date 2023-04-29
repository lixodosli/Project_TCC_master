using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance { get; private set; }

    #region Events
    public delegate void SceneTransitionCallback();
    public SceneTransitionCallback OnSceneTransitionStart;
    public SceneTransitionCallback OnSceneTransitionEnd;
    #endregion

    #region Configs
    [SerializeField] private float m_AnimationTime = 0.3f;
    [SerializeField] private GameObject m_Transition;
    [SerializeField] private Transform m_EndPoint;
    [SerializeField] private Transform m_StartPoint;
    private Vector3 _TargetPosition;
    private Vector3 _StartPosition;
    private float _CompletePercent;
    private float _Count;
    private bool _IsMoving;
    private int _MovingState = 0;
    private bool _IsPaused = true;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (!_IsMoving)
            return;

        if (!_IsPaused)
        {
            _Count += Time.deltaTime;
            _CompletePercent = _Count / m_AnimationTime;

            m_Transition.transform.position = Vector3.Lerp(_StartPosition, _TargetPosition, _CompletePercent);
        }

        if(_Count >= m_AnimationTime)
        {
            if(_MovingState == 1)
            {
                _Count = 0;
                _IsPaused = true;
                OnSceneTransitionStart?.Invoke();
                StartCoroutine(DoTheFadeOut());
            }
            else if (_MovingState == 2)
            {
                StopMovement();
            }
        }
    }

    private void StopMovement()
    {
        _MovingState = 0;
        m_Transition.transform.position = m_StartPoint.position;
        _IsMoving = false;
        _IsPaused = true;
    }

    private IEnumerator DoTheFadeOut()
    {
        bool allFunctionsExecuted = false;
        while (!allFunctionsExecuted)
        {
            yield return null;
            // Check if any functions are still subscribed to the event
            if (OnSceneTransitionStart == null)
            {
                allFunctionsExecuted = true;
            }
        }

        Invoke(nameof(StartFadeOut), 0.75f);
    }

    public void StartFadeIn()
    {
        _StartPosition = m_StartPoint.position;
        _TargetPosition = m_EndPoint.position;
        _Count = 0;
        _MovingState = 1;
        _IsMoving = true;
        _IsPaused = false;
        GameStateManager.Game.RaiseChangeGameState(GameState.Cutscene);
    }
    
    public void StartFadeOut()
    {
        _StartPosition = m_EndPoint.position;
        _TargetPosition = m_StartPoint.position;
        _Count = 0;
        _MovingState = 2;
        _IsMoving = true;
        _IsPaused = false;
        OnSceneTransitionEnd?.Invoke();
        GameStateManager.Game.RaiseChangeGameState(GameState.World_Free);
    }
}
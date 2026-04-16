using UnityEngine;

namespace ARPaws.Core
{
    /// <summary>
    /// Lightweight behavior driver: reacts to mood and interactions,
    /// picks a target point, and moves the pet with simple steering.
    /// </summary>
    public class PetCompanionBrain : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] PetNeedsController needs;
        [SerializeField] Animator animator;

        [Header("Movement")]
        [SerializeField] float roamRadius = 1.6f;
        [SerializeField] float moveSpeed = 0.45f;
        [SerializeField] float rotationLerp = 8f;
        [SerializeField] float minArriveDistance = 0.06f;

        [Header("Decision Timing")]
        [SerializeField] float minDecisionDelay = 2f;
        [SerializeField] float maxDecisionDelay = 5f;

        [Header("Optional spots")]
        [SerializeField] PetDesignatedSpot bedSpot;
        [SerializeField] PetDesignatedSpot foodSpot;
        [SerializeField] PetDesignatedSpot playSpot;

        [Header("Animator parameters (optional)")]
        [SerializeField] string movingParam = "IsMoving";
        [SerializeField] string happyTrigger = "Happy";
        [SerializeField] string eatTrigger = "Eat";
        [SerializeField] string playTrigger = "Play";
        [SerializeField] string cleanTrigger = "Clean";
        [SerializeField] string sleepTrigger = "Sleep";

        Vector3 _homePosition;
        Vector3 _targetPosition;
        bool _hasTarget;
        float _nextDecisionTime;

        void Awake()
        {
            _homePosition = transform.position;
            _targetPosition = _homePosition;
        }

        void OnEnable()
        {
            if (needs != null)
            {
                needs.OnMoodChanged += OnMoodChanged;
                needs.OnInteraction += OnInteraction;
            }
        }

        void OnDisable()
        {
            if (needs != null)
            {
                needs.OnMoodChanged -= OnMoodChanged;
                needs.OnInteraction -= OnInteraction;
            }
        }

        void Update()
        {
            if (needs == null)
                return;

            if (Time.unscaledTime >= _nextDecisionTime)
            {
                PickNextTarget(needs.CurrentMood);
                _nextDecisionTime = Time.unscaledTime + Random.Range(minDecisionDelay, maxDecisionDelay);
            }

            TickMovement(Time.deltaTime);
        }

        void PickNextTarget(PetMood mood)
        {
            switch (mood)
            {
                case PetMood.Grumpy:
                    if (bedSpot != null)
                    {
                        SetTarget(bedSpot.GetPoint());
                        TriggerIfSet(sleepTrigger);
                        return;
                    }
                    break;

                case PetMood.Playful:
                    if (playSpot != null)
                    {
                        SetTarget(playSpot.GetPoint());
                        TriggerIfSet(playTrigger);
                        return;
                    }
                    break;
            }

            // Default fallback: roam around home within a bounded radius.
            var offset = Random.insideUnitCircle * roamRadius;
            SetTarget(new Vector3(_homePosition.x + offset.x, transform.position.y, _homePosition.z + offset.y));
        }

        void SetTarget(Vector3 target)
        {
            _targetPosition = target;
            _hasTarget = true;
        }

        void TickMovement(float dt)
        {
            if (!_hasTarget)
            {
                SetMoving(false);
                return;
            }

            var toTarget = _targetPosition - transform.position;
            toTarget.y = 0f;

            var dist = toTarget.magnitude;
            if (dist <= minArriveDistance)
            {
                _hasTarget = false;
                SetMoving(false);
                return;
            }

            var dir = toTarget / Mathf.Max(dist, 0.0001f);
            transform.position += dir * (moveSpeed * dt);

            if (dir.sqrMagnitude > 0.0001f)
            {
                var targetRot = Quaternion.LookRotation(dir, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationLerp * dt);
            }

            SetMoving(true);
        }

        void SetMoving(bool moving)
        {
            if (animator != null && !string.IsNullOrEmpty(movingParam))
                animator.SetBool(movingParam, moving);
        }

        void OnMoodChanged(PetMood mood)
        {
            PickNextTarget(mood);
            if (mood == PetMood.Content || mood == PetMood.Playful)
                TriggerIfSet(happyTrigger);
        }

        void OnInteraction(PetInteractionType interaction)
        {
            if (interaction == PetInteractionType.Feed)
            {
                if (foodSpot != null)
                    SetTarget(foodSpot.GetPoint());
                TriggerIfSet(eatTrigger);
            }
            else if (interaction == PetInteractionType.Clean)
            {
                TriggerIfSet(cleanTrigger);
            }
            else if (interaction == PetInteractionType.Pet)
            {
                TriggerIfSet(happyTrigger);
            }
            else if (interaction == PetInteractionType.Play)
            {
                if (playSpot != null)
                    SetTarget(playSpot.GetPoint());
                TriggerIfSet(playTrigger);
            }
        }

        void TriggerIfSet(string triggerName)
        {
            if (animator == null || string.IsNullOrEmpty(triggerName))
                return;
            animator.SetTrigger(triggerName);
        }
    }
}

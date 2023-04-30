using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("VARIABLES")]
    public float acceleration = 50f;
    public float maxSpeed = 12f;

    [Space]
    public float spinSpeed = 10f;

    [Space]
    public float collisionPenalty_MinSpeed = 3f;
    public float collisionPenalty_Duration = 0.5f;

    [Space]
    [HideInInspector] public Goal currentGoal = null;
    public List<Goal> goals = new List<Goal>();


    [Space] [Header("REFERENCES")]
    public Rigidbody2D rb;
    public TrailRenderer[] tyreTrails;
    public DashboardUI dashboardUI;

    [Space]
    public GameObject thrownPackage_Prefab;
    public LayerMask thrownPackage_LayerMask;

    [Space]
    public Transform steeringWheel;
    public float steeringWheel_MaxRotation;

    [Space]
    public Transform speedNeedle;
    public float speedNeedle_MaxRotation;

    [Space]
    public GameObject toDestroy_Navigation;
    public GameObject toDestroy_Dashboard;
    public ParticleSystem winParticles;
    public GameObject winText;

    bool isAccelerating = false;
    Vector2 input_Intention = Vector2.zero;
    Vector2 input_Direction = Vector2.zero;

    bool isActionMode = false;

    bool isDisabled = false;

    Coroutine collisionPenaltyThread = null;
    Coroutine actionThread = null;

    private void Update()
    {
        if (isDisabled) return;

        GetInput();
        UpdateUI();
    }

    private void GetInput()
    {
        // Get inputs.
        GetInput_ActionMode();
        GetInput_Movement();


        // Action mode input.
        void GetInput_ActionMode()
        {
            // Check whether to enter action mode.
            if (!isActionMode && Input.GetMouseButtonUp(1)) EnterActionMode();

            // If not action mode, return.
            if (!isActionMode) return;

            // If there is an active action thread, return.
            if (actionThread != null) return;


            // Throw Y/N?
            if (Input.GetMouseButtonDown(1)) ThrowPackage();


            // Cancel back to driving mode Y/N?
            if (Input.GetMouseButtonDown(0)) ExitActionMode(true);
        }


        // Movement input.
        void GetInput_Movement()
        {
            // Cancel if in action mode.
            if (isActionMode) return;


            // Accelerate Y/N?
            if (Input.GetMouseButton(0)) isAccelerating = true;
            else isAccelerating = false;

            // Turn in direction towards cursor.
            input_Direction = transform.up;
            if (isAccelerating)
            {
                input_Intention = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                input_Direction = (input_Intention - (Vector2)transform.position).normalized;
                input_Direction = (input_Direction + (Vector2)transform.up * 5).normalized;
            }
        }
    }


    private void ThrowPackage()
    {
        if (actionThread != null) StopCoroutine(actionThread);
        actionThread = StartCoroutine(ThrowPackage());

        IEnumerator ThrowPackage()
        {
            // Hold package for as long as click is held.
            dashboardUI.Anim_PreThrowPackage();

            float holdTimer = 0f;
            while (Input.GetMouseButton(1))
            {
                holdTimer += Time.unscaledDeltaTime;
                yield return null;
            }

            // If package was not held for long enough, cancel throw.
            if (holdTimer < 0.15f)
            {
                dashboardUI.Anim_CancelThrowPackage();

                actionThread = null;

                yield break;
            }

            // Otherwise, toss away!
            dashboardUI.Anim_ThrowPackage();
            yield return new WaitForSecondsRealtime(0.2f);

            yield return ThrowPackage_Check();

            //yield return new WaitForSecondsRealtime(0.25f);

            // Reset action thread reference.
            actionThread = null;
        }


        IEnumerator ThrowPackage_Check()
        {
            // Check what the package hit.
            Vector2 targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            List<RaycastHit2D> potentialHits = new List<RaycastHit2D>(Physics2D.RaycastAll(transform.position, (targetPoint - (Vector2)transform.position).normalized, 20f, thrownPackage_LayerMask));

            Vector2 hitPoint = targetPoint;
            Goal hitGoal = null;
            for (int i = 0; i < potentialHits.Count; i++)
            {
                if (potentialHits[i].collider.tag == "NoPenalty" || potentialHits[i].collider.transform == transform)
                {
                    potentialHits.RemoveAt(i);
                    i--;
                    continue;
                }

                hitPoint = potentialHits[i].point;
                if (potentialHits[i].collider.tag == "Goal")
                {
                    hitGoal = potentialHits[i].collider.gameObject.GetComponent<Goal>();
                    if (!hitGoal.active) hitGoal = null;
                }
                break;
            }


            // If package hit a goal...
            if (hitGoal != null)
            {
                hitGoal.Score();
                if (!hitGoal.active) NextGoal();

                Statics.SFX.PlaySound(SoundEffects.scorePackage);
            }

            // Spawn thrown package!
            ThrownPackage newThrownPackage = Instantiate(thrownPackage_Prefab).GetComponent<ThrownPackage>();
            newThrownPackage.ThrowPackage(transform.position, hitPoint);

            yield break;
        }
    }


    public void NextGoal()
    {
        if (currentGoal != null) goals.RemoveAt(0);

        if (goals.Count <= 0)
        {
            Win();
            return;
        }

        currentGoal = goals[0];
        currentGoal.Activate();
    }


    private void Win()
    {
        StartCoroutine(Win());

        IEnumerator Win()
        {
            isDisabled = true;

            Time.timeScale = 0.1f;

            Statics.CameraController.zoomController.ApplyZoom(0.5f, 1f, Curves.Curve.Smooth);

            Statics.VFX.FlashScreen(0.5f, 0.5f, 1.5f, new Color(0.8f, 0.8f, 0.8f));

            yield return new WaitForSecondsRealtime(0.5f);

            winParticles.Play();

            toDestroy_Navigation.SetActive(false);
            toDestroy_Dashboard.SetActive(false);

            winText.SetActive(true);

            yield return new WaitForSecondsRealtime(7f);

            Statics.VFX.FlashScreen(2f, 2f, 2f, Color.black);

            yield return new WaitForSecondsRealtime(2f);

            if (Statics.tutorialMode)
            {
                Statics.tutorialMode = false;
                SceneManager.LoadScene("Main");
            }
            else SceneManager.LoadScene("Finish");
        }
    }



    private void UpdateUI()
    {
        UpdateUI_SteeringWheel();
        UpdateUI_SpeedNeedle();


        void UpdateUI_SteeringWheel()
        {
            float steeringAngle = 0f;
            if (collisionPenaltyThread == null) steeringAngle = Statics.Maths.GetAngleFromVectorDirection(rb.velocity) - Statics.Maths.GetAngleFromVectorDirection(input_Direction);

            Quaternion steeringWheel_DesiredRotation = Quaternion.Euler(0f, 0f, steeringAngle);

            steeringWheel.rotation = Quaternion.Lerp(steeringWheel.rotation, steeringWheel_DesiredRotation, Time.deltaTime * 10f);
        }

        void UpdateUI_SpeedNeedle()
        {
            Quaternion steeringWheel_DesiredRotation = Quaternion.Euler(0f, 0f, Mathf.LerpUnclamped(0, speedNeedle_MaxRotation, rb.velocity.magnitude / maxSpeed));

            speedNeedle.rotation = Quaternion.Lerp(speedNeedle.rotation, steeringWheel_DesiredRotation, Time.deltaTime * 10f);
        }
    }


    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        // Let jesus take the wheel if in collision penalty state c:
        if (collisionPenaltyThread != null) return;


        // Add acceleration if input is there.
        if (isAccelerating)
        {
            rb.AddForce(input_Direction * acceleration * rb.mass);
            if (rb.velocity.magnitude > maxSpeed) rb.velocity = rb.velocity.normalized * maxSpeed;
        }


        // Rotate to face point of intent.
        Quaternion currentRotation = transform.rotation;
        Quaternion desiredRotation = Quaternion.Euler(0, 0, (180f / Mathf.PI * Mathf.Atan2(input_Intention.y - transform.position.y, input_Intention.x - transform.position.x)) - 90f);

        transform.rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * spinSpeed * rb.velocity.magnitude / maxSpeed);
    }

    private void EnterActionMode()
    {
        isActionMode = true;

        Time.fixedDeltaTime = 0.005f;
        Time.timeScale = 0.1f;

        isAccelerating = true;

        dashboardUI.Anim_HoldPackage();
    }
    private void ExitActionMode(bool wasCancelled = false)
    {
        isActionMode = false;

        Time.fixedDeltaTime = 0.02f;
        Time.timeScale = 1.0f;

        if (wasCancelled) dashboardUI.Anim_HidePackage();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collision feedback.
        Statics.VFX.screenShake_Magnitude = Mathf.Lerp(0f, 0.3f, rb.velocity.magnitude / maxSpeed);
        Statics.VFX.screenShake_Time = 0.25f;


        // Check if collision should cause penalty.
        if (collision.transform.tag == "NoPenalty") return;


        // Check if car is going fast enough to cause collision penalty.
        if (rb.velocity.magnitude >= collisionPenalty_MinSpeed)
        {
            if (collisionPenaltyThread != null) StopCoroutine(collisionPenaltyThread);
            collisionPenaltyThread = StartCoroutine(CollisionPenalty());
        }


        // Collision penalty enumerator.
        IEnumerator CollisionPenalty()
        {
            // Feedback.
            rb.angularVelocity = 0f;
            rb.AddTorque(3f * (Random.value < 0.5f ? 1f : -1f) * (0.5f + (rb.velocity.magnitude / maxSpeed / 2f)) * rb.mass, ForceMode2D.Impulse);

            foreach (TrailRenderer tyreTrail in tyreTrails) tyreTrail.emitting = false;


            // Wait and cancel penalty.
            yield return new WaitForSeconds(collisionPenalty_Duration);
            collisionPenaltyThread = null;


            // Return to normal.
            foreach (TrailRenderer tyreTrail in tyreTrails) tyreTrail.emitting = true;
        }
    }
}

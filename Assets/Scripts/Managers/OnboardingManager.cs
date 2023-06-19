using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;


public class OnboardingManager : MonoBehaviour
{
    [SerializeField] private GameObject grid;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject highlightPrefab;
    [SerializeField] private GameObject switchButton;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mannequin;
    [SerializeField] private GameObject loadingWeaponBar;
    [SerializeField] private GameObject powerUpObject;
    [SerializeField] private GameObject portal;
    [SerializeField] private TextMeshProUGUI instructionsText;
    [SerializeField] private GameObject animaeCount;
    [SerializeField] private MorningStar morningStar;
    [SerializeField] private Laser laser;
    [SerializeField] private PlayerAttackController playerAttackController;
    private List<OnboardingStep> steps = new List<OnboardingStep>();
    private int currentStep = 0;
    private bool mannequinHasBeenHit = false;
    private bool laserHasHit = false;
    private bool hasOpenedPowerUp = false;
    private bool hasTapped = false;
    private bool hasStarted = false;
    private GameObject arrow;
    private float scaleDifference = 260f; // the highlighted circle has a bigger scale than the objects on screen
    private GameObject instantiatedHighlightPrefab;
    void OnEnable()
    {

        // disable tilemap and objects
        grid.SetActive(false);
        mannequin.SetActive(false);
        portal.SetActive(false);
        animaeCount.SetActive(false);
        powerUpObject.SetActive(false);

        // make instructions appear
        panel.SetActive(true);

        // wait a moment to load all the references and load the first step
        StartCoroutine(InitOnboarding());
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            hasTapped = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            hasTapped = false;
        }

#else
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
    
                if (touch.phase == TouchPhase.Began) {
                    hasTapped = true;
                }
                // else {
                //     isTouchingScreen = (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved);
                // }
            } else {
                hasTapped = false;
            }
#endif

        if (morningStar != null && morningStar.hasHit)
        {
            mannequinHasBeenHit = true;
        }

        if (currentStep == 4 && player.GetComponentInChildren<Laser>() != null && player.GetComponentInChildren<Laser>().hasHit)
        {
            // laserHasHit = true;
            Invoke("SetLaserHasHit", 0.5f);
            powerUpObject.SetActive(true);
        }
        else if (currentStep == 5 && CanvasManager.Instance.flag)
        {
            //GameManager.Instance.playChurchOst();
            hasOpenedPowerUp = true;
        }

        if (currentStep < steps.Count)
        {
            if (hasStarted && steps[currentStep].targetArrow != null)
            {
                arrow.transform.rotation = Quaternion.LookRotation(Vector3.forward, steps[currentStep].targetArrow.transform.position - arrow.transform.position);
            }
        }

        CheckStepIsCompleted();

    }

    void SetLaserHasHit()
    {
        laserHasHit = true;
    }

    IEnumerator InitOnboarding()
    {
        yield return new WaitForSecondsRealtime(0.5f); // wait a moment to load all the references

        player = GameManager.Instance.player.gameObject;
        arrow = player.GetComponent<Player>().portalArrow;
        arrow.SetActive(false);
        morningStar = player.GetComponent<PlayerAttackController>().morningStar.GetComponentInChildren<MorningStar>();
        playerAttackController = player.GetComponent<PlayerAttackController>();

        LoadSteps();

        LoadCurrentStep();

        hasStarted = true;
    }

    // this function load the instructions text for the current step, highlights the needed object and set the arrow
    private void LoadCurrentStep()
    {
        instructionsText.text = steps[currentStep].instructions;

        if (steps[currentStep].objectToHighlight != null)
        {
            steps[currentStep].objectToHighlight.SetActive(true);

            InstantiateHighlightedCircle(steps[currentStep].objectToHighlight.transform);
        }

        if (steps[currentStep].targetArrow != null)
        {
            steps[currentStep].targetArrow.SetActive(true);
            arrow.SetActive(true);
        }
        else
        {
            arrow.SetActive(false);
        }
    }

    private void CheckStepIsCompleted()
    {
        // activate next step
        if (currentStep < steps.Count && steps[currentStep].completionCondition())
        {
            NextStep();
        }
    }

    private void NextStep()
    {
        currentStep++;

        if (currentStep < steps.Count)
        {
            if (instantiatedHighlightPrefab != null) Destroy(instantiatedHighlightPrefab);

            LoadCurrentStep();
        }
        else
        {
            GameManager.Instance.playChurchOst();
            portal.SetActive(true);
            grid.SetActive(true);
            animaeCount.SetActive(true);
            GameManager.Instance.turnLightsOn();
            PlayerPrefs.SetInt("isFirstLaunch", 0); // create the key and set the value as 0 (false)
        }
    }

    private void InstantiateHighlightedCircle(Transform objectToHighlight)
    {
        instantiatedHighlightPrefab = Instantiate(highlightPrefab, objectToHighlight.transform.position, Quaternion.identity, panel.transform);
        instantiatedHighlightPrefab.transform.localScale = objectToHighlight.localScale * scaleDifference;
    }

    // this function adds all the onboarding steps to the step List
    private void LoadSteps()
    {
        steps.Add(new OnboardingStep(
            "This is Mikael! Touch anywhere on the screen and slide your finger to make him move",
            null,
            null,
            () => hasTapped) // tap to continue
        );

        steps.Add(new OnboardingStep(
            "Move in circles to use the morning star. Look around for the mannequin to train",
            null,
            mannequin,
            () => mannequinHasBeenHit) // hit the mannequin to continue
        );

        steps.Add(new OnboardingStep(
            "Click to switch to the ranged weapon",
            switchButton,
            null,
            () => playerAttackController.hasRanged) // click the switch button to continue
        );

        steps.Add(new OnboardingStep(
            "Stand still to allow the weapon to load",
            loadingWeaponBar,
            null,
            () => hasTapped) // tap to continue
        );

        steps.Add(new OnboardingStep(
            "When it's loaded, start moving to hit the closest enemy",
            null,
            mannequin,
            () => laserHasHit) // hit enemy with the ranged weapon to continue
        );

        steps.Add(new OnboardingStep(
            "Now that you know how to fight, look for the statue to upgrade your skills",
            null,
            powerUpObject,
            () => hasOpenedPowerUp) // touch altar to continue
        );

        steps.Add(new OnboardingStep(
        "You are ready to fight! Get to the gate and take on this dangerous journey!",
            null,
            portal,
            () => hasTapped) // tap to continue
        );
    }

    
}
public class OnboardingStep
{
    public string instructions; // text to show up
    public GameObject objectToHighlight; // object that will be highlighted, can be null
    public GameObject targetArrow; // object that the arrow will point to, can be null
    public System.Func<bool> completionCondition; // check the status of the step

    public OnboardingStep(string instructions, GameObject objectToHighlight, GameObject targetArrow, System.Func<bool> completionCondition)
    {
        this.instructions = instructions;
        this.objectToHighlight = objectToHighlight;
        this.targetArrow = targetArrow;
        this.completionCondition = completionCondition;
    }
}


// public class OnboardingManager : MonoBehaviour
// {

//     // this coroutine allows the player to test the latest instruction before going to the next step
//     private IEnumerator DelayNextStep()
//     {
//         if (!stepIsActive) {
//             yield return new WaitUntil(() => steps[currentStep].completionCondition());
//             yield return new WaitForSeconds(1f); // wait 2 seconds
//             if(steps[currentStep].completionCondition())    NextStep();
//         }
//     }

//     private void InstantiateHighlightedCircle(Transform objectToHighlight) {
//         instantiatedHighlightPrefab = Instantiate(highlightPrefab, objectToHighlight.transform.position, Quaternion.identity, transform);
//         instantiatedHighlightPrefab.transform.localScale = objectToHighlight.localScale * scaleDifference;
//     }

//     // this function load the instructions text for the current step and highlights the needed object
//     private void LoadCurrentStep() {
//         instructionsText.text = steps[currentStep].instructions;

//         if (steps[currentStep].objectToHighlight != null) {
//             InstantiateHighlightedCircle(steps[currentStep].objectToHighlight.transform);
//         }
//     }

//     private void CheckStepIsCompleted() {
//         // activate next step
//         if (stepIsActive && currentStep < steps.Count && steps[currentStep].completionCondition()) {
//             stepIsActive = false;
//             StartCoroutine(DelayNextStep());
//         }
//     }

//     // this functions increments the step index and checks if there are steps left
//     private void NextStep() {
//         if (isTouchingScreen) {
//             isTouchingScreen = false;
//             return;
//         }

//         currentStep++;
//         if (currentStep < steps.Count) {
//             panel.SetActive(true);
//             stepIsActive = false;

//             if (instantiatedHighlightPrefab != null)    Destroy(instantiatedHighlightPrefab);

//             LoadCurrentStep();
//         } else {
//             Debug.Log("finished onboarding");
//             player.GetComponent<Player>().portalArrow.SetActive(true);
//             PlayerPrefs.SetInt("isFirstLaunch", 0); // create the key and set the value as 0 (false)
//         }
//     }

//     // this function adds all the onboarding steps to the step List
//     private void LoadSteps() {
//         steps.Add(new OnboardingStep(
//             "This is Mikael! Touch anywhere on the screen and slide your finger to make him move",
//             player,
//             () => true) // tap to continue
//         );

//         steps.Add(new OnboardingStep(
//             "Move in circles to use the morning star. Look around for the mannequin to train",
//             null,
//             () => mannequinHasBeenHit) // hit the mannequin to continue
//         );

//         steps.Add(new OnboardingStep(
//             "Click to switch to the ranged weapon",
//             switchButton,
//             () => playerAttackController.hasRanged) // click the switch button to continue
//         );

//         steps.Add(new OnboardingStep(
//             "Stand still to allow the weapon to load",
//             loadingWeaponBar,
//             () => true) // tap to continue
//         );

//         steps.Add(new OnboardingStep(
//             "When it's loaded, start moving to hit the closest enemy",
//             mannequin,
//             () => laserHasHit) // hit enemy with the ranged weapon to continue
//         );

//         steps.Add(new OnboardingStep(
//             "Now that you know how to fight, look for the statue on the right to upgrade your skills",
//             null,
//             () => hasOpenedPowerUp) // touch altar to continue
//         );

//         steps.Add(new OnboardingStep(
//         "You are ready to fight! Get to the gate and take on this dangerous journey!",
//             null,
//             () => true) // tap to continue
//         );
//     }

// }


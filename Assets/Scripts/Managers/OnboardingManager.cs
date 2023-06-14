using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnboardingManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject highlightPrefab;
    [SerializeField] private GameObject switchButton;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mannequin;
    [SerializeField] private GameObject loadingWeaponBar;
    [SerializeField] private GameObject powerUpObject;
    [SerializeField] private MorningStar morningStar;
    [SerializeField] private Laser laser;
    [SerializeField] private PlayerAttackController playerAttackController;
    [SerializeField] private TextMeshProUGUI instructionsText;
    private float scaleDifference = 260f; // the highlighted circle has a bigger scale than the objects on screen
    private GameObject instantiatedHighlightPrefab;
    private int currentStep = 0;
    private bool stepIsActive = false; // check if the user is doing something related to a step
    private List <OnboardingStep> steps = new List<OnboardingStep>();
    private bool isTouchingScreen = false;
    private bool mannequinHasBeenHit = false;
    private bool laserHasHit = false;
    private bool hasOpenedPowerUp = false;

    void Start()
    {
        // wait a moment to load all the references and load the first step
        StartCoroutine(InitOnboarding());
    }

    private void Update() {
        #if UNITY_EDITOR
            if (Input.GetMouseButtonUp(0)) {
                panel.SetActive(false);
                stepIsActive = true;
                isTouchingScreen = false;
            } else {
                isTouchingScreen = Input.GetMouseButton(0);
            }
        
        #else
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
    
                if (touch.phase == TouchPhase.Ended) {
                    panel.SetActive(false);
                    stepIsActive = true;
                    isTouchingScreen = false;
                } else {
                    isTouchingScreen = (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved);
                }
            } else {
                isTouchingScreen = false;
            }

        #endif

        if (morningStar != null && morningStar.hasHit) {
            mannequinHasBeenHit = true;
        }

        if (currentStep == 4 && player.GetComponentInChildren<Laser>() != null && player.GetComponentInChildren<Laser>().hasHit) {
            laserHasHit = true;
        } else if (currentStep == 5 && powerUpObject.GetComponent<ShowPowerUp>().toShow.activeInHierarchy) {
            hasOpenedPowerUp = true;
        }

        CheckStepIsCompleted();

    }

    IEnumerator InitOnboarding()
    {
        yield return new WaitForSeconds(0.1f); // wait a moment to load all the references

        player = GameManager.Instance.player.gameObject;
        player.GetComponent<Player>().portalArrow.SetActive(false);
        morningStar = player.GetComponent<PlayerAttackController>().morningStar.GetComponentInChildren<MorningStar>();
        playerAttackController = player.GetComponent<PlayerAttackController>();

        LoadSteps();

        panel.SetActive(true);

        LoadCurrentStep();
    }

    // this coroutine allows the player to test the latest instruction before going to the next step
    private IEnumerator DelayNextStep()
    {
        if (!stepIsActive) {
            yield return new WaitUntil(() => steps[currentStep].completionCondition());
            yield return new WaitForSeconds(1f); // wait 2 seconds
            if(steps[currentStep].completionCondition())    NextStep();
        }
    }

    private void InstantiateHighlightedCircle(Transform objectToHighlight) {
        instantiatedHighlightPrefab = Instantiate(highlightPrefab, objectToHighlight.transform.position, Quaternion.identity, transform);
        instantiatedHighlightPrefab.transform.localScale = objectToHighlight.localScale * scaleDifference;
    }

    // this function load the instructions text for the current step and highlights the needed object
    private void LoadCurrentStep() {
        instructionsText.text = steps[currentStep].instructions;

        if (steps[currentStep].objectToHighlight != null) {
            InstantiateHighlightedCircle(steps[currentStep].objectToHighlight.transform);
        }
    }

    private void CheckStepIsCompleted() {
        // activate next step
        if (stepIsActive && currentStep < steps.Count && steps[currentStep].completionCondition()) {
            stepIsActive = false;
            StartCoroutine(DelayNextStep());
        }
    }

    // this functions increments the step index and checks if there are steps left
    private void NextStep() {
        if (isTouchingScreen) {
            isTouchingScreen = false;
            return;
        }

        currentStep++;
        if (currentStep < steps.Count) {
            panel.SetActive(true);
            stepIsActive = false;

            if (instantiatedHighlightPrefab != null)    Destroy(instantiatedHighlightPrefab);

            LoadCurrentStep();
        } else {
            Debug.Log("finished onboarding");
            player.GetComponent<Player>().portalArrow.SetActive(true);
            PlayerPrefs.SetInt("isFirstLaunch", 0); // create the key and set the value as 0 (false)
        }
    }

    // this function adds all the onboarding steps to the step List
    private void LoadSteps() {
        steps.Add(new OnboardingStep(
            "This is Mikael! Touch anywhere on the screen and slide your finger to make him move",
            player,
            () => true) // tap to continue
        );
        
        steps.Add(new OnboardingStep(
            "Move in circles to use the morning star. Look around for the mannequin to train",
            null,
            () => mannequinHasBeenHit) // hit the mannequin to continue
        );

        steps.Add(new OnboardingStep(
            "Click to switch to the ranged weapon",
            switchButton,
            () => playerAttackController.hasRanged) // click the switch button to continue
        );

        steps.Add(new OnboardingStep(
            "Stand still to allow the weapon to load",
            loadingWeaponBar,
            () => true) // tap to continue
        );

        steps.Add(new OnboardingStep(
            "When it's loaded, start moving to hit the closest enemy",
            mannequin,
            () => laserHasHit) // hit enemy with the ranged weapon to continue
        );

        steps.Add(new OnboardingStep(
            "Now that you know how to fight, look for the statue on the right to upgrade your skills",
            null,
            () => hasOpenedPowerUp) // touch altar to continue
        );

        steps.Add(new OnboardingStep(
        "You are ready to fight! Get to the gate and take on this dangerous journey!",
            null,
            () => true) // tap to continue
        );
    }

}

public class OnboardingStep {
    public string instructions; // text to show up
    public GameObject objectToHighlight; // object that will be highlighted, can be null
    public System.Func<bool> completionCondition; // check the status of the step

    public OnboardingStep(string instructions, GameObject objectToHighlight, System.Func<bool> completionCondition) {
        this.instructions = instructions;
        this.objectToHighlight = objectToHighlight;
        this.completionCondition = completionCondition;
    }
}
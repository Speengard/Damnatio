using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class OnboardingManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject highlightPrefab;
    [SerializeField] private GameObject switchButton;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mannequin;
    [SerializeField] private GameObject loadingWeaponBar;
    [SerializeField] private MorningStar morningStar;
    [SerializeField] private TextMeshProUGUI instructionsText;
    private float scaleDifference = 260f; // the highlighted circle has a bigger scale than the objects on screen
    private GameObject instantiatedHighlightPrefab;
    private int currentStep = 0;
    [SerializeField] private bool stepIsActive = false; // check if the user is doing something related to a step
    private List <OnboardingStep> steps = new List<OnboardingStep>();


    void Start()
    {
        StartCoroutine(ExecuteAfterDelay());

        LoadSteps();

        LoadCurrentStep();
    }

    private void Update() {
        #if UNITY_EDITOR
            if (Input.GetMouseButtonUp(0)) {
                panel.SetActive(false);
                stepIsActive = true;
            }
        
        #else
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Ended) {
                    Debug.Log("sollevato ditino");
                    panel.SetActive(false);
                    stepIsActive = true;
                }
            }  

        #endif

        // activate next step
        if (stepIsActive && currentStep < steps.Count && steps[currentStep].completionCondition()) {
            Debug.Log("OK MANDATO");
            StartCoroutine(DelayNextStep());
        }

    }

    IEnumerator ExecuteAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);

        player = GameManager.Instance.player.gameObject;
        morningStar = player.GetComponent<PlayerAttackController>().morningStar.GetComponentInChildren<MorningStar>();

        InstantiateHighlightedCircle(player.transform);

    }

    // this coroutine allows the player to test the latest instruction before going to the next step
    private IEnumerator DelayNextStep()
    {
        yield return new WaitForSeconds(2f); // wait 2 seconds
        NextStep();
    }

    private void InstantiateHighlightedCircle(Transform objectToHighlight) {
        instantiatedHighlightPrefab = Instantiate(highlightPrefab, objectToHighlight.transform.position, Quaternion.identity, transform);
        instantiatedHighlightPrefab.transform.localScale = objectToHighlight.localScale * scaleDifference;
    }

    // this function load the instructions text for the current step and highlights the needed object
    private void LoadCurrentStep() {
        instructionsText.text = steps[currentStep].instructions;
    }

    // this functions increments the step index and checks if there are steps left
    private void NextStep() {
        panel.SetActive(true);
        stepIsActive = false;

        if (instantiatedHighlightPrefab != null)    Destroy(instantiatedHighlightPrefab);

        currentStep++;
        if (currentStep < steps.Count) {
            LoadCurrentStep();
        } else {
            Debug.Log("finished onboarding");
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
            "Hit enemies with the morning star by moving in circles. Look around for the mannequin to train",
            null,
            () => morningStar.hasHit) // hit the mannequin to continue
        );

        steps.Add(new OnboardingStep(
            "Click on the button to switch to the ranged weapon",
            switchButton,
            () => false) // click the switch button to continue
        );

        steps.Add(new OnboardingStep(
            "Stand still to allow the weapon to load",
            loadingWeaponBar,
            () => false) // tap to continue
        );

        steps.Add(new OnboardingStep(
            "When the weapon is loaded, it will automatically hit the closest enemy",
            mannequin,
            () => false) // hit enemy with the ranged weapon to continue
        );

        steps.Add(new OnboardingStep(
            "Now that you know how to fight the demons, look for the altar to upgrade your skills",
            null,
            () => false) // touch altar to continue
        );

        steps.Add(new OnboardingStep(
        "You are ready to fight! Get to the gate and take on this dangerous journey!",
            null,
            () => false) // tap to continue
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
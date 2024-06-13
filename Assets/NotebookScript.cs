using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotebookScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject textObj;
    private List<string> progressReports = new List<string>();
    private TextMeshProUGUI myText;
    private Coroutine CheckForRotation;
    [SerializeField] private int currentPageNum = 0;
    private bool FuelDoorHint = false;
    private bool GeigarHint = false;
    private bool HelmetHallucination = false;
    private bool SkyboxGlitch = false;
    private bool SystemButton = false;
    private bool HelmetAvoidance = false;
    private bool PoisonGasHint = false;
    private bool DayGlitch = false;


    void Start()
    {
        myText = textObj.GetComponent<TextMeshProUGUI>();
        progressReports.Add(" ");
    }


    public void TurnBack()
    {
        if (currentPageNum > 0)
        {
            myText.text = progressReports[currentPageNum - 1];
            currentPageNum -= 1;
            AudioManager.Instance.Play("sfx_turnpage", transform, 1.0f, false);
        }
    }

    public void TurnForward()
    {
        if(currentPageNum < progressReports.Count - 1)
        {
            myText.text = progressReports[currentPageNum + 1];
            currentPageNum += 1;
            AudioManager.Instance.Play("sfx_turnpage", transform, 1.0f, false);
        }
    }

    public void WriteInNotebook()
    {
        string additionalNotes = "Day " + GameStateManager.Instance.dayCount + ": ";
        bool nothingToReport = true;
        if (!DayGlitch && GameStateManager.Instance.HelmetDayGlitch)
        {
            DayGlitch = true;
            nothingToReport = false;
            additionalNotes += "My helmet's day counter bugged out to some insane number\n";
        }
        if(!SystemButton && GameStateManager.Instance.ClickedSystemButton)
        {
            SystemButton = true;
            nothingToReport = false;
            additionalNotes += "there's an extra button on the console which lets me see a few key ship systems and disable them, not sure why'd i want to turn any of those off though\n";
        }
        if (!FuelDoorHint && GameStateManager.Instance.FuelCellDoorHint)
        {
            FuelDoorHint = true;
            nothingToReport = false;
            additionalNotes += "I think the safety door for the fuel cell storage room got jostled open by the impacts, looks like theres no damage to the cells luckily\n";
        }
        if (!GeigarHint && GameStateManager.Instance.GeigarCounterDiscovered)
        {
            GeigarHint = true;
            nothingToReport = false;
            additionalNotes += "Found a radiation detector in the potted plant, it reads low radiation even in the front of the ship...\n";
        }
        if(!SkyboxGlitch && GameStateManager.Instance.SkyboxGlitchSeen)
        {
            SkyboxGlitch = true;
            nothingToReport = false;
            additionalNotes += "power of the ship cut out when I checked the console today, saw some weird flashing outside the ship, not really sure what to make of it\n";
        }
        if(!PoisonGasHint && HelmetUIManager.Instance.PoisonDiscovered)
        {
            PoisonGasHint = true;
            nothingToReport = false;
            additionalNotes += "I pulled a loose panel off of the ship and found a tank of poison gas connected to some ventilation pipes, why would that even be there???\n";
        }
        if(GameStateManager.Instance.HelmetReassurances)
        {
            if (HelmetAvoidance)
            {
                nothingToReport = false;
                additionalNotes += "my helmet keeps telling me weird things...\n";
            }
            else
            {
                nothingToReport = false;
                additionalNotes += "my helmet ui displayed a weird message when i put it on... should i ignore it?\n";
            }
            HelmetAvoidance = true;
        }
        if (GameStateManager.Instance.SeenHallucination)
        {
            if (HelmetHallucination)
            {
                nothingToReport = false;
                additionalNotes += "i don't trust this helmet anymore\n";
            }
            else
            {
                nothingToReport = false;
                additionalNotes += "i have to be seeing things now, i saw a bunch of words when i put on my helmet today, what do they mean??\n";
            }
            HelmetHallucination = true;
        }
        if((GeigarHint || FuelDoorHint) && PoisonGasHint && HelmetHallucination && HelmetAvoidance)
        {
            nothingToReport = false;
            additionalNotes += "something isn't adding up, they told me there was high radiation in the cockpit, but there doesn't seem to be any. So then if there's no radiation, why do they make me wear this strange helmet? And what's up with the poison gas?";
        }

        if (nothingToReport)
        {
            additionalNotes += "nothing to report";
        }
        progressReports.Add(additionalNotes);
        myText.text = additionalNotes;
        currentPageNum = GameStateManager.Instance.dayCount;

    }

    private IEnumerator RotatePages()
    {
        yield return null;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

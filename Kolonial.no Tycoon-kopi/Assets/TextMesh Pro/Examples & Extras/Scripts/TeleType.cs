using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{
    
    public class TeleType : MonoBehaviour
    {


        //[Range(0, 100)]
        //public int RevealSpeed = 50;



        private TMP_Text body;


        void Awake()
        {
            // Get Reference to TextMeshPro Component
            body = GetComponent<TMP_Text>();
            body.enableWordWrapping = true;
            body.alignment = TextAlignmentOptions.Top;

        }

        private void OnEnable()
        {
            StartTeletype();
        }

        public void StartTeletype ()
        {
            StartCoroutine (Teletype());
        }


        IEnumerator Teletype()
        {

            // Force and update of the mesh to get valid information.
       


            int totalVisibleCharacters = body.textInfo.characterCount; // Get # of Visible Character in text object
            int counter = 0;
            int visibleCount = 0;

            while (true)
            {
                visibleCount = counter % (totalVisibleCharacters + 1);
                     Debug.Log(visibleCount);
                body.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

                // Once the last character has been revealed, wait 1.0 second and start over.
                if (visibleCount >= totalVisibleCharacters)
                {
                    yield return new WaitForSeconds(1.0f);
                }

                counter += 1;

                yield return new WaitForSeconds(0.05f);
            }

            //Debug.Log("Done revealing the text.");
        }

    }
}
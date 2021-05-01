using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitScript : MonoBehaviour
{
    private int currentDigit = 8;
    private int previousDigit = 8;
    public int CurrentDigit
    {
        get { return currentDigit;}
        set
        {
            previousDigit = currentDigit;
            currentDigit = value;
            updateSegments();
        }
}

    public Material raisedMaterial;
    public Material loweredMaterial;

    private bool[] zeroPattern = new[] {true, true, true, true, true, true, false};
    private readonly Vector3 raisedPosition = Vector3.zero;
    private readonly Vector3 loweredPosition = new Vector3(0, -0.6f, 0);


    private readonly Dictionary<string, int> segmentIndexes = new Dictionary<string, int>()
    {
        ["top"] = 0,
        ["topright"] = 1,
        ["bottomright"] = 2,
        ["bottom"] = 3,
        ["bottomleft"] = 4,
        ["topleft"] = 5,
        ["middle"] = 6
    };

    private readonly Dictionary<int, bool[]> patterns = new Dictionary<int, bool[]>()
    {
        [0] = new[] {true, true, true, true, true, true, false},
        [1] = new[] {false, true, true, false, false, false, false},
        [2] = new[] {true, true, false, true, true, false, true},
        [3] = new[] {true, true, true, true, false, false, true},
        [4] = new[] {false, true, true, false, false, true, true},
        [5] = new[] {true, false, true, true, false, true, true},
        [6] = new[] {true, false, true, true, true, true, true},
        [7] = new[] {true, true, true, false, false, false, false},
        [8] = new[] {true, true, true, true, true, true, true},
        [9] = new[] {true, true, true, true, false, true, true},
    };

    private Transform[] childTransforms;

    // Start is called before the first frame update
    void Start()
    {
        childTransforms = gameObject.GetComponentsInChildren<Transform>();
        updateSegments();
    }

    // Update is called once per frame
    void updateSegments()
    {
        if (childTransforms == null) return;

        foreach (var currentSegment in childTransforms)
        {

            var currentName = currentSegment.name;
            if (currentName == transform.name) continue; //todo just dont add the parent in the first place...

            var indexForCurrentSegment = segmentIndexes[currentName];

            var currentSegmentRaised = patterns[currentDigit][indexForCurrentSegment];
            var previousSegmentRaised = patterns[previousDigit][indexForCurrentSegment];

            if (currentSegmentRaised && !previousSegmentRaised)
                StartCoroutine(Raise(currentSegment));
            else if (!currentSegmentRaised && previousSegmentRaised)
            {
                StartCoroutine(Lower(currentSegment));
            }

        }
    }

    IEnumerator Lower(Transform currentTransform)
    {
        currentTransform.gameObject.GetComponent<Renderer>().material = loweredMaterial;
        /*float inTime = 0.25f;
        for (float t = 0f; t <= 1; t += Time.deltaTime / inTime) {
            currentTransform.localPosition = Vector3.Lerp (raisedPosition, loweredPosition, t);
            yield return null;
        }*/
        yield break;
    }

    IEnumerator Raise(Transform currentTransform)
    {
        /*float inTime = 0.25f;
        for (float t = 0f; t <= 1; t += Time.deltaTime / inTime) {
            currentTransform.localPosition = Vector3.Lerp (loweredPosition, raisedPosition, t);
            yield return null;
        }*/
        currentTransform.gameObject.GetComponent<Renderer>().material = raisedMaterial;
        yield break;
    }
}

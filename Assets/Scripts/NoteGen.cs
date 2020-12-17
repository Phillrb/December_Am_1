using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NoteGen : MonoBehaviour
{
    public float tuning = 440.0f;

    public List<float> frequencies = new List<float>();
    // Start is called before the first frame update

    public float actualFrequency = 387.54f;
    public float closestFrequency = -1;
    public int closestNote = -1;

    public float noteAbove, noteBelow;


    public int noteIdx;

    public void Update()
    {
        FindClosest();
    }

    [ContextMenu("Gen Notes")]
    public void GenNotes()
    {
        frequencies.Clear();
        float rootOf12 = Mathf.Pow( 2f, ( 1f / 12f ) );
        for( int i = 0; i < 28; i++ )
        {
            if( i == 0 )
                frequencies.Add( tuning );
            else
            {
                frequencies.Add( frequencies[i - 1] * rootOf12 );
            }
        }

        for( int i = 0; i < 32; i++ )
        {
            frequencies.Insert( 0, frequencies[0] / rootOf12 );
        }
    }

    [ContextMenu( "Find Closest" )]
    private void FindClosest()
    {
        int closestIdx = -1;
        float closestAmountSoFar = float.MaxValue;

        for( int i = 0; i < frequencies.Count; i++ )
        {
            float dist = Mathf.Abs( actualFrequency - frequencies[i] );
            if (dist <= closestAmountSoFar)
            {
                closestIdx = i;
                closestAmountSoFar = dist;
            }
        }

        closestFrequency = frequencies[closestIdx];
        closestNote = closestIdx;

        int startIdx;
        int endIdx;

        if (actualFrequency > closestFrequency)
        {
            startIdx = closestIdx;
            endIdx = startIdx + 1;
            if( endIdx >= frequencies.Count )
                endIdx = startIdx;
        }
        else
        {
            endIdx = closestIdx;
            startIdx = endIdx - 1;
            if( startIdx < 0 )
                startIdx = 0;
        }

        float startFreq = frequencies[startIdx];
        float endFreq = frequencies[endIdx];

        float selector = ( actualFrequency - startFreq ) / ( endFreq - startFreq );



        // Get Note idx 
        noteIdx = (closestNote - 8) % 12;

        if( noteIdx < 0 ) noteIdx = 12 + noteIdx;

        startIdx = 0;
        endIdx = 0;

        if( actualFrequency > closestFrequency )
        {
            startIdx = noteIdx;
            endIdx = noteIdx + 1;
            if( endIdx >= 12 )
                endIdx = 0;
        }
        else
        {
            endIdx = noteIdx;
            startIdx = endIdx - 1;
            if( startIdx < 0 )
                startIdx = 11;
        }

        //boringBox.GetComponent<MeshRenderer>().material.color = Color.Lerp( colorArray[startIdx], colorArray[endIdx], selector );

    }

}

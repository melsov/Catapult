using UnityEngine;
using System.Collections.Generic;

public class ChoiceMode
{
    //private DuckOriginals dos;
    private int index;
    private int duckIntensity;
    private void increaseIntensity() {
        index = 0;
        duckIntensity++;
        duckIntensity = duckIntensity > pickArrays.Count - 1 ? pickArrays.Count - 1 : duckIntensity; 
    }

    //public ChoiceMode(DuckOriginals dos) {
    //    //this.dos = dos;
    //}

    public int getPick() {
        int[] choices = indices;
        int result = choices[index];
        index = ++index % choices.Length;
        return result;
    }

    public void setIntensity(int i) {
        if (i > duckIntensity) {
            increaseIntensity();
        }
    }

    protected int[] indices {
        get {
            return pickArrays[duckIntensity];
        }
    }
    public int Length { get { return pickArrays.Count; } }

    protected List<int[]> _pickArrays;
    protected virtual List<int[]> pickArrays {
        get {
            if (_pickArrays == null || _pickArrays.Count == 0) {
                _pickArrays = new List<int[]> {
                    new int[] { 2 },
                    new int[] { 0, 0, 0, 1 },
                    new int[] { 0, 2, 1, 1 },
                    new int[] { 1 },
                };
            }
            return _pickArrays;
        }
    }
}

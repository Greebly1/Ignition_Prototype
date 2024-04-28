using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularArray<DataType>
{
    readonly static int ARRAYWIDTH = 5;

    DataType[] _DataArray = new DataType[ARRAYWIDTH];
    public DataType[] DataArray
    {
        get { return _DataArray; }
        private set { _DataArray = value; }
    }

    int _numOfFilledDataSlots = 0; //the array starts empty, we need to make sure we keep track of how many data slots are filled
    int numOfFilledDataSlots
    {
        get { return _numOfFilledDataSlots; }
        set
        {
            _numOfFilledDataSlots = Mathf.Clamp(value, 0, ARRAYWIDTH); //cant have more than the max array width of data slots filled
        } 
  
    }
    bool arrayFull
    {
        get { return ARRAYWIDTH == numOfFilledDataSlots; }
    }

    int _currIndex = 0;
    public int currIndex //property to protect index from ever becoming invalid
    {
        get { 
            return _currIndex; 
        }
        set
        {
            if (value < 0) { currIndex = 0; } //cannot allow ourselves to set a negative index

            _currIndex = Mathf.Clamp(value, minIndex, maxIndex);
        }
    }

    int _maxIndex = 0;
    int maxIndex
    {
        get
        {
            return _maxIndex;
        }
        set
        {
            if (value < 0) { _maxIndex = 0; } //cannot allow ourselves to set a negative index
            _maxIndex = value;
            
        }
    }

    int _minIndex = 0;
    int minIndex
    {
        get
        {
            return _minIndex;
        }
        set
        {
            if (value < 0) { _minIndex = 0; } //cannot allow ourselves to set a negative index
            _minIndex = value;
        }
    }

    /// <summary>
    /// Functions a bit like a stack, inserts at maxIndex+1 but overwrites any data inside
    /// </summary>
    /// <param name="data"></param>
    public void Insert(DataType data)
    {
        numOfFilledDataSlots++;
        DataArray[MapIndexToArray(maxIndex+1)] = data;
        
        IncrementMaxIndex();
        RecalculateMinIndex();

        currIndex = maxIndex;
    }

    //cuts off the upper items in the array
    public void ClampArrayToCurrentIndex()
    {
        int itemsDeleted = 0;

        //for each item between current index and maxIndex+1
        for (int index = currIndex+1; index <= maxIndex; index++)
        {
            DataArray[MapIndexToArray(index)] = default; //default is what value a variable contains if it is declared but not assigned. For value types like int and bool, the default is 0. However for reference types it is null
            itemsDeleted++;
        }

        numOfFilledDataSlots -= itemsDeleted;

        maxIndex = currIndex;
    }

    int IncrementMaxIndex()
    {
        if (numOfFilledDataSlots == 1)
        {
            //do nothing
            return maxIndex;
        }
        maxIndex++;
        return maxIndex;
    }

    int RecalculateMinIndex()
    {
        if (numOfFilledDataSlots == 0) { return minIndex; }

        minIndex = (maxIndex - (numOfFilledDataSlots - 1));
        return minIndex;
    }

    public int MapIndexToArray(int index)
    { 
        return index % ARRAYWIDTH;
    }

    public void LogSelf()
    {
        Debug.Log("---------Circular Array Log---------");
        Debug.Log("Array Width: " + ARRAYWIDTH);
        Debug.Log("Number of items: " + numOfFilledDataSlots);
        Debug.Log("Is full: " + arrayFull);
        Debug.Log("Current Index: " + currIndex);
        Debug.Log("Max Index: " + maxIndex);
        Debug.Log("Min Index: " + minIndex);
        Debug.Log("DATA is the FOLLOWING:");
        foreach (DataType data in DataArray)
        {
            Debug.Log(data.ToString());
        }
        Debug.Log("------------------------------------");
    }



    public DataType DataAtCurrIndex()
    {
        return DataArray[MapIndexToArray(currIndex)];
    }
}

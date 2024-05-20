using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Savable<T> where T : class
{
    public void Save_data(); // saves the data
    T Load_data(); // retrieves the data
}

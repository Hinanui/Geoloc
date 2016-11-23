using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputAverage
{

  public List<float> datas = new List<float>();
  public int nbValues = 10;

	public void AddInput(float data)
  {
    if(datas.Count >= nbValues)
    {
      datas.RemoveAt(0);
    }
    datas.Add(data);
  }

  public float getAverage()
  {
    float avg = 0;
    for(int i = 0; i < datas.Count; i++)
    {
      avg += datas[i];
    }
    avg /= datas.Count;

    return avg;
  }
}

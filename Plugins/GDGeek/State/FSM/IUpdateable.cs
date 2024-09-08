using UnityEngine;
using System.Collections;

public interface IUpdateable{

	void update (float d);
    void pause();
    void continuePlay();
}

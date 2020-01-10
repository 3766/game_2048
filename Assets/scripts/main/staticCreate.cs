using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  staticCreate{
    public static float XStartPos=-3f;
    public static float YStartPos=6.5f;
    public static float XOffset=2f;
    public static float YOffset=2f;

    // public static float XStartPos=-2f;
    // public static float YStartPos=4f;
    // public static float XOffset=1.0f;
    // public static float YOffset=1.0f;

    // public static float XStartPos=-2.2f;
    // public static float YStartPos=4.5f;
    // public static float XOffset=0.88f;
    // public static float YOffset=0.88f;

    public static bool isWin=false;
    public static int playMode=4;
    public static float []playScore=null;
    static staticCreate(){
        playScore=new float[]{0,0,0};
    }

} 
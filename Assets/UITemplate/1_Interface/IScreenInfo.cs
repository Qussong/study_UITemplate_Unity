using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GH
{
    public interface IScreenInfo
    {
        // 화면 정보
        Vector2 GetScreenPixel();   // 화면의 픽셀 정보를 반환한다. 현재 화면의 크기를 픽셀 단위로 제공
        Vector2 GetScreenResolution();  // 화면의 해상도를 반환한다. 화면의 실질적인 크기를 의미한다.
        float GetScreenAspectRatio();   // 화면의 종횡비를 반환한다. 가로/세로
    }
}


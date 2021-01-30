Shader "Shaders/SeeThruKey"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Geometry-1" } // To make this shader render before SeeThruObj

		ColorMask 0 // Makes every pixel in this shader invisible

		ZWrite Off // Might as well not write to the z buffer if not writing to the frame buffer (the pixels are invivible) 

		Stencil {
			Ref 1 // what the value each pixel in the frame buffer will be compared to
			Comp always // when comparing pixels in the buffer, always return true
			Pass replace // the value 1 will be written to the stencil buffer for each rendered pixel or fragment for the triangles that use this shader
		}

        Pass
        {
            
        }
    }
}

Shader "Custom/DisableZWriteTUT"
{
	SubShader{
		Tags{
			"RenderType" = "Qpaque"
			}
			Pass{
				ZWrite Off
			}
		}
	}
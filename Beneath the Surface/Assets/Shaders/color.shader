Shader "Solid" {
    Properties {
    	_Color ("Color", COLOR) = (1, 1, 1, 1)
    }
    SubShader {
        Pass { Color [_Color] }
    }
} 
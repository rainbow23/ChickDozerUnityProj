Shader "Custom/Transform Vertex" {
	Properties {
		//_MainTex("Texture", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert
		struct Input {
			float4 color : COLOR;
		};
		void vert(inout appdata_full v){
			v.vertex.x += 0.2 * v.normal.x * sin(v.vertex.y * 3.14 * 16);
			v.vertex.z += 0.2 * v.normal.z * sin(v.vertex.y * 3.14 * 16);
		}
		
		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = half3(1, 0.5, 0.5);
			
		}
		ENDCG
	} 
	FallBack "Diffuse"
}

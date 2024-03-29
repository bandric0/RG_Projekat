#version 330 core
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D scene;
uniform sampler2D bloomBlur;
uniform bool bloom;
uniform bool hdr;
uniform float exposure;

void main()
{
    const float gamma = 2.2;
    vec3 hdrColor = texture(scene, TexCoords).rgb;
    vec3 bloomColor = texture(bloomBlur, TexCoords).rgb;

    if (bloom) {
        hdrColor += bloomColor;
    }

    vec3 result = hdrColor;

    if(hdr){
        result = vec3(1.0) - exp(-hdrColor * exposure);
        result = pow(result, vec3(1.0 / gamma));
    }else{
        result = pow(hdrColor, vec3(1.0 / gamma));
    }

    FragColor = vec4(result, 1.0);
}
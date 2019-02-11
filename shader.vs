attribute vec2 aVertexPosition;
//attribute vec2 aVertexPos;

// uniform mat4 uMVMatrix;
varying vec3 rayDir;
float focale;

void main(void) {	
	focale = 50.0;
	rayDir = vec3(aVertexPosition.x*18.0,focale,aVertexPosition.y*12.0);
	gl_Position = vec4(aVertexPosition,0.0, 1.0); //  uMVMatrix
}

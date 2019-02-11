

// =====================================================
// Mouse management
// =====================================================
var mouseDown = false;
var lastMouseX = null;
var lastMouseY = null;
var rotY = 0;
var rotX = 0;

// =====================================================
window.requestAnimFrame = (function()
{
	return window.requestAnimationFrame ||
         window.webkitRequestAnimationFrame ||
         window.mozRequestAnimationFrame ||
         window.oRequestAnimationFrame ||
         window.msRequestAnimationFrame ||
         function(/* function FrameRequestCallback */ callback,
									/* DOMElement Element */ element)
         {
            window.setTimeout(callback, 1000/60);
         };
})();

// ==========================================
function tick() {
	requestAnimFrame(tick);
	drawScene();
}

// =====================================================
function degToRad(degrees) {
	return degrees * Math.PI / 180;
}

// =====================================================
function handleMouseDown(event) {
	mouseDown = true;
	lastMouseX = event.clientX;
	lastMouseY = event.clientY;
}


// =====================================================
function handleMouseUp(event) {
	mouseDown = false;
}


// =====================================================
function handleMouseMove(event) {
	if (!mouseDown) {
		return;
	}
	var newX = event.clientX;
	var newY = event.clientY;

	// TODO : calculer mouvement de souris depuis dernier passage
	var deltaX = newX - lastMouseX;
	var deltaY = newY - lastMouseY;

	// TODO : définir un angle en radians à partir de ce mouvement
	//   utilisez la fonction : angleRadians = degToRad(angleDegres);
	// rotY += degToRad(deltaX/2);
	// rotX += degToRad(deltaY/2);
	

	// TODO : définir la matrice de rotation
    mat4.identity(objMatrix);
    // mat4.rotate(objMatrix, rotY, [ 0 , 1 , 0 ]);
    // mat4.rotate(objMatrix, rotX, [ 1 , 0 , 0 ])

	lastMouseX = newX;
	lastMouseY = newY;
}

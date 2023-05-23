var jsFunctions = {};
let projectName = "BlazorWebApp";
jsFunctions.calculateSquareRoot = function () {
    const number = prompt("Enter your number");

    DotNet.invokeMethodAsync(projectName, "CalculateSquareRoot", parseInt(number))
        .then(result => {
            let el = document.getElementById("string-result");
            el.innerHTML = result;
        });
}

jsFunctions.calculateSquareRootIdentifierName = function () {
    const number = prompt("Enter your number");
    let IdentifierName = CalculateSquareRootWithJustResult;
    DotNet.invokeMethodAsync(projectName, IdentifierName, parseInt(number))
        .then(result => {
            let el = document.getElementById("string-result");
            el.innerHTML = result;
        });
}

jsFunctions.showMouseCoordinates = function (dotNetObjRef) {
    dotNetObjRef.invokeMethodAsync("ShowCoordinates",
        {
            x: window.event.screenX,
            y: window.event.screenY
        }
    );
}

jsFunctions.registerMouseCoordinatesHandler = function (dotNetObjRef) {
    function mouseCoordinatesHandler() {
        dotNetObjRef.invokeMethodAsync("ShowCoordinates",
            {
                x: window.event.screenX,
                y: window.event.screenY
            }
        );
    };

    mouseCoordinatesHandler();

    document.getElementById("coordinates").onmousemove = mouseCoordinatesHandler;
}


window.JsInteropFunctions = {
    GetXPos: function (controlId) {
        var element = document.getElementById(controlId);
        var left = element.getBoundingClientRect().left;
        return left;
    }
};
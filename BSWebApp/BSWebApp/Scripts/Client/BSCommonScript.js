

(function ($) {
    $.getAntiForgeryToken = function (tokenWindow, appPath) {

        tokenWindow = tokenWindow &&
        typeof tokenWindow === typeof window ? tokenWindow : window;
        appPath = appPath && typeof appPath === "string" ? "_" + appPath.toString() : "";
        var tokenName = "__RequestVerificationToken" + appPath;
        var inputElements = tokenWindow.document.getElementsByTagName("input");
        for (var i = 0; i < inputElements.length; i++) {
            var inputElement = inputElements[i];
            if (inputElement.type === "hidden" && inputElement.name === tokenName) {
                return {
                    name: tokenName,
                    value: inputElement.value
                };
            }
        }
    };

    $.appendAntiForgeryToken = function (data, token) {
        if (data && typeof data !== "string") {
            data = $.param(data);
        }

        // Gets token from current window by default.
        token = token ? token : $.getAntiForgeryToken();
        // $.getAntiForgeryToken(window).

        data = data ? data + "&" : "";
        alert(encodeURIComponent(token.value));
        // If token exists, appends {token.name}={token.value} to data.
        return token ? data + encodeURIComponent(token.name) + "=" + encodeURIComponent(token.value) : data;
    };

    // Wraps $.post(url, data, callback, type) for most common scenarios.
    $.postAntiForgery = function (url, data, callback, type) {
        return $.post(url, $.appendAntiForgeryToken(data), callback, type);
    };

    // Wraps $.ajax(settings).
    $.ajaxAntiForgery = function (settings) {
        var token = settings.token ? settings.token : $.getAntiForgeryToken(settings.tokenWindow, settings.appPath);
        settings.data = $.appendAntiForgeryToken(settings.data, token);
        return $.ajax(settings);
    };
})(jQuery);
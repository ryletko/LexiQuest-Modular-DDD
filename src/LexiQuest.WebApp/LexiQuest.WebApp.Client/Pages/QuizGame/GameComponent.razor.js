﻿export function init(dotNetReference) {
    let tryReconnectSignalR = function () {
        dotNetReference.invokeMethodAsync('TryReconnectSignalR');
    };

    window.addEventListener('focus', function () {
        tryReconnectSignalR();
    });
}


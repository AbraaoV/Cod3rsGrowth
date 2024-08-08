sap.ui.define([
], function () {
    "use strict";

    const request = {
        _request: function (metodo, url, corpo) {
            const opcoesDoRequest = {
                method: metodo,
                headers: {
                    "Content-Type": "application/json",
                },
            };

            if (corpo) {
                opcoesDoRequest.body = JSON.stringify(corpo);
            }

            return fetch(url, opcoesDoRequest)
                .then(response => {
                    if (!response.ok) {
                        return Promise.reject(response);
                    };
                    return response;
                })
                .then(response=> {
                    return response.json().catch(() => response.headers.get('Content-Type'));
                })
        },
    }
    return request;
});

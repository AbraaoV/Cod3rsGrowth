sap.ui.define([], () => {
    "use strict";

    return {
        formatarCpf: function(sCpf) {
            return sCpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
        },
		formatarCnpj: function(sCnpf){
            return sCnpf.replace(/^(\d{2})(\d{3})?(\d{3})?(\d{4})?(\d{2})?/, "$1.$2.$3/$4-$5");
		}
    };
});
